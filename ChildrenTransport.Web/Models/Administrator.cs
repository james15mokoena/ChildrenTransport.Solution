using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChildrenTransport.Web.Models
{
    [Table("Administrator")]
    public class Administrator
    {
        [Key]
        [Required]
        public required string Email { get; set; }

        [Required]
        public required string FirstName { get; set; }

        [Required]
        public required string LastName { get; set; }

        [Required]        
        public required string Phone { get; set; }

        [Required]        
        public required string Password { get; set; }
    }
}
