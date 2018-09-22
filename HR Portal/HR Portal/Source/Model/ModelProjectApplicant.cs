using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Portal.Source
{
    public class projekt_jelolt_kapcs
    {
        public int projekt_id { get; set; }
        public int jelolt_id { get; set; }
        public int hr_id { get; set; }
        public string datum { get; set; }
    }


    public class kompetencia_jelolt_kapcs_struct
    {
        public int id { get; set; }
        public int interju_id { get; set; }
        public int projekt_id { get; set; }
        public int jelolt_id { get; set; }
        public int hr_id { get; set; }
        public int k1_id { get; set; }
        public int k1_val { get; set; }
        public int k2_id { get; set; }
        public int k2_val { get; set; }
        public int k3_id { get; set; }
        public int k3_val { get; set; }
        public int k4_id { get; set; }
        public int k4_val { get; set; }
        public int k5_id { get; set; }
        public int k5_val { get; set; }
        public int tamogatom { get; set; }
    }
    
    public class kompetencia_summary_struct
    {
        public int k1_val { get; set; }
        public int k2_val { get; set; }
        public int k3_val { get; set; }
        public int k4_val { get; set; }
        public int k5_val { get; set; }
        public int tamogatom { get; set; }
    }

    public class kompetencia_tamogatas
    {
        public int tamogatom { get; set; }
    }
}
