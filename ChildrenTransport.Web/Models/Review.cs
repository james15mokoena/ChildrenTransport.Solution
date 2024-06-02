using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChildrenTransport.Web.Models
{
    [Table("Review")]
    public class Review
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public required string ParentId { get; set; }

        [Required]
        public required string Comment { get; set; }

        [Required]
        public int Rating { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DateReviewed { get; set; }

        [Required]
        [ForeignKey("ParentId")]
        public required Parent Parent { get; set; }
    }
}
