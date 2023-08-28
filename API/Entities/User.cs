namespace API.Entities;

public class User
{
    public User()
    {
        Id = Guid.NewGuid();
    }

    public Guid Id { get; set; }
    public string Name { get; set; }
}
