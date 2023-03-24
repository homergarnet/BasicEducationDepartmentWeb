using BasicEducationDepartmentWeb.Helpers;
using BasicEducationDepartmentWeb.Models.DTO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace BasicEducationDepartmentWeb.Services
{
    public class ResetPasswordSvc
    {

        private readonly string _api = ConfigurationManager.AppSettings["api"];
        private readonly HttpWebRequestHelper _httpRequest = new HttpWebRequestHelper();

        public HttpResponseCustom ResetPassword(UserChangePasswordDTO dto)
        {
            HttpResponseCustom response = _httpRequest.Post($"{_api}api/reset-password", dto.Serialize());

            return response;
        }

    }
}