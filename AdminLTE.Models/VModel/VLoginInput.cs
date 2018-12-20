using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AdminLTE.Models.VModel
{
    public class VLoginInput
    {
        [Required]
        [Display(Name = "用户名")]
        [StringLength(10, ErrorMessage = "用户名过长")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "密码")]
        [StringLength(20, ErrorMessage = "密码过长")]
        public string Password { get; set; }
    }
}
