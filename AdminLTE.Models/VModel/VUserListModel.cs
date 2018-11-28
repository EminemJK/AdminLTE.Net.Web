using AdminLTE.Models.Enum;
using Banana.Utility.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdminLTE.Models.VModel
{
    public class VUserListModel : VUserModel
    {
        public string Name { get; set; }

        public string Phone { get; set; }  

        public DateTime CreateTime { get; set; }

        public EUserSex Sex { get; set; }

        public EUserState Enable { get; set; }

        public string SexString
        {
            get
            {
                return EnumDescription.GetText(Sex);
            }
        }

        public string EnableString
        {
            get
            {
                return EnumDescription.GetText(Enable);
            }
        }
    }
}
