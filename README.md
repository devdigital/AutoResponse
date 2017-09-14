# AutoResponse

Create standardised responses for RESTful APIs

AutoResponse allows exceptions or action results to be mapped to HTTP JSON responses, reducing boileplate and standardising response shapes.

AutoResponse supports the following:

* OWIN
* Web API 2

> ASP.NET Core version coming soon...

AutoResponse comes with a number of pre-built exceptions, or you can map your own:

| Exception                                                                     | Status Code | Body |
|-------------------------------------------------------------------------------|-------------|------|
| `UnauthenticatedException`                                                    | 401         |      |
| `EntityPermissionException`, `EntityPermissionException<TEntity>`             | 403         |      |
| `EntityCreatePermissionException`, `EntityCreatePermissionException<TEntity>` | 403         |      |
| `EntityNotFoundException`, `EntityNotFoundException<TEntity>`                 | 404         |      |
| `EntityNotFoundQueryException`, `EntityNotFoundQueryException<TEntity>`       | 404         |      |
| `EntityValidationException`                                                   | 422         |      |
| `ServiceErrorException`                                                       | 500         |      |

## Configuring

### OWIN

```
this.app.UseAutoResponse(new AutoResponseOptions { ... });
```

`AutoResponseOptions` include:

| Option                  | Description                          | Default                                  |
|-------------------------|--------------------------------------|------------------------------------------|
| Logger                  | Logger for OWIN unhandled exceptions | `NullAutoResponseLogger`                 |
| EventHttpResponseMapper | Mapper for events to HTTP responses  | `AutoResponseApiEventHttpResponseMapper` |

### Web API 2

```
configuration.Services.Replace(
    typeof(IExceptionHandler),
    new AutoResponseExceptionHandler());
```

## NuGet packages

* AutoResponse.Core
* AutoResponse.Owin
* AutoResponse.WebApi2
* AutoResponse.WebApi2.Autofac
* AutoResponse.Client
