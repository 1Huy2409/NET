using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBS.DTOs.Order
{
    public class OrderCreateDTO
    {
        [Required(ErrorMessage = "Vui lòng chọn khách hàng")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập ngày đặt hàng")]
        public DateTime OrderDate { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tổng tiền")]
        [Range(0, double.MaxValue, ErrorMessage = "Tổng tiền không hợp lệ")]
        public decimal TotalAmount { get; set; }
    }
}
