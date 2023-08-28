using API.Models;
using API.ViewModels;

namespace API.Services.Interfaces
{
    public interface IUsersService
    {
        Task<ResultResponse<UserViewModel>> GetByIdAsync(Guid id);
        Task<ResultResponse<IEnumerable<UserViewModel>>> GetAllAsync();
        Task<BaseResponse> Insert(UserViewModel model);
    }
}
