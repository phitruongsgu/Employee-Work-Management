using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Entities
{
    public class Staff
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Staff_ID { get; set; }
        public string Staff_Name { get; set; }
        public string Birthday { get; set; }
        public string ID_Card { get; set; }
        public string Gender { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public bool Status { get; set; }

        public int Position_ID { get; set; }
        public int Account_ID { get; set; }

    }
}
