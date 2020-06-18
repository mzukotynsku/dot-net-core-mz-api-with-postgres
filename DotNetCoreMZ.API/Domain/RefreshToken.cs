using System;

namespace DotNetCoreMZ.API.Domain
{
    public class RefreshToken
    {
        public string Token { get; set; }

        public string JwtId { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime ExpiryDate { get; set; }
        
        public string UserId { get; set; }
    }
}
