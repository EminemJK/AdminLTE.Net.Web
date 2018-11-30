using System;
using System.Collections.Generic;
using System.Text;
using AdminLTE.Models;
using System.Collections;
using System.Linq;
using Banana.Utility.Encryption;
using AdminLTE.Models.VModel;
using AdminLTE.Models.Enum;
using Banana.Uow.Models;
using Banana.Utility.Common;

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
        /// 获取最近6个注册用户
        /// </summary>
        public List<VUserListModel> GetIndexUsers(int count = 8)
        {
            var res = new List<VUserListModel>();
            var list = Repository.QueryList("CreateTime>=@time  ORDER BY CreateTime desc", new { time = "2018-11-01" }).Take(count).ToList();
            list.ForEach(u =>
            {
                VUserListModel model = ModelConvertUtil<UserInfo, VUserListModel>.ModelCopy(u);
                res.Add(model);
            });
            return res;
        }

        public List<VUserListModel> GetUserList(VUserListConditionInput input, out int pageCount)
        {
            List<string> sqlwhere = new List<string>();
            string whereStr = "";
            if (!string.IsNullOrEmpty(input.name))
            {
                sqlwhere.Add(" name like @name");
            }
            if (!string.IsNullOrEmpty(input.phone))
            {
                sqlwhere.Add(" phone = @phone");
            }
            if (input.sex > -1)
            {
                sqlwhere.Add(" sex = @sex");
            }
            if (sqlwhere.Count > 0)
            {
                whereStr = string.Join("and", sqlwhere);
            }
            var user = Repository.QueryList(input.pageNum, input.pageSize, whereString: whereStr, param: new { name = "%" + input.name + "%", phone = input.phone, sex = input.sex }, order: "createTime");
            pageCount = user.pageCount;
            var res = ModelConvertUtil<UserInfo, VUserListModel>.ModelCopy(user.data);
            return res;
        }

        public int Save(VUserInfoInput inputUserInfo)
        {
            var user = new UserInfo()
            {
                Id = inputUserInfo.Id,
                Name = inputUserInfo.Name,
                UserName = inputUserInfo.UserName,
                CreateTime = DateTime.Now,
                Enable =  EUserState.Enabled,
                HeaderImg = inputUserInfo.HeaderImg.Substring(inputUserInfo.HeaderImg.IndexOf("/upload")),
                Password = MD5.Encrypt(inputUserInfo.Password),
                Phone = inputUserInfo.Phone,
                Sex = inputUserInfo.Sex
            };
            if (user.Id == 0)
            {
                return (int)Repository.Insert(user);
            }
            else
            {
                return Repository.Update(user) ? 1: 0;
            }
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
