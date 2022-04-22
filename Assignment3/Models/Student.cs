using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Assignment3.Models
{
    public class Student
    {
        public int StudentID { get; set; }
        public string StudentName { get; set; }
        public string StudentNumber { get; set; }
        public DateTime Enroldate { get; set; }
 
        //public List<Classes> ClassName { get; set;}
        public string ClassName { get; set; }
        public int ClassID { get; set; }
    }
}