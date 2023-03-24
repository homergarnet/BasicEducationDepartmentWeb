using BasicEducationDepartmentWeb.Helpers;
using BasicEducationDepartmentWeb.Models.DTO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace BasicEducationDepartmentWeb.Services
{
    public class ForgotPasswordSvc
    {

        private readonly string _api = ConfigurationManager.AppSettings["api"];
        private readonly HttpWebRequestHelper _httpRequest = new HttpWebRequestHelper();

        public HttpResponseCustom ForgotPassword(AccountProfileDTO dto)
        {
            HttpResponseCustom response = _httpRequest.Post($"{_api}api/create-f-p-token", dto.Serialize());

            return response;
        }

    }
}