using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasicEducationDepartmentWeb.Helpers
{
    public class Sessions
    {

        public static int? UserId
        {
            get
            {
                if (HttpContext.Current.Session["userid"] == null)
                    return null;

                return Convert.ToInt32(HttpContext.Current.Session["userid"]);
            }

            set
            {
                HttpContext.Current.Session["userid"] = value;
            }
        }

        public static string AdminToken
        {
            get
            {
                if (HttpContext.Current.Session["admin_token"] == null)
                    return "";

                return HttpContext.Current.Session["admin_token"].ToString();
            }

            set
            {
                HttpContext.Current.Session["admin_token"] = value;
            }
        }      
        
        public static string StudentToken
        {
            get
            {
                if (HttpContext.Current.Session["student_token"] == null)
                    return "";

                return HttpContext.Current.Session["student_token"].ToString();
            }

            set
            {
                HttpContext.Current.Session["student_token"] = value;
            }
        }

        public static string TeacherToken
        {
            get
            {
                if (HttpContext.Current.Session["teacher_token"] == null)
                    return "";

                return HttpContext.Current.Session["teacher_token"].ToString();
            }

            set
            {
                HttpContext.Current.Session["teacher_token"] = value;
            }
        }

        public static string Username
        {
            get
            {
                if (HttpContext.Current.Session["username"] == null)
                    return "";

                return HttpContext.Current.Session["username"].ToString();
            }

            set
            {
                HttpContext.Current.Session["username"] = value;
            }
        }

        public static string FirstName
        {
            get
            {
                if (HttpContext.Current.Session["firstName"] == null)
                    return "";

                return HttpContext.Current.Session["firstName"].ToString();
            }

            set
            {
                HttpContext.Current.Session["firstName"] = value;
            }
        }

        public static string LastName
        {
            get
            {
                if (HttpContext.Current.Session["lastName"] == null)
                    return "";

                return HttpContext.Current.Session["lastName"].ToString();
            }

            set
            {
                HttpContext.Current.Session["lastName"] = value;
            }
        }

        public string Name
        {
            get
            {
                return LastName + ", " + FirstName;
            }
        }

        public static int? UserTypeId
        {
            get
            {
                if (HttpContext.Current.Session["userTypeId"] == null)
                    return null;

                return Convert.ToInt32(HttpContext.Current.Session["userTypeId"]);
            }

            set
            {
                HttpContext.Current.Session["userTypeId"] = value;
            }
        }


        public static string UserType
        {
            get
            {
                if (HttpContext.Current.Session["userType"] == null)
                    return null;

                return HttpContext.Current.Session["userType"].ToString();
            }

            set
            {
                HttpContext.Current.Session["userType"] = value;
            }
        }

    }
}