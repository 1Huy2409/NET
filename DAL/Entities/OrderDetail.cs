using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBS.DAL
{
    public class OrderDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required]
        public int quantity { get; set; }
        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal price { get; set; }
        // quan he 1-1 voi book
        [Required]
        public int BookId { get; set; }
        public Book Book { get; set; }
        // quan he 1-1 voi order
        [Required]
        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}
