using API.Entities;
using API.Models;
using API.Repositories.Interfaces;
using API.Services.Interfaces;
using API.ViewModels;

namespace API.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _userRepository;

        public UsersService(IUsersRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ResultResponse<UserViewModel>> GetByIdAsync(Guid id)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(id);
                if (user == null)
                {
                    return new ResultResponse<UserViewModel>(
                        success: false,
                        message: "User not found.",
                        result: null
                    );
                }
                var userModel = new UserViewModel { Id = user.Id, Name = user.Name, };

                return new ResultResponse<UserViewModel>(
                    success: true,
                    message: "User found.",
                    userModel
                );
            }
            catch (Exception ex)
            {
                return new ResultResponse<UserViewModel>(
                    success: false,
                    message: $"An error occurred: {ex.Message}",
                    result: null
                );
            }
        }

        public async Task<ResultResponse<IEnumerable<UserViewModel>>> GetAllAsync()
        {
            try
            {
                var users = await _userRepository.GetAllAsync();
                if (users == null || !users.Any())
                {
                    return new ResultResponse<IEnumerable<UserViewModel>>(
                        success: false,
                        message: "No users found.",
                        result: null
                    );
                }

                var userModels = users
                    .Select(user => new UserViewModel { Id = user.Id, Name = user.Name })
                    .ToList();

                return new ResultResponse<IEnumerable<UserViewModel>>(
                    success: true,
                    message: "Users found.",
                    userModels
                );
            }
            catch (Exception ex)
            {
                return new ResultResponse<IEnumerable<UserViewModel>>(
                    success: false,
                    message: $"An error occurred: {ex.Message}",
                    result: null
                );
            }
        }

        public async Task<BaseResponse> Insert(UserViewModel model)
        {
            try
            {
                if (model == null)
                {
                    return new BaseResponse(success: false, message: "Model is null.");
                }

                var user = new User { Name = model.Name };

                await _userRepository.Insert(user);

                return new BaseResponse(
                    success: true,
                    message: $"Successfully Inserted User with Id: {user.Id}."
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
    }
}
