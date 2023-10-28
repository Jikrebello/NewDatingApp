using API.Entities;
using API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly DataContext _dataContext;

        public UsersRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            return await _dataContext.Users.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<User> GetByEmailAddressAsync(string emailAddress)
        {
            return await _dataContext.Users.SingleOrDefaultAsync(
                x => x.EmailAddress == emailAddress
            );
        }

        public IAsyncEnumerable<User> GetAllAsync()
        {
            return _dataContext.Users.AsNoTracking().AsAsyncEnumerable();
        }

        public async Task Register(User model)
        {
            await _dataContext.Users.AddAsync(model);
            await _dataContext.SaveChangesAsync();
        }
    }
}
