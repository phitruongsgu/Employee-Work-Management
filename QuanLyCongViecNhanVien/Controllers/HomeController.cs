using Core.Entities;
using Infrastructure.Persistance.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using QuanLyCongViecNhanVien.Models;
using QuanLyCongViecNhanVien.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyCongViecNhanVien.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWorkRepository WorkRepository;
        private readonly IStatusRepository StatusRepository;
        private readonly IAccountRepository accountRepository;
        private readonly IStaffRepository staffRepository;
        private readonly IPositionRepository positionRepository;
        private readonly IStaff_WorkRepository staff_WorkRepository;
        private readonly IRoleRepository roleRepository;

        public HomeController(IWorkRepository _workRepository, IStatusRepository _statusRepository, IAccountRepository _accountRepository,
            IStaffRepository _staffRepository, IPositionRepository _positionRepository, IStaff_WorkRepository _staff_WorkRepository,
            IRoleRepository _roleRepository)
        {
            this.WorkRepository = _workRepository;
            this.StatusRepository = _statusRepository;
            this.accountRepository = _accountRepository;
            this.staffRepository = _staffRepository;
            this.positionRepository = _positionRepository;
            this.staff_WorkRepository = _staff_WorkRepository;
            this.roleRepository = _roleRepository;
        }

        public IActionResult Index()
        {
            if (!checkSession())
            {
                Response.Redirect("/Login/Index");
            }
            String user = HttpContext.Session.GetString("login");
            Account acc = accountRepository.Accounts().Where(p => p.Account_ID == int.Parse(user)).FirstOrDefault();
            String roleName = roleRepository.Roles().Where(p => p.Role_ID == acc.Role_ID).FirstOrDefault().RoleName;
            TempData["role"] = roleName;

            Staff staff = staffRepository.Staffs().Where(p => p.Account_ID == acc.Account_ID).FirstOrDefault();
            TempData["staffName"] = staff.Staff_Name;
            TempData["gender"] = staff.Gender;

            // lấy hết Staff_Work
            List<Staff_Work> staff_Works = staff_WorkRepository.Staff_Works().ToList();

            // khởi tạo mảng để đếm
            List<Work> works = new List<Work>();
            List<Work> doingForWorks = new List<Work>();
            List<Work> lateWorks = new List<Work>();
            List<Work> finishWorks = new List<Work>();

            if (roleName == "Admin")
            {
                foreach (var j in WorkRepository.Works())
                {
                    works.Add(j);
                    String status_Name = StatusRepository.Statuses().Where(p => p.Status_ID == j.Status_ID).FirstOrDefault().Status_Name;
                    if (status_Name == "Đang thực hiện")
                    {
                        doingForWorks.Add(j);
                    }
                    if (status_Name == "Trễ hạn")
                    {
                        lateWorks.Add(j);
                    }
                    if (status_Name == "Đã hoàn thành")
                    {
                        finishWorks.Add(j);
                    }
                }

                HomeViewModel vm = new HomeViewModel
                {
                    countWorks = works.Count(),
                    countDoingForWorks = doingForWorks.Count(),
                    countLateForWorks = lateWorks.Count(),
                    countFinishForWorks = finishWorks.Count(),
                    GetStatuses = StatusRepository.Statuses(),
                    GetWorks = works.OrderByDescending(p => p.Work_ID)
                };
                return View(vm);
            }
            else
            {
                foreach (var work in WorkRepository.Works())
                {
                    Staff_Work sw = staff_WorkRepository.Staff_Works()
                        .Where(p => p.Staff_ID == staff.Staff_ID && p.Work_ID == work.Work_ID)
                        .FirstOrDefault();

                    if (sw!= null || work.Range == "Public")
                    {
                        works.Add(work);
                        String status_Name = StatusRepository.Statuses().Where(p => p.Status_ID == work.Status_ID).FirstOrDefault().Status_Name;
                        if (status_Name == "Đang thực hiện")
                        {
                            doingForWorks.Add(work);
                        }
                        if (status_Name == "Trễ hạn")
                        {
                            lateWorks.Add(work);
                        }
                        if (status_Name == "Đã hoàn thành")
                        {
                            finishWorks.Add(work);
                        }
                    }
                }

                HomeViewModel vm = new HomeViewModel
                {
                    countWorks = works.Count(),
                    countDoingForWorks = doingForWorks.Count(),
                    countLateForWorks = lateWorks.Count(),
                    countFinishForWorks = finishWorks.Count(),
                    GetStatuses = StatusRepository.Statuses(),
                    GetWorks = works.OrderByDescending(p => p.Work_ID)
                };
                return View(vm);
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public Boolean checkSession()
        {
            String user = HttpContext.Session.GetString("login");
            if(user != null)
            {
                return true;
            }
            return false;
        }
    }
}
