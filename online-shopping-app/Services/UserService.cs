using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using online_shopping_app.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;

namespace online_shopping_app.Services
{
    public class UserService : IUserService
    {
        private readonly IMongoCollection<User> _users;
        public readonly IConfiguration _config;

        public UserService(IDatabaseSettings settings, IMongoClient mongoClient, IConfiguration config)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _users = database.GetCollection<User>(settings.CollectionForUsers);
            _config = config;
        }

        public User UserRegistration(User user)
        {
            var checkEmail = _users.Find(x => x.Email == user.Email).FirstOrDefault();
            var checkUserName = _users.Find(x => x.LoginId == user.LoginId).FirstOrDefault();

            if (checkEmail == null && checkUserName == null)
            {
                _users.InsertOne(user);
                return user;
            }
            else
            {
                return null;
            }
        }

        public User GetUserById(string id)
        {
            return _users.Find(user => user.UserId == id).FirstOrDefault();
        }
        public User GetUserByUserName(string userName)
        {
            return _users.Find(user => user.LoginId == userName).FirstOrDefault();
        }

        public User AuthenticateUser(User user)
        {
            var userfound = this._users.Find(x => x.LoginId == user.LoginId && x.Password == user.Password).FirstOrDefault();
            if (userfound != null)
            {
                return user;
            }
            else
            {
                return null;
            }
        }

        public string GenerateToken(User users)
        {
            var secuirityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(secuirityKey, SecurityAlgorithms.HmacSha256);
            var u = GetUserByUserName(users.LoginId);
            var claims = new[]
            {
                new Claim("userName", users.LoginId),
                new Claim("userId", u.UserId),
                new Claim("role", u.Role)
            };

            var token = new JwtSecurityToken
            (
                _config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims, 
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: credentials
             );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public void ForgotPassword(string userName, User user)
        {
            _users.ReplaceOne(user => user.LoginId == userName, user);
        }
    }

}
