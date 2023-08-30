using System.ComponentModel.DataAnnotations;

namespace API.Entities;

public class User
{
    public User()
    {
        Id = Guid.NewGuid();
    }

    public Guid Id { get; set; }

    [Required]
    public string EmailAddress { get; set; }
    public string Name { get; set; }
    public byte[] PasswordHash { get; set; }
    public byte[] PasswordSalt { get; set; }
}
