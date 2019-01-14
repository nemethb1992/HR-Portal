using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using HR_Portal.Source.Model;

namespace HR_Portal.Source
{
    class Files
    { 

        public static List<ModelJeloltFile> Read(int ApplicantID)
        {
            List<ModelJeloltFile> list = new List<ModelJeloltFile>();

            try
            {
                FileInfo[] articles = new DirectoryInfo(GetApplicantUrl() + ApplicantID).GetFiles();
                foreach (FileInfo file in articles)
                {
                    list.Add(new ModelJeloltFile { fajlnev = file.Name, color = (file.Extension == ".pdf" ? "#e3202a" : "#2a579a"), path = file.FullName });
                }
            }
            catch (Exception)
            {
            }
            return list;
        }

        public static List<ModelJeloltFile> ReadProfession(int ApplicantID)
        {
            List<ModelJeloltFile> list = new List<ModelJeloltFile>();

            try
            {
                FileInfo[] articles = new DirectoryInfo(GetApplicantUrl()+ "ProfessionDocuments\\" + ApplicantID).GetFiles();
                foreach (FileInfo file in articles)
                {
                    list.Add(new ModelJeloltFile { fajlnev = file.Name, color = "White", path = file.FullName });
                }
            }
            catch (Exception)
            {
            }
            return list;
        }

        public static string GetApplicantUrl()
        {
            return MySql.GetRootUrl("SELECT url FROM ROOTurl WHERE id=0");
        }

        public static string GetProfessionUrl()
        {
            return MySql.GetRootUrl("SELECT url FROM ROOTurl WHERE id=1");
        }

        public void DeleteFolder(int ApplicantID)
        {
            try
            {

                Directory.Delete(GetApplicantUrl() + ApplicantID, true);
            }
            catch
            {

            }
        }

        public void DeleteProfessionFolder(int ApplicantID)
        {
            Directory.Delete(GetProfessionUrl()+ ApplicantID, true);
        }

        public static void CopyAll(DirectoryInfo source, DirectoryInfo target)
        {
            Directory.CreateDirectory(target.FullName);
            
            foreach (FileInfo fi in source.GetFiles())
            {
                Console.WriteLine(@"Copying {0}\{1}", target.FullName, fi.Name);
                fi.CopyTo(Path.Combine(target.FullName, fi.Name), true);
            }
            
            foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
            {
                DirectoryInfo nextTargetSubDir =
                    target.CreateSubdirectory(diSourceSubDir.Name);
                CopyAll(diSourceSubDir, nextTargetSubDir);
            }
        }


        public static void Upload(int to)
        {
            Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog();

            ofd.Filter = @"All Files|**.docx;*.doc;*.pdf;|Word File (.docx ,.doc)|*.docx;*.doc|PDF (.pdf)|*.pdf";

            ofd.Multiselect = true;

            ofd.ShowDialog();

            string[] FilePaths = ofd.FileNames;

            string[] FileNames = ofd.SafeFileNames;

            string newPath = GetApplicantUrl() + "\\" + to + "\\";

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
