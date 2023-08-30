using API.DTOs;

namespace API.Services.Interfaces
{
    public interface IUsersService
    {
        Task<ResultResponse<UserDTO>> GetByIdAsync(Guid id);
        Task<ResultResponse<IEnumerable<UserDTO>>> GetAllAsync();
        Task<BaseResponse> Create(RegisterUserDTO model);
        Task<ResultResponse<UserDTO>> Login(LoginDTO model);
    }
}
