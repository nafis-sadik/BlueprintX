using Data.ViewModels;
using Services.Abstraction;

namespace Services.Implementation
{
    public class UserService : IUserService
    {
        public Task DeleteAccount()
        {
            throw new NotImplementedException();
        }

        public Task<User> GetDetails()
        {
            throw new NotImplementedException();
        }

        public Task<string> Login(User userModel)
        {
            throw new NotImplementedException();
        }

        public Task ResetPassword()
        {
            throw new NotImplementedException();
        }

        public Task<User> SignUp(User userModel)
        {
            throw new NotImplementedException();
        }

        public Task<User> UpdateInfoAsync(User userModel)
        {
            throw new NotImplementedException();
        }
    }
}
