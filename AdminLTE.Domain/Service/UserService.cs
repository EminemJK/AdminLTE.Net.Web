using System;
using System.Collections.Generic;
using System.Text;
using AdminLTE.Models;
using System.Collections;
using System.Linq;
using Banana.Utility.Encryption;
using AdminLTE.Models.VModel;
using AdminLTE.Models.Enum;

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
        /// 获取最近注册用户
        /// </summary>
        public List<VUserListModel> GetIndexUsers()
        {
            var res = new List<VUserListModel>();
            var list = Repository.QueryList("CreateTime>=@time  ORDER BY CreateTime desc", new { time = "2018-11-01" });
            list.ForEach(u =>
            {
                VUserListModel model = new VUserListModel()
                {
                    Id = u.Id,
                    UserName = u.Name,
                    UserHeader = GetUserHeader(u.UserName),
                    Time = DateTime.Now,
                    CreateTime = u.CreateTime,
                    Enable = u.Enable,
                    Sex = u.Sex,
                    Name = u.Name
                };
                res.Add(model);
            });
            return res;
        }

        public List<VUserListModel> GetUserList(int pageNum, int pageSize, string whereString = null, object param = null, string order = null, bool asc = false)
        {
            var user = Repository.QueryList(pageNum, pageSize, whereString, param, order, asc);
            return null;
        }

        public static string GetUserHeader(string userName)
        {
            return @"/images/userHeader/" + userName + ".jpg";
        }

        /// <summary>
        /// Demo
        /// </summary>
        public void Create()
        {
            List<UserInfo> ls = new List<UserInfo>();
            ls.Add(new UserInfo() { Name = "Monkey D. Luffy", Phone = "15878451111", Password = "12345678", Sex =  EUserSex.Man, UserName = "Luffy", CreateTime = DateTime.Now, Enable = EUserState.Enabled });
            ls.Add(new UserInfo() { Name = "索隆", Phone = "13355526663", Password = "12345678", Sex = EUserSex.Man, UserName = "Zoro", CreateTime = DateTime.Now, Enable = EUserState.Enabled });
            ls.Add(new UserInfo() { Name = "娜美", Phone = "15878451111", Password = "12345678", Sex = EUserSex.Woman, UserName = "Nami", CreateTime = DateTime.Now, Enable = EUserState.Enabled });
            ls.Add(new UserInfo() { Name = "山治", Phone = "17755602229", Password = "12345678", Sex = EUserSex.Man, UserName = "Sanji", CreateTime = DateTime.Now, Enable = EUserState.Enabled });
            ls.Add(new UserInfo() { Name = "乌索普", Phone = "14799995555", Password = "12345678", Sex = EUserSex.Man, UserName = "Usopp", CreateTime = DateTime.Now, Enable = EUserState.Enabled });
            ls.Add(new UserInfo() { Name = "乔巴", Phone = "18966660000", Password = "12345678", Sex = EUserSex.Man, UserName = "Chopper", CreateTime = DateTime.Now, Enable = EUserState.Enabled });
            ls.Add(new UserInfo() { Name = "罗宾", Phone = "13122227878", Password = "12345678", Sex = EUserSex.Woman, UserName = "Robin", CreateTime = DateTime.Now, Enable = EUserState.Enabled });
            ls.Add(new UserInfo() { Name = "弗兰奇", Phone = "15962354412", Password = "12345678", Sex = EUserSex.Man, UserName = "Franky", CreateTime = DateTime.Now, Enable = EUserState.Enabled });
            ls.Add(new UserInfo() { Name = "布鲁克", Phone = "14322221111", Password = "12345678", Sex = EUserSex.Man, UserName = "Brook", CreateTime = DateTime.Now, Enable = EUserState.Enabled });
            ls.Add(new UserInfo() { Name = "甚平", Phone = "15655479960", Password = "12345678", Sex = EUserSex.Man, UserName = "Jinbe", CreateTime = DateTime.Now, Enable = EUserState.Enabled });
            Repository.InsertBatch("  INSERT INTO dbo.T_User( UserName ,Password ,Name ,Sex,Phone ,Enable ,CreateTime) VALUES  ( @UserName ,@Password ,@Name ,@Sex ,@Phone ,@Enable ,@CreateTime)", ls);
        }
    }
}
