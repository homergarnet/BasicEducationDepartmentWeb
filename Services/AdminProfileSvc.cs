using BasicEducationDepartmentWeb.Helpers;
using BasicEducationDepartmentWeb.Models.DTO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace BasicEducationDepartmentWeb.Services
{
    public class AdminProfileSvc
    {
        private readonly BEDLogger _logger = new BEDLogger();
        private readonly string _api = ConfigurationManager.AppSettings["api"];
        private readonly HttpWebRequestHelper _httpRequest = new HttpWebRequestHelper();
        public string _responseMessage = "";

        public AdminProfileSvc(string token)
        {
            _httpRequest = new HttpWebRequestHelper(token);
        }

        public AccountProfileDTO GetAdminById()
        {

            var result = new AccountProfileDTO();
            HttpResponseCustom response = _httpRequest.Get($"{_api}api/get-admin-by-id");

            if (response.StatusCode == 200)
                result = response.Result.Deserialize<AccountProfileDTO>();
            else
            {
                _responseMessage = response.Result;
                _logger.LogEvents($"Error on Get Admin By ID | response: {response.Serialize()}");
            }

            return result;

        }

        public HttpResponseCustom UpdateAdminPassword(UserChangePasswordDTO dto)
        {

            HttpResponseCustom response = _httpRequest.Put($"{_api}api/update-admin-password", dto.Serialize());

            return response;

        }

        public List<StudentReferralFormDTO> StatusList(StudentReferralFormDTO dto)
        {

            var Messages = new List<StudentReferralFormDTO>();
            HttpResponseCustom response = _httpRequest.Post($"{_api}api/get-status-list", dto.Serialize());

            if (response.StatusCode == 200)
                Messages = response.Result.DeserializeToList<StudentReferralFormDTO>();
            else
            {
                _responseMessage = response.Result;
                _logger.LogEvents($"Call to list | response: {response.Serialize()}");
            }

            return Messages;

        }

        public StudentReferralFormDTO GetStudentReferralFormById(StudentReferralFormDTO dto)
        {

            var result = new StudentReferralFormDTO();
            HttpResponseCustom response = _httpRequest.Post($"{_api}api/get-student-referral-form-by-id", dto.Serialize());

            if (response.StatusCode == 200)
                result = response.Result.Deserialize<StudentReferralFormDTO>();
            else
            {
                _responseMessage = response.Result;
                _logger.LogEvents($"Error on Get Student Referral Form By ID | response: {response.Serialize()}");
            }

            return result;

        }

        public HttpResponseCustom UpdateQueingStatus(StudentReferralFormDTO dto)
        {

            HttpResponseCustom response = _httpRequest.Put($"{_api}api/update-queing-status", dto.Serialize());

            return response;

        }

        public DashboardDTO GetTotalReferralFormCount()
        {

            var result = new DashboardDTO();
            HttpResponseCustom response = _httpRequest.Get($"{_api}api/get-total-referral-form-count");

            if (response.StatusCode == 200)
                result = response.Result.Deserialize<DashboardDTO>();
            else
            {
                _responseMessage = response.Result;
                _logger.LogEvents($"Error on Get GetTotalReferralFormCount | response: {response.Serialize()}");
            }

            return result;

        }

    }
}