using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Bilbayt.Domain;

namespace Bilbayt.Data.Interfaces
{
    public interface IUserRepository : IBaseRepository<ApplicationUser>
    {
        Task<ApplicationUser> GetUserByUsernameAsync(string username);
        Task<ApplicationUser> GetUserByEmailAsync(string email);
    }
}
