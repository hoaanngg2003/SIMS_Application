using Microsoft.AspNetCore.Mvc;
using SIMS_Application.Models;
using System.Data.SqlClient;
using System.Security.Principal;

namespace SIMS_Application.Controllers
{
    public class AdminController : Controller
    {
        private readonly IConfiguration _configuration;

        public AdminController (IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IActionResult CrudAdmin()
        {
            int totalCourses;
            int totalStudents;
            int totalTeachers;

            // Get the connection string from IConfiguration
            string connectionString = _configuration.GetConnectionString("MyConnectionString");

            // Connect and query the database
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Execute the query to calculate the total number of courses
                string query = "SELECT COUNT(*) FROM Course";
                SqlCommand command = new SqlCommand(query, connection);
                totalCourses = (int)command.ExecuteScalar();
            }
            // Connect and query the database for total students
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Execute the query to calculate the total number of students with Role = 'Student'
                string studentQuery = "SELECT COUNT(*) FROM UserAccount WHERE Role = 'Student'";
                SqlCommand studentCommand = new SqlCommand(studentQuery, connection);
                totalStudents = (int)studentCommand.ExecuteScalar();
            }
            // Connect and query the database for total students
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Execute the query to calculate the total number of teachers with Role = 'Teacher'
                string teacherQuery = "SELECT COUNT(*) FROM UserAccount WHERE Role = 'Teacher'";
                SqlCommand teacherCommand = new SqlCommand(teacherQuery, connection);
                totalTeachers = (int)teacherCommand.ExecuteScalar();
            }

            // Pass the total number of courses to the View
            ViewData["TotalCourses"] = totalCourses;
            ViewData["TotalStudents"] = totalStudents;
            ViewData["TotalTeachers"] = totalTeachers;

            return View();
        }
        public IActionResult ListCourse()
        {
            return View();
        }
        
    }
}
