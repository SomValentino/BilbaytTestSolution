using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bilbayt.Data.Interfaces;
using Bilbayt.Domain;
using Bilbayt.Web.API.Services.Interfaces;
using Bilbayt.Web.API.utils;
using Microsoft.Extensions.Configuration;

namespace Bilbayt.Web.API.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailTemplateService _emailTemplateService;
        private readonly IEmailQueueRepository _emailQueueRepository;
        private readonly IConfiguration _configuration;

        public UserService(IUserRepository userRepository, IEmailTemplateService emailTemplateService, IEmailQueueRepository emailQueueRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _emailTemplateService = emailTemplateService;
            _emailQueueRepository = emailQueueRepository;
            _configuration = configuration;
        }
        public async Task<ApplicationUser> CreateUser(ApplicationUser user)
        {
            user.Password = user.Password.Hash();
            var usrdb = await _userRepository.CreateAsync(user);

            if (usrdb == null && string.IsNullOrEmpty(usrdb.Id)) throw new Exception("Failed to create user");

            // create email queue
            var template = await _emailTemplateService.CreateRegistrationEmailTemplate(usrdb);

            await _emailQueueRepository.AddEmailToQueue(new Domain.Models.EmailQueue
            {
                Status = Domain.Enums.EmailStatus.Pending,
                ToAddresses = usrdb.Email,
                FromAddress = _configuration.GetValue<string>("toEmailAddress"),
                Body = template,
                Subject = "User Registration",
                CreatedDate = DateTime.Now
            });

            return usrdb;
        }

        public async Task<ApplicationUser> GetUserByEmailAsync(string email)
        {
            return await _userRepository.GetUserByEmailAsync(email);
        }

        public async Task<ApplicationUser> GetUserByIdAsync(string id)
        {
            return await _userRepository.GetAsync(id);
        }

        public async Task<ApplicationUser> GetUserByUsernameAsync(string username)
        {
            return await _userRepository.GetUserByUsernameAsync(username);
        }
    }
}
