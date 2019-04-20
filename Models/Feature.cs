using System.ComponentModel.DataAnnotations;

namespace angular_netcore.Models
{
    public class Feature
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
    }
}