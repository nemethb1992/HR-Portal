using HR_Portal.Source.Model.Applicant;
using System;
using System.Collections.Generic;
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

        public void Delete(int ApplicantID)
        {
            string command = "DELETE FROM profession_jeloltek WHERE id="+ApplicantID;
            MySql.Execute(command);
        }

    }
}
