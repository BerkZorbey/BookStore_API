using AutoMapper;
using BookStore_API.Models;
using MongoDB.Driver;

namespace BookStore_API.Services
{
    public class EmailService
    {
        private readonly IMongoCollection<UserEmailVerificationModel> _userEmailVerification;
        private readonly TokenGeneratorService _tokenGeneratorService;

        public EmailService(IConfiguration configuration, TokenGeneratorService tokenGeneratorService)
        {
            MongoClient client = new MongoClient(configuration.GetConnectionString("BookStoreMongoDb"));
            IMongoDatabase database = client.GetDatabase("BookStoreDb");
            _userEmailVerification = database.GetCollection<UserEmailVerificationModel>("UserEmailVerification");
            _tokenGeneratorService = tokenGeneratorService;
        }
        public async void CreateEmailVerificationToken(string id)
        {
            var emailModel = new UserEmailVerificationModel();
            var token = _tokenGeneratorService.GenerateToken();
            emailModel.EmailVerificationToken = token;
            emailModel.UserId = id;
            await _userEmailVerification.InsertOneAsync(emailModel);
        }
        public UserEmailVerificationModel GetEmailVerification(string id)
        {
            var emailVerification = _userEmailVerification.Find(x => x.UserId == id).FirstOrDefault();
            return emailVerification;
        }
    }
}
