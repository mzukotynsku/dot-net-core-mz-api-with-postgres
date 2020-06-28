using System.ComponentModel.DataAnnotations;

namespace DotNetCoreMZ.API.Contracts.V1.Requests
{
    public class UserRegistrationRequest
    {
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
