using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dblayer.Models
{
    [Table("CartTable")]
    public class CartTable
    {
        [Key]
        public int CartId { get; set; }

        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual UserTable User { get; set; }

        // This allows us to see the list of items in this cart
        public virtual ICollection<CartDetailTable> CartDetails { get; set; }
    }
}