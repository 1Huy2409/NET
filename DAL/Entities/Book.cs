using QLBS.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBS.DAL
{
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required(ErrorMessage = "Tên sách không được để trống")]
        [StringLength(200, MinimumLength = 5, ErrorMessage = "Tên sách ít nhất 5 ký tự")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Tên tác giả không được để trống")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Tên tác giả ít nhất 3 ký tự")]
        public string Author { get; set; }

        // Khóa ngoại đến Category
        [Required]
        public int CategoryId { get; set; }

        public Category Category { get; set; }

        [Required(ErrorMessage = "Giá không được để trống")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Giá sách phải lớn hơn 0")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Tồn kho không được để trống")]
        [Range(0, int.MaxValue, ErrorMessage = "Số lượng không được âm")]
        public int Stock { get; set; }

        [Required(ErrorMessage = "Link ảnh không được để trống")]
        public string ImageUrl { get; set; }
    }

}