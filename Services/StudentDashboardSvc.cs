using BasicEducationDepartmentWeb.Helpers;
using BasicEducationDepartmentWeb.Models.DTO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace BasicEducationDepartmentWeb.Services
{
    public class StudentDashboardSvc
    {

        private readonly BEDLogger _logger = new BEDLogger();
        private readonly string _api = ConfigurationManager.AppSettings["api"];
        private readonly HttpWebRequestHelper _httpRequest = new HttpWebRequestHelper();
        public string _responseMessage = "";

        public StudentDashboardSvc(string token)
        {
            _httpRequest = new HttpWebRequestHelper(token);
        }

        public HttpResponseCustom StudentReferralForm(StudentReferralFormDTO dto)
        {

            HttpResponseCustom response = _httpRequest.Post($"{_api}api/create-student-referral-form", dto.Serialize());

            return response;

        }

        public HttpResponseCustom UpdateStudentPassword(UserChangePasswordDTO dto)
        {

            HttpResponseCustom response = _httpRequest.Put($"{_api}api/update-student-password", dto.Serialize());

            return response;

        }

        public List<StudentReferralFormDTO> StatusList(StudentReferralFormDTO dto)
        {

            var Messages = new List<StudentReferralFormDTO>();
            HttpResponseCustom response = _httpRequest.Post($"{_api}api/student/get-status-list", dto.Serialize());

            if (response.StatusCode == 200)
                Messages = response.Result.DeserializeToList<StudentReferralFormDTO>();
            else
            {
                _responseMessage = response.Result;
                _logger.LogEvents($"Call to list | response: {response.Serialize()}");
            }

            return Messages;

        }

    }
}