using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminLTE.Domain.Service;
using AdminLTE.Models.VModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AdminLTE.Net.Web.Pages.User
{
    public class UserListModel : PageModel
    {
        public UserListModel(UserService service)
        {
            userService = service;
        }
        public void OnGet()
        {
        }

        protected UserService userService { get; set; }

        public List<VUserListModel> UserList { get; set; }


    }
}