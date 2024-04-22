using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Labb2.Models
{
    public class Class
    {
        [Key]
        public int ClassId { get; set; }
        [DisplayName("Klass")]
        public string ClassName { get; set; }
        [ForeignKey("Teacher")]
        public int FK_TeacherId { get; set; }
        public virtual Teacher? Teachers { get; set; }        
    }
}
