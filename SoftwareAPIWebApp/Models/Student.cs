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

        [Required(ErrorMessage = "Ім’я обов’язкове")]
        [MinLength(2, ErrorMessage = "Ім’я має містити щонайменше 2 символи")]
        [RegularExpression(@"^[A-Za-zА-Яа-яІіЇїЄє\s\-]+$", ErrorMessage = "Ім’я може містити лише літери, пробіли та дефіси")]
        [Display(Name = "Ім'я студента")]
        public string Name { get; set; }

        [EmailAddress(ErrorMessage = "Некоректна електронна адреса. Приклад: student@example.com")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Факультет обов'язковий")]
        [Range(1, int.MaxValue, ErrorMessage = "FacultyId має бути додатним числом")]
        public int FacultyId { get; set; }
        public virtual Faculty? Faculty { get; set; }

        public virtual ICollection<Installation> Installations { get; set; }
    }
}
