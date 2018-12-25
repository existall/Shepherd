#tool "nuget:?package=xunit.runner.console"
#addin "Cake.FileHelpers"

//build.cake ExampleC#

// Target - The task you want to start. Runs the Default task if not specified.
var target = Argument("Target", "Default");
// Configuration - The build configuration (Debug/Release) to use.
// 1. If command line parameter parameter passed, use that.
// 2. Otherwise if an Environment variable exists, use that.

var configuration =
    HasArgument("Configuration") ? Argument("Configuration", "Release") :
    EnvironmentVariable("Configuration") != null ? EnvironmentVariable("BuildNumber") : "Release";
// The build number to use in the version number of the built NuGet packages.
// There are multiple ways this value can be passed, this is a common pattern.
// 1. If command line parameter parameter passed, use that.
// 2. Otherwise if running on AppVeyor, get it's build number.
// 3. Otherwise if running on Travis CI, get it's build number.
// 4. Otherwise if an Environment variable exists, use that.
// 5. Otherwise default the build number to 0.
var buildNumber =
    HasArgument("BuildNumber") ? Argument<int>("BuildNumber") :
    AppVeyor.IsRunningOnAppVeyor ? AppVeyor.Environment.Build.Number :
    TravisCI.IsRunningOnTravisCI ? TravisCI.Environment.Build.BuildNumber :
    EnvironmentVariable("BuildNumber") != null ? int.Parse(EnvironmentVariable("BuildNumber")) : 0;
// A directory path to an Artifacts directory.
var artifactsDirectory = Directory("./Artifacts");
// Deletes the contents of the Artifacts folder if it should contain anything from a previous build.
Task("Clean")
    .Does(() =>
    {
        CleanDirectory(artifactsDirectory);
    });
// Run dotnet restore to restore all package references.
Task("Restore")
    .IsDependentOn("Clean")
    .Does(() =>
    {
        DotNetCoreRestore();
    });
// Find all crproj projects and build them using the build configuration specified as an argument.
 Task("Build")
    .IsDependentOn("Restore")
    .Does(() =>
    {
        var projects = GetFiles("./{Core,tests}}/*.csproj");
        foreach(var project in projects)
        {
            DotNetCoreBuild(
                project.GetDirectory().FullPath,
                new DotNetCoreBuildSettings()
                {
                    Configuration = configuration
                });
        }
    });
// Look under a 'Tests' folder and run dotnet test against all of those projects.
// Then drop the XML test results file in the Artifacts folder at the root.
Task("Test")
    .IsDependentOn("Build")
    .Does(() =>
    {
        var projects = GetFiles("./tests/**/*.UnitTests.csproj");
        foreach(var project in projects)
        {
            var settings = new DotNetCoreTestSettings
            {
         Configuration = "Release"
     };

            DotNetCoreTest(
                project.FullPath,
                new DotNetCoreTestSettings()
                {
                    ArgumentCustomization = (args) => args
                         .Append("-l")
                         .Append("\"trx;LogFileName=" + artifactsDirectory.Path.CombineWithFilePath(project.GetFilenameWithoutExtension()).FullPath + ".trx\""),
                    Configuration = configuration,
                    NoBuild = true
                });
        }
    });
// Run dotnet pack to produce NuGet packages from our projects. Versions the package
// using the build number argument on the script which is used as the revision number
// (Last number in 1.0.0.0). The packages are dropped in the Artifacts directory.
Task("Pack")
    .IsDependentOn("Test")
    .Does(() =>
    {
        var revision = buildNumber.ToString("D4");
        foreach (var project in GetFiles("./Core/**/*.csproj"))
        {
            DotNetCorePack(
                project.GetDirectory().FullPath,
                new DotNetCorePackSettings()
                {
                    Configuration = configuration,
                    OutputDirectory = artifactsDirectory
                });
        }
    });
// The default task to run if none is explicitly specified. In this case, we want
// to run everything starting from Clean, all the way up to Pack.

Task("NuGet-Push")
    .IsDependentOn("Pack")
    .Does(() =>
{
        var apikey = FileReadText("./params.txt");

        foreach (var file in GetFiles("./Artifacts/*.nupkg"))
		{
			if (file.ToString().Contains(".symbols.nupkg"))
			{
				NuGetPush(file, new NuGetPushSettings
				{
					Source = "https://api.nuget.org/v3/package",
					ApiKey = apikey
				});
			}
			else
			{
				NuGetPush(file, new NuGetPushSettings
				{
					Source = "https://api.nuget.org/v3/index.json",
					ApiKey = apikey
				});
			}
		}

});

Task("Default")
    .IsDependentOn("Pack");
// Executes the task specified in the target argument.
RunTarget(target);