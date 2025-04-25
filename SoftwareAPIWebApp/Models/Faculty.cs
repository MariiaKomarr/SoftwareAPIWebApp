using System.ComponentModel.DataAnnotations;

namespace SoftwareAPIWebApp.Models
{
    public class Faculty
    {
        public Faculty()
        {
            Students = new List<Student>();
        }

        [Key]
        public int FacultyId { get; set; }

        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [Display(Name = "Факультет")]
        public string Name { get; set; }

        public string Dean { get; set; }

        public virtual ICollection<Student> Students { get; set; }
    }
}
