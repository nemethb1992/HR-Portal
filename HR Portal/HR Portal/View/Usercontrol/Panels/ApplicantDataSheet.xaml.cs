using HR_Portal.Source;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using HR_Portal.Source.Model;
using HR_Portal.Source.Model.Applicant;
using HR_Portal.Source.ViewModel;
using HR_Portal.Source.Model.Project;

namespace HR_Portal.View.Usercontrol.Panels
{
    /// <summary>
    /// Interaction logic for Applicant_DataView.xaml
    /// </summary>
    public partial class ApplicantDataSheet : UserControl
    {
        Applicant Applicant = new Applicant();
        CommonUtility Utility = new CommonUtility();
        Files fControl = new Files();

        private ProjectDataSheet projectDataSheet;
        private Grid grid;

        public ApplicantDataSheet(Grid grid)
        {
            this.grid = grid;
            InitializeComponent();
            formLoader();
        }

        protected void formLoader()
        {
            List<ModelFullApplicant> list = Applicant.GetFullApplicant();

            applicant_profile_title.Text = list[0].nev;
            app_input_1.Text = list[0].email;
            app_input_2.Text = list[0].telefon.ToString();
            app_input_3.Text = list[0].lakhely;
            app_input_5.Text = list[0].nyelvtudas.ToString();
            app_input_6.Text = list[0].nyelvtudas2.ToString();
            app_input_8.Text = list[0].munkakor;
            app_input_9.Text = list[0].ertesult.ToString();
            app_input_10.Text = list[0].szuldatum.ToString();
            projekt_cbx.ItemsSource = Applicant.Data_PorjectListSmall();
            csatolmany_listBox.ItemsSource = Files.Read(Session.ApplicantID);
            commentLoader(megjegyzes_listBox);
            kapcsolodo_projekt_list.ItemsSource = Applicant.Data_ProjectList();
        }

        protected void commentLoader(ListBox lb)
        {
            lb.ItemsSource = Utility.Data_Comment();
        }

        protected void navigateToProjectDataSheet(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            ModelSmallProject items = button.DataContext as ModelSmallProject;

            Session.ProjektID = items.id;
            grid.Children.Clear();
            grid.Children.Add(projectDataSheet = new ProjectDataSheet(grid));
        }

        protected void projectDelete(object sender, RoutedEventArgs e)
        {
            MenuItem delete = sender as MenuItem;
            ModelSmallProject items = delete.DataContext as ModelSmallProject;

            Applicant.DeleteProject(items.id);
            kapcsolodo_projekt_list.ItemsSource = Applicant.Data_ProjectList();
        }

        protected void commentDelete(object sender, RoutedEventArgs e)
        {
            MenuItem item = sender as MenuItem;
            ModelComment items = item.DataContext as ModelComment;

            Comment.Delete(items.id, Session.UserData[0].id, 0, Session.ApplicantID);
            commentLoader(megjegyzes_listBox);
        }

        protected void textBoxKeyUp(object sender, KeyEventArgs e)
        {
            TextBox textbox = sender as TextBox;

            if (e.Key != System.Windows.Input.Key.Enter) return;
            e.Handled = true;
            Comment.Add(comment_tartalom.Text, 0, Session.ApplicantID, 0);
            commentLoader(megjegyzes_listBox);
            textbox.Text = "";
        }

        protected void commentGotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tbx = sender as TextBox;

            if(tbx.Text == "Új megjegyzés")
            {
                tbx.Text = "";
            }
        }

        protected void commentLostFocus(object sender, RoutedEventArgs e)
        {
            TextBox tbx = sender as TextBox;

            if (tbx.Text == "")
            {
                tbx.Text = "Új megjegyzés";
            }
        }

        protected void projektClick(object sender, RoutedEventArgs e)
        {
            ComboBox cbx = projekt_cbx as ComboBox;
            ModelSmallProject item = cbx.SelectedItem as ModelSmallProject;
            if(item != null)
            {
                Applicant.AddToProject(Session.ApplicantID, item.id);
                kapcsolodo_projekt_list.ItemsSource = Applicant.Data_ProjectList();
            }
        }

        protected void attachmentOpenClick(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            ModelJeloltFile item = btn.DataContext as ModelJeloltFile;
            Process.Start(item.path);
        }

        private void UploadClick(object sender, RoutedEventArgs e)
        {
            Files.Upload(Session.ApplicantID);
            csatolmany_listBox.ItemsSource = Files.Read(Session.ApplicantID);
        }
    }
}
