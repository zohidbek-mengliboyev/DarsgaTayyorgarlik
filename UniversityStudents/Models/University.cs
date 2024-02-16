namespace UniversityStudents.Models
{
    public class University
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
        public List<string> Faculties { get; set; }
        public List<string> EducationalStages { get; set; }
        public int Branches { get; set; }
        public decimal Budget { get; set; }

        public List<Student> Students { get; set; }
    }
}
