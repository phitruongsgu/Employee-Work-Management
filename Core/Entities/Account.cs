using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Account
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Account_ID { get; set; }
        public string User_Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool Account_Status { get; set; }
        
        public int Role_ID { get; set; }

    }
}
