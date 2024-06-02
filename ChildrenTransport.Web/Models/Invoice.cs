using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChildrenTransport.Web.Models
{
    [Table("Invoice")]
    public class Invoice
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public required string ParentId { get; set; }

        [Required]
        public required string ChildId { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public required DateTime ForMonth { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public required DateTime DatePaid { get; set; }
      
        // Navigation property

        [ForeignKey("ParentId")]
        public required Parent Parent { get; set; }

        [ForeignKey("ChildId")]
        public required Child Child { get; set; }
    }
}
