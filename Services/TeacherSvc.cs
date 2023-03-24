using BasicEducationDepartmentWeb.Helpers;
using BasicEducationDepartmentWeb.Models.DTO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace BasicEducationDepartmentWeb.Services
{
    public class TeacherSvc
    {

        private readonly BEDLogger _logger = new BEDLogger();
        private readonly string _api = ConfigurationManager.AppSettings["api"];
        private readonly HttpWebRequestHelper _httpRequest = new HttpWebRequestHelper();
        public string _responseMessage = "";

        public TeacherSvc(string token)
        {
            _httpRequest = new HttpWebRequestHelper(token);
        }

        public HttpResponseCustom StudentReferralForm(StudentReferralFormDTO dto)
        {

            HttpResponseCustom response = _httpRequest.Post($"{_api}api/create-teacher-referral-form", dto.Serialize());

            return response;

        }

        public HttpResponseCustom UpdateTeacherPassword(UserChangePasswordDTO dto)
        {

            HttpResponseCustom response = _httpRequest.Put($"{_api}api/update-teacher-password", dto.Serialize());

            return response;

        }

        public AccountProfileDTO GetTeacherById()
        {

            var result = new AccountProfileDTO();
            HttpResponseCustom response = _httpRequest.Get($"{_api}api/get-teacher-by-id");

            if (response.StatusCode == 200)
                result = response.Result.Deserialize<AccountProfileDTO>();
            else
            {
                _responseMessage = response.Result;
                _logger.LogEvents($"Error on Get Teacher By ID | response: {response.Serialize()}");
            }

            return result;

        }


    }
}