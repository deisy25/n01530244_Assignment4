using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Assignment3.Models
{
    public class Teacher
    {
        public int TeacherId { get; set; }
        public string TeacherFName { get; set; }
        public string TeacherLName { get; set; }
        public decimal Teachersalary { get; set; }
        public string Teachernumber { get; set; }
        public DateTime TeacherHire { get; set; }
        public string ClassName { get; set; }
        public int ClassId { get; set; }

        public bool IsValid()
        {
            bool valid = true;
            if (TeacherFName == null || TeacherLName == null || Teachernumber == null || Teachersalary == 0)
            {
                valid = false;
            } else
            {
                if (TeacherLName.Length <3) valid = false;
                if (TeacherFName.Length <3) valid = false;
            }

            return valid;
        }
    }

}