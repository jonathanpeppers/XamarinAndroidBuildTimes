#load "helpers.cake"

// Input args
string target = Argument("target", "Default");
string configuration = Argument("configuration", "Release");

// Add new projects here, after the git submodule is setup
var projects = new[]
{
    Project("FormsBuildTime",
        csFile:       "./FormsBuildTime/FormsBuildTime.Android/MainActivity.cs",
        arFile:       "./FormsBuildTime/FormsBuildTime.Android/Resources/values/styles.xml",
        description:  "File -> New Project -> Forms Master Detail"),
    Project("Evolve2016",
        csFile:       "./src/Conference.Android/MainActivity.cs",
        arFile:       "./src/Conference.Android/Resources/values/styles.xml",
        projectOrSln: "./src/Conference.Android/Conference.Android.csproj",
        nugetRestore: "./src/Conference.sln",
        description:  "Evolve 2016 Conference App")
};

//Clone/update git submodules as needed
Task ("Git-Submodule")
    .Does(() =>
    {
        StartProcess("git", "submodule update --init --recursive");
    });

//Go to each git submodule and clean/reset it
Task ("Git-Clean")
    .Does(() =>
    {
        StartProcess("git", "submodule foreach git clean -dxf");
        StartProcess("git", "submodule foreach git reset --hard HEAD");
    });

//Delete the dir containing log files
Task ("Clean-Output")
    .Does(() => CleanDirectory ("./output/"));

//NuGet restore each project
Task("NuGet-Restore")
    .Does(() =>
    {
        foreach (var project in projects)
        {
            Information($"nuget restore {project.NuGetRestore}");
            NuGetRestore(project.NuGetRestore);
        }
    });

//A "clean" build
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

//The second build after a "clean" build
Task("Second-Build-Projects")
    .IsDependentOn("Build-Projects")
    .Does(() =>
    {
        foreach (var project in projects)
        {
            Build(project, "Second");
        }
    });

//A C# file is modified after a clean build
Task("Touch-CS-Build-Projects")
    .IsDependentOn("Build-Projects")
    .Does(() =>
    {
        foreach (var project in projects)
        {
            FileAppendText (project.CSharpFile, $"{Environment.NewLine}//comment{Environment.NewLine}");
            Build(project, "TouchCS");
        }
    });

//An Android resource file is modified after a clean build
Task("Touch-AR-Build-Projects")
    .IsDependentOn("Build-Projects")
    .Does(() =>
    {
        foreach (var project in projects)
        {
            FileAppendText (project.AndroidResourceFile, $"{Environment.NewLine}<!--comment-->{Environment.NewLine}");
            Build(project, "TouchAR");
        }
    });

//Builds everything
Task("Default")
    .IsDependentOn("Build-Projects")
    .IsDependentOn("Second-Build-Projects")
    .IsDependentOn("Touch-CS-Build-Projects")
    .IsDependentOn("Touch-AR-Build-Projects");

RunTarget(target);