# AutoResponse

Create standardised responses for RESTful APIs

AutoResponse allows exceptions or action results to be mapped to HTTP JSON responses, reducing boilerplate and standardising response shapes.

AutoResponse supports the following:

* ASP.NET Core
* .NET 4.x/OWIN

## Getting Started

### ASP.NET Core

```
install-package AutoResponse.AspNetCore
```

### .NET 4.x/OWIN

```
install-package AutoResponse.Owin
```

## Configuring

### ASP.NET Core

```csharp
services.AddSingleton<IApiEventHttpResponseMapper>(
    new AutoResponseApiEventHttpResponseMapper(new AspNetCoreContextResolver()));
```

```csharp
app.UseAutoResponse(new AutoResponseOptions { ... });
```

### OWIN

```csharp
// E.g. using Autofac
builder.RegisterType<IdentityExceptionHttpResponseMapper>()
    .As<IApiEventHttpResponseMapper>();
```

```csharp
app.UseAutoResponse(new AutoResponseOptions { ... });
```

### Web API 2

```
configuration.Services.Replace(
    typeof(IExceptionHandler),
    new AutoResponseExceptionHandler());
```

## AutoResponse Options

`AutoResponseOptions` include:

| Option                   | Description                          | Default                                  |
| ------------------------ | ------------------------------------ | ---------------------------------------- |
| Logger                   | Logger for unhandled exceptions      | `NullAutoResponseLogger`                 |
| EventHttpResponseMapper  | Mapper for events to HTTP responses  | `AutoResponseApiEventHttpResponseMapper` |
| DomainResultPropertyName | Result property on domain exceptions | "Event"                                  |

## Provided Exceptions

AutoResponse comes with a number of pre-built exceptions, or you can map your own.

The following table shows the built in exceptions and the status code to which they map:

| Exception                                                                     | Status Code |
| ----------------------------------------------------------------------------- | ----------- |
| `UnauthenticatedException`                                                    | 401         |
| `EntityPermissionException`, `EntityPermissionException<TEntity>`             | 403         |
| `EntityCreatePermissionException`, `EntityCreatePermissionException<TEntity>` | 403         |
| `EntityNotFoundException`, `EntityNotFoundException<TEntity>`                 | 404         |
| `EntityNotFoundQueryException`, `EntityNotFoundQueryException<TEntity>`       | 404         |
| `EntityValidationException`                                                   | 422         |
| `ServiceErrorException`                                                       | 500         |

## Throwing An Exception

You can throw one of the provided exceptions (or a custom exception, see custom exceptions later), and the default mapper will map the exception to an API response:

```csharp
public class MyController : Controller
{
    [HttpGet]
    public IActionResult Get()
    {
        throw new EntityNotFoundException("entity", "1");
    }
}
```

The `EntityNotFoundException` for example takes an `entity` and an `entityId`, these are mapping to the response `resource` and `resourceId` fields:
```json
{
  "resource": "entity",
  "resourceId": "1",
  "message": "The entity resource with identifier '1' was not found.",
  "code": "AR404"
}
```

> Note that by default AutoResponse includes an `AR` prefixed error code, useful on the client to distinguish errors. You can override these values within the exception constructor or a custom mapper.

## Custom Mapping

If you wish to map custom exceptions or action results to custom responses, then you can implement `IApiEventHttpResponseMapper` and register the implementation within the `AutoResponseOptions`.


The easiest way is to derive from the supplied `AutoResponseApiEventHttpResponseMapper`, this requires an `IContextResolver` and optional `IAutoResponseExceptionFormatter`. You override the `ConfigureMappings(ExceptionHttpResponseConfiguration configuration)` method and call the base implementation:

```csharp
public class SampleHttpResponseMapper : AutoResponseApiEventHttpResponseMapper
{
    public SampleHttpResponseMapper() : base(new AspNetCoreContextResolver())
    {
    }

    protected override void ConfigureMappings(ExceptionHttpResponseConfiguration configuration)
    {
        // Register existing exception mappings
        base.ConfigureMappings(configuration);

        // Add custom mappings here
        configuration.AddMapping<MyApiEvent>(
            (c, e) => new ResourceValidationHttpResponse(
                code: null,
                validationErrorDetails: new ValidationErrorDetails(e.Message)));
    }
}
```

## Custom API Events

The custom mapper maps API events to responses. For custom mappings you must:

