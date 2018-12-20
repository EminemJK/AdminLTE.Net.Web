using AdminLTE.Models.Entity;
using Banana.Uow.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdminLTE.Domain.Repository.Interface
{
    public interface IUserRepository : IRepository<UserInfo>
    {
        UserInfo GetUserInfo(string userName, string psw);
    }
}
