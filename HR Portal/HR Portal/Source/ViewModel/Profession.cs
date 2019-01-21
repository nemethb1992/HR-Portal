using HR_Portal.Source.Model;
using HR_Portal.Source.Model.Applicant;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Portal.Source.ViewModel
{
    class Profession
    {
        public List<ModelProfession> GetAll()
        {
            string command = "SELECT * FROM profession_jeloltek";
            return new ModelProfession().Get(command); 
        }
        
        public int Fullify(ModelProfession prof)
        {
            MySql mySql = new MySql();
            string command = "INSERT INTO jeloltek (nev,email,telefon,reg_date" +
                (!prof.szuldatum.Equals("") ? ",szuldatum" : "") +
                (!prof.lakhely.Equals("") ? ",lakhely" : "") +
                (!prof.neme.Equals(9999) ? ",neme": "") +
                (!prof.vegzettseg.Equals(9999) ? ",vegz_terulet" : "") +
                (!prof.nyelvtudas.Equals(9999) ? ",nyelvtudas" : "") +
                (!prof.ertesult.Equals(9999) ? ",ertesult" : "") 
                +") VALUES ('" + prof.name + "','" + prof.email + "','" + prof.telephone + "','" + prof.reg_date + "'" +
                (!prof.szuldatum.Equals("") ? ",'" + prof.szuldatum.ToString()+"'" : "") +
                (!prof.lakhely.Equals("") ? ",'" + prof.lakhely.ToString()+"'" : "") +
                (!prof.neme.Equals(9999) ? ","+prof.neme.ToString() : "") +
                (!prof.vegzettseg.Equals(9999) ? "," + prof.vegzettseg.ToString() : "") +
                (!prof.nyelvtudas.Equals(9999) ? "," + prof.nyelvtudas.ToString() : "") +
                (!prof.ertesult.Equals(9999) ? "," + prof.ertesult.ToString() : "")
                +")";
            mySql.Execute(command);
            mySql.Close();
            ModelFullApplicant udata = new Applicant().GetFullApplicantByEmail(prof.email);
            DirectoryInfo profession = new DirectoryInfo(Files.GetProfessionUrl() + prof.id);
            DirectoryInfo newID = new DirectoryInfo(Files.GetApplicantUrl() + udata.id);
            Delete(prof.id);
            Files.CopyAll(profession, newID);
            return (udata != null ? udata.id : 0);
        }

        public void Delete(int ApplicantID)
        {
            MySql mySql = new MySql();
            string command = "DELETE FROM profession_jeloltek WHERE id="+ApplicantID;
            mySql.Execute(command);
            mySql.Close();
        }

    }
}
