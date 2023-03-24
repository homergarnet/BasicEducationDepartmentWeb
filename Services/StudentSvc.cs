using BasicEducationDepartmentWeb.Helpers;
using BasicEducationDepartmentWeb.Models.DTO;
using System.Configuration;

namespace BasicEducationDepartmentWeb.Services
{
    public class StudentSvc
    {

        private readonly BEDLogger _logger = new BEDLogger();
        private readonly string _api = ConfigurationManager.AppSettings["api"];
        private readonly HttpWebRequestHelper _httpRequest = new HttpWebRequestHelper();
        public string _responseMessage = "";

        public AccountProfileDTO GetTokenByIdAndType(int accountId, string accountType)
        {

            var result = new AccountProfileDTO();
            HttpResponseCustom response = _httpRequest.Get($"{_api}api/get-token-by-id-and-type/{accountId}/{accountType}");

            if (response.StatusCode == 200)
                result = response.Result.Deserialize<AccountProfileDTO>();
            else
            {
                _responseMessage = response.Result;
                _logger.LogEvents($"Error on Get TokenByIdAndType By ID | response: {response.Serialize()}");
            }

            return result;

        }

        //public HttpResponseCustom GetTokenByIdAndType(int accountId, string accountType)
        //{

        //    HttpResponseCustom response = _httpRequest.Post($"{_api}api/get-token-by-id-and-type/{accountId}/{accountType}");

        //    return response;

        //}

    }
}