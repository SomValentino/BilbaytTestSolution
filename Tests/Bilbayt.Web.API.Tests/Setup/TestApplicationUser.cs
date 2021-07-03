using System;
using Bilbayt.Domain;
using MongoDB.Bson;

namespace Bilbayt.Web.API.Tests.Setup
{
    public class TestApplicationUser
    {
        public static ApplicationUser GetApplicationUser()
        {
            return new ApplicationUser
            {
                Id = ObjectId.GenerateNewId().ToString(),
                FullName = "TestTest",
                Email = "test@gmail.com",
                Username = "testam",
                Password = "B4fnNF37uOioSnn8aXvKHt0lMW0bEgKNGdaHlDLNAkebl4Jb"
            };
        }

        public static string GetJWTSecret()
        {
            return "test_key_1234465758";
        }

        public static double GetJWTExpry()
        {
            return 3600;
        }

        public static string BaseUrl()
        {
            return "http://localhost:5000";
        }
    }
}
