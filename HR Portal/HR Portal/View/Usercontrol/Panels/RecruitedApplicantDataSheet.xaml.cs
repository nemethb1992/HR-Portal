using HR_Portal.Source;
using HR_Portal.Source.Model;
using HR_Portal.Source.Model.Project;
using HR_Portal.Source.ViewModel;
using HR_Portal.View.Usercontrol.Panels;
using HR_Portal_Test.Source.Model.Applicant;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HR_Portal_Test.View.Usercontrol.Panels
{
    /// <summary>
    /// Interaction logic for RecruitedApplicantDataSheet.xaml
    /// </summary>
    public partial class RecruitedApplicantDataSheet : UserControl
    {
        private Grid grid;

        ModelFreelancerApplicant freelancerApplicant;
        Applicant applicant;
        public RecruitedApplicantDataSheet(Grid grid, ModelFreelancerApplicant freelancerApplicant)
        {
            this.freelancerApplicant = freelancerApplicant;
            this.applicant = new Applicant(freelancerApplicant.id);
            this.grid = grid;
            InitializeComponent();
            DataContext = freelancerApplicant;
            FormLoader();
        }
        protected void FormLoader()
        {
            csatolmany_listBox.ItemsSource = Files.ReadApplicantFiles(freelancerApplicant.id);
            kapcsolodo_projekt_list.ItemsSource = applicant.Data_RecruitedProjectList();
            projekt_cbx.ItemsSource = new Utilities().Data_PorjectListSmall();
        }
        private void UploadClick(object sender, RoutedEventArgs e)
        {
            Files.Upload(freelancerApplicant.id);
            csatolmany_listBox.ItemsSource = Files.ReadApplicantFiles(freelancerApplicant.id);
        }
        protected void attachmentOpenClick(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            ModelFile item = btn.DataContext as ModelFile;
            Process.Start(item.path);
        }
        private void BackButton(object sender, RoutedEventArgs e)
        {
             Utilities.NavigateTo(grid, new RecruitedList(grid));
        }
        protected void AddProjektClick(object sender, RoutedEventArgs e)
        {
            ComboBox cbx = projekt_cbx as ComboBox;
            ModelSmallProject item = cbx.SelectedItem as ModelSmallProject;
            if (item != null)
            {
                applicant.AddRecruitedToProject(item.id);
                kapcsolodo_projekt_list.ItemsSource = applicant.Data_ProjectList();
            }
        }
        protected void projectDelete(object sender, RoutedEventArgs e)
        {
            MenuItem delete = sender as MenuItem;
            ModelSmallProject items = delete.DataContext as ModelSmallProject;

            applicant.DeleteRecruitedProjectConnection(items.id, freelancerApplicant.id);
            kapcsolodo_projekt_list.ItemsSource = applicant.Data_ProjectList();
        }
    }
}
