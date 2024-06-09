using Exam.Database.Models;
using Exam.Shared.DTOs;

namespace Exam.BusinessLogic.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserRegisterDTO> RegisterUserAsync(UserRegisterDTO userRegisterDTO);

        Task<UserDeleteDTO> DeleteUserAsync(int userId);

        Task<IEnumerable<UserGetAllDTO>> GetAllUsersAsync();
        Task<string> LoginUserAsync(UserLoginDTO userLoginDTO);
    }
}
