using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChildrenTransport.Web.Models
{
    [Table("Address")]
    public class Address
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public required string HouseNo { get; set; }

        [Required]
        public required string Extension { get; set; }

        [Required]
        public required string Section { get; set; }

        [Required]
        public required string Location { get; set; }

        [Required]
        public required string Town { get; set; }

        [Required]
        public required string Code { get; set; }

        [Required]
        public required string ParentId { get; set; }

        [Required]
        public required string ChildId { get; set; }

        // Navigation properties

        [ForeignKey("ParentId")]
        public required Parent Parent { get; set; }

        [ForeignKey("ChildId")]
        public required Child Child { get; set; }
    }
}
