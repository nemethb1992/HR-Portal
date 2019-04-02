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

        public static List<ModelFile> ReadApplicantFiles(int ApplicantID)
        {
            List<ModelFile> list = new List<ModelFile>();

            try
            {
                FileInfo[] articles = new DirectoryInfo(GetApplicantUrl() + ApplicantID).GetFiles();
                foreach (FileInfo file in articles)
                {
                    list.Add(new ModelFile { fajlnev = file.Name, color = (file.Extension == ".pdf" ? "#e3202a" : "#2a579a"), path = file.FullName });
                }
            }
            catch (Exception)
            {
            }
            return list;
        }

        public static List<ModelFile> ReadStatistics(string type)
        {
            List<ModelFile> list = new List<ModelFile>();

            try
            {
                FileInfo[] articles = new DirectoryInfo(GetStatisticsUrl()+ "Systematic\\"+type).GetFiles();
                foreach (FileInfo file in articles)
                {
                    list.Add(new ModelFile { fajlnev = file.Name, color = "White", path = file.FullName });
                }
            }
            catch (Exception)
            {
            }
            return list;
        }

        public static string GetApplicantUrl()
        {
            MySql mySql = new MySql();
            string data = mySql.GetRootUrl("SELECT url FROM ROOTurl WHERE id=0");
            mySql.Close();
            return data;
        }

        public static string GetStatisticsUrl()
        {
            MySql mySql = new MySql();
            string data = mySql.GetRootUrl("SELECT url FROM ROOTurl WHERE id=1");
            mySql.Close();
            return data;   
        }

        public static void DeleteFolder(int ApplicantID)
        {
            try
            {
                Directory.Delete(GetApplicantUrl() + ApplicantID, true);
            }
            catch
            {

            }
        }

        public static void DeleteProfessionFolder(int ApplicantID)
        {
            try
            {
                Directory.Delete(GetStatisticsUrl()+ ApplicantID, true);
            }
            catch
            {

            }
        }

        public static void CopyAll(DirectoryInfo source, DirectoryInfo target)
        {
            Directory.CreateDirectory(target.FullName);

            try
            {
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
            catch
            {

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
