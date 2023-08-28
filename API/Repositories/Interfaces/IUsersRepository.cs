using API.Entities;

namespace API.Repositories.Interfaces
{
    public interface IUsersRepository
    {
        Task<User> GetByIdAsync(Guid id);
        Task<IEnumerable<User>> GetAllAsync();
        Task Insert(User model);
    }
}
