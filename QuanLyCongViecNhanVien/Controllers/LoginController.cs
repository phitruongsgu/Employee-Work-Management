using Core.Entities;
using Infrastructure.Persistance.DBcontext;
using Infrastructure.Persistance.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using QuanLyCongViecNhanVien.Models;
using System;
using System.Linq;

namespace QuanLyCongViecNhanVien.Controllers
{
    public class LoginController : Controller
    {
        private readonly IAccountRepository accountRepository;

        public LoginController(IAccountRepository _accountRepository)
        {
            accountRepository = _accountRepository;
        }

        public IActionResult Index()
        {
            String user = HttpContext.Session.GetString("login");

            if (checkSession())
            {
                Response.Redirect("/Home/Index");
            }
            return View(new LoginModel());
        }

        [HttpPost]
        public IActionResult Index(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                String user = model.username;
                String pass = SeedData.ToMD5(model.password);

                Account acc = accountRepository.Accounts().Where(p => p.Email == user && p.Password == pass).FirstOrDefault();

                if (acc != null)
                {
                    HttpContext.Session.SetString("login", JsonConvert.SerializeObject(acc.Account_ID));
                    Response.Redirect("/Home/Index");
                }
                else
                {
                    Response.Redirect("/Login/Index");
                }

            }
            return View();
        }

        public void Logout()
        {
            HttpContext.Session.Remove("login");
            Response.Redirect("/Login/Index");
        }

        public Boolean checkSession()
        {
            String user = HttpContext.Session.GetString("login");
            if (user != null)
            {
                return true;
            }
            return false;
        }

    }
}

#region xử lý login bằng ajax
//[HttpPost]
//[ValidateAntiForgeryToken] // chống hack
//public JsonResult xulydangnhap([FromBody] LoginModel data)
//{
//    String username = data.username;
//    String password = data.password;

//    if (username.Equals("phitruong") && password.Equals("123"))
//    {
//        return Json("Login successed.!!! \nUsername is: " + username + "\nPassword is: " + password);
//    }
//    return Json("User/Pass is not correct");
//}
#endregion