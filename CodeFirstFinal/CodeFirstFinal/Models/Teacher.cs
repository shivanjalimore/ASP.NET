using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeFirstFinal.Models
{
    public class Teacher
    {
        [Key]     //primary key
        public int Id { get; set; }

        [Column("TeacherName",TypeName ="varchar(100)")]
        [Required]
        public string Name { get; set; }

        [Column("Gender",TypeName ="varchar(50)")]
        [Required]
        public string Gender { get; set; }

        [Required]
        public int? Age { get; set; }

        [Required]
        public string Subject { get; set; }
    }
}
