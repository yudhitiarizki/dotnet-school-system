namespace SchoolSystem.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Enrollment
    {
        public int Id { get; set; }

        [Required]
        public required int StudentId { get; set; }
        public Student? Student { get; set; }

        [Required]
        public required int SubjectId { get; set; }
        public Subject? Subject { get; set; }

        public DateTime EnrollmentDate { get; set; }
    }
}
