using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using HR_Portal.Source.Model;

namespace HR_Portal.Source
{
    class File
    {
        public static List<ModelJeloltFile> Read(int ApplicantID)
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
        public static string ROOTurl()
        {
            return MySql.GetRootUrl("SELECT * FROM ROOTurl");
        }
        //public static string upload()
        //{
        //    byte[] array;
        //    string filename;

        //    using (OpenFileDialog openFileDialog1 = new OpenFileDialog())
        //    {
        //        if (openFileDialog1.ShowDialog() != DialogResult.OK)
        //            return;
        //        filename = openFileDialog1.FileName;
        //        array = File.ReadAllBytes(filename);
        //    }
        //}
    }
}
