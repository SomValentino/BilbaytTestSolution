﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bilbayt.Data.Interfaces;
using Bilbayt.Domain;
using Bilbayt.Web.API.Services.Interfaces;
using Bilbayt.Web.API.utils;

namespace Bilbayt.Web.API.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<ApplicationUser> CreateUser(ApplicationUser user)
        {
            user.Password = user.Password.Hash();
            var usrdb = await _userRepository.Create(user);

            return usrdb;
        }
    }
}
