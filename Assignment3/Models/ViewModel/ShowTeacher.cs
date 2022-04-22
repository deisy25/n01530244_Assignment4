using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Assignment3.Models.ViewModel
{
    public class ShowTeacher
    {
        public Teacher Teacher;
        public IEnumerable<Classes> classesTaught;
    }
}