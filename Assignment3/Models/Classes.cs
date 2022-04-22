using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Assignment3.Models
{
    public class Classes
    {
        public int ClassID { get; set; }
        public string TeacherName { get; set; }
        public string ClassCode { get; set; }   
        public string ClassName { get; set; }
        public DateTime StartClass { get; set; }
        public DateTime EndClass { get; set; }
        public int TeacherID { get; set; }
    }
}