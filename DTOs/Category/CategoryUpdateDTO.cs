using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBS.DTOs.Category
{
    public class CategoryUpdateDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên thể loại")]
        [StringLength(50, ErrorMessage = "Tên thể loại không được quá 50 ký tự")]
        public string Name { get; set; }
    }
}
