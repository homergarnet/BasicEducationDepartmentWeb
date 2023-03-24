using BasicEducationDepartmentWeb.Helpers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace BasicEducationDepartmentWeb.Services
{
    public class UserSvc
    {

        private readonly BEDLogger _logger = new BEDLogger();
        private readonly string _api = ConfigurationManager.AppSettings["api"];
        private readonly HttpWebRequestHelper _httpRequest;
        public string _responseMessage = "";
        public UserSvc(string token)
        {
            _httpRequest = new HttpWebRequestHelper(token);
        }

    }
}