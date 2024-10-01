namespace SchoolSystem.Models
{
    public class Student
    {
        public int Id { get; set; }

        public required string FirstName { get; set; }

        public string? LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        // Inisialisasi dengan koleksi kosong untuk menghindari null
        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    }
}
