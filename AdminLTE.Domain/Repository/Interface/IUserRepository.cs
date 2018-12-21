using AdminLTE.Models.Entity;
using AdminLTE.Models.VModel;
using Banana.Uow.Interface;
using Banana.Uow.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdminLTE.Domain.Repository.Interface
{
    public interface IUserRepository : IRepository<UserInfo>
    {
        UserInfo GetUserInfo(string userName, string psw);

        List<UserInfo> GetTopUserList(int top);

        IPage<UserInfo> GetUserInfoByQueryCondition(VUserListConditionInput input, string orderBy, bool isAsc);
    }
}
