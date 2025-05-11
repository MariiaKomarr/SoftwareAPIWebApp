using System.ComponentModel.DataAnnotations;

namespace SoftwareAPIWebApp.Models
{
    public class Installation
    {
        [Key]
        public int InstallId { get; set; }

        [Required(ErrorMessage = "Дата встановлення обов’язкова")]
        [DataType(DataType.DateTime)]
        public DateTime InstallDate { get; set; }

        [Required(ErrorMessage = "Потрібно вказати студента")]
        [Range(1, int.MaxValue, ErrorMessage = "ID студента має бути додатним числом")]
        public int StudentId { get; set; }
        public virtual Student? Student { get; set; }

        [Required(ErrorMessage = "Потрібно вказати програмне забезпечення")]
        [Range(1, int.MaxValue, ErrorMessage = "ID програмного забезпечення має бути додатним числом")]
        public int SoftwareId { get; set; }
        public virtual Software? Software { get; set; }
    }
}
