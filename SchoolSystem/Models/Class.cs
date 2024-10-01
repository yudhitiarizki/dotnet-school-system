namespace SchoolSystem.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Class
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public required string ClassName { get; set; }

        [Required]
        public required int AcademicYear { get; set; }

        public ICollection<Subject>? Subjects { get; set; }
    }
}
