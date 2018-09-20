using HR_Portal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Portal.Model
{

    public class JeloltListItems
    {
        public int id { get; set; }
        public string nev { get; set; }
        public string munkakor { get; set; }
        public string munkakor2 { get; set; }
        public string munkakor3 { get; set; }
        public int szuldatum { get; set; }
        public string email { get; set; }
        public int interjuk_db { get; set; }
        public int allapota { get; set; }
        public string kolcsonzott { get; set; }
        public string allapot_megnevezes { get; set; }
        public string reg_datum { get; set; }
        public bool Checked { get; set; }
    }

    public class JeloltListBox
    {
        public int id { get; set; }
        public string nev { get; set; }
        public int interjuk_db { get; set; }
    }

    public class SubJelolt
    {
        public int id { get; set; }
        public string nev { get; set; }
    }

    public class JeloltExtendedList
    {
        public int id { get; set; }
        public string nev { get; set; }
        public string email { get; set; }
        public string telefon { get; set; }
        public string lakhely { get; set; }
        public string ertesult { get; set; }
        public int id_ertesult { get; set; }
        public int szuldatum { get; set; }
        public string neme { get; set; }
        public int id_neme { get; set; }
        public int tapasztalat_ev { get; set; }
        public string munkakor { get; set; }
        public string munkakor2 { get; set; }
        public string munkakor3 { get; set; }
        public int id_munkakor { get; set; }
        public int id_munkakor2 { get; set; }
        public int id_munkakor3 { get; set; }
        public string vegz_terulet { get; set; }
        public int id_vegz_terulet { get; set; }
        public string nyelvtudas { get; set; }
        public string nyelvtudas2 { get; set; }
        public int id_nyelvtudas { get; set; }
        public int id_nyelvtudas2 { get; set; }
        public string reg_date { get; set; }
        public string megjegyzes { get; set; }
        public string folderUrl { get; set; }
    }

    public class JeloltSearchItems
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

}
