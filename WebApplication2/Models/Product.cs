using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication2.Models
{
    public class Product
    {
        [Required(ErrorMessage = "Không được bỏ trống")]
        [RegularExpression("\\d+", ErrorMessage = "Phải là số")]
        [Range(1000, 2000, ErrorMessage = "ID phải từ 1000-2000")]
        public int item_number { get; set; }

        [Required(ErrorMessage = "Không được bỏ trống")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Tên sản phẩm từ 5 đến 100 ký tụ")]
        public string item_name { get; set; }

        [Required(ErrorMessage = "Không được bỏ trống")]
        [Range(1.0, 100.0, ErrorMessage = "Giá từ 1-100 USD")]
        public double amount { get; set; }

        [Required(ErrorMessage = "Không được bỏ trống")]
        [RegularExpression("\\d+", ErrorMessage = "Phải là số")]
        [Range(1, 100, ErrorMessage = "Số lượng phải từ 1-100")]
        public int quantity { get; set; }
    }
}
