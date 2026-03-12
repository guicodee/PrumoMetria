namespace PrumoMetria.Settings;

public class JwtSettings
{
    public string Key { get; set; } = string.Empty;
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public int ExpiresInHours { get; set; } = 1;
    public string LoginProvider { get; set; } = string.Empty;
    public string RefreshTokenName { get; set; } = string.Empty;
}