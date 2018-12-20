using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminLTE.Net.Web.AuthenticationAttr
{
    /// <summary>
    /// 跳过权限验证 属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class SkipUserAuthorizeAttribute : Attribute, IFilterMetadata
    {

    }
}
