using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Staff_Work
    {
        public int Staff_ID { get; set; }
        public int Work_ID { get; set; }

        public float Progress_Personal { get; set; }
    }
}