* Implement an API event (`IAutoResponseApiEvent`)
* If you wish to return the API event from a controller action, create a custom result (derive from `AutoResponseResult`)
* If you wish to return the API event from an exception, create a custom exception (derive from `AutoResponseException`)
* Add the event mapping to the mapper

For example:

```csharp
public class MyUnauthenticatedApiEvent : IAutoResponseApiEvent
{
    public MyUnauthenticatedApiEvent(string username)
    {
        this.Message = $"The username '{username}' is not valid.";
    }

    public string Code { get; } = "username-invalid";

    public string Message { get; }
}

public class MyUnauthenticatedResult : AutoResponseResult
{
    public MyUnauthenticatedResult(string username)
        : base(new MyUnauthenticatedApiEvent(username))
    {
    }
}

public class MyUnauthenticatedException : AutoResponseException
{
    public MyUnauthenticatedException(string username) 
        : base($"The username {username} is invalid.")
    {
        this.EventObject = new MyUnauthenticatedApiEvent(username);
    }

    public override object EventObject { get; }
}

// Within your mapper:
protected override void ConfigureMappings(ExceptionHttpResponseConfiguration configuration)
{
    // Register existing exception mappings
    base.ConfigureMappings(configuration);

    // Add custom mappings here
    configuration.AddMapping<MyUnauthenticatedApiEvent>(
        (c, e) => new UnauthenticatedHttpResponse(
            message: c.Formatter.Message(e.Message),
            code: c.Formatter.Code(e.Code)));
}
```

> Note that the API response and the exception messages can be different, if you wish to have more information within the exception message. You can also change the response message within the mapper.

You can now either return your custom result from an action controller, or throw your custom exception, and the API event will be mapped in both cases using the mapping configuration within your mapper:

```csharp
public class MyController : Controller
{
    [HttpGet]
    public IEnumerable<string> Get()
    {
        throw new MyUnauthenticatedException("username");
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        return new MyUnauthenticatedResult("username");
    }
}
```

## Domain Exceptions

AutoResponse also supports dealing with domain exceptions. These are exceptions that don't derive from `AutoResponseException`. This prevents the need to reference `AutoResponse` within your domain logic.

To support a domain exception, it must have an `Event` property which is an instance of the API event.

The name `"Event"` is configurable as the `DomainResultPropertyName` option within `AutoResponseOptions`.

For example:

```csharp
public class MyUnauthenticatedException : Exception
{
    public UserAlreadyAuthenticatedException(string username)
        : base($"The username {username} is invalid.")
    {
        this.Event = new MyUnauthenticatedApiEvent(username);
    }

    public MyUnauthenticatedApiEvent Event { get; }
}
```

You can now throw the exception from within your domain.

## Formatting 

The `ConfigureMappings` method takes an `ExceptionHttpResponseConfiguration` which provides `AddMapping`, `UpdateMapping`, and `RemoveMapping` methods.

You typically call the `base.ConfigureMappings` method to add the built in API event mappings, but you can use `UpdateMapping` or `RemoveMapping` to manipulate these as necessary.

The `AddMapping` method has the following signature:

```csharp
public void AddMapping<TApiEvent>(
    Func<ExceptionHttpResponseContext, TApiEvent, IHttpResponse> mapping)
    where TApiEvent : class
```

In other words, it expects a `Func` which takes a context, and the API event specified as the generic parameter, and is expecting an implementation of `IHttpResponse` to be returned.

The `ExceptionHttpResponseContext` includes a `Formatter` property, which is an instance of `IAutoResponseExceptionFormatter` configured within the `AutoResponseOptions`. By default, this is the provided `AutoResponseExceptionFormatter`, but you can write your own.

The interface provides four methods:

```csharp
public interface IAutoResponseExceptionFormatter
{
    string Message(string message);
    string Resource(string entityType);
    string Field(string entityProperty);
    string Code(string code);
}
```

The intention is that:

* `Message` formats any general messages on the outgoing response 
* `Resource` formats any resource names
* `Field` formats any field names
* `Code` formats the error code

By default, `ApiModel` or `Dto` postfixes on resources etc will be stripped, and the values will be converted to kebab-case.

Using the formatter is entirely optional within each `AddMapping` statement within the mapper.

## NuGet packages

* AutoResponse.AspNetCore
* AutoResponse.Core
* AutoResponse.Owin
* AutoResponse.WebApi2
* AutoResponse.WebApi2.Autofac
* AutoResponse.Client
