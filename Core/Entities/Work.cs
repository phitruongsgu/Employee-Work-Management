using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Work
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Work_ID { get; set; }
        public string Title { get; set; }
        public string Start_Date { get; set; }
        public string End_Date { get; set; }
        public string Range { get; set; }
        public string FileName { get; set; }
        public int staffCreate_ID { get; set; }

        public int Status_ID { get; set; }

    }
}
