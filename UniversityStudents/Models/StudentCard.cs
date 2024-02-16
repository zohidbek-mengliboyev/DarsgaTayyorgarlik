namespace UniversityStudents.Models
{
    public class StudentCard
    {
        public int Id { get; set; }
        public string CardNumber { get; set; }
        public string BankName { get; set; }
        public string ContactNumber { get; set; }
        public decimal Stipend { get; set; }
        public int AdmissionYear { get; set; }

        public int StudentId { get; set; }
        public Student Student { get; set; }
    }
}
