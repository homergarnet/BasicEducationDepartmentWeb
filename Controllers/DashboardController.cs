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

    [StudentAuthorizationFilter]
    [HandleError]
    public class DashboardController : Controller
    {

        // GET: Dashboard
        [Route("Dashboard/Profile", Name = "Profile")]
        public ActionResult Profile()
        {
            return View();
        }

        [HttpGet]
        [Route("get-student-by-id")]
        public JsonResult GetStudentById()
        {

            try
            {

                var svc = new StudentProfileSvc(Sessions.StudentToken);
                var response = svc.GetStudentById();
                return Json(response, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }

        }


        [Route("Dashboard/Referral-Form")]
        public ActionResult Form()
        {
            return View();
        }

        [Route("dashboard/referral-status")]
        public ActionResult ReferralStatus()
        {
            return View();
        }

        [HttpGet, Route("student/status-list")]
        public PartialViewResult StatusList(StudentReferralFormDTO dto)
        {
            try
            {

                var svc = new StudentDashboardSvc(Sessions.StudentToken);

                List<StudentReferralFormDTO> msg = svc.StatusList(dto);
                return PartialView(@"~/Views/Dashboard/_statusList.cshtml", msg);

            }
            catch (Exception ex)
            {

                return PartialView();

            }
        }

        [HttpPost]
        [Route("dashboard/referral-form")]
        public JsonResult StudentReferralForm(StudentReferralFormDTO dto)
        {

            if (!ModelState.IsValid)
                return Json(new { status = 400, txt = ModelState.Values.SelectMany(x => x.Errors).Select(s => s.ErrorMessage).FirstOrDefault() });

            var studentDashboardSvc = new StudentDashboardSvc(Sessions.StudentToken);

            HttpResponseCustom response = studentDashboardSvc.StudentReferralForm(dto);

            return Json(new { status = response.StatusCode.ToString(), txt = response.Result });

        }

        [HttpGet, Route("dashboard/carret-field")]
        public PartialViewResult CarretField()
        {
            try
            {

                return PartialView(@"~/Views/Dashboard/_carretField.cshtml");

            }
            catch (Exception ex)
            {

                return PartialView();

            }
        }

        [Route("Dashboard/change-password")]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [Route("Dasboard/change-password")]
        public JsonResult ChangePassword(UserChangePasswordDTO dto)
        {

            if (!ModelState.IsValid)
                return Json(new { status = 400, txt = ModelState.Values.SelectMany(x => x.Errors).Select(s => s.ErrorMessage).FirstOrDefault() });

            var studentDashboardSvc = new StudentDashboardSvc(Sessions.StudentToken);

            HttpResponseCustom response = studentDashboardSvc.UpdateStudentPassword(dto);

            return Json(new { status = response.StatusCode.ToString(), txt = response.Result });

        }

    }
}