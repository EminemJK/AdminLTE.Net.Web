using AdminLTE.Application.Service.UserSvr.Dto;
using AdminLTE.Models.Entity;
using AdminLTE.Models.VModel;
using Banana.Uow;
using Banana.Uow.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdminLTE.Application.Service.UserSvr
{
    public interface IUserService
    {
        UserInfoDto Login(string userName, string psw);

        UserInfoDto GetUserInfo(int id);

        List<UserInfoDto> GetUserInfos();

        List<UserInfoDto> GetIndexUsers(int count = 8);

        List<UserInfoDto> GetUserList(VUserListConditionInput input, out int pageCount);

        int Save(UserInfoDto inputUserInfo);
    }
}
