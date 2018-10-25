using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using HR_Portal.Source.Model;

namespace HR_Portal.Source
{
    class Files
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

        public static void Upload(int to)
        {
            Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog();

            ofd.Filter = @"All Files|**.docx;*.doc;*.pdf;|Word File (.docx ,.doc)|*.docx;*.doc|PDF (.pdf)|*.pdf";

            ofd.Multiselect = true;

            ofd.ShowDialog();

            string[] FilePaths = ofd.FileNames;

            string[] FileNames = ofd.SafeFileNames;

            string newPath = ROOTurl() + "\\" + to + "\\";

            List<byte[]> fileInByteList = new List<byte[]>();
            
            foreach (var item in FilePaths)
            {
                byte[] content = File.ReadAllBytes(item);
                fileInByteList.Add(content);
            }

            for (int i = 0; i < fileInByteList.Count; i++)
            {
                Directory.CreateDirectory(newPath);
                File.WriteAllBytes(newPath + FileNames[i], fileInByteList[i]);
            }
        }
    }
}
