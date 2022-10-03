using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Announce
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Announce_ID { get; set; }
        public string Content { get; set; }
        public int Announce_Status { get; set; }

        public int Staff_ID { get; set; }
        public int Work_ID { get; set; }
    }
}
