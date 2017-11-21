#addin "Cake.FileHelpers"
#addin nuget:?package=Newtonsoft.Json&version=9.0.1

var target = Argument("Target", "Default");
var configuration = Argument("Configuration", "Release");

Information("Running target " + target + " in configuration " + configuration);

var packageJsonText = FileReadText("./package.json");
var packageJson = Newtonsoft.Json.Linq.JObject.Parse(packageJsonText);
var buildNumber = packageJson.Property("version").Value;

var artifactsDirectory = Directory("./artifacts");

var net45Projects = new List<string>
{
  "./Source/AutoResponse.Owin/AutoResponse.Owin.csproj",
  "./Source/AutoResponse.WebApi2/AutoResponse.WebApi2.csproj",
  "./Source/AutoResponse.WebApi2.Autofac/AutoResponse.WebApi2.Autofac.csproj"
};

var netCoreProjects = new List<string>
{
  "./Source/AutoResponse.Core/AutoResponse.Core.csproj",
  "./Source/AutoResponse.Client/AutoResponse.Client.csproj",
  "./Source/AutoResponse.AspNetCore/AutoResponse.AspNetCore.csproj"
};

Task("Clean")
    .Does(() =>
    {
        CleanDirectory(artifactsDirectory);
    });

Task("Restore")
    .IsDependentOn("Clean")
    .Does(() =>
    {
        foreach(var project in netCoreProjects)
        {
          DotNetCoreRestore(project);
        }
    });

Task("Build")
    .IsDependentOn("Restore")
    .Does(() =>
    {
        var solutions = GetFiles("**/*.sln");
        foreach(var solution in solutions)
        {
            Information("Building solution " + solution);
            DotNetBuild(
                solution.ToString(),
                settings => settings.SetConfiguration(configuration));
        }
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
                    // Currently not possible? https://github.com/dotnet/cli/issues/3114
                    // ArgumentCustomization = args => args
                    //     .Append("-xml")
                    //     .Append(artifactsDirectory.Path.CombineWithFilePath(project.GetFilenameWithoutExtension()).FullPath + ".xml"),
                    Configuration = configuration,
                    NoBuild = true
                });
        }
    });

Task("Pack")
    .IsDependentOn("Build")
    .Does(() => {
      var version = buildNumber.ToString();
      foreach (var project in net45Projects)
      {
        Information("Packing dotnet45 project " + project);
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
