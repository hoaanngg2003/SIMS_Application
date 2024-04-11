namespace SIMS_Application.Models
{
    public class CourseModel
    {
        public int IdCourse { get; set; }
        public string NameCourse { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string DescriptionCourse { get; set; }
        public byte[] CourseImage { get; set; }
        public CourseModel() 
        { 

        }

    }
}
