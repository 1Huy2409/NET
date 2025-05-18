using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLBS.Utils
{
    public class ValidationHelper
    {
        public static bool Validate(object model, Control container, ErrorProvider errorProvider)
        {
            // xóa lỗi cũ
            foreach(Control control in container.Controls)
            {
                errorProvider.SetError(control, "");
            }
            var validationContext = new ValidationContext(model);
            var validationResults = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(model, validationContext, validationResults, true);
            if (!isValid)
            {
                // Hiển thị tất cả lỗi trong MessageBox
                string errorMessages = string.Join("\n", validationResults.Select(r => r.ErrorMessage)); 
                MessageBox.Show(errorMessages, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return isValid;
        }
        public static bool ValidateLogin(string username, string password)
        {
            if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show("Vui lòng nhập UserName!");
                return false;
            }
            if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng nhập Password!");
                return false;
            }
            return true;
        }
    }
}
