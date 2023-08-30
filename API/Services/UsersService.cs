using System.Security.Cryptography;
using System.Text;
using API.Entities;
using API.DTOs;
using API.Repositories.Interfaces;
using API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _userRepository;

        public UsersService(IUsersRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ResultResponse<UserDTO>> GetByIdAsync(Guid id)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(id);
                if (user == null)
                {
                    return new ResultResponse<UserDTO>(
                        success: false,
                        message: "User not found.",
                        result: null
                    );
                }
                var userModel = new UserDTO { Id = user.Id, Name = user.Name, };

                return new ResultResponse<UserDTO>(
                    success: true,
                    message: "User found.",
                    userModel
                );
            }
            catch (Exception ex)
            {
                return new ResultResponse<UserDTO>(
                    success: false,
                    message: $"An error occurred: {ex.Message}",
                    result: null
                );
            }
        }

        public async Task<ResultResponse<IEnumerable<UserDTO>>> GetAllAsync()
        {
            try
            {
                var users = await _userRepository.GetAllAsync().ToListAsync();

                if (!users.Any())
                {
                    return new ResultResponse<IEnumerable<UserDTO>>(
                        success: false,
                        message: "No users found.",
                        result: null
                    );
                }

                var userModels = users
                    .Select(user => new UserDTO { Id = user.Id, Name = user.Name })
                    .ToList();

                return new ResultResponse<IEnumerable<UserDTO>>(
                    success: true,
                    message: "Users found.",
                    userModels
                );
            }
            catch (Exception ex)
            {
                return new ResultResponse<IEnumerable<UserDTO>>(
                    success: false,
                    message: $"An error occurred: {ex.Message}",
                    result: null
                );
            }
        }

        public async Task<BaseResponse> Create(RegisterUserDTO dto)
        {
            try
            {
                if (await UserExists(dto.EmailAddress))
                {
                    return new BaseResponse(
                        success: false,
                        message: "Email Address is already taken"
                    );
                }

                using var hmac = new HMACSHA512();

                var user = new User
                {
                    Name = dto.Name,
                    EmailAddress = dto.EmailAddress.ToLower(),
                    PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(dto.Password)),
                    PasswordSalt = hmac.Key
                };

                await _userRepository.Create(user);

                return new BaseResponse(
                    success: true,
                    message: $"Successfully Inserted User: {user.Name} with Email Address: {user.EmailAddress}."
                );
            }
            catch (Exception ex)
            {
                return new BaseResponse(
                    success: false,
                    message: $"An error occurred: {ex.Message}"
                );
            }
        }

        public async Task<ResultResponse<UserDTO>> Login(LoginDTO dto)
        {
            var user = await _userRepository.GetByEmailAddressAsync(dto.EmailAddress);

            if (user == null)
            {
                return new ResultResponse<UserDTO>(
                    success: false,
                    message: "Invalid Email Address.",
                    result: null
                );
            }

            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(dto.Password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i])
                {
                    return new ResultResponse<UserDTO>(
                        success: false,
                        message: "Invalid Password.",
                        result: null
                    );
                }
            }

            return new ResultResponse<UserDTO>(
                true,
                "User Logged In.",
                new UserDTO { Id = user.Id, Name = user.Name }
            );
        }

        private async Task<bool> UserExists(string emailAddress)
        {
            return await _userRepository
                .GetAllAsync()
                .AnyAsync(x => x.EmailAddress == emailAddress.ToLower());
        }
    }
}
