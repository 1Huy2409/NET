using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBS.DAL.Entities
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // tự sinh id tăng dần
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Tên danh mục không được để trống")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Tên danh mục từ 3 đến 50 ký tự")]
        public string Name { get; set; }

        public ICollection<Book> Books { get; set; }
    }

}
