using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChildrenTransport.Web.Models
{
    [Table("BankingDetails")]
    public class BankingDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
     
        [Required]
        public required int CardNo { get; set; }

        [Required]
        public required int CVV { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public required DateTime ExpireDate { get; set; }        

        [Required]
        public required string ParentId { get; set; }

        // Navigation property
        [ForeignKey("ParentId")]
        public required Parent Parent { get; set; }
    }
}
