using BasicEducationDepartmentWeb.Helpers;
using BasicEducationDepartmentWeb.Models.DTO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace BasicEducationDepartmentWeb.Services
{
    public class StudentProfileSvc
    {

        private readonly BEDLogger _logger = new BEDLogger();
        private readonly string _api = ConfigurationManager.AppSettings["api"];
        private readonly HttpWebRequestHelper _httpRequest = new HttpWebRequestHelper();
        public string _responseMessage = "";

        public StudentProfileSvc(string token)
        {
            _httpRequest = new HttpWebRequestHelper(token);
        }

        public AccountProfileDTO GetStudentById()
        {

            var result = new AccountProfileDTO();
            HttpResponseCustom response = _httpRequest.Get($"{_api}api/get-student-by-id");

            if (response.StatusCode == 200)
                result = response.Result.Deserialize<AccountProfileDTO>();
            else
            {
                _responseMessage = response.Result;
                _logger.LogEvents($"Error on Get Student By ID | response: {response.Serialize()}");
            }

            return result;

        }

    }
}