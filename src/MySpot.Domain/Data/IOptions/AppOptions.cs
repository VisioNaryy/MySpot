namespace MySpot.Domain.Data.IOptions;

public sealed class AppOptions
{
    public const string SectionName = "App";
    public string Name { get; set; }
}