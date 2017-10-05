#addin "Cake.FileHelpers"
#addin nuget:?package=Newtonsoft.Json&version=9.0.1

var target = Argument("Target", "Default");
var configuration = Argument("Configuration", "Release");

Information("Running target " + target + " in configuration " + configuration);

var packageJsonText = FileReadText("./package.json");
var packageJson = Newtonsoft.Json.Linq.JObject.Parse(packageJsonText);
var buildNumber = packageJson.Property("version").Value;

var artifactsDirectory = Directory("./artifacts");

var projects = new List<string>
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
        foreach(var project in projects)
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
            DotNetCoreBuild(
                solution.ToString(),
                new DotNetCoreBuildSettings()
                {
                    Configuration = configuration,
                });
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
    .Does(() =>
    {
        var version = buildNumber.ToString();
        foreach (var project in projects)
        {
            Information("Packing project " + project);
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
    .IsDependentOn("Pack");

RunTarget(target);
