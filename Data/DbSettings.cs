namespace RMSPrivateServerAPI.Data;

public class DbSettings
{
    // https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/compiler-messages/nullable-warnings#nonnullable-reference-not-initialized
    public string DefaultConnection { get; set; } = null!;
}