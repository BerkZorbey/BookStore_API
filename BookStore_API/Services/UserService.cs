using AutoMapper;
using BookStore_API.Models;
using BookStore_API.Models.DTOs;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace BookStore_API.Services
{
    public class UserService
    {
        private readonly IMongoCollection<User> _users;
        private readonly IMapper _mapper;
        public UserService(IConfiguration configuration, IMapper mapper)
        {
            MongoClient client = new MongoClient(configuration.GetConnectionString("BookStoreMongoDb"));
            IMongoDatabase database = client.GetDatabase("BookStoreDb");
            _users = database.GetCollection<User>("Users");
            _mapper = mapper;

        }

        public async Task<User> AddUser(UserRegisterDTO user, string user_ıd)
        {
            var newUser = _mapper.Map<User>(user);
            newUser.Id = user_ıd;
            await _users.InsertOneAsync(newUser);
            return newUser;
        }
        public User GetUser(UserLoginDTO loginUser)
        {
            var userModel = _mapper.Map<User>(loginUser);
            var user = _users.Find(x => x.Email == userModel.Email).FirstOrDefault();
            return user;
        }
        public User GetUserById(string Id)
        {
            var user = _users.Find(x => x.Id == Id).FirstOrDefault();
            return user;
        }
        public void UpdateUser(User user)
        {
            _users.ReplaceOne(x => x.Id == user.Id, user);
        }
    }
}
