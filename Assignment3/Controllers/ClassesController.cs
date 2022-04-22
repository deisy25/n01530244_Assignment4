using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Assignment3.Models;
using Assignment3.Models.ViewModel;
using System.Dynamic;


namespace Assignment3.Controllers
{
    public class ClassesController : Controller
    {
        private StudentDataController StudentDataController = new StudentDataController();
        // GET: Classes
        public ActionResult Index()
        {
            return View();
        }

        //GET: /Classes/List
        public ActionResult List()
        {
            ClassesDataController controller=new ClassesDataController();
            IEnumerable<Classes> classes = controller.ListClass();
            return View(classes); 
        }


        //get: /Classes/Show/{id}
        public ActionResult Show(int id)
        {
            ShowStudents ViewModel = new ShowStudents();

            ClassesDataController controller = new ClassesDataController();
            IEnumerable<Student> Student = StudentDataController.getStudentsForClasses(id);
            Classes NewClasses = controller.FindClass(id);

            ViewModel.Classes= NewClasses;
            ViewModel.Students = Student;
            return View(ViewModel);
        }
    }
}