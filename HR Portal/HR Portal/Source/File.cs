using System;
using System.Collections.Generic;
using System.IO;
using HR_Portal.Source.Model;

namespace HR_Portal.Source
{
    class File
    {
        public List<ModelJeloltFile> Read(int ApplicantID)
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
            return MySql.GetRootUrl("SELECT * FROM ROOTurl");
        }
    }
}
