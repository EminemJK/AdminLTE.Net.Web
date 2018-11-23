using Microsoft.AspNetCore.Authentication.Cookies;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using AdminLTE.Common;

namespace AdminLTE.Domain.Service
{
    public static class CookieService
    {
        public const string AuthenticationScheme = CookieAuthenticationDefaults.AuthenticationScheme +"_BananeSystem";

        /// <summary>
        /// 加密用户信息
        /// </summary>
        public static string GetDesEncrypt<T>(T info)
        {
            return  DigestHelper.DesEncrypt(JsonConvert.SerializeObject(info));
        }

        /// <summary>
        /// 解密cookie
        /// </summary>
        /// <typeparam name="T">用户信息</typeparam>
        /// <param name="des">加密字符串</param>
        public static T GetDesDecrypt<T>(string des)
        {
            string code = DigestHelper.DesDecrypt(des);
            return JsonConvert.DeserializeObject<T>(code);
        }
    }
}
