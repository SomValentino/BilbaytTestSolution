﻿using Bilbayt.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bilbayt.Data.Interfaces;
using Bilbayt.Data.Context;

namespace Bilbayt.Data.Repositories
{
    public class UserRepository : BaseRepository<ApplicationUser>, IUserRepository
    {
        public UserRepository(IDataContextSetting dataContextSetting):base(dataContextSetting)
        {

        }

        public async Task<ApplicationUser> GetUserByUsernameAsync(string username)
        {
            var user = (await FindAsync(x => x.Username == username)).FirstOrDefault();

            return user;
        }
    }
}
