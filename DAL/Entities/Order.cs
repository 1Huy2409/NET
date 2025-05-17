using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBS.DAL
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)] //giai thich 
        public DateTime OrderDate { get; set; }
        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal TotalPrice { get; set; }
        // quan hệ 1-1 với user
        public int UserId { get; set; }
        public User User { get; set; }
        // quan hệ 1-n với OrderDetail
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
