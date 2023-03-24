using BasicEducationDepartmentWeb.Helpers;
using BasicEducationDepartmentWeb.Models.DTO;
using BasicEducationDepartmentWeb.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BasicEducationDepartmentWeb.Areas.Admin.Controllers
{

    [RouteArea("Admin", AreaPrefix = "home")]
    [RoutePrefix("home")]
    public class HomeController : Controller
    {

        private readonly string _api = ConfigurationManager.AppSettings["api"];

        // GET: Admin/Login
        public ActionResult Login(string returnUrl)
        {

            ViewBag.ReturnUrl = returnUrl;

            if (Session["admin_token"] == null)
            {

                return View();

            }
            else
            {

                return RedirectToAction("Dashboard", "Dashboard", new { area = "Admin" });

            }

        }

        [HttpPost]
        [Route("signin")]
        public JsonResult Signin(UsersSigninDTO dto)
        {
            if (!ModelState.IsValid)
                return Json(new { status = 400, txt = ModelState.Values.SelectMany(x => x.Errors).Select(s => s.ErrorMessage).FirstOrDefault() });

            var userAuthSvc = new UserAuthSvc();

            HttpResponseCustom response = userAuthSvc.AdminLogin(dto);

            if (response.StatusCode == 200)
            {
                Sessions.AdminToken = response.GetCustomMessage().Trim();

                var userSvc = new UserSvc(Sessions.AdminToken);
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


            return Json(new { status = response.StatusCode.ToString(), txt = response.Result, token = Sessions.AdminToken, apiLink = apiLink });
        }

        public ActionResult ForgotPassword()
        {
            return View();
        }


        [Route("reset-password/{id}")]
        public ActionResult ResetPassword(int id)
        {

            ViewBag.AccountID = id;
            ViewBag.Api = _api;
            return View();

        }

        public ActionResult Logout()
        {

            Session.Remove("admin_token");
            Session.Abandon();
            return Redirect("Login");

        }

    }
}