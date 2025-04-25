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

        [Required]
        public string Name { get; set; }

        public string Version { get; set; }

        public int TypeId { get; set; }
        public virtual SoftwareType Type { get; set; }

        public string Author { get; set; }

        public string UsageTerms { get; set; }

        public DateTime DateAdded { get; set; }

        public string Annotation { get; set; }

        public virtual ICollection<Installation> Installations { get; set; }
    }
}
