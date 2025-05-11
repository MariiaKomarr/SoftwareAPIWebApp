using System.ComponentModel.DataAnnotations;

namespace SoftwareAPIWebApp.Models
{
    public class SoftwareType
    {
        public SoftwareType()
        {
            Softwares = new List<Software>();
        }

        [Key]
        public int TypeId { get; set; }

        [Required(ErrorMessage = "Назва типу обов’язкова")]
        [MinLength(3, ErrorMessage = "Назва типу має містити щонайменше 3 символи")]
        [RegularExpression(@"^[A-Za-zА-Яа-яІіЇїЄє0-9\s\-]+$", ErrorMessage = "Назва типу може містити літери, цифри, пробіли та дефіси")]
        [Display(Name = "Тип")]
        public string Name { get; set; }

        [MinLength(10, ErrorMessage = "Опис має містити щонайменше 10 символів")]
        public string Description { get; set; }

        public virtual ICollection<Software> Softwares { get; set; }
    }
}
