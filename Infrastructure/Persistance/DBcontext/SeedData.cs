using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistance.DBcontext
{
    public class SeedData
    {

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
        public static void createDataOnBuild(EFContext context)
        {

            // cau lenh ben duoi dung de ensure(tao moi) data khi table chua co data
            context.Database.EnsureCreated();

            // kiem tra da co du lieu san trong table chua, neu chua thi tao moi, neu co roi thi nhung lan
            // build sau ko can tao them
            if (context.Staffs.Any()) return;
            context.Staffs.AddRange(new List<Staff>
            {
                new Staff
                {
                   Staff_Name="Vũ Văn Thiên",
                   Birthday="08/12/2000",
                   ID_Card="285823454",
                   Gender="Male",
                   EmailAddress="vuvanthien0812@gmail.com",
                   PhoneNumber="0946091371",
                   Status=true,
                   Position_ID=4,
                   Account_ID=1
                },
                 new Staff
                {
                   Staff_Name="Nguyễn Vũ Phi Trường",
                   Birthday="08/12/2000",
                   ID_Card="285821325",
                   Gender="Male",
                   EmailAddress="truong30112000@gmail.com",
                   PhoneNumber="0946091371",
                   Status=true,
                   Position_ID=4,
                   Account_ID=2

                },
                   new Staff
                {
                   Staff_Name="Huỳnh Quang Vinh",
                   Birthday="01/06/1999",
                   ID_Card="285834566",
                   Gender="Male",
                   EmailAddress="huynhquangvinh1999@gmail.com",
                   PhoneNumber="0946345321",
                   Status=true,
                   Position_ID=4,
                   Account_ID=3

                },
                     new Staff
                {
                   Staff_Name="Huấn Rose",
                   Birthday="01/06/1999",
                   ID_Card="285834566",
                   Gender="Male",
                   EmailAddress="chanlysong@gmail.com",
                   PhoneNumber="0946345321",
                   Status=true,
                   Position_ID=3, 
                   Account_ID=4

                },
                     new Staff
                {
                   Staff_Name="Khá Bảnh",
                   Birthday="27/11/1993",
                   ID_Card="285834566",
                   Gender="Male",
                   EmailAddress="muaquatchuyennghiep@gmail.com",
                   PhoneNumber="0946345321",
                   Status=true,
                   Position_ID=3, 
                   Account_ID=5
                }
            });
            if (context.Accounts.Any()) return;
            context.Accounts.AddRange(new List<Account>
            {
                new Account
                {
                    User_Name="hollygraid",
                    Password=ToMD5("123456"),
                    Email="vuvanthien0812@gmail.com",
                    Account_Status=true,
                    Role_ID=1
                },
                new Account
                {
                    User_Name="phitruong",
                    Password=ToMD5("123456"),
                    Email="truong30112000@gmail.com",
                    Account_Status=true,
                    Role_ID=1
                },
                 new Account
                {
                    User_Name="quangvinh",
                    Password=ToMD5("123456"),
                    Email="huynhquangvinh1999@gmail.com",
                    Account_Status=true,
                    Role_ID=1
                },
                   new Account
                {
                    User_Name="huanrose",
                    Password=ToMD5("123456"),
                    Email="chanlysong@gmail.com",
                    Account_Status=true,
                    Role_ID=2
                },
                    new Account
                {
                    User_Name="khabanh",
                    Password=ToMD5("123456"),
                    Email="muaquatchuyennghiep@gmail.com",
                    Account_Status=true,
                    Role_ID=2
                }
            });
            if (context.Positions.Any()) return;
            context.Positions.AddRange(new List<Position>
            {
                new Position
                {
                    Position_Name="Giám đốc",
                    Detail="Không có ghi chú"
                },
                 new Position
                {
                    Position_Name="Nhân viên",
                    Detail="Không có ghi chú"
                },
                  new Position
                {
                    Position_Name="Quản lý",
                    Detail="Quản lý bộ phận"
                },
                    new Position
                {
                    Position_Name="Thư ký",
                    Detail="Không có ghi chú"
                },

            });
            if (context.Roles.Any()) return;
            context.Roles.AddRange(new List<Role>
            {
                new Role
                {
                    RoleName="Admin"
                },
                 new Role
                {
                    RoleName="Staff"
                }

            });
            if (context.Statuses.Any()) return;
            context.Statuses.AddRange(new List<Status>
            {
                new Status
                {
                    Status_Name="Đang thực hiện"
                },
                 new Status
                {
                    Status_Name="Trễ hạn"
                },
                  new Status
                {
                    Status_Name="Đã hoàn thành"
                }
            });
            if (context.Account_Roles.Any()) return;
            context.Account_Roles.AddRange(new List<Account_Role>
            {
                new Account_Role
                {
                    Account_ID=1,
                    Role_ID=1
                },
                new Account_Role
                {
                    Account_ID=2,
                    Role_ID=1
                },
                 new Account_Role
                {
                    Account_ID=3,
                    Role_ID=2
                },
            });
            context.SaveChanges();
        }

    }
}
