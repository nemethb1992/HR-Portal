using HR_Portal.Source;
using HR_Portal.Source.Model.Applicant;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Portal.Source
{
    public class ModelApplicantSearch
    {
        public string nev { get; set; }
        public string lakhely { get; set; }
        public string email { get; set; }
        public int szuldatum { get; set; }
        public int tapasztalat { get; set; }
        public string regdate { get; set; }
        public int interju { get; set; }
        public int neme { get; set; }
        public string munkakor { get; set; }
        public string vegztettseg { get; set; }
    }

    public class ModelApplicantListbox
    {
        public int id { get; set; }
        public string nev { get; set; }
        public int interjuk_db { get; set; }
    }
}
