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
using System.Threading.Tasks;

namespace HR_Portal.View.Usercontrol.Panels
{
    /// <summary>
    /// Interaction logic for Applicant_DataView.xaml
    /// </summary>
    public partial class ApplicantDataSheet : UserControl
    {
        Utilities Utility = new Utilities();
        Files fControl = new Files();

        private Applicant applicant;
        private Grid grid;
        

        public ApplicantDataSheet(Grid grid, Applicant applicant)
        {
            this.grid = grid;
            this.applicant = applicant;
            DataContext = applicant.data;
            InitializeComponent();
            formLoader();
        }

        protected void formLoader()
        {
            applicant_profile_title.Text = applicant.data.nev + (applicant.data.profession_type == 1 ? " - Profession" : "");
            header.Text = "Tisztelt "+ applicant.data.nev + "!";
            projekt_cbx.ItemsSource = new Utilities().Data_PorjectListSmall();
            csatolmany_listBox.ItemsSource = Files.ReadApplicantFiles(Session.ApplicantID);
            megjegyzes_listBox.ItemsSource = Utility.Data_CommentApplicant();
            interju_listBox.ItemsSource = new Interview().Data_Interview();
            kapcsolodo_projekt_list.ItemsSource = applicant.Data_ProjectList();
        }

        protected void navigateToProjectDataSheet(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            ModelSmallProject items = button.DataContext as ModelSmallProject;

            Session.ProjektID = items.id;
            Utilities.SetReturnPage(Utilities.Views.ApplicantDataSheet);
            Utilities.NavigateTo(grid, new ProjectDataSheet(grid, new Project(items.id)));
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
                applicant.AddToProject(item.id);
                kapcsolodo_projekt_list.ItemsSource = applicant.Data_ProjectList();
            }
        }

        protected void attachmentOpenClick(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            ModelFile item = btn.DataContext as ModelFile;
            Process.Start(item.path);
        }

        private void UploadClick(object sender, RoutedEventArgs e)
        {
            Files.Upload(Session.ApplicantID);
            csatolmany_listBox.ItemsSource = Files.ReadApplicantFiles(Session.ApplicantID);
        }

        private void BackButton(object sender, RoutedEventArgs e)
        {
            switch (Session.lastPage)
            {
                case Utilities.Views.ApplicantList:
                    Utilities.NavigateTo(grid, new ApplicantList(grid));
                    break;
                case Utilities.Views.ProjectJeloltDataSheet:
                    Utilities.NavigateTo(grid, new ProjektJeloltDataSheet(grid, new Project(0), applicant));
                    break;
                case Utilities.Views.FavoritePanel:
                    Utilities.NavigateTo(grid, new FavoritesPanel(grid));
                    break;
                default:
                    Utilities.NavigateTo(grid, new ApplicantList(grid));
                    break;
            }

        }


        private void interjuOpenClick(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            ModelInterview item = btn.DataContext as ModelInterview;
            Session.InterViewID = item.id;
            Session.ProjektID = item.projekt_id;
            Utilities.SetReturnPage(Utilities.Views.ApplicantDataSheet);
            Utilities.NavigateTo(grid, new InterviewPanel(grid,new Project(item.projekt_id), new Applicant(item.jelolt_id)));

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

        protected void CimkeSearchInput_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tbx = sender as TextBox;

            if (tbx.Text == tbx.Tag.ToString())
            {
                tbx.Text = "";
            }
        }

        protected void CimkeSearchInput_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox tbx = sender as TextBox;

            if (tbx.Text == "")
            {
                tbx.Text = tbx.Tag.ToString();
            }
        }

        private void Add_cimke_relation_btn_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            ModelCimkek item = btn.DataContext as ModelCimkek;
            new ModelCimkek().AddRelation(applicant.data.id, item.id);
            cimke_searched_list.ItemsSource = new ModelCimkek().GetAll();
            cimke_related_list.ItemsSource = new ModelCimkek().GetRelated(applicant.data.id);
        }

        private void Remove_cimke_relation_btn_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            ModelCimkek item = btn.DataContext as ModelCimkek;
            new ModelCimkek().DeleteRelation(applicant.data.id, item.id);
            cimke_related_list.ItemsSource = new ModelCimkek().GetRelated(applicant.data.id);
        }

        private async void Cimke_search_tbx_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textbox = (TextBox)sender;
            int fisrtLength = textbox.Text.Length;

            await Task.Delay(250);
            if (fisrtLength == textbox.Text.Length)
            {
                if(textbox.Text == textbox.Tag.ToString())
                {
                    cimke_searched_list.ItemsSource = new ModelCimkek().GetAll();
                    return;
                }
                List<ModelCimkek> list = new ModelCimkek().GetSearched(textbox.Text);
                cimke_searched_list.ItemsSource = list;
            }
        }

        private void CimkePanel_Close(object sender, RoutedEventArgs e)
        {
            var grid = (Grid)this.FindName("ui_bg");
            grid.Visibility = Visibility.Hidden;
            Cimke_Grid.Visibility = Visibility.Hidden;
        }

        private void CimkePanel_Open(object sender, RoutedEventArgs e)
        {
            var grid = (Grid)this.FindName("ui_bg");
            grid.Visibility = Visibility.Visible;
            Cimke_Grid.Visibility = Visibility.Visible;
            cimke_searched_list.ItemsSource = new ModelCimkek().GetAll();
            cimke_related_list.ItemsSource = new ModelCimkek().GetRelated(applicant.data.id);
        }
    }
}
