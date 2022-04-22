using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Assignment3.Models;
using MySql.Data.MySqlClient;
using System.Web.Http.Cors;

namespace Assignment3.Controllers
{
    public class StudentDataController : ApiController
    {
        // The database context class which allows us to access our MySQL Database.
        public SchoolDBContext school = new SchoolDBContext();

        //This Controller Will access the students table of our school database.
        /// <summary>
        /// Returns a list of students in the system
        /// </summary>
        /// <example>GET api/StudentData/ListStudent</example>
        /// <returns>
        /// A list of Students (first names and last names)
        /// </returns>
        [HttpGet]
        public IEnumerable<Student> ListStudent()
        {
            //Create an instance of a connection
            MySqlConnection Conn = school.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select * from students";

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Create an empty list of student Names
            List<Student> studentNames = new List<Student>();

            //Loop Through Each Row the Result Set
            while (ResultSet.Read())
            {
                //Access Column information by the DB column name as an index
                Student studentList = new Student();
                studentList.StudentID = Convert.ToInt32(ResultSet["studentid"]);
                studentList.StudentName = ResultSet["studentfName"].ToString() + " " + ResultSet["studentlName"].ToString();

                //Add the student Name to the List
                studentNames.Add(studentList);
            }

            //Close the connection between the MySQL Database and the WebServer
            Conn.Close();

            //Return the final list of student names
            return studentNames;
        }

        public Student FindStudent(int id)
        {
            Student newStudent = new Student();
            //Create an instance of a connection
            MySqlConnection Conn = school.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select * from students left join studentsxclasses on studentsxclasses.studentid=students.studentid join classes on classes.classid=studentsxclasses.classid where studentsxclasses.studentid = " + id;

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            while (ResultSet.Read())
            {
                int studentID = Convert.ToInt32(ResultSet["studentid"]);
                string studentfname = ResultSet["studentfname"].ToString();
                string studentlname = ResultSet["studentlname"].ToString();
                string studentNumber = ResultSet["studentnumber"].ToString();
                DateTime enrolDate = (DateTime)ResultSet["enroldate"];
                string className = ResultSet["classname"].ToString();
                

                newStudent.StudentID = studentID;
                newStudent.StudentName = studentfname + " " + studentlname;
                newStudent.StudentNumber = studentNumber;
                newStudent.Enroldate = enrolDate;
                newStudent.ClassName = className;   
               

            }

            return newStudent;
        }


        [HttpGet]
        [Route("api/StudentData/ListStudent/{ClassID}")]
        [EnableCors(origins: "*", methods: "*", headers: "*")]
        public IEnumerable<Student> getStudentsForClasses(int classid)
        {
            List<Student> Students = new List<Student>();

            MySqlConnection Conn = school.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            cmd.CommandText = "Select* from students left join studentsxclasses on studentsxclasses.studentid = students.studentid join classes on classes.classid = studentsxclasses.classid where classes.classid=@classid";
            cmd.Parameters.AddWithValue("@classid", classid);
            cmd.Prepare();

            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Loop Through Each Row the Result Set               
            while (ResultSet.Read())
            {
                int ClassId = Convert.ToInt32(ResultSet["classid"]);
                string classname = ResultSet["classname"].ToString();

                Student NewStudent = new Student();
                NewStudent.ClassID = ClassId;
                NewStudent.StudentID= Convert.ToInt32(ResultSet["studentid"]);
                NewStudent.StudentName = ResultSet["studentfname"].ToString() + " " + ResultSet["studentlname"].ToString(); ;
                NewStudent.ClassName = classname;

                Students.Add(NewStudent);
            }

            return Students;

        }
    }
}
