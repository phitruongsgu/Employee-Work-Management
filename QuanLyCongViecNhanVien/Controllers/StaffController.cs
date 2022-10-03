using Core.Entities;
using Infrastructure.Persistance.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuanLyCongViecNhanVien.Models;
using QuanLyCongViecNhanVien.ViewModel;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace QuanLyCongViecNhanVien.Controllers
{
    public class StaffController : Controller
    {
        private readonly IAccountRepository accountRepository;
        private readonly IRoleRepository roleRepository;
        private readonly IStaffRepository staffRepository;
        private readonly IPositionRepository positionRepository;

        public StaffController(IAccountRepository _accountRepository, IRoleRepository _roleRepository, IStaffRepository _staffRepository, IPositionRepository _positionRepository)
        {
            this.accountRepository = _accountRepository;
            this.roleRepository = _roleRepository;
            this.staffRepository = _staffRepository;
            this.positionRepository = _positionRepository;
        }

        public IActionResult Index()
        {
            if (!checkSession()) // checkSession == false
            {
                Response.Redirect("/Login/Index");
            }
            String user = HttpContext.Session.GetString("login");
            Account acc = accountRepository.Accounts().Where(p => p.Account_ID == int.Parse(user)).FirstOrDefault();
            String role = roleRepository.Roles().Where(p => p.Role_ID == acc.Role_ID).FirstOrDefault().RoleName;

            ModelView m = new ModelView()
            {
                Staffs = staffRepository.Staffs().OrderByDescending(p =>p.Staff_ID),
                Positions = positionRepository.Positions(),
                Accounts = accountRepository.Accounts()
            };
            return View(m);
        }

        [HttpPost]
        public IActionResult Index(string search)
        {
            if (!checkSession()) // checkSession == false
            {
                Response.Redirect("/Login/Index");
            }
            search = RemoveVietnameseTone(search);
            var staffs = staffRepository.Staffs().Where(p => RemoveVietnameseTone((p.Staff_Name.ToLower())).Contains(search.ToLower())).ToList();
            ModelView m = new ModelView()
            {
                Staffs = staffs,
                Positions = positionRepository.Positions(),

            };
            return View(m);
        }
        public static string RemoveVietnameseTone(string text)
        {
            string result = text.ToLower();
            result = Regex.Replace(result, "à|á|ạ|ả|ã|â|ầ|ấ|ậ|ẩ|ẫ|ă|ằ|ắ|ặ|ẳ|ẵ|/g", "a");
            result = Regex.Replace(result, "è|é|ẹ|ẻ|ẽ|ê|ề|ế|ệ|ể|ễ|/g", "e");
            result = Regex.Replace(result, "ì|í|ị|ỉ|ĩ|/g", "i");
            result = Regex.Replace(result, "ò|ó|ọ|ỏ|õ|ô|ồ|ố|ộ|ổ|ỗ|ơ|ờ|ớ|ợ|ở|ỡ|/g", "o");
            result = Regex.Replace(result, "ù|ú|ụ|ủ|ũ|ư|ừ|ứ|ự|ử|ữ|/g", "u");
            result = Regex.Replace(result, "ỳ|ý|ỵ|ỷ|ỹ|/g", "y");
            result = Regex.Replace(result, "đ", "d");
            return result;
        }
        public IActionResult CreateStaff()
        {
            if (!checkSession())
            {
                Response.Redirect("/Login/Index");
            }
            return View(new StaffModel());
        }

        [HttpPost]
        public IActionResult CreateStaff(StaffModel sm)
        {

            if (ModelState.IsValid) // check các validate của StaffModel
            {
                String user = HttpContext.Session.GetString("login");
                int IDAccount = accountRepository.Accounts().Where(p => p.Account_ID == int.Parse(user)).FirstOrDefault().Account_ID;
                int IDStaff = staffRepository.Staffs().Where(p => p.Account_ID == IDAccount).FirstOrDefault().Staff_ID;

                var s = new Staff();
                s.Staff_Name = sm.Staff_Name;
                s.Birthday = sm.Birthday.ToString().Split(" ")[0];
                s.ID_Card = sm.ID_Card;
                s.Gender = sm.Gender;
                s.EmailAddress = sm.EmailAddress;
                s.PhoneNumber = sm.PhoneNumber;
                s.Status = true;
                s.Position_ID = sm.Position_ID;
                s.Account_ID = accountRepository.Accounts().LastOrDefault().Account_ID;
                staffRepository.createStaff(s);
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult CreateAccount()
        {
            if (!checkSession())
            {
                Response.Redirect("/Login/Index");
            }
            return View(new AccountModel());
        }

        [HttpPost]
        public IActionResult CreateAccount(AccountModel am)
        {

            if (ModelState.IsValid) // check các validate của StaffModel
            {
                String user = HttpContext.Session.GetString("login");
                int IDAccount = accountRepository.Accounts().Where(p => p.Account_ID == int.Parse(user)).FirstOrDefault().Account_ID;
                int IDStaff = staffRepository.Staffs().Where(p => p.Account_ID == IDAccount).FirstOrDefault().Staff_ID;

                var a = new Account();
                a.User_Name = am.User_Name;
                a.Password = ToMD5(am.Password);
                a.Email = am.Email;
                a.Account_Status = true;
                a.Role_ID = 2;
                accountRepository.createAccount(a);
                return RedirectToAction("CreateStaff");
            }
            return View();
        }

        public static string ToMD5(string str)
        {

            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();

            byte[] bHash = md5.ComputeHash(Encoding.UTF8.GetBytes(str));

            StringBuilder sbHash = new StringBuilder();

            foreach (byte b in bHash)
            {

                sbHash.Append(String.Format("{0:x2}", b));

            }
            return sbHash.ToString();

        }
        public IActionResult Edit(int id)
        {
            if (!checkSession())
            {
                Response.Redirect("/Login/Index");
            }
            ModelView m = new ModelView
            {
                staff = staffRepository.FindByID(id),
                Positions = positionRepository.Positions(),
                Accounts = accountRepository.Accounts()
            };
            return View(m);
        }

        [HttpPost]
        public void Edit(StaffModel sm)
        {
            if (!checkSession())
            {
                Response.Redirect("/Login/Index");
            }
            if (ModelState.IsValid)
            {
                if (sm.StaffID != 0)
                {
                    Staff s = staffRepository.FindByID(sm.StaffID);
                    s.Staff_Name = sm.Staff_Name;
                    s.Birthday = sm.Birthday.ToString().Split(" ")[0];
                    s.ID_Card = sm.ID_Card;
                    s.Gender = sm.Gender;
                    s.EmailAddress = sm.EmailAddress;
                    s.PhoneNumber = sm.PhoneNumber;
                    s.Status = sm.Status;
                    
                    staffRepository.editStaff(s);
                }
            }
            Response.Redirect("/Staff/Index");
        }

        public void DeleteAccount()
        {
            if (!checkSession())
            {
                Response.Redirect("/Login/Index");
            }
            int idAccount = accountRepository.Accounts().LastOrDefault().Account_ID;
            accountRepository.removeAccount(idAccount);
            Response.Redirect("/Staff/Index");
        }
        public void Delete(int idStaff)
        {
            if (!checkSession())
            {
                Response.Redirect("/Login/Index");
            }
            int idAccount = staffRepository.Staffs().Where(p => p.Staff_ID == idStaff).FirstOrDefault().Account_ID;
            staffRepository.removeStaff(idStaff);
            accountRepository.removeAccount(idAccount);
            Response.Redirect("/Staff/Index");
        }

        public Boolean checkSession()
        {
            String user = HttpContext.Session.GetString("login");
            if (user != null) // có session
            {
                return true;
            }
            return false;
        }
    }
}