using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdminLTE.Domain.Repository.Interface;
using AdminLTE.Models.Entity;
using Banana.Uow;

namespace AdminLTE.Domain.Repository
{
    public class UserRepository : Repository<UserInfo>, IUserRepository
    {
        public UserInfo GetUserInfo(string userName, string psw)
        {
            return QueryList("UserName=@UserName and Password =@Password", new { UserName = userName, Password = psw }).FirstOrDefault();
        }
    }
}
