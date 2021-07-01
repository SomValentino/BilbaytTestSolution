using Bilbayt.Domain;
using Bilbayt.Web.API.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Bilbayt.Web.API.Services
{
    public class EmailTemplateService : IEmailTemplateService
    {
        private readonly IWebHostEnvironment _hostinEnvironment;

        public EmailTemplateService(IWebHostEnvironment hostingEnvironment)
        {
            _hostinEnvironment = hostingEnvironment;
        }
        public async Task<string> CreateRegistrationEmailTemplate(ApplicationUser user)
        {
            var filePath = Path.Combine(_hostinEnvironment.ContentRootPath, "Templates/Register.txt");
            var emailTemplate = await File.ReadAllTextAsync(filePath);

            return string.Format(emailTemplate, user.FullName);
        }
    }
}
