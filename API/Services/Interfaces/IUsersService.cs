using API.DTOs;

namespace API.Services.Interfaces
{
    public interface IUsersService
    {
        Task<ResultResponse<UserDTO>> GetByIdAsync(Guid id);
        Task<ResultResponse<UserDTO>> GetByEmailAddressAsync(string emailAddress);
        Task<ResultResponse<IEnumerable<UserDTO>>> GetAllAsync();
        Task<ResultResponse<UserDTO>> Register(RegisterUserDTO dto);
        Task<ResultResponse<UserDTO>> Login(LoginDTO dto);
    }
}
