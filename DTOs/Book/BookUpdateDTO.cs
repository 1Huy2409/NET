using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBS.DTOs.Book
{
    public class BookUpdateDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên sách")]
        [StringLength(100, ErrorMessage = "Tên sách không được quá 100 ký tự")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tác giả")]
        [StringLength(100, ErrorMessage = "Tên tác giả không được quá 100 ký tự")]
        public string Author { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập giá")]
        [Range(0, double.MaxValue, ErrorMessage = "Giá không hợp lệ")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập số lượng")]
        [Range(0, int.MaxValue, ErrorMessage = "Số lượng không hợp lệ")]
        public int Stock { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn thể loại")]
        public int CategoryId { get; set; }

        public string ImageUrl { get; set; }
    }
}
