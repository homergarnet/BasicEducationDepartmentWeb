using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BasicEducationDepartmentWeb.Filters
{
    public class TeacherAuthorizationFilter : ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            if (filterContext != null)
            {
                HttpSessionStateBase Session = filterContext.HttpContext.Session;

                if (Session["teacher_token"] == null)
                {
                    if (filterContext.HttpContext.Request.IsAjaxRequest())
                    {
                        filterContext.Result = new JsonResult
                        {
                            Data = new { redirect = true },
                            JsonRequestBehavior = JsonRequestBehavior.AllowGet
                        };
                    }
                    else
                    {

                        var returnUrl = filterContext.HttpContext.Request.Url.AbsolutePath;

                        filterContext.Result = new RedirectToRouteResult(
                            new RouteValueDictionary(
                                new
                                {
                                    action = "Login",
                                    controller = "Home",
                                    returnUrl = returnUrl,
                                    //area = "Admin"
                                }
                            )
                        );

                    }
                }
            }

        }

    }
}