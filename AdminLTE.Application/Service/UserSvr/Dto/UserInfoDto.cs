using AdminLTE.Models.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdminLTE.Application.Service.UserSvr.Dto
{
    public class UserInfoDto
    {
        public int Id { get; set; }

        public string UserName { get; set; }
        public string Password { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public EUserSex Sex { get; set; }

        public string sexString
        {
            get
            {
                return Banana.Utility.Common.EnumDescription.GetText(Sex);
            }
        }

        public EUserState Enable { get; set; }

        public string enableString
        {
            get
            {
                return Banana.Utility.Common.EnumDescription.GetText(Enable);
            }
        }

        public DateTime CreateTime { get; set; }

        public string HeaderImg { get; set; }
    }
}
