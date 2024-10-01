namespace SchoolSystem.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Subject
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public required string SubjectName { get; set; }

        public int TeacherId { get; set; }
        public Teacher? Teacher { get; set; }

        public int ClassId { get; set; }
        public Class? Class { get; set; }

        public ICollection<Enrollment>? Enrollments { get; set; }
    }
}
