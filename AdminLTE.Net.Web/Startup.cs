using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using log4net.Repository;
using log4net;
using log4net.Config;
using System.IO;
using AdminLTE.Common.LogHelper;
using AdminLTE.Application.Service.UserSvr;
using AdminLTE.Application.Service;
using AdminLTE.Application;
using AdminLTE.Domain.Repository.Interface;
using AdminLTE.Domain.Repository;

namespace AdminLTE.Net.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration; 

            //log4net
            repository = LogManager.CreateRepository(LogHelper.LogRepo);
            //指定配置文件
            XmlConfigurator.Configure(repository, new FileInfo("log4net.config"));
        }

        /// <summary>
        /// log4net 仓储库
        /// </summary>
        public static ILoggerRepository repository { get; set; }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            //所有时间输出统一序列化格式
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1).AddJsonOptions(option=>
            {
                option.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            });

            //增加了"XSRF-TOKEN"标识,值为表单自动生成的防伪标记
            services.AddAntiforgery(o => o.HeaderName = "XSRF-TOKEN"); 
            services.AddAuthentication(CookieService.AuthenticationScheme)
                    .AddCookie(CookieService.AuthenticationScheme, o =>
            {
                o.LoginPath = new PathString("/Login");
            });

            //log日志注入
            services.AddSingleton<ILoggerHelper, LogHelper>();

            //配置
            services.Configure<AppConfig>(Configuration); 

            //注入业务
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc();

            app.UseConfigRegist(Configuration);

            app.UseAuthentication();
        }
    }
}
