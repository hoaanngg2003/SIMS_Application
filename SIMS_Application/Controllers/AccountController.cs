using Microsoft.AspNetCore.Mvc;
using SIMS_Application.Models;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Http;

namespace SIMS_Application.Controllers
{
    public class AccountController : Controller
    {
        private readonly IConfiguration _configuration;

        public AccountController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(AccountModel account)
        {
            string connectionString = _configuration.GetConnectionString("MyConnectionString");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM UserAccount WHERE Email = @Email AND Password = @Password";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", account.Email);
                    command.Parameters.AddWithValue("@Password", account.Password);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Lấy giá trị của trường Role từ dữ liệu đọc được
                            string role = reader["Role"].ToString();
                            string name = reader["Name"].ToString();

                            // Đăng nhập thành công, chuyển hướng dựa vào vai trò
                            if (role == "Student")
                            {
                                HttpContext.Session.SetString("Name", name);
                                return RedirectToAction("Index", "Home");
                            }
                            else if (role == "Admin")
                            {
                                HttpContext.Session.SetString("Name", name);
                                return RedirectToAction("CrudAdmin", "Admin");
                            }
                            else if (role == "Teacher")
                            {
                                HttpContext.Session.SetString("Name", name);
                                return RedirectToAction("CrudTeacher", "Teacher");
                            }
                        }
                    }
                }
            }

            // Đăng nhập không thành công, hiển thị lại view đăng nhập
            return View();
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(AccountModel account)
        {
            // Kiểm tra xem dữ liệu được gửi từ form có hợp lệ không
            if (ModelState.IsValid)
            {
                // Lấy chuỗi kết nối từ cấu hình
                string connectionString = _configuration.GetConnectionString("MyConnectionString");

                // Tạo câu lệnh SQL để chèn dữ liệu vào cơ sở dữ liệu
                string query = "INSERT INTO UserAccount (Name, Email, Password) VALUES (@Name, @Email, @Password)";

                // Thực hiện kết nối và thực thi câu lệnh SQL

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Thêm các tham số vào câu lệnh SQL
                        command.Parameters.AddWithValue("@Name", account.Name);
                        command.Parameters.AddWithValue("@Email", account.Email);
                        command.Parameters.AddWithValue("@Password", account.Password);

                        // Thực thi câu lệnh SQL
                        command.ExecuteNonQuery();
                    }
                    connection.Close();
                }

                // Chuyển hướng người dùng đến trang đăng nhập sau khi đăng ký thành công
                return RedirectToAction("Login");
            }

            // Trả về view đăng ký với model hiện tại nếu dữ liệu không hợp lệ
            return View(account);
        }

    }
}
