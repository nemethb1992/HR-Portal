using HR_Portal.Source;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
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
    /// Interaction logic for projekt_jelolt_DataView.xaml
    /// </summary>
    /// 
    public partial class ProjektJeloltDataSheet : UserControl
    {

        Applicant Applicant = new Applicant();
        Utility Utility = new Utility();

        private ProjectDataSheet projectDataSheet;
        private ApplicantDataSheet applicantDataSheet;
        private InterviewPanel interviewPanel;
        private Grid grid;
        private Project project;

        List<ModelFullApplicant> applicantData;

        public ProjektJeloltDataSheet(Grid grid, Project project)
        {
                this.grid = grid;
            this.project = project;
                InitializeComponent();
            projectFormLoader();
        }

        protected void projectFormLoader()
        {
            applicantData = Applicant.GetFullApplicant();
            List<ModelFullProject> projectList = Project.GetFullProject();
            List<ModelKompetenciak> kompetenciaList = Interview.Data_Kompetencia();
            List<ModelKompetenciaSummary> summaryList = Utility.Data_KompetenciaJeloltKapcs();

            projekt_jelolt_title_tbl.Text = projectList[0].megnevezes_projekt + " - " + applicantData[0].nev;
            jelolt_telefon.Text = "( " + applicantData[0].telefon + " )";
            megjegyzes_listBox.ItemsSource = Utility.Data_CommentApplicant();
            kapcs_jeloltek_listBox.ItemsSource = new Interview().Data_Interview();
            inter_cim.Items.Add("HR interjú");
            inter_cim.Items.Add("Szakmai + HR");
            inter_cim.Items.Add("Szakmai Interjú 1.");
            inter_cim.Items.Add("Szakmai Interjú 2.");
            inter_cim.Items.Add("Szakmai Interjú 3.");
            inter_cim.Items.Add("Szakmai Interjú 4.");
            inter_cim.SelectedIndex = 0;
            jeloltTamogatasa();
            try
            {
                k_1.Value = summaryList[0].k1_val;
                k_2.Value = summaryList[0].k2_val;
                k_3.Value = summaryList[0].k3_val;
                k_4.Value = summaryList[0].k4_val;
                k_5.Value = summaryList[0].k5_val;
            }
            catch (Exception)
            {
            }
            foreach (var item in kompetenciaList)
            {
                if (item.id == projectList[0].kepesseg1)
                { kompetencia_1.Text = item.kompetencia_megnevezes; }
                if (item.id == projectList[0].kepesseg2)
                { kompetencia_2.Text = item.kompetencia_megnevezes; }
                if (item.id == projectList[0].kepesseg3)
                { kompetencia_3.Text = item.kompetencia_megnevezes; }
                if (item.id == projectList[0].kepesseg4)
                { kompetencia_4.Text = item.kompetencia_megnevezes; }
                if (item.id == projectList[0].kepesseg5)
                { kompetencia_5.Text = item.kompetencia_megnevezes; }
            }
            telephoneInspectedLayout();
        }

        protected void telephoneInspectedLayout()
        {
            if (Session.TelefonSzurt == 1)
            {
                telefonos_igen_btn.Visibility = Visibility.Hidden;
                szurt_tbl.Visibility = Visibility.Visible;
            }
        }

        protected void backToProjectDataSheet(object sender, RoutedEventArgs e)
        {
            grid.Children.Clear();
            grid.Children.Add(projectDataSheet = new ProjectDataSheet(grid,new Project(Session.ProjektID)));
        }

        protected void enterComment(object sender, KeyEventArgs e)
        {
            TextBox tbx = sender as TextBox;

            if (e.Key != Key.Enter) return;
            e.Handled = true;
            Comment.Add(comment_tartalom.Text, 0, Session.ApplicantID);
            megjegyzes_listBox.ItemsSource = Utility.Data_CommentApplicant();
            tbx.Text = "";
        }

        protected void commentGotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tbx = sender as TextBox;

            if (tbx.Text == "Új megjegyzés")
            {
                tbx.Text = "";
            }
        }

        protected void commentLostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textbox = sender as TextBox;

            if (textbox.Text == "")
            {
                textbox.Text = "Új megjegyzés";
            }
        }

        protected void commentDeleteClick(object sender, RoutedEventArgs e)
        {
            ModelComment items = (sender as MenuItem).DataContext as ModelComment;

            Comment.Delete(items.id);
            megjegyzes_listBox.ItemsSource = Utility.Data_CommentApplicant();
        }

        protected void telephonePanelOpenClick(object sender, RoutedEventArgs e)
        {
            ismerte_cbx.Items.Add("nem");
            ismerte_cbx.Items.Add("igen");
            ismerte_cbx.SelectedIndex = 0;
            grid_telefonosszuro.Height = 400;
            telefonos_igen_btn.IsEnabled = false;
            telefonos_nem_btn.IsEnabled = false;
        }

        protected void telephonePanelCloseClick(object sender, RoutedEventArgs e)
        {
            grid_telefonosszuro.Height = 100;
            ismerte_cbx.Items.Clear();
            muszakok_tbx.Text = "";
            utazas_tbx.Text = "";
            telefonos_igen_btn.IsEnabled = true;
            telefonos_nem_btn.IsEnabled = true;
        }

        protected void telephoneAcceptClick(object sender, RoutedEventArgs e)
        {
            int ismerte = 0;

            if(ismerte_cbx.SelectedItem.ToString() == "igen")
            {
                ismerte = 1;
            }
            Interview.telephoneFilterInsert(ismerte,Convert.ToInt32(muszakok_tbx.Text),utazas_tbx.Text);
            Session.TelefonSzurt = 1;
            grid_telefonosszuro.Height = 100;
            telefonos_igen_btn.IsEnabled = true;
            telefonos_nem_btn.IsEnabled = true;
            telephoneInspectedLayout();
        }

        protected void telephoneDeclineClick(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Biztosan elutasítja? \n", "HR Cloud", MessageBoxButton.YesNoCancel);

            switch (result)
            {
                case MessageBoxResult.Yes:
                    Project project = new Project(0);
                    project.jeloltKapcsUpdate(Session.ApplicantID, 3);
                    grid.Children.Clear();
                    grid.Children.Add(projectDataSheet = new ProjectDataSheet(grid, project));
                    EmailTemplate email = new EmailTemplate();
                    new Email().Send(applicantData[0].email, email.Elutasito_Email(applicantData[0].nev));
                    break;
                case MessageBoxResult.No:
                    break;
                case MessageBoxResult.Cancel:
                    break;
            }
        }

        protected void interviewPanelOpen()
        {
            var grid = (Grid)this.FindName("ui_bg");
            var grid2 = (Grid)this.FindName("uj_interju_panel");

            grid.Visibility = Visibility.Visible;
            grid2.Visibility = Visibility.Visible;
        }

        protected void interviewPanelClose()
        {
            var grid = (Grid)this.FindName("ui_bg");
            var grid2 = (Grid)this.FindName("uj_interju_panel");

            grid.Visibility = Visibility.Hidden;
            grid2.Visibility = Visibility.Hidden;
        }

        protected void Uj_Interju_felvetele_Click(object sender, RoutedEventArgs e)
        {
            interviewPanelOpen();
        }

        protected void uj_interju_megsem_btn_Click(object sender, RoutedEventArgs e)
        {
            interviewPanelClose();
        }

        protected void ui_bg_MouseDown(object sender, MouseButtonEventArgs e)
        {
            interviewPanelClose();
        }

        protected void uj_interju_mentes_btn_Click(object sender, RoutedEventArgs e)
        {
            string datum = "";
            string[] seged = inter_date.SelectedDate.ToString().Split(' ');
            try
            {
                datum = seged[0] + seged[1] + seged[2];
            }
            catch{ }
            try
            {
                new Interview().addInterview(datum, inter_cim.SelectedItem.ToString(), inter_leiras.Text, inter_helyszin.Text, inter_idopont.Text);
                projectFormLoader();
                interviewPanelClose();
            }
            catch (Exception)
            {
                MessageBox.Show("Nincs kitöltve minden mező!");
            }
        }

        protected void navigateToInterviewPanel(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            ModelInterview items = btn.DataContext as ModelInterview;

            Session.InterViewID = items.id;
            grid.Children.Clear();
            grid.Children.Add(interviewPanel = new InterviewPanel(grid));
        }

        protected void deleteInterview(object sender, RoutedEventArgs e)
        {
            MenuItem menu = sender as MenuItem;
            ModelInterview items = menu.DataContext as ModelInterview;

            new Interview().interviewDelete(items.id);
            projectFormLoader();
        }

        protected void numericTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        protected void jeloltTamogatasa()
        {
            List<ModelTamogatas> list = new List<ModelTamogatas>();

            list = Utility.Data_KompetenciaTamogatas();
            int igen = 0, ossz = 0 ;
            foreach (var item in list)
            {
                ossz++;
                if (item.tamogatom == 1)
                {
                    igen++;
                }

            }
            tamogatasok_input_tbx.Text = igen.ToString() + "/" + ossz.ToString();
        }

        private void ApplicantDataSheetNavigation(object sender, RoutedEventArgs e)
        {
            Utility.SetReturnPage(Utility.Views.ProjectJeloltDataSheet);
            grid.Children.Clear();
            grid.Children.Add(applicantDataSheet = new ApplicantDataSheet(grid));
        }
    }

}
