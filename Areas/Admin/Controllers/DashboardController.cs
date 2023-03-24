using BasicEducationDepartmentWeb.Filters;
using BasicEducationDepartmentWeb.Helpers;
using BasicEducationDepartmentWeb.Models.DTO;
using BasicEducationDepartmentWeb.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace BasicEducationDepartmentWeb.Areas.Admin.Controllers
{
    [RoutePrefix("dashboard")]
    [AdminAuthorizationFilter]
    public class DashboardController : Controller
    {
        // GET: Admin/Dashboard
        public ActionResult Home() => View();

        public ActionResult Dashboard()
        {
            return View();
        }

        public ActionResult Profile()
        {
            return View();
        }

        [HttpGet, Route("admin-carret-field")]
        public PartialViewResult CarretField()
        {
            try
            {

                return PartialView(@"~/Areas/Admin/Views/Dashboard/_carretField.cshtml");

            }
            catch (Exception ex)
            {

                return PartialView();

            }
        }

        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [Route("admin-change-password")]
        public JsonResult ChangePassword(UserChangePasswordDTO dto)
        {

            if (!ModelState.IsValid)
                return Json(new { status = 400, txt = ModelState.Values.SelectMany(x => x.Errors).Select(s => s.ErrorMessage).FirstOrDefault() });

            var adminProfileSvc = new AdminProfileSvc(Sessions.AdminToken);

            HttpResponseCustom response = adminProfileSvc.UpdateAdminPassword(dto);

            return Json(new { status = response.StatusCode.ToString(), txt = response.Result });

        }

        [HttpGet]
        [Route("get-admin-by-id")]
        public JsonResult GetAdminById()
        {

            try
            {

                var svc = new AdminProfileSvc(Sessions.AdminToken);
                var response = svc.GetAdminById();
                return Json(response, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpGet, Route("status-list")]
        public PartialViewResult StatusList(StudentReferralFormDTO dto)
        {
            try
            {

                var svc = new AdminProfileSvc(Sessions.AdminToken);
       
                List<StudentReferralFormDTO> msg = svc.StatusList(dto);
                return PartialView(@"~/Areas/Admin/Views/Dashboard/_statusList.cshtml", msg);

            }
            catch (Exception ex)
            {

                return PartialView();

            }
        }

        [HttpGet]
        [Route("get-student-referral-form-by-id")]
        public JsonResult GetStudentReferralFormById(StudentReferralFormDTO dto)
        {

            try
            {

                var svc = new AdminProfileSvc(Sessions.AdminToken);
                var response = svc.GetStudentReferralFormById(dto);
                return Json(response, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        [Route("update-queing-status")]
        public JsonResult UpdateQueingStatus(StudentReferralFormDTO dto)
        {

            if (!ModelState.IsValid)
                return Json(new { status = 400, txt = ModelState.Values.SelectMany(x => x.Errors).Select(s => s.ErrorMessage).FirstOrDefault() });

            var studentDashboardSvc = new AdminProfileSvc(Sessions.AdminToken);

            HttpResponseCustom response = studentDashboardSvc.UpdateQueingStatus(dto);

            return Json(new { status = response.StatusCode.ToString(), txt = response.Result });

        }

        [HttpGet]
        [Route("get-total-referral-form-count")]
        public JsonResult GetTotalReferralFormCount()
        {

            try
            {

                var svc = new AdminProfileSvc(Sessions.AdminToken);
                var response = svc.GetTotalReferralFormCount();
                return Json(response, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult Status() => View();


    }
}