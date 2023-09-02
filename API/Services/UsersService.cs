using System.Security.Cryptography;
using System.Text;
using API.Entities;
using API.DTOs;
using API.Repositories.Interfaces;
using API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace API.Services
{
    public class UsersService : IUsersService
    {
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly IUsersRepository _userRepository;

        public UsersService(
            ITokenService tokenService,
            IMapper mapper,
            IUsersRepository userRepository
        )
        {
            _tokenService = tokenService;
            _mapper = mapper;
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

                var userModel = _mapper.Map<UserDTO>(user);
                userModel.Token = _tokenService.CreateToken(user);

                return new ResultResponse<UserDTO>(
                    success: true,
                    message: "User found.",
                    result: userModel
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

        public async Task<ResultResponse<UserDTO>> GetByEmailAddressAsync(string emailAddress)
        {
            try
            {
                var user = await _userRepository.GetByEmailAddressAsync(emailAddress);
                if (user == null)
                {
                    return new ResultResponse<UserDTO>(
                        success: false,
                        message: "User not found.",
                        result: null
                    );
                }

                var userModel = _mapper.Map<UserDTO>(user);
                userModel.Token = _tokenService.CreateToken(user);

                return new ResultResponse<UserDTO>(
                    success: true,
                    message: "User found.",
                    result: userModel
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

                var userModels = _mapper.Map<IEnumerable<UserDTO>>(users);

                return new ResultResponse<IEnumerable<UserDTO>>(
                    success: true,
                    message: "Users found.",
                    result: userModels
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

        public async Task<ResultResponse<UserDTO>> Register(RegisterUserDTO dto)
        {
            try
            {
                if (await UserExists(dto.EmailAddress))
                {
                    return new ResultResponse<UserDTO>(
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

                await _userRepository.Register(user);

                var userDTO = _mapper.Map<UserDTO>(user);
                userDTO.Token = _tokenService.CreateToken(user);

                return new ResultResponse<UserDTO>(
                    success: true,
                    message: $"Successfully Registered User: {user.Name} with Email Address: {user.EmailAddress}.",
                    result: userDTO
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

            var userDTO = _mapper.Map<UserDTO>(user);
            userDTO.Token = _tokenService.CreateToken(user);

            return new ResultResponse<UserDTO>(
                success: true,
                message: "User Logged In.",
                result: userDTO
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
