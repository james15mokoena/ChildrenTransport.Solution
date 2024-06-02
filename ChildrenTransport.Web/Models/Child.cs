using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChildrenTransport.Web.Models
{
    [Table("Child")]
    public class Child
    {
        [Key]
        [Required]
        public required string ChildId { get; set; }

        [Required]
        public required string FirstName { get; set; }

        [Required]
        public required string LastName { get; set; }

        [Required]
        [Column(TypeName = "char(1)")]
        public required char Gender { get; set; }

        [Required]
        public required string ParentId { get; set; }

        // Navigation property

        [ForeignKey("ParentId")]
        public required Parent Parent { get; set; }
    }
}
