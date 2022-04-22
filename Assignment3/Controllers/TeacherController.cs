using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Assignment3.Models;
using Assignment3.Models.ViewModel;
using System.Diagnostics;
using System.Dynamic;


namespace Assignment3.Controllers
{
    public class TeacherController : Controller
    {

        private ClassesDataController classesDataController = new ClassesDataController();
        private TeacherDataController controller = new TeacherDataController();
        // GET: Teacher
        public ActionResult Index()
        {

            return View();
        }


        //get: /Teacher/List
        public ActionResult List(string SearchKey=null)
        {
            TeacherDataController controller=new TeacherDataController();
            IEnumerable<Teacher> Teachers = controller.ListTeacher(SearchKey);
            return View(Teachers);
        }

        //get: /Teacher/Show/{id}
        public ActionResult Show(int id)
        {
            ShowTeacher ViewModel = new ShowTeacher();

            TeacherDataController controller = new TeacherDataController();
            IEnumerable<Classes> classTaught = classesDataController.GetClassesForTeacher(id);
            Teacher newTeacher = controller.FindTeacher(id);

            ViewModel.Teacher = newTeacher;
            ViewModel.classesTaught = classTaught;
            return View(ViewModel);
        }


        //GET: /Teacher/New
        public ActionResult New()
        {
            return View();
        }

        //GET: /Teacher/Ajax_new
        public ActionResult Ajax_new()
        {
            return View();
        }

        //POST:/Teacher/Create
        [HttpPost]
        public ActionResult Create(string teacherFname, string teacherlname, string teacherNumber, decimal salary)
        {
            Teacher newTeacher = new Teacher();
            newTeacher.TeacherFName = teacherFname;
            newTeacher.TeacherLName = teacherlname;
            newTeacher.Teachernumber = teacherNumber;
            newTeacher.Teachersalary = salary;

            TeacherDataController controller = new TeacherDataController();
            controller.AddTeacher(newTeacher);

            return RedirectToAction("List");
        }


        //GET: /Teacher/DeleteConfirm/{id}
        public ActionResult DeleteConfirm(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher newTeacher = controller.FindTeacher(id);

            return View(newTeacher);
        }


        //POST: /Teacher/Delete/{id}
        public ActionResult Delete(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            controller.DeleteTeacher(id);
            return RedirectToAction("List");
        }
        
        public ActionResult Ajax_update(int id)
        {
            Teacher SelectedTeacher = controller.FindTeacher(id);
            return View(SelectedTeacher);
        }

        /// <summary>
        /// routes to a dynamicaly generated "teacher Update" page.
        /// </summary>
        /// <param name="id">ID of the teacher</param>
        /// <returns></returns>
        /// <example>GET: /Teacher/Update/5</example>
        public ActionResult Update(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher SelectedTeacher = controller.FindTeacher(id);
            return View(SelectedTeacher);
        }

        [HttpPost]
        public ActionResult Update(int id, string TeacherFName, string TeacherLName, string TeacherNumber, decimal Salary)
        {
            Teacher TeacherInfo = new Teacher();
            TeacherInfo.TeacherFName= TeacherFName;
            TeacherInfo.TeacherLName = TeacherLName;
            TeacherInfo.Teachernumber= TeacherNumber;
            TeacherInfo.Teachersalary= Salary;

            TeacherDataController controller = new TeacherDataController();
            controller.UpdateTeacher(id, TeacherInfo);

            return RedirectToAction("Show/" + id);
        }
    }
}