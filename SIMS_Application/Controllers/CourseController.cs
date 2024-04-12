using Microsoft.AspNetCore.Mvc;
using SIMS_Application.Models;
using System.Data.SqlClient;

namespace SIMS_Application.Controllers
{
    public class CourseController : Controller
    {
        private readonly IConfiguration _configuration;

        public CourseController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IActionResult CourseIndex()
        {
            List<CourseModel> courses = GetCoursesFromDatabase();
            return View(courses);
        }
        public IActionResult CourseDetail()
        {
            return View();
        }
        [HttpGet]
        public IActionResult CourseManagement()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CourseManagement(CourseModel course)
        {
            if (ModelState.IsValid)
            {
                // Get the connection string from IConfiguration
                string connectionString = _configuration.GetConnectionString("MyConnectionString");

                string query = "INSERT INTO Course (NameCourse, StartTime, EndTime, DescriptionCourse, Schedule) VALUES (@NameCourse, @StartTime, @EndTime, @DescriptionCourse, @Schedule)";

                // Connect and query the database
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Thêm các tham số vào câu lệnh SQL
                        command.Parameters.AddWithValue("@NameCourse", course.NameCourse);
                        command.Parameters.AddWithValue("@StartTime", course.StartTime);
                        command.Parameters.AddWithValue("@EndTime", course.EndTime);
                        command.Parameters.AddWithValue("@DescriptionCourse", course.DescriptionCourse);
                        command.Parameters.AddWithValue("@Schedule", course.Schedule);

                        // Thực thi câu lệnh SQL
                        command.ExecuteNonQuery();
                    }
                    connection.Close();
                }
                return RedirectToAction("ListCourse","Admin");
            }
            return View(course);
        }
        private List<CourseModel> GetCoursesFromDatabase()
        {
            List<CourseModel> courses = new List<CourseModel>();

            string connectionString = _configuration.GetConnectionString("MyConnectionString");

            string query = "SELECT * FROM Course";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            CourseModel course = new CourseModel();
                            course.IdCourse = reader.GetInt32(0); // Giả sử Id là cột đầu tiên trong kết quả truy vấn
                            course.NameCourse = reader.GetString(1); // Giả sử Name là cột thứ hai trong kết quả truy vấn
                            // Tiếp tục lấy giá trị của các thuộc tính khác của Course từ kết quả truy vấn

                            courses.Add(course);
                        }
                    }
                }
                connection.Close();
            }

            return courses;
        }
    }
}
