using Data.ViewModels;

namespace Services.Abstraction
{
    public interface IUserService
    {
        Task<User> SignUp(User userModel);
        Task<string> Login(User userModel);
        Task<User> UpdateInfoAsync(User userModel);
        Task<User> GetDetails();
        Task ResetPassword();
        Task DeleteAccount();
    }
}
