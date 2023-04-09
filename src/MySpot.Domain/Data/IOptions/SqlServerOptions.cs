namespace MySpot.Domain.Data.IOptions;

public class SqlServerOptions
{
    public const string SectionName = "SqlServer";
    public string ConnectionString { get; set; }
}