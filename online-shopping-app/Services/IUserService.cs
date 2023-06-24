using online_shopping_app.Models;

namespace online_shopping_app.Services
{
    public interface IUserService
    {
        User GetUserById(string id);
        User GetUserByUserName(string userName);
        User? UserRegistration(User user);
        User AuthenticateUser(User user);
        public string GenerateToken(User user);
        void ForgotPassword(string userName, User user);
    }
}
