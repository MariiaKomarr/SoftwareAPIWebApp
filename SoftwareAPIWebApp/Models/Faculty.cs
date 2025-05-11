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

        [Required(ErrorMessage = "Назва факультету обов’язкова")]
        [MinLength(3, ErrorMessage = "Назва має містити щонайменше 3 символи")]
        [RegularExpression(@"^[A-Za-zА-Яа-яІіЇїЄє\s\-]+$", ErrorMessage = "Назва має містити лише літери, пробіли або дефіси")]
        [Display(Name = "Факультет")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Ім'я декана обов’язкове")]
        [RegularExpression(@"^[A-Za-zА-Яа-яІіЇїЄє\s\-]+$", ErrorMessage = "Ім’я декана має містити лише літери, пробіли або дефіси")]
        public string Dean { get; set; }

        public virtual ICollection<Student> Students { get; set; }
    }
}
