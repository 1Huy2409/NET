using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBS.DAL
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required(ErrorMessage ="Họ tên là bắt buộc")]
        [StringLength(100, ErrorMessage ="Họ tên không quá 100 ký tự")]
        public string Name { get; set; }
        [Required(ErrorMessage ="Email là bắt buộc")]
        [EmailAddress(ErrorMessage ="Email không hợp lệ")]
        public string Email { get; set; }
        [Required(ErrorMessage ="Tên đăng nhập là bắt buộc")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Tên đăng nhập từ 3-50 ký tự")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Mật khẩu là bắt buộc")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Mật khẩu từ 6-100 ký tự")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage ="Địa chỉ là bắt buộc")]
        public string Address { get; set; }
        [Required(ErrorMessage ="Vai trò là bắt buộc")]
        [StringLength(20, ErrorMessage ="Vai trò không dài quá 20 ký tự")]
        public string Role { get; set; } // Admin hoặc Customer
        [Required(ErrorMessage ="Số điện thoại là bắt buộc")]
        [Phone(ErrorMessage = "Số điện thoại không hợp lệ")]
        public string Phone { get; set; }
    }
}
