using System;
using System.Collections.Generic;
using System.Text;
using AdminLTE.Models.Enum;
using Banana.Uow.Models;

namespace AdminLTE.Models.Entity
{
    [Table("T_User")]
    public class UserInfo : IEntity
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

        public string HeaderImg { get; set; }
    }
}
