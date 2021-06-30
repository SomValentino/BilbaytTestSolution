using Bilbayt.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bilbayt.Web.API.Services.Interfaces
{
    public interface IEmailTemplateService
    {
        Task<string> CreateRegistrationEmailTemplate(ApplicationUser user);
    }
}
