using Bilbayt.Domain;
using Bilbayt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bilbayt.Web.API.Services.Interfaces
{
    public interface IUserService
    {
        Task<ApplicationUser> CreateUser(ApplicationUser user);
        Task<ApplicationUser> GetUserByUsername(string username);
    }
}
