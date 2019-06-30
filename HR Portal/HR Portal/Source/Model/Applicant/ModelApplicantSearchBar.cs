using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Portal.Source.Model.Applicant
{
    public class ModelApplicantSearchBar
    {
        public string nev { get; set; }
        public string lakhely { get; set; }
        public string email { get; set; }
        public string eletkor { get; set; }
        public string tapasztalat { get; set; }
        public string regdate { get; set; }
        public string interjuk { get; set; }
        public string nemekStr { get; set; }
        public int nemekIndex { get; set; }
        public int bekuldo { get; set; }
        public string munkakorStr { get; set; }
        public int munkakorIndex { get; set; }
        public string vegzettsegStr { get; set; }
        public int vegzettsegIndex { get; set; }
        public string cimke { get; set; }
        public string szabad { get; set; }
        public bool szabadBool { get; set; }
        public bool allasbanBool { get; set; }
        public string HeaderSelected { get; set; }
        public string sorrend { get; set; }
        public double numberLimit { get; set; }
    }
}
