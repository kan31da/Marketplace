using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Marketplace.Infrastructure.Data
{
    public class Cart
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

        //[ForeignKey("Id")]
        //public virtual ApplicationUser User { get; set; }
    }
}
