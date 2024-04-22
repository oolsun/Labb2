using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.ComponentModel;

namespace Labb2.Models
{
    public enum Grade
    {
        A, B, C, D, E, F
    }
    public class Enrollment
    {
        [Key]
        public int EnrollmentId { get; set; }
        [DisplayName("Betyg")]
        public Grade? Grade { get; set; }
        [ForeignKey("Subject")]
        public int FK_SubjectId { get; set; }
        [ForeignKey("Student")]
        public int FK_StudentId { get; set; }
        public virtual Subject? Subject { get; set; }
        public virtual Student? Student { get; set; }
    }
}
