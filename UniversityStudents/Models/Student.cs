namespace UniversityStudents.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public bool IsMarried { get; set; }
        public string Major { get; set; }
        public string Address { get; set; }
        public double GPA { get; set; }
        public EducationType EducationType { get; set; }

        public int UniversityId { get; set; }
        public University University { get; set; }
    }
}
