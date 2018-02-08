#load "nuget:?package=Cake.Mix&version=0.13.0"

var target = Argument("Target", "Default");
var configuration = Argument("Configuration", "Release");

Information("Running target " + target + " in configuration " + configuration);

var packageJson = new PackageJson(Context, "./package.json");
var buildNumber = packageJson.GetVersion();

var artifactsDirectory = Directory("./artifacts");

var solution = "./Source/AutoResponse.sln";

var net46AndMultiTargetProjects = new List<string>
{
  "./Source/AutoResponse.Core/AutoResponse.Core.csproj",
  "./Source/AutoResponse.Client/AutoResponse.Client.csproj",
  "./Source/AutoResponse.Owin/AutoResponse.Owin.csproj",
  "./Source/AutoResponse.WebApi2/AutoResponse.WebApi2.csproj",
  "./Source/AutoResponse.WebApi2.Autofac/AutoResponse.WebApi2.Autofac.csproj"
};

var netCoreProjects = new List<string>
{
  "./Source/AutoResponse.AspNetCore/AutoResponse.AspNetCore.csproj"
};

Task("Clean")
    .Does(() =>
    {
        CleanDirectory(artifactsDirectory);
    });

Task("Restore")
    .IsDependentOn("Clean")
    .Does(() => {
      NuGetRestore(solution);
    });

Task("RestoreCore")
    .IsDependentOn("Restore")
    .Does(() =>
    {
        foreach(var project in netCoreProjects)
        {
          DotNetCoreRestore(project);
        }
    });

// Build .NET 4.x and multi-target projects
Task("Build")
    .IsDependentOn("Restore")
    .Does(() =>
    {
      var buildCommand = new DotNetBuildBuilder(Context)
        .WithProjects(net46AndMultiTargetProjects)
        .WithConfiguration(configuration)      
        .WithBuildPlatform(MSBuildPlatform.Automatic)
        .WithParameter("Platform", "AnyCPU")
        .Build();
        
      buildCommand.Execute();
    });

Task("Test")
    .IsDependentOn("Build")
    .Does(() =>
    {
        var projects = GetFiles("./test/**/*Test.csproj");
        foreach(var project in projects)
        {
            Information("Testing project " + project);
            DotNetCoreTest(
                project.ToString(),
                new DotNetCoreTestSettings()
                {
                    Configuration = configuration,
                    NoBuild = true
                });
        }
    });

// Pack .NET 4.x and multi-target projects
Task("Pack")
    .IsDependentOn("Build")
    .Does(() => {
      var version = buildNumber.ToString();
      foreach (var project in net46AndMultiTargetProjects)
      {
        Information("Packing project " + project);
        var nuspecPath = project.Replace(".csproj", ".nuspec");
        NuGetPack(
          nuspecPath,
          new NuGetPackSettings()
          {
            Version = version,
            OutputDirectory = artifactsDirectory
          }
        );
      }
    });

Task("PackCore")
    .IsDependentOn("Pack")
    .Does(() =>
    {
        var version = buildNumber.ToString();
        foreach (var project in netCoreProjects)
        {
            Information("Packing dotnetcore project " + project);
            DotNetCorePack(
                project.ToString(),
                new DotNetCorePackSettings()
                {
                    Configuration = configuration,
                    OutputDirectory = artifactsDirectory,
                    VersionSuffix = version,
                    ArgumentCustomization  = builder => builder.Append("/p:PackageVersion=" + version),
                });
        }
    });

Task("Default")
    .IsDependentOn("PackCore");

RunTarget(target);
