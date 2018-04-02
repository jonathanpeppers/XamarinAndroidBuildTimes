ProjectToBuild Project(
    string path, string csFile, string arFile,
    string projectOrSln = null, string nugetRestore = null, string description = null)
{
    var project = new ProjectToBuild
    {
        Directory   = Directory($"./external/{path}/"),
        LogFile     = $"./output/{path}-{{0}}.binlog",
        Description = description ?? path,
    };
    project.ProjectPath         = project.Directory.CombineWithFilePath(projectOrSln ?? $"./{path}.sln");
    project.NuGetRestore        = nugetRestore == null ? project.ProjectPath : project.Directory.CombineWithFilePath(nugetRestore);
    project.CSharpFile          = project.Directory.CombineWithFilePath(csFile);
    project.AndroidResourceFile = project.Directory.CombineWithFilePath(arFile);
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

void FileAppendText(FilePath file, string text)
{
    System.IO.File.AppendAllText(file.FullPath, text);
}

class ProjectToBuild
{
    public string Description { get; set; }

    public string LogFile { get; set; }

    public FilePath CSharpFile { get; set; }

    public FilePath AndroidResourceFile { get; set; }

    public DirectoryPath Directory { get; set; }

    public FilePath NuGetRestore { get; set; }

    public FilePath ProjectPath { get; set; }
}