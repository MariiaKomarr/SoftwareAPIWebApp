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

        [Required]
        [Display(Name = "Тип")]
        public string Name { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Software> Softwares { get; set; }
    }
}
