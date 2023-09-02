using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        [Required]
        public string EmailAddress { get; set; }
        public string Token { get; set; }
    }

    public class RegisterUserDTO
    {
        [Required]
        public string EmailAddress { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Password { get; set; }
    }

    public class LoginDTO
    {
        [Required]
        public string EmailAddress { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
