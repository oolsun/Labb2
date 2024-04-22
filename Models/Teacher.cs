using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Labb2.Models
{
    public class Teacher
    {
        [Key]
        public int TeacherId { get; set; }
        [DisplayName("Lärarens namn")]
        public string TeacherName { get; set; }
        public virtual ICollection<Subject> Subjects { get; set; }
    }
}
