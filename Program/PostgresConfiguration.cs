namespace HistoryTelegramBot;

public class PostgresConfiguration
{
    public static string Configuration => "PostgresConnectionConfiguration";
    public string ConnectionString { get; set; } = string.Empty;
}