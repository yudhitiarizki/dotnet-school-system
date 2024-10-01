namespace SchoolSystem.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Teacher
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public required string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public required string LastName { get; set; }

        [Required]
        [MaxLength(100)]
        public required string Specialization { get; set; }

        public ICollection<Subject>? Subjects { get; set; }
    }
}
