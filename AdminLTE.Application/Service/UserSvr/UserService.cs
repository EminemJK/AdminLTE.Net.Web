using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Linq;
using Banana.Utility.Encryption;
using AdminLTE.Models.Enum;
using Banana.Utility.Common;
using AdminLTE.Application.Service.UserSvr.Dto;
using AdminLTE.Models.Entity;
using AdminLTE.Models.VModel;
using AdminLTE.Domain.Repository.Interface;

namespace AdminLTE.Application.Service.UserSvr
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository; 

        public UserService(IUserRepository repo)
        {
            this._repository = repo;
        }
        
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="psw">密码</param> 
        public UserInfoDto Login(string userName, string psw)
        {
            string md5Psw = MD5.Encrypt(psw);
            var user = _repository.GetUserInfo(userName, md5Psw);
            return ModelConvertUtil<UserInfo, UserInfoDto>.ModelCopy(user);
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        public UserInfoDto GetUserInfo(int id)
        {
            var user = _repository.Query(id);
            return ModelConvertUtil<UserInfo, UserInfoDto>.ModelCopy(user);
        }

        /// <summary>
        /// 获取全部用户
        /// </summary>
        public List<UserInfoDto> GetUserInfos()
        {
            var ls = _repository.QueryList();
            return ModelConvertUtil<UserInfo, UserInfoDto>.ModelCopy(ls);
        }

        /// <summary>
        /// 获取最近6个注册用户
        /// </summary>
        public List<UserInfoDto> GetIndexUsers(int count = 8)
        {
            var list = _repository.QueryList("CreateTime>=@time  ORDER BY CreateTime desc", new { time = "2018-11-01" }).Take(count).ToList();
            return ModelConvertUtil<UserInfo, UserInfoDto>.ModelCopy(list);
        }

        public List<UserInfoDto> GetUserList(VUserListConditionInput input, out int pageCount)
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
            var user = _repository.QueryList(input.pageNum, input.pageSize, whereString: whereStr, param: new { name = "%" + input.name + "%", phone = input.phone, sex = input.sex }, order: "createTime");
            pageCount = user.pageCount;
            return ModelConvertUtil<UserInfo, UserInfoDto>.ModelCopy(user.data);
        }

        public int Save(VUserInfoInput inputUserInfo)
        {
            var dto = ModelConvertUtil<VUserInfoInput, UserInfoDto>.ModelCopy(inputUserInfo);
            var user = ModelConvertUtil<UserInfoDto, UserInfo>.ModelCopy(dto);
            user.CreateTime = DateTime.Now;
            user.Enable = EUserState.Enabled;
            user.HeaderImg = user.HeaderImg.Substring(inputUserInfo.HeaderImg.IndexOf("/upload"));
            user.Password = MD5.Encrypt(user.Password); 
            if (user.Id == 0)
            {
                return (int)_repository.Insert(user);
            }
            else
            {
                return _repository.Update(user) ? 1: 0;
            }
        }

        /// <summary>
        /// Test data
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
            _repository.Insert(ls);
        }
    }
}
