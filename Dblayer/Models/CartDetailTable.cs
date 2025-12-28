using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dblayer.Models
{
    [Table("CartDetailTable")]
    public class CartDetailTable
    {
        [Key]
        public int CartDetailId { get; set; }

        public int CartId { get; set; }

        public int StockMenuItemId { get; set; }

        public int Quantity { get; set; }

        [ForeignKey("CartId")]
        public virtual CartTable Cart { get; set; }

        [ForeignKey("StockMenuItemId")]
        public virtual StockMenuItemTable StockMenuItem { get; set; }
    }
}