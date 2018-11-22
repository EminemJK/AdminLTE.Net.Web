
using System;
using System.Collections.Generic;
using System.Text;
using AdminLTE.Models.AppSetting;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Banana.Uow;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;

namespace AdminLTE.Domain
{
    public  static class StaticConfigRegist
    {
        /// <summary>
        /// 静态配置
        /// </summary>
        public static IApplicationBuilder UseConfigRegist(this IApplicationBuilder app, IConfiguration Configuration)
        {
            var reg = Configuration.Get<AppConfig>(); 
            //注册数据库
            ConnectionBuilder.ConfigRegist(reg.dBSetting.ConnectionString, reg.dBSetting.DBType);

            return app;
        }
    }
}
