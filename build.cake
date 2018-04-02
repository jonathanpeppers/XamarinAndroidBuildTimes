#tool nuget:?package=NUnit.ConsoleRunner&version=3.8.0

// Input args
string target = Argument("target", "Default");
string configuration = Argument("configuration", "Release");

Task("NuGet-Restore")
    .Does(() =>
    {
        //TODO
    });

Task("Build-Projects")
    .IsDependentOn("NuGet-Restore")
    .Does(() =>
    {
        //TODO
    });

Task("Default")
    .IsDependentOn("Build-Projects");

RunTarget(target);