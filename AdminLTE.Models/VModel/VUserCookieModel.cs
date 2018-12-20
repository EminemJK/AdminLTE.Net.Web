using System;
using System.Collections.Generic;
using System.Text;

namespace AdminLTE.Models.VModel
{
    public class VUserCookieModel
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public DateTime LoginTime { get; set; }

        public string HeaderImg { get; set; }
    }
}
