namespace FCG.Payments.Api._Common;

public class AuthenticationSettings
{
    public required string Authority { get; set; }

    public required string Audience { get; set; }

    public required bool RequireHttpsMetadata { get; set; }
}
