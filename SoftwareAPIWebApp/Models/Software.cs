using System.ComponentModel.DataAnnotations;

namespace SoftwareAPIWebApp.Models
{
    public class Software
    {
        public Software()
        {
            Installations = new List<Installation>();
        }

        [Key]
        public int SoftwareId { get; set; }

        [Required(ErrorMessage = "Назва обов’язкова")]
        [MinLength(3, ErrorMessage = "Назва має містити щонайменше 3 символи")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Вкажіть версію")]
        [RegularExpression(@"^\d+(\.\d+){1,2}$", ErrorMessage = "Версія має бути у форматі 1.0 або 1.0.0")]
        public string Version { get; set; }

        [Required(ErrorMessage = "Тип обов'язковий")]
        [Range(1, int.MaxValue, ErrorMessage = "TypeId має бути додатним числом")]
        public int TypeId { get; set; }
        public virtual SoftwareType? Type { get; set; }

        [Required(ErrorMessage = "Автор обов'язковий")]
        [RegularExpression(@"^[A-Za-zА-Яа-яІіЇїЄє\s\-]+$", ErrorMessage = "Ім’я автора має містити лише літери, пробіли або дефіси")]
        public string Author { get; set; }

        public string UsageTerms { get; set; }

        [Required(ErrorMessage = "Дата додавання обов’язкова")]
        public DateTime DateAdded { get; set; }

        [MinLength(10, ErrorMessage = "Анотація має містити щонайменше 10 символів")]
        public string Annotation { get; set; }

        public virtual ICollection<Installation> Installations { get; set; }
    }
}
