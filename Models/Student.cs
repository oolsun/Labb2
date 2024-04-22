using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Labb2.Models
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }
        [DisplayName("Elevens namn")]
        public string StudentName { get; set; }
        public virtual ICollection<Enrollment>? Enrollments { get; set; }

    }
}
