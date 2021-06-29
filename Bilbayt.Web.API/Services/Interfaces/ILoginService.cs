using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bilbayt.Domain;
using Bilbayt.Web.API.Models;

namespace Bilbayt.Web.API.Services.Interfaces
{
    public interface ILoginService
    {
        Task<IdentityResult> SignIn(ApplicationUser user, string password);
    }
}
