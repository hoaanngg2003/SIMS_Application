namespace SIMS_Application.Models
{
    public class AccountModel
    {
        public int UserID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; } = "Student";
        public AccountModel()
        {

        }
        public AccountModel(string Name, string Email, string Password)
        {
            this.Name = Name;
            this.Email = Email;
            this.Password = Password;
        }

    }
}
