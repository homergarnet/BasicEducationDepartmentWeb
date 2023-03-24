using BasicEducationDepartmentWeb.Filters;
using BasicEducationDepartmentWeb.Helpers;
using BasicEducationDepartmentWeb.Models.DTO;
using BasicEducationDepartmentWeb.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BasicEducationDepartmentWeb.Controllers
{

    [HandleError]
    public class TeacherController : Controller
    {

        private readonly string _api = ConfigurationManager.AppSettings["api"];

        // GET: Teacher
        public ActionResult Login(string returnUrl)
        {

            ViewBag.ReturnUrl = returnUrl;

            if (Session["teacher_token"] == null)
            {

                return View();

            }
            else
            {

                return RedirectToAction("Form", "Teacher", new { area = "" });

            }

        }

        [HttpPost]
        [Route("teacher/signin")]
        public JsonResult Signin(UsersSigninDTO dto)
        {
            if (!ModelState.IsValid)
                return Json(new { status = 400, txt = ModelState.Values.SelectMany(x => x.Errors).Select(s => s.ErrorMessage).FirstOrDefault() });

            var userAuthSvc = new UserAuthSvc();

            HttpResponseCustom response = userAuthSvc.TeacherLogin(dto);

            if (response.StatusCode == 200)
            {
                Sessions.TeacherToken = response.GetCustomMessage().Trim();

                var userSvc = new UserSvc(Sessions.TeacherToken);
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


            return Json(new { status = response.StatusCode.ToString(), txt = response.Result, token = Sessions.TeacherToken, apiLink = apiLink });
        }

        public ActionResult Signup(string returnUrl)
        {

            ViewBag.ReturnUrl = returnUrl;
            ViewBag.ApiLink = ConfigurationManager.AppSettings["api"]; ;
            if (Session["teacher_token"] == null)
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

        [Route("teacher/reset-password/{id}")]
        public ActionResult ResetPassword(int id)
        {

            ViewBag.AccountID = id;
            ViewBag.Api = _api;
            return View();

        }

        [HttpGet, Route("teacher/carret-field")]
        public PartialViewResult CarretField()
        {
            try
            {

                return PartialView(@"~/Views/Teacher/_carretField.cshtml");

            }
            catch (Exception ex)
            {

                return PartialView();

            }
        }

        [TeacherAuthorizationFilter]
        [Route("teacher/referral-form")]
        public ActionResult Form()
        {
            return View();
        }


        [TeacherAuthorizationFilter]
        [HttpGet]
        [Route("get-teacher-by-id")]
        public JsonResult GetStudentById()
        {

            try
            {

                var svc = new TeacherSvc(Sessions.TeacherToken);
                var response = svc.GetTeacherById();
                return Json(response, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }

        }

        [TeacherAuthorizationFilter]
        [HttpPost]
        [Route("teacher/referral-form")]
        public JsonResult StudentReferralForm(StudentReferralFormDTO dto)
        {

            if (!ModelState.IsValid)
                return Json(new { status = 400, txt = ModelState.Values.SelectMany(x => x.Errors).Select(s => s.ErrorMessage).FirstOrDefault() });

            var teacherSvc = new TeacherSvc(Sessions.TeacherToken);

            HttpResponseCustom response = teacherSvc.StudentReferralForm(dto);

            return Json(new { status = response.StatusCode.ToString(), txt = response.Result });

        }

        [Route("teacher/change-password")]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [Route("teacher/change-password")]
        public JsonResult ChangePassword(UserChangePasswordDTO dto)
        {

            if (!ModelState.IsValid)
                return Json(new { status = 400, txt = ModelState.Values.SelectMany(x => x.Errors).Select(s => s.ErrorMessage).FirstOrDefault() });

            var teacherSvc = new TeacherSvc(Sessions.TeacherToken);

            HttpResponseCustom response = teacherSvc.UpdateTeacherPassword(dto);

            return Json(new { status = response.StatusCode.ToString(), txt = response.Result });

        }

        public ActionResult Logout()
        {

            Session.Remove("teacher_token");
            Session.Abandon();
            return Redirect("Login");

        }

    }
}