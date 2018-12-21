using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdminLTE.Domain.Repository.Interface;
using AdminLTE.Models.Entity;
using AdminLTE.Models.VModel;
using Banana.Uow;
using Banana.Uow.Models;

namespace AdminLTE.Domain.Repository
{
    public class UserRepository : Repository<UserInfo>, IUserRepository
    {
        public List<UserInfo> GetTopUserList(int top)
        {
            return QueryList(1, top, "CreateTime>=@time", new { time = "2018-11-01" }, order: "CreateTime", asc: false).data;
        }

        public UserInfo GetUserInfo(string userName, string psw)
        {
            return QueryList("UserName=@UserName and Password =@Password", new { UserName = userName, Password = psw }).FirstOrDefault();
        }

        public IPage<UserInfo> GetUserInfoByQueryCondition(VUserListConditionInput input, string orderBy, bool isAsc)
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
            return QueryList(input.pageNum, input.pageSize, whereString: whereStr, param: new { name = "%" + input.name + "%", phone = input.phone, sex = input.sex }, order: orderBy,asc: isAsc);
        }
    }
}
