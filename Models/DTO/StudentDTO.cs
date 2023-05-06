using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasicEducationDepartmentWeb.Models.DTO
{
    public class StudentDTO
    {
        StudentDetails StudentDetails { get; set; }
        public string Token { get; set; }
        public string ApiLink { get; set; }
        public long Reference_Number { get; set; }
    }

    public class StudentDetails
    {
        public long Reference_Number { get; set; }
        public long Student_number { get; set; }
        public long LRN { get; set; }
        public string Last_Name { get; set; }
        public string First_Name { get; set; }
        public string Middle_Name { get; set; }
        public string Nick_Name { get; set; }
        public string AdmittedSY { get; set; }
        public string Gradelevel { get; set; }
        public string Section { get; set; }
        public string Gender { get; set; }
    }
}