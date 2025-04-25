using System.ComponentModel.DataAnnotations;

namespace SoftwareAPIWebApp.Models
{
    public class Installation
    {
        [Key]
        public int InstallId { get; set; }

        public DateTime InstallDate { get; set; }

        public int StudentId { get; set; }
        public virtual Student Student { get; set; }

        public int SoftwareId { get; set; }
        public virtual Software Software { get; set; }
    }
}
