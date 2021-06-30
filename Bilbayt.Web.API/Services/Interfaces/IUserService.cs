using Bilbayt.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bilbayt.Web.API.Services.Interfaces
{
    public interface IUserService
    {
        Task<ApplicationUser> CreateUser(ApplicationUser user);
        Task<ApplicationUser> GetUserByUsernameAsync(string username);
        Task<ApplicationUser> GetUserByEmailAsync(string username);
        Task<ApplicationUser> GetUserByIdAsync(string id);
    }
}
