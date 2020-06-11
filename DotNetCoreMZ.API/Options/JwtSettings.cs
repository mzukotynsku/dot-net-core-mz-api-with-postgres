using System;

namespace DotNetCoreMZ.API.Options
{
    public class JwtSettings
    {
        public string  Secret { get; set; }

        public TimeSpan TokenLifeTime { get; set; }
    }
}
