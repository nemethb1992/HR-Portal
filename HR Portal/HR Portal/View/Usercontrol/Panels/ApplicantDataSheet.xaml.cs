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
using HR_Portal.Public.templates;
namespace HR_Portal.View.Usercontrol.Panels
{
    /// <summary>
    /// Interaction logic for Applicant_DataView.xaml
    /// </summary>
    public partial class ApplicantDataSheet : UserControl
    {
        Utility Utility = new Utility();
        Files fControl = new Files();

        private Applicant applicant;
        private Grid grid;
        

        public ApplicantDataSheet(Grid grid, Applicant applicant)
        {
            this.grid = grid;
            this.applicant = applicant;
            InitializeComponent();
            formLoader();
        }

        protected void formLoader()
        {

            applicant_profile_title.Text = applicant.data.nev;
            header.Text = "Tisztelt "+ applicant.data.nev + "!";
            app_input_1.Text = applicant.data.email;
            app_input_2.Text = applicant.data.telefon.ToString();
            app_input_3.Text = applicant.data.lakhely;
            app_input_5.Text = applicant.data.nyelvtudas.ToString();
            app_input_6.Text = applicant.data.nyelvtudas2.ToString();
            app_input_8.Text = applicant.data.munkakor;
            app_input_9.Text = applicant.data.ertesult.ToString();
            app_input_10.Text = applicant.data.szuldatum.ToString();
            projekt_cbx.ItemsSource = applicant.Data_PorjectListSmall();
            csatolmany_listBox.ItemsSource = Files.Read(Session.ApplicantID);
            megjegyzes_listBox.ItemsSource = Utility.Data_CommentApplicant();
            interju_listBox.ItemsSource = new Interview().Data_Interview();
            kapcsolodo_projekt_list.ItemsSource = applicant.Data_ProjectList();
        }

        protected void navigateToProjectDataSheet(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            ModelSmallProject items = button.DataContext as ModelSmallProject;

            Session.ProjektID = items.id;
            Utility.SetReturnPage(Utility.Views.ApplicantDataSheet);
            Utility.NavigateTo(grid, new ProjectDataSheet(grid, new Project(items.id)));
        }

        protected void projectDelete(object sender, RoutedEventArgs e)
        {
            MenuItem delete = sender as MenuItem;
            ModelSmallProject items = delete.DataContext as ModelSmallProject;

            applicant.DeleteProject(items.id);
            kapcsolodo_projekt_list.ItemsSource = applicant.Data_ProjectList();
        }

        protected void commentDeleteClick(object sender, RoutedEventArgs e)
        {
            MenuItem item = sender as MenuItem;
            ModelComment items = item.DataContext as ModelComment;

            Comment.Delete(items.id);
            megjegyzes_listBox.ItemsSource = Utility.Data_CommentApplicant();
        }

        protected void enterComment(object sender, KeyEventArgs e)
        {
            TextBox textbox = sender as TextBox;

            if (e.Key != System.Windows.Input.Key.Enter) return;
            e.Handled = true;
            Comment.Add(comment_tartalom.Text, 0, Session.ApplicantID);
            megjegyzes_listBox.ItemsSource = Utility.Data_CommentApplicant();
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
                applicant.AddToProject(Session.ApplicantID, item.id);
                kapcsolodo_projekt_list.ItemsSource = applicant.Data_ProjectList();
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

        private void BackButton(object sender, RoutedEventArgs e)
        {
            if (Session.lastPage == Utility.Views.ProjectJeloltDataSheet)
            {
                Utility.NavigateTo(grid, new ProjektJeloltDataSheet(grid, new Project(0), applicant));
            }
            else
            {
                Utility.NavigateTo(grid, new ApplicantList(grid));
            }
        }

        private void megjegyzes_listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void interjuOpenClick(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            ModelInterview item = btn.DataContext as ModelInterview;
            Session.InterViewID = item.id;
            Session.ProjektID = item.projekt_id;
            Utility.SetReturnPage(Utility.Views.ApplicantDataSheet);
            Utility.NavigateTo(grid, new InterviewPanel(grid,new Project(item.projekt_id), new Applicant(item.jelolt_id)));

        }

        private void SendCustomMail(object sender, RoutedEventArgs e)
        {
            new Email().Send(applicant.data.email,new EmailTemplate().Egyedi_Email(email_content.Text, applicant.data.nev));
            email_content.Text = "";
            mailPanelClose();
        }

        protected void send_mail_megsem(object sender, RoutedEventArgs e)
        {
            mailPanelClose();
        }

        protected void mailPanelOpenClick(object sender, RoutedEventArgs e)
        {
            mailPanelOpen();
        }

        protected void ui_bg_MouseDown(object sender, MouseButtonEventArgs e)
        {
            mailPanelClose();
        }

        protected void mailPanelOpen()
        {
            var grid = (Grid)this.FindName("ui_bg");
            var grid2 = (Grid)this.FindName("send_mail");

            grid.Visibility = Visibility.Visible;
            grid2.Visibility = Visibility.Visible;
        }

        protected void mailPanelClose()
        {
            var grid = (Grid)this.FindName("ui_bg");
            var grid2 = (Grid)this.FindName("send_mail");

            grid.Visibility = Visibility.Hidden;
            grid2.Visibility = Visibility.Hidden;
        }
    }
}
