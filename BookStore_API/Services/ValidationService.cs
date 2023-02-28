using MongoDB.Driver;
using Microsoft.Extensions.Configuration;
using BookStore_API.Models.DTOs;
using BookStore_API.Models;

namespace BookStore_API.Services
{
    public class ValidationService
    {
        private readonly IMongoCollection<User> _users;
        public ValidationService(IConfiguration configuration)
        {
            MongoClient client = new MongoClient(configuration.GetConnectionString("BookStoreMongoDb"));
            IMongoDatabase database = client.GetDatabase("BookStoreDb");
            _users = database.GetCollection<User>("Users");

        }
        public bool IsEmailValid(UserRegisterDTO user)
        {
            var email = _users.Find(x => x.Email == user.Email).FirstOrDefault();
            if (email == null)
            {
                return false;
            }
            return true;
        }
        public bool IsUserNameValid(UserRegisterDTO user)
        {
            var userName = _users.Find(x => x.UserName == user.UserName).FirstOrDefault();
            if (userName == null)
            {
                return false;
            }
            return true;
        }
        public bool IsPasswordValid(UserRegisterDTO user)
        {
            var password = user.Password;
            var passwordLength = password.Length;
            var passwordLower = password.Any(char.IsLower);
            var passwordUpper = password.Any(char.IsUpper);
            var passwordDigit = password.Any(char.IsDigit);

            if (passwordLength >= 8 && passwordLower == true && passwordUpper == true && passwordDigit == true)
            {
                return true;
            }
            return false;

        }
        public bool IsConditionsValid(bool checkEmail, bool checkUserName, bool checkPassword)
        {
            if (checkEmail == false && checkUserName == false && checkPassword == true) return true;

            return false;
        }
    }
}
