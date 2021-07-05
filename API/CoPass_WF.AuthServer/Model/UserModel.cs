using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoPass_WF.AuthServer.API.Model
{
    public class UserModel
    {
        public string username { get; set; }
        public string password { get; set; }
    }
    public class AuthResponse
    {
        public bool status { get; set; }
        public string message { get; set; }
        public string token { get; set; }
    }
}
