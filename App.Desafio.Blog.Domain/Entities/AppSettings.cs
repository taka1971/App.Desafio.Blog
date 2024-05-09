using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Desafio.Blog.Domain.Entities
{
    public record LoggingSettings(LogLevelSettings LogLevel);
    public record LogLevelSettings(string Default, string Microsoft_AspNetCore);
    public record JwtSettings(string Secret, string Issuer, string Audience, uint ExpiresInMinutes);

    public record ConnectionStrings(string DefaultConnection);

    public record HealthCheckSettings(string Url);

    public record IpRateLimitingSettings(bool EnableEndpointRateLimiting, bool StackBlockedRequests, List<GeneralRule> GeneralRules);
    public record GeneralRule(string Endpoint, string Period, int Limit);

    public record AppSettings(
        LoggingSettings Logging,
        JwtSettings JwtSettings,
        ConnectionStrings ConnectionStrings,
        HealthCheckSettings HealthCheckSettings,
        IpRateLimitingSettings IpRateLimiting
    );

}
