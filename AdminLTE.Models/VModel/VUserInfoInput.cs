using AdminLTE.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AdminLTE.Models.VModel
{
    public class VUserInfoInput
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "用户名未填写")]
        [Display(Name = "用户名")]
        [StringLength(10, ErrorMessage = "用户名过长")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "头像未上传")]
        [Display(Name = "头像")]
        public string HeaderImg { get; set; }

        [Required(ErrorMessage = "姓名未填写")]
        [Display(Name = "姓名")]
        [StringLength(20, ErrorMessage = "姓名过长")]
        public string Name { get; set; }

        [Required(ErrorMessage = "联系方式未填写")]
        [Display(Name = "联系方式")]
        [StringLength(11, ErrorMessage = "联系方式过长")]
        [RegularExpression(@"^1\d{10}$", ErrorMessage = "手机号码格式不正确")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "密码未填写")]
        [Display(Name = "密码")]
        [StringLength(maximumLength: 20, MinimumLength = 6, ErrorMessage = "密码长度限制为6~20位")]
        public string Password { get; set; }

        public EUserSex Sex { get; set; } = EUserSex.Man;

        public EUserState Enable { get; set; } = EUserState.Enabled;
    }
}
