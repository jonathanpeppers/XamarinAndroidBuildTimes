ProjectToBuild Project (string path, string projectOrSln = null, string nugetRestore = null, string description = null)
{
    var project = new ProjectToBuild
    {
        Directory = Directory($"./external/{path}/"),
        LogFile = $"./output/{path}-{{0}}.binlog",
        Description = description ?? path,
    };
    project.ProjectPath = project.Directory.CombineWithFilePath(projectOrSln ?? $"./{path}.sln");
    project.NuGetRestore = nugetRestore == null ? project.ProjectPath : project.Directory.CombineWithFilePath(nugetRestore);
    return project;
}

void Build(ProjectToBuild project, string logSuffix)
{
    MSBuild(project.ProjectPath, new MSBuildSettings
    {
        BinaryLogger = new MSBuildBinaryLogSettings
        {
            Enabled = true,
            FileName = string.Format(project.LogFile, logSuffix),
        }
    }
    .WithTarget("SignAndroidPackage"));
}

class ProjectToBuild
{
    public string Description { get; set; }

    public string LogFile { get; set; }

    public DirectoryPath Directory { get; set; }

    public FilePath NuGetRestore { get; set; }

    public FilePath ProjectPath { get; set; }
}