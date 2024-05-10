using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Desafio.Blog.Domain.Entities
{
    using System.Collections.Generic;

    public class LoggingSettings
    {
        public LogLevelSettings LogLevel { get; set; }

        public LoggingSettings()
        {
            LogLevel = new LogLevelSettings();
        }
    }

    public class LogLevelSettings
    {
        public string Default { get; set; }
        public string Microsoft_AspNetCore { get; set; }

        public LogLevelSettings()
        {
            Default = "";
            Microsoft_AspNetCore = "";
        }
    }

    public class JwtSettings
    {
        public string Secret { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public uint ExpiresInMinutes { get; set; }

        public JwtSettings()
        {
            Secret = "";
            Issuer = "";
            Audience = "";
            ExpiresInMinutes = 0;
        }
    }

    public class ConnectionStrings
    {
        public string DefaultConnection { get; set; }

        public ConnectionStrings()
        {
            DefaultConnection = "";
        }
    }

    public class HealthCheckSettings
    {
        public string Url { get; set; }

        public HealthCheckSettings()
        {
            Url = "";
        }
    }

    public class IpRateLimitingSettings
    {
        public bool EnableEndpointRateLimiting { get; set; }
        public bool StackBlockedRequests { get; set; }
        public List<GeneralRule> GeneralRules { get; set; }

        public IpRateLimitingSettings()
        {
            GeneralRules = new List<GeneralRule>();
        }
    }

    public class GeneralRule
    {
        public string Endpoint { get; set; }
        public string Period { get; set; }
        public int Limit { get; set; }

        public GeneralRule()
        {
            Endpoint = "";
            Period = "";
            Limit = 0;
        }
    }

    public class BlogChannelSettings
    {
        public string Endpoint { get; set; }

        public BlogChannelSettings()
        {
            Endpoint = "";
        }
    }

    public class AppSettings
    {
        public LoggingSettings Logging { get; set; }
        public JwtSettings JwtSettings { get; set; }
        public ConnectionStrings ConnectionStrings { get; set; }
        public HealthCheckSettings HealthCheckSettings { get; set; }
        public IpRateLimitingSettings IpRateLimiting { get; set; }
        public BlogChannelSettings BlogChannelSettings { get; set; }

        public AppSettings()
        {
            Logging = new LoggingSettings();
            JwtSettings = new JwtSettings();
            ConnectionStrings = new ConnectionStrings();
            HealthCheckSettings = new HealthCheckSettings();
            IpRateLimiting = new IpRateLimitingSettings();
            BlogChannelSettings = new BlogChannelSettings();
        }
    }


}
