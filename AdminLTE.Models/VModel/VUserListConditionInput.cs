using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminLTE.Models.VModel
{
    public class VUserListConditionInput
    {
        public int pageNum { get; set; } = 1;
        public int pageSize { get; set; } = 8;
        public int draw { get; set; } = 1;

        public string phone { get; set; }

        public string name { get; set; }

        public int sex { get; set; } = -1;
    }
}
