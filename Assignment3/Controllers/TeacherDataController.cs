using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Assignment3.Models;
using MySql.Data.MySqlClient;
using System.Diagnostics;
using System.Web.Http.Cors;

namespace Assignment3.Controllers
{
    public class TeacherDataController : ApiController
    {
        // The database context class which allows us to access our MySQL Database.
        public SchoolDBContext school = new SchoolDBContext();

        //This Controller Will access the teacher table of our school database.
        /// <summary>
        /// Returns a list of Teacher in the system
        /// </summary>
        /// <example>GET api/TeacherData/ListTeacher</example>
        /// <returns>
        /// A list of teacher (first names and last names)
        /// </returns>
        [HttpGet]
        [Route("api/TeacherData/ListTeacher/{SearchKey}")]
        public IEnumerable<Teacher> ListTeacher(string SearchKey=null)
        {
            //Create an instance of a connection
            MySqlConnection Conn = school.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select * from teachers where lower(teacherfname) like lower(@key) or lower(teacherlname) like lower(@key) or lower(concat(teacherfname, ' ', teacherlname)) like lower(@key)";
            cmd.Parameters.AddWithValue("@key", "%" + SearchKey + "%");

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Create an empty list of Teacher Names
            List<Teacher> teacherNames = new List<Teacher>();

            //Loop Through Each Row the Result Set
            while (ResultSet.Read())
            {
                //Access Column information by the DB column name as an index
                Teacher teacherList = new Teacher();
                teacherList.TeacherId = (int)ResultSet["teacherid"];
                teacherList.TeacherFName = ResultSet["teacherfname"].ToString();
                teacherList.TeacherLName = ResultSet["teacherlname"].ToString();

                //Add the teacher Name to the List
                teacherNames.Add(teacherList);
            }

            //Close the connection between the MySQL Database and the WebServer
            Conn.Close();

            //Return the final list of Teacher names
            return teacherNames;
        }

        /// <summary>
        /// find teacher in the system that given an id
        /// </summary>
        /// <param name="id">the teacher primary key</param>
        /// <returns>An teacher objects</returns>
        public Teacher FindTeacher(int id)
        {
            Teacher newTeacher = new Teacher();
            //Create an instance of a connection
            MySqlConnection Conn = school.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select * from teachers left join classes on teachers.teacherid=classes.teacherid where teachers.teacherid = " + id;

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            while (ResultSet.Read())
            {

                //Access Column information by the DB column name as an index
                int teacherId = (int)ResultSet["teacherId"];
                string teacherFName = ResultSet["teacherfname"].ToString();
                string teacherLName = ResultSet["teacherlname"].ToString();
                decimal teacherSalary = (decimal)ResultSet["salary"];
                string teacherNumber = ResultSet["employeenumber"].ToString();
                DateTime teacherHire = (DateTime)ResultSet["hiredate"];
                string className = ResultSet["classname"].ToString();
              
                if (ResultSet["classid"].ToString() == "")
                {
                    newTeacher.TeacherId = teacherId;
                    newTeacher.TeacherFName = teacherFName;
                    newTeacher.TeacherLName = teacherLName;
                    newTeacher.Teachersalary = teacherSalary;
                    newTeacher.Teachernumber = teacherNumber;
                    newTeacher.TeacherHire = teacherHire;
                } else
                {
                    newTeacher.TeacherId = teacherId;
                    newTeacher.ClassId = (int)ResultSet["classid"];
                    newTeacher.TeacherFName = teacherFName;
                    newTeacher.TeacherLName = teacherLName;
                    newTeacher.Teachersalary = teacherSalary;
                    newTeacher.Teachernumber = teacherNumber;
                    newTeacher.TeacherHire = teacherHire;
                    newTeacher.ClassName = className;
                }
            }

            return newTeacher;
        }

        /// <summary>
        /// Add a Teacher to the SQL Database
        /// </summary>
        /// <param name="newTeacher"></param>
        /// <example>
        /// POST api/TeacherData/AddTeacher
        /// FORM data/post data/request body
        /// {
        ///     "teacherFname":"Bryan",
        ///     "teacherLname":"Smith",
        ///     "teachernumber":"T105",
        ///     "teachersalary":"40.5"
        /// }
        /// </example>
        /// 

        [HttpPost]
        [EnableCors(origins:"*", methods:"*", headers:"*")]
        public void AddTeacher([FromBody]Teacher newTeacher)
        {
            MySqlConnection conn = school.AccessDatabase();

            conn.Open();

            MySqlCommand cmd = conn.CreateCommand();

            cmd.CommandText = "insert into teachers(teacherfname,teacherlname,employeenumber,hiredate,salary) values(@teacherFname,@teacherlname,@teacherNumber,CURRENT_DATE(),@salary)";
            cmd.Parameters.AddWithValue("@teacherFname", newTeacher.TeacherFName);
            cmd.Parameters.AddWithValue("@teacherlname", newTeacher.TeacherLName);
            cmd.Parameters.AddWithValue("@teacherNumber", newTeacher.Teachernumber);
            cmd.Parameters.AddWithValue("@salary", newTeacher.Teachersalary);
            cmd.Prepare();  

            cmd.ExecuteNonQuery();
            conn.Close();

        }

        /// <summary>
        /// Delete teacher from connected database if the ID of the teacher exists
        /// </summary>
        /// <param name="id">The id of the teacher</param>
        /// <example>POST /api/teacherdata/DeleteTeacher/3</example>

        public void DeleteTeacher(int id)
        {
            MySqlConnection conn=school.AccessDatabase();

            conn.Open();

            MySqlCommand cmd=conn.CreateCommand();

            cmd.CommandText = "delete from teachers where teacherid=@id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();
            cmd.ExecuteNonQuery ();
            conn.Close ();  
        }

        /// <summary>
        /// Update a teacher on the sql database
        /// </summary>
        /// <param name="TeacherInfo"></param>
        /// <example>
        /// POST api/teacherData/UpdateTeacher/5
        /// </example>
        [HttpPost]
        [EnableCors(origins: "*", methods: "*", headers: "*")]
        public void UpdateTeacher(int id, [FromBody] Teacher TeacherInfo)
        {
            MySqlConnection conn = school.AccessDatabase();

            conn.Open();

            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "update teachers set teacherfname=@teacherfname,teacherlname=@teacherlname,employeenumber=@employeenumber, salary=@teachersalary where teacherid=@teacherid";
            cmd.Parameters.AddWithValue("@teacherfname", TeacherInfo.TeacherFName);
            cmd.Parameters.AddWithValue("@teacherlname", TeacherInfo.TeacherLName);
            cmd.Parameters.AddWithValue("@employeenumber", TeacherInfo.Teachernumber);
            cmd.Parameters.AddWithValue("@teachersalary", TeacherInfo.Teachersalary);
            cmd.Parameters.AddWithValue("@teacherid", id);
            cmd.Prepare ();

            cmd.ExecuteNonQuery();

        }
    }
}