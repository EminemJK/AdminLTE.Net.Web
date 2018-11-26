using System;
using System.Collections.Generic;
using System.Text;
using AdminLTE.Models.Enum;
using Banana.Uow.Models;
using Dapper.Contrib.Extensions;

namespace AdminLTE.Models
{
    [Table("T_User")]
    public class UserInfo : BaseModel
    {
        [Key]
        public int Id { get; set; }

        public string UserName { get; set; }
        public string Password { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public EUserSex Sex { get; set; }

        public EUserState Enable { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
