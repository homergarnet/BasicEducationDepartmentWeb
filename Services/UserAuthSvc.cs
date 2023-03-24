using BasicEducationDepartmentWeb.Helpers;
using BasicEducationDepartmentWeb.Models.DTO;
using System.Configuration;

namespace BasicEducationDepartmentWeb.Services
{
    public class UserAuthSvc
    {
        private readonly string _api = ConfigurationManager.AppSettings["api"];
        private readonly HttpWebRequestHelper _httpRequest = new HttpWebRequestHelper();

        public HttpResponseCustom AdminLogin(UsersSigninDTO dto)
        {
            HttpResponseCustom response = _httpRequest.Post($"{_api}api/admin-login", dto.Serialize());

            return response;
        }

        public HttpResponseCustom StudentLogin(UsersSigninDTO dto)
        {
            HttpResponseCustom response = _httpRequest.Post($"{_api}api/login", dto.Serialize());

            return response;
        }

        public HttpResponseCustom TeacherLogin(UsersSigninDTO dto)
        {
            HttpResponseCustom response = _httpRequest.Post($"{_api}api/teacher-login", dto.Serialize());

            return response;
        }

    }
}