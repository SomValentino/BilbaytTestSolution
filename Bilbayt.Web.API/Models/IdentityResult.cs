using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bilbayt.Web.API.Models
{
    public class IdentityResult
    {
        public bool IsSuccess { get; set; }
        public string Error { get; set; }
        public string Token { get; set; }
    }
}
