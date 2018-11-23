using System;
using System.Collections.Generic;
using System.Text;
using Banana.Uow;
using AdminLTE.Common;
using AdminLTE.Models;
using System.Collections;
using System.Linq;

namespace AdminLTE.Domain.Service
{
    public class UserService : BaseService<UserInfo>
    {
        public UserService() { }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="psw">密码</param> 
        public UserInfo Login(string userName, string psw)
        {
            string md5 = MD5.Encrypt(psw);
            return Repository.QueryList("UserName=@userName and Password =@psw", new { userName = userName, psw = md5 }).FirstOrDefault();
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        public UserInfo GetUserInfo(int id)
        {
            return Repository.Query(id);
        }

        /// <summary>
        /// 获取全部用户
        /// </summary>
        public List<UserInfo> GetUserInfos()
        {
            return Repository.QueryList();
        }

        /// <summary>
        /// 获取全部用户
        /// </summary>
        public bool CreateUser()
        {
            UserInfo userInfo = new UserInfo()
            {
                Name = "EminemJK",
                Password = MD5.Encrypt("12345678"),
                Phone = "17777075292",
                Sex = 1,
                UserName = "admin",
                CreateTime = DateTime.Now,
                 Enable = 1
            };
             Repository.Insert(userInfo);
            return true;
        }
    }
}
