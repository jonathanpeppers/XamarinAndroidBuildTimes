ProjectToBuild Project (string path, string projectOrSln = null, string description = null)
{
    return new ProjectToBuild
    {
        Path = Directory ($"./external/{path}") + File(projectOrSln ?? $"{path}.sln"),
        Description = description ?? path,
    };
}

class ProjectToBuild
{
    public string Description { get; set; }

    public FilePath Path { get; set; }
}