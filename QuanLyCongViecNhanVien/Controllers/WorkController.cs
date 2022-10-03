using Core.Entities;
using Infrastructure.Persistance.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuanLyCongViecNhanVien.AjaxModel;
using QuanLyCongViecNhanVien.Models;
using QuanLyCongViecNhanVien.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace QuanLyCongViecNhanVien.Controllers
{
    public class WorkController : Controller
    {
        private readonly IWorkRepository WorkRepository;
        private readonly IStatusRepository StatusRepository;
        private readonly IAccountRepository accountRepository;
        private readonly IStaffRepository staffRepository;
        private readonly IPositionRepository positionRepository;
        private readonly IStaff_WorkRepository staff_WorkRepository;
        private readonly ICommentRepository commentRepository;
        private readonly IRoleRepository roleRepository;
        private readonly IAnnounceRepository announceRepository;

        public WorkController(IWorkRepository _workRepository, IStatusRepository _statusRepository,
            IAccountRepository _accountRepository, IStaffRepository _staffRepository, IPositionRepository _positionRepository
            , IStaff_WorkRepository _staff_WorkRepository, ICommentRepository _commentRepository, IRoleRepository _roleRepository,
            IAnnounceRepository _announceRepository)
        {
            this.WorkRepository = _workRepository;
            this.StatusRepository = _statusRepository;
            this.accountRepository = _accountRepository;
            this.staffRepository = _staffRepository;
            this.positionRepository = _positionRepository;
            this.staff_WorkRepository = _staff_WorkRepository;
            this.commentRepository = _commentRepository;
            this.roleRepository = _roleRepository;
            this.announceRepository = _announceRepository;
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
            if (role == "Admin")
            {
                ModelView m = new ModelView()
                {
                    Works = WorkRepository.Works().OrderByDescending(p => p.Work_ID),
                    Statuses = StatusRepository.Statuses(),
                    Staff_ID = 0
                };
                return View(m);
            }
            else
            {
                int IDStaff = staffRepository.Staffs().Where(p => p.Account_ID == acc.Account_ID).FirstOrDefault().Staff_ID;
                List<Staff_Work> staff_Works = staff_WorkRepository.Staff_Works().Where(p => p.Staff_ID == IDStaff).ToList();
                List<Work> works = new List<Work>();
                foreach (var i in staff_Works)
                {
                    foreach (var j in WorkRepository.Works())
                    {
                        if (j.Work_ID == i.Work_ID)
                        {
                            works.Add(j);
                        }
                    }
                }
                ModelView m = new ModelView()
                {
                    Works = works.OrderByDescending(p => p.Work_ID),
                    Statuses = StatusRepository.Statuses(),
                    Staff_ID = IDStaff
                };
                return View(m);
            }
        }

        [HttpPost]
        public IActionResult Index(string search)
        {
            if (!checkSession()) // checkSession == false
            {
                Response.Redirect("/Login/Index");
            }
            search = RemoveVietnameseTone(search);
            var works = WorkRepository.Works().Where(p => RemoveVietnameseTone((p.Title.ToLower())).Contains(search.ToLower())).ToList();
            ModelView m = new ModelView()
            {
                Works = works,
                Statuses = StatusRepository.Statuses()

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
        public IActionResult Detail(int id)
        {
            if (!checkSession())
            {
                Response.Redirect("/Login/Index");
            }
            Work W = WorkRepository.FindByID(id); 
            var ST = StatusRepository.Statuses();
            var S_W = staff_WorkRepository.Staff_Works().Where(p=>p.Work_ID == id);
            var S = staffRepository.Staffs();
            var CM = commentRepository.Comments().Where(p => p.Work_ID == id);
            String userID = HttpContext.Session.GetString("login");
            ModelView m = new ModelView()
            {
                work = W,
                Statuses = ST,
                Staff_Works = S_W,
                Staffs = S,
                Comments = CM,
                Account_ID = int.Parse(userID),
                Accounts = accountRepository.Accounts(),
                Announces = announceRepository.Announces().Where(p=>p.Work_ID == id)
            };
            return View(m);
        }

        public IActionResult Create()
        {
            if (!checkSession())
            {
                Response.Redirect("/Login/Index");
            }
            return View(new WorkModel());
        }

        [HttpPost]
        public IActionResult Create(WorkModel wm)
        {

            if (ModelState.IsValid) // check các validate
            {
                String user = HttpContext.Session.GetString("login");
                int IDAccount = accountRepository.Accounts().Where(p => p.Account_ID == int.Parse(user)).FirstOrDefault().Account_ID;
                int IDStaff = staffRepository.Staffs().Where(p => p.Account_ID == IDAccount).FirstOrDefault().Staff_ID;

                var w = new Work();
                w.Title = wm.Title;
                w.Start_Date = DateTime.Parse(wm.StartDate.ToString().Split(" ")[0]).ToString("dd-MM-yyyy");
                w.End_Date = DateTime.Parse(wm.EndDate.ToString().Split(" ")[0]).ToString("dd-MM-yyyy");
                w.Range = wm.Range;
                if (wm.EndDate < DateTime.Now.Date)
                {
                    w.Status_ID = 2;
                }
                else
                {
                    w.Status_ID = 1;
                }
                w.staffCreate_ID = IDStaff;
                WorkRepository.createWork(w);

                staff_WorkRepository.createStaff_Work(new Staff_Work
                {
                    Staff_ID = IDStaff,
                    Work_ID = WorkRepository.Works().LastOrDefault().Work_ID
                });

                return RedirectToAction("AddStaffToWork");
            }
            return View();
        }

        public IActionResult AddStaffToWork()
        {
            int idLastOfWork = WorkRepository.Works().LastOrDefault().Work_ID;
            List<Staff_Work> listSW = staff_WorkRepository.Staff_Works().Where(p => p.Work_ID == idLastOfWork).ToList();
            String user = HttpContext.Session.GetString("login");
            int IDAccount = accountRepository.Accounts().Where(p => p.Account_ID == int.Parse(user)).FirstOrDefault().Account_ID;
            int IDStaff = staffRepository.Staffs().Where(p => p.Account_ID == IDAccount).FirstOrDefault().Staff_ID;

            ModelView vm = new ModelView
            {
                Positions = positionRepository.Positions(),
                Staffs = staffRepository.Staffs(),
                Staff_Works = listSW,
                work = WorkRepository.Works().LastOrDefault(),
                Staff_ID = IDStaff,
                Roles = roleRepository.Roles(),
                Accounts = accountRepository.Accounts()
            };

            return View(vm);
        }

        public void AddNewStaffToWork(int id)
        {
            staff_WorkRepository.createStaff_Work(new Staff_Work
            {
                Staff_ID = id,
                Work_ID = WorkRepository.Works().LastOrDefault().Work_ID,
                Progress_Personal = 0
            });
            Response.Redirect("/Work/AddStaffToWork");
        }

        #region thêm nhân viên bằng ajax
        //public JsonResult AddNewStaffToWork([FromBody] AddNewStaffToWork data)
        //{
        //    int ID = data.IDStaff;
        //    Staff s = staffRepository.Staffs().Where(p => p.Staff_ID == ID).FirstOrDefault();
        //    String positionName = positionRepository.Positions().Where(p => p.Position_ID == s.Position_ID).FirstOrDefault().Position_Name;
        //    staff_WorkRepository.createStaff_Work(new Staff_Work
        //    {
        //        Staff_ID = ID,
        //        Work_ID = WorkRepository.Works().LastOrDefault().Work_ID,
        //        Progress_Personal = 0
        //    });
        //    String html = "<tr>" +
        //        "<td>" + s.Staff_ID + "</td>" +
        //        "<td>" +s.Staff_Name +"</td>" +
        //        "<td>" + s.Birthday + "</td>" +
        //        "<td>" + s.ID_Card + "</td>" +
        //        "<td>" + s.Gender + "</td>" +
        //        "<td>" + s.EmailAddress + "</td>" +
        //        "<td>" + s.PhoneNumber + "</td>" +
        //        "<td>" + positionName + "</td>" +
        //        "<td><a onclick='return CheckDelete()' asp-route-id=" + s.Staff_ID +"asp-action='RemoveStaffToWork' asp-controller='Work' class='btn btn-danger'><i class='fas fa-trash-alt'></i> Loại bỏ</a></td>" +
        //        "</tr>?" +ID;
        //    return Json(html.ToString());
        //}
        #endregion

        public void RemoveStaffToWork(int id)
        {
            int idLastOfWork = WorkRepository.Works().LastOrDefault().Work_ID;
            staff_WorkRepository.removeStaff_Work(id,idLastOfWork);
            Response.Redirect("/Work/AddStaffToWork");
        }

        public void RemoveStaffToWorkForEdit(int idStaff , int idWork)
        {
            staff_WorkRepository.removeStaff_Work(idStaff, idWork);
            Response.Redirect("/Work/EditStaffToWork?idWork=" + idWork);
        }

        public void AddNewStaffToWorkForEdit(int idStaff, int idWork)
        {
            staff_WorkRepository.createStaff_Work(new Staff_Work
            {
                Staff_ID = idStaff,
                Work_ID = idWork,
                Progress_Personal = 0
            });
            Response.Redirect("/Work/EditStaffToWork?idWork=" + idWork);
        }

        public IActionResult EditStaffToWork(int idWork)
        {
            List<Staff_Work> listSW = staff_WorkRepository.Staff_Works().Where(p => p.Work_ID == idWork).ToList();
            String user = HttpContext.Session.GetString("login");
            int IDAccount = accountRepository.Accounts().Where(p => p.Account_ID == int.Parse(user)).FirstOrDefault().Account_ID;
            int IDStaff = staffRepository.Staffs().Where(p => p.Account_ID == IDAccount).FirstOrDefault().Staff_ID;
            
            ModelView vm = new ModelView
            {
                Positions = positionRepository.Positions(),
                Staffs = staffRepository.Staffs(),
                Staff_Works = listSW,
                work = WorkRepository.Works().Where(p => p.Work_ID == idWork).FirstOrDefault(),
                Staff_ID = IDStaff,
                Roles = roleRepository.Roles(),
                Accounts = accountRepository.Accounts()
            };

            return View(vm);
        }

        public IActionResult Edit(int id)
        {
            if (!checkSession())
            {
                Response.Redirect("/Login/Index");
            }
            ModelView m = new ModelView {
                work = WorkRepository.FindByID(id),
                Statuses = StatusRepository.Statuses()
            };
            return View(m);
        }

        [HttpPost]
        public void Edit(WorkModel wm)
        {
            if (!checkSession())
            {
                Response.Redirect("/Login/Index");
            }
            if (ModelState.IsValid)
            {
                if(wm.WorkID != 0)
                {
                    Work w = WorkRepository.FindByID(wm.WorkID);
                    w.Title = wm.Title;
                    w.Start_Date = DateTime.Parse(wm.StartDate.ToString().Split(" ")[0]).ToString("dd-MM-yyyy");
                    w.End_Date = DateTime.Parse(wm.EndDate.ToString().Split(" ")[0]).ToString("dd-MM-yyyy");
                    w.Range = wm.Range;
                    //w.FileName = wm.FileName;
                    w.Status_ID = wm.StatusID;
                    WorkRepository.editWork(w);         
                }
            }
            Response.Redirect("/Work/Index");
        }

        public void Delete(int id)
        {
            if (!checkSession())
            {
                Response.Redirect("/Login/Index");
            }
            WorkRepository.removeWork(id);
            staff_WorkRepository.removeStaff_Work_By_WorkID(id);
            Response.Redirect("/Work/Index");
        }

        [HttpPost]
        public JsonResult xulycmt([FromBody] CommentAjax data)
        {
            String userID = HttpContext.Session.GetString("login");
            string cmt = data.content;
            String workID = data.workID;
            Staff staff = staffRepository.Staffs().Where(p => p.Account_ID == int.Parse(userID)).FirstOrDefault();
            commentRepository.createComment(new Comment
            {
                Content = cmt,
                Account_ID = int.Parse(userID),
                Work_ID = int.Parse(workID)
            });
            String js = cmt + "-" + staff.Staff_Name;
            return Json(js);
        }

        [HttpPost]
        public JsonResult xulynhacnho([FromBody] CommentAjax data)
        {
            String userID = HttpContext.Session.GetString("login");
            string ann = data.content;
            String workID = data.workID;
            Staff staff = staffRepository.Staffs().Where(p => p.Account_ID == int.Parse(userID)).FirstOrDefault();
            announceRepository.createAnnounce(new Announce { 
                Content = ann,
                Announce_Status = 0,
                Staff_ID = staff.Staff_ID,
                Work_ID = int.Parse(workID)
            });
            String js = ann + "-" + staff.Staff_Name;
            return Json(js);
        }

        [HttpPost]
        public JsonResult xoacmt([FromBody] removeCommentByID data)
        {
            int cmtID = data.cmtID;
            commentRepository.removeComment(cmtID);
            return Json("OK");
        }

        [HttpPost]
        public JsonResult xoaloinhac([FromBody] removeCommentByID data)
        {
            int annID = data.cmtID;
            announceRepository.removeAnnounce(annID);
            return Json("OK");
        }

        public void danhdaucongviec(int id)
        {
            Work w = WorkRepository.Works().Where(p => p.Work_ID == id).FirstOrDefault();
            w.Status_ID = 3;
            WorkRepository.editWork(w);
            Response.Redirect("/Work/Index");
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
