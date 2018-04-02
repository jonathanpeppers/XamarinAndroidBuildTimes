ProjectToBuild Project (string path, string projectOrSln = null, string nugetRestore = null, string description = null)
{
    var project = new ProjectToBuild
    {
        Directory = Directory($"./external/{path}/"),
        LogFile = $"./output/{path}.binlog",
        Description = description ?? path,
    };
    project.ProjectPath = project.Directory.CombineWithFilePath(projectOrSln ?? $"./{path}.sln");
    project.NuGetRestore = nugetRestore == null ? project.ProjectPath : project.Directory.CombineWithFilePath(nugetRestore);
    return project;
}

class ProjectToBuild
{
    public string Description { get; set; }

    public string LogFile { get; set; }

    public DirectoryPath Directory { get; set; }

    public FilePath NuGetRestore { get; set; }

    public FilePath ProjectPath { get; set; }
}