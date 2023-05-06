using BasicEducationDepartmentWeb.Helpers;
using BasicEducationDepartmentWeb.Models.DTO;
using BasicEducationDepartmentWeb.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BasicEducationDepartmentWeb.Controllers
{

    [HandleError]
    public class HomeController : Controller
    {

        private readonly string _api = ConfigurationManager.AppSettings["api"];

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login(string returnUrl)
        {

            ViewBag.ReturnUrl = returnUrl;

            if (Session["student_token"] == null)
            {

                return View();

            }
            else
            {

                return RedirectToAction("Form", "Dashboard", new { area = "" });

            }

        }

        [HttpPost]
        [Route("home/student/signin")]
        public JsonResult Signin(UsersSigninDTO dto)
        {
            if (!ModelState.IsValid)
                return Json(new { status = 400, txt = ModelState.Values.SelectMany(x => x.Errors).Select(s => s.ErrorMessage).FirstOrDefault() });

            var userAuthSvc = new UserAuthSvc();

            var response = userAuthSvc.StudentLogin(dto);

 
            // Deserialize the JSON string to an object
            var deserializedStudentDetails = JsonConvert.DeserializeObject<StudentDTO>(response.Result);

            if (response.StatusCode == 200)
            {

                Sessions.StudentToken = deserializedStudentDetails.Token;
                var userSvc = new UserSvc(Sessions.StudentToken);
                //UsersReturnDTO getProfile = userSvc.Profile();
                //if (getProfile != null)
                //{
                //    Sessions.UserId = getProfile.Id;
                //    Sessions.Username = getProfile.EmailAddress;
                //    Sessions.FirstName = getProfile.Name;
                //    Sessions.UserTypeId = getProfile.UsertypeID;
                //}
            }

            string apiLink = ConfigurationManager.AppSettings["api"];


            return Json(new { status = response.StatusCode.ToString(), txt = response, token = Sessions.StudentToken, apiLink = apiLink });
        }

        public ActionResult Signup(string returnUrl)
        {

            ViewBag.ReturnUrl = returnUrl;
            ViewBag.ApiLink = ConfigurationManager.AppSettings["api"]; ;
            if (Session["student_token"] == null)
            {

                return View();

            }
            else
            {

                return RedirectToAction("Profile", "Dashboard", new { area = "" });

            }

        }

        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [Route("home/forgot-password")]
        public JsonResult ForgotPassword(AccountProfileDTO dto)
        {

            if (!ModelState.IsValid)
                return Json(new { status = 400, txt = ModelState.Values.SelectMany(x => x.Errors).Select(s => s.ErrorMessage).FirstOrDefault() });

            var forgotPasswordSvc = new ForgotPasswordSvc();

            HttpResponseCustom response = forgotPasswordSvc.ForgotPassword(dto);

            if (response.StatusCode == 200)
            {

                //UsersReturnDTO getProfile = userSvc.Profile();
                //if (getProfile != null)
                //{
                //    Sessions.UserId = getProfile.Id;
                //    Sessions.Username = getProfile.EmailAddress;
                //    Sessions.FirstName = getProfile.Name;
                //    Sessions.UserTypeId = getProfile.UsertypeID;
                //}

            }

            return Json(new { status = response.StatusCode.ToString(), txt = response.Result });

        }


        [Route("home/reset-password/{id}")]
        public ActionResult ResetPassword(int id)
        {

            ViewBag.AccountID = id;
            ViewBag.Api = _api;
            return View();

        }

        [HttpPost]
        [Route("home/student/reset-password")]
        public JsonResult ResetPassword(UserChangePasswordDTO dto)
        {
            if (!ModelState.IsValid)
                return Json(new { status = 400, txt = ModelState.Values.SelectMany(x => x.Errors).Select(s => s.ErrorMessage).FirstOrDefault() });

            var resetPasswordSvc = new ResetPasswordSvc();

            HttpResponseCustom response = resetPasswordSvc.ResetPassword(dto);

            if (response.StatusCode == 200)
            {

                //UsersReturnDTO getProfile = userSvc.Profile();
                //if (getProfile != null)
                //{
                //    Sessions.UserId = getProfile.Id;
                //    Sessions.Username = getProfile.EmailAddress;
                //    Sessions.FirstName = getProfile.Name;
                //    Sessions.UserTypeId = getProfile.UsertypeID;
                //}

            }

            return Json(new { status = response.StatusCode.ToString(), txt = response.Result });
        }

        [HttpGet]
        [Route("student-get-token-by-id-and-type/{accountId}/{accountType}")]
        public JsonResult GetTokenByIdAndType(int accountId, string accountType)
        {

            try
            {

                var studentSvc = new StudentSvc();
                var response = studentSvc.GetTokenByIdAndType(accountId, accountType);
                return Json(response, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult Logout()
        {

            Session.Remove("student_token");
            Session.Abandon();
            return Redirect("Login");

        }


    }
}