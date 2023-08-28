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

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _dataContext.Users.ToListAsync();
        }

        public async Task Insert(User model)
        {
            await _dataContext.Users.AddAsync(model);
            await _dataContext.SaveChangesAsync();
        }
    }
}
