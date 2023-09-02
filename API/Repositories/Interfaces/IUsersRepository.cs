using API.Entities;

namespace API.Repositories.Interfaces
{
    public interface IUsersRepository
    {
        Task<User> GetByIdAsync(Guid id);
        IAsyncEnumerable<User> GetAllAsync();
        Task Register(User model);
        Task<User> GetByEmailAddressAsync(string emailAddress);
    }
}
