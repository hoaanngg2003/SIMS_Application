namespace SIMS_Application.Models
{
    public class StudentModel
    {
        public int StudentID { get; set; }
        public string StudentName { get; set; }
        public int StudentAge { get; set; }
        public DateTime DoB {  get; set; }
        public string StudentEmail { get; set;}
        public string StudentPhone { get; set;}
        public string StudentAddress{ get; set;}
        public StudentModel()
        {

        }

    }
}
