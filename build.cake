#load "helpers.cake"

// Input args
string target = Argument("target", "Default");
string configuration = Argument("configuration", "Release");

var projects = new[]
{
    Project ("FormsBuildTime", 
        description:  "File -> New Project -> Forms Master Detail"),
    Project ("Evolve2016",
        projectOrSln: "./src/Conference.Android/Conference.Android.csproj",
        nugetRestore: "./src/Conference.sln",
        description:  "Evolve 2016 Conference App")
};

Task ("Git-Submodule")
    .Does(() =>
    {
        StartProcess("git", "submodule update --init --recursive");
    });

Task ("Git-Clean")
    .Does(() =>
    {
        StartProcess("git", "submodule foreach git clean -dxf");
    });

Task ("Clean-Output")
    .Does(() => CleanDirectory ("./output/"));

Task("NuGet-Restore")
    .Does(() =>
    {
        foreach (var project in projects)
        {
            Information($"nuget restore {project.NuGetRestore}");
            NuGetRestore(project.NuGetRestore);
        }
    });

Task("Build-Projects")
    .IsDependentOn("Clean-Output")
    .IsDependentOn("Git-Submodule")
    .IsDependentOn("Git-Clean")
    .IsDependentOn("NuGet-Restore")
    .Does(() =>
    {
        foreach (var project in projects)
        {
            Build(project, "Clean");
        }
    });

Task("Second-Build-Projects")
    .IsDependentOn("Build-Projects")
    .Does(() =>
    {
        foreach (var project in projects)
        {
            Build(project, "Second");
        }
    });


Task("Default")
    .IsDependentOn("Second-Build-Projects");

RunTarget(target);