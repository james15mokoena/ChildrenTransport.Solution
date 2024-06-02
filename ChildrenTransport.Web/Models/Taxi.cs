using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChildrenTransport.Web.Models
{
    [Table("Taxi")]
    public class Taxi
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public required string Name { get; set; }

        [Required]
        public int NumSeats { get; set; }

        [Required]
        public required string Image { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}
