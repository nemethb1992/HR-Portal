using System;
using System.Collections.Generic;
using System.IO;
using HR_Portal.Source.Model;

namespace HR_Portal.Control
{
    class ControlFile
    {
        Source.MySql mySql = new Source.MySql();

        public List<ModelJeloltFile> Applicant_FolderReadOut(int ApplicantID)
        {
            DirectoryInfo directory;
            List<ModelJeloltFile> list = new List<ModelJeloltFile>();
            FileInfo[] articles;

            try
            {
                directory = new DirectoryInfo(ROOTurl() + ApplicantID);
                articles = directory.GetFiles("*.pdf");
                foreach (FileInfo file in articles)
                {
                    list.Add(new ModelJeloltFile { fajlnev = file.Name.Split('.')[0], path = file.FullName });
                }
            }
            catch (Exception)
            {
            }
            return list;
        }
        public string ROOTurl()
        {
            return mySql.getRootUrl("SELECT * FROM ROOTurl");
        }
    }
}
