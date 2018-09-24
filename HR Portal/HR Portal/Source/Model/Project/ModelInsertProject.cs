using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Portal.Source.Model.Project
{
    public class ModelInsertProject
    {
        public int id { get; set; }
        public int hr_id { get; set; }
        public string megnevezes_projekt { get; set; }
        public int pc { get; set; }
        public int vegzettseg { get; set; }
        public int tapasztalat_ev { get; set; }
        public int statusz { get; set; }
        public string fel_datum { get; set; }
        public string le_datum { get; set; }
        public int nyelvtudas { get; set; }
        public int munkakor { get; set; }
        public int szuldatum { get; set; }
        public int ber { get; set; }
        public int kepesseg1 { get; set; }
        public int kepesseg2 { get; set; }
        public int kepesseg3 { get; set; }
        public int kepesseg4 { get; set; }
        public int kepesseg5 { get; set; }
        public string feladatok { get; set; }
        public string elvarasok { get; set; }
        public string kinalunk { get; set; }
    }
}
