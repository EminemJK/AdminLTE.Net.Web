using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdminLTE.Application.Service;
using AdminLTE.Application.Service.UserSvr;
using AdminLTE.Models;
using AdminLTE.Models.VModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
namespace AdminLTE.Net.Web.Pages
{
    public class BasePageModel : PageModel
    {
        protected IUserService userService { get; set; }
        public BasePageModel(IUserService userService)
        {
            this.userService = userService;
        }

        public VUserCookieModel CurrentUser
        {
            get
            {
                return CookieService.GetDesDecrypt<VUserCookieModel>(this.User.Claims.FirstOrDefault().Value);
            }
        }

        [HttpPost]
        public virtual async Task<IActionResult> OnPostUploadAsync([FromServices]IHostingEnvironment env)
        {
            UploadFileModel upload = new UploadFileModel();
            var files = Request.Form.Files;
            if (files.Count > 0)
            {
                var file = files[0];
                string md5code = string.Empty;
                using (var inputStream = file.OpenReadStream())
                {
                    using (var md5 = System.Security.Cryptography.MD5.Create())
                    {
                        byte[] retVal = md5.ComputeHash(inputStream);
                        StringBuilder md5sb = new StringBuilder();
                        for (int i = 0; i < retVal.Length; i++)
                        {
                            md5sb.Append(retVal[i].ToString("x2"));
                        }
                        md5code = md5sb.ToString();
                    }
                }

                // 文件名完整路径  
                upload.extension = Path.GetExtension(file.FileName);
                upload.fileName = md5code + upload.extension;
                var path = string.Format(@"\upload\{0}\{1}", DateTime.Today.Year.ToString(), DateTime.Today.Month.ToString().PadLeft(2, '0'));
                upload.path = string.Format(@"{0}\{1}", path, upload.fileName);
                var savedFilePath = env.WebRootPath + path;
            
                if (!Directory.Exists(savedFilePath))
                {
                    Directory.CreateDirectory(savedFilePath);
                }
                var fullFileNamePath = Path.Combine(savedFilePath, upload.fileName);
                if (!System.IO.File.Exists(fullFileNamePath))
                {
                    try
                    {
                        using (var fileStream = new FileStream(fullFileNamePath, FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                    
                } 
            }
            return new JsonResult(upload);
        }
    }
}
