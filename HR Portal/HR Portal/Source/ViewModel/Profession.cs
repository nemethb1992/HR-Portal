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
        
        public int Fullify(ModelProfession prof, ModelFullApplicant applicant = null)
        {
            if(applicant == null)
            {
                string command = "INSERT INTO jeloltek (nev,email,telefon,reg_date) VALUES ('" + prof.name + "','" + prof.email + "','" + prof.telephone + "','" + prof.reg_date + "')";
                MySql.Execute(command);
            }
            else
            {
                string command = "INSERT INTO jeloltek (nev,email,telefon,reg_date) VALUES ('" + prof.name + "','" + prof.email + "','" + prof.telephone + "','" + prof.reg_date + "')";
                MySql.Execute(command);
            }
            ModelFullApplicant udata = new ApplicantImplementation().GetFullApplicantByEmail(prof.email);
            DirectoryInfo profession = new DirectoryInfo(Files.GetProfessionUrl() + prof.id);
            DirectoryInfo newID = new DirectoryInfo(Files.GetApplicantUrl() + udata.id);
            Files.CopyAll(profession,newID);
            return (udata != null ? udata.id : 0);
        }

        public void Delete(int ApplicantID)
        {
            string command = "DELETE FROM profession_jeloltek WHERE id="+ApplicantID;
            MySql.Execute(command);
        }

    }
}
