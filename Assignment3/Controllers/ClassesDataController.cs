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
    public class ClassesDataController : ApiController
    {
        // The database context class which allows us to access our MySQL Database.
        public SchoolDBContext school = new SchoolDBContext();

        //This Controller Will access the students table of our school database.
        /// <summary>
        /// Returns a list of Class in the system
        /// </summary>
        /// <example>GET api/ClassesData/ListClass</example>
        /// <returns>
        /// A list of Class
        /// </returns>
        [HttpGet]
        public IEnumerable<Classes> ListClass()
        {
            //Create an instance of a connection
            MySqlConnection Conn = school.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select * from classes";

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Create an empty list of student Names
            List<Classes> ClassList = new List<Classes>();

            //Loop Through Each Row the Result Set
            while (ResultSet.Read())
            {
                //Access Column information by the DB column name as an index
                Classes ListedClass = new Classes();
                ListedClass.ClassID = Convert.ToInt32(ResultSet["classid"]);
                ListedClass.ClassCode = ResultSet["classcode"].ToString();
                ListedClass.ClassName = ResultSet["classname"].ToString();
                //Add the student Name to the List
                ClassList.Add(ListedClass);
            }

            //Close the connection between the MySQL Database and the WebServer
            Conn.Close();

            //Return the final list of Class
            return ClassList;
        }

        /// <summary>
        /// Find a classes from the sql database through id
        /// </summary>
        /// <param name="id">the id for the class as primary key</param>
        /// <returns>the detail of class according to id</returns>
        [HttpGet]
        public Classes FindClass(int id)
        {
            Classes newClass = new Classes();
            //Create an instance of a connection
            MySqlConnection Conn = school.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select * from classes left join teachers on teachers.teacherid=classes.teacherid where classes.classid = " + id;

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            while (ResultSet.Read())
            {
                //Access Column information by the DB column name as an index
                int classesID = Convert.ToInt32(ResultSet["classid"]);
                string classCode=ResultSet["classcode"].ToString();
                string className = ResultSet["classname"].ToString();
                string teacherfname = ResultSet["teacherfname"].ToString();
                string teacherlname=ResultSet["teacherlname"].ToString() ;
                DateTime startClass = (DateTime)ResultSet["startdate"];
                DateTime endClass = (DateTime)ResultSet["finishdate"];


                newClass.ClassID = classesID;
                newClass.ClassCode = classCode; 
                newClass.ClassName = className; 
                newClass.TeacherName= teacherfname + " " + teacherlname;
                newClass.StartClass = startClass;   
                newClass.EndClass = endClass;
                newClass.TeacherID= Convert.ToInt32(ResultSet["teacherid"]);
            }

            return newClass;
        }


        [HttpGet]
        [Route("api/ClassesData/ListClass/{TeacherId}")]
        [EnableCors(origins: "*", methods: "*", headers: "*")]
        public IEnumerable<Classes> GetClassesForTeacher(int Teacherid)
        {
            List<Classes> classes = new List<Classes>();

            MySqlConnection Conn = school.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            cmd.CommandText = "Select * from classes left join teachers on teachers.teacherid = classes.teacherid where classes.teacherid=@teacherid";
            cmd.Parameters.AddWithValue("@teacherid", Teacherid);
            cmd.Prepare();

            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Loop Through Each Row the Result Set               
            while (ResultSet.Read())
            {
                int ClassId = Convert.ToInt32(ResultSet["classid"]);
                string classcode=ResultSet["classcode"].ToString();
                string classname=ResultSet["classname"].ToString();

                Classes NewClass= new Classes(); 
                NewClass.ClassID = ClassId;
                NewClass.ClassCode = classcode;
                NewClass.ClassName = classname; 

                classes.Add(NewClass);
            }

            return classes;

        }
    }
}
