using System;
using System.Collections.Generic;
using System.Text;
using Banana.Utility.Common;

namespace AdminLTE.Models.Enum
{
    /// <summary>
    /// 用户启用禁用
    /// </summary>
    public enum EUserState
    {
        [EnumDescription("禁用")]
        Disabled = 0,

        [EnumDescription("启用")]
        Enabled = 1
    }
}
