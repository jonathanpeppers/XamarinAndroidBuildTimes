#load "helpers.cake"

// Input args
string target = Argument("target", "Default");
string configuration = Argument("configuration", "Release");

var projects = new[]
{
    Project ("FormsBuildTime", description: "File -> New Project -> Forms Master Detail")
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

Task("NuGet-Restore")
    .Does(() =>
    {
        foreach (var project in projects)
        {
            NuGetRestore(project.Path);
        }
    });

Task("Build-Projects")
    .IsDependentOn("Git-Submodule")
    .IsDependentOn("Git-Clean")
    .IsDependentOn("NuGet-Restore")
    .Does(() =>
    {
        foreach (var project in projects)
        {
            MSBuild(project.Path);
        }
    });

Task("Default")
    .IsDependentOn("Build-Projects");

RunTarget(target);