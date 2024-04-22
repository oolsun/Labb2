using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Labb2.Models
{
    public class Subject
    {
        [Key]
        public int SubjectId { get; set; }
        [DisplayName("Ämne")]
        public string SubjectName { get; set; }
        [ForeignKey("Teacher")]
        public int FK_TeacherId { get; set; }
        public Teacher? Teacher { get; set; }
        public virtual ICollection<Enrollment>? Enrollments { get; set; }


    }
}
