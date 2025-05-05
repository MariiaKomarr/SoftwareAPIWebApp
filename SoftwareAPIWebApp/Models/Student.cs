using System.ComponentModel.DataAnnotations;

namespace SoftwareAPIWebApp.Models
{
    public class Student
    {
        public Student()
        {
            Installations = new List<Installation>();
        }

        [Key]
        public int StudentId { get; set; }

        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [Display(Name = "Ім'я студента")]
        public string Name { get; set; }

        public string Email { get; set; }

        public int FacultyId { get; set; }
        public virtual Faculty? Faculty { get; set; }

        public virtual ICollection<Installation> Installations { get; set; }
    }
}
