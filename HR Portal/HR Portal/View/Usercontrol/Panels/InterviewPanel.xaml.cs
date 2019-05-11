using HR_Portal.Source;
using HR_Portal.Public.templates;
using HR_Portal.View.Usercontrol.Panels.SzakmaiLayouts;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using HR_Portal.Source.Model;
using HR_Portal.Source.Model.Project;
using HR_Portal.Source.ViewModel;
using Microsoft.Exchange.WebServices.Data;
using HR_Portal.Controller;
using System.Windows.Media;

namespace HR_Portal.View.Usercontrol.Panels
{
    /// <summary>
    /// Interaction logic for interju_panel.xaml
    /// </summary>
    public partial class InterviewPanel : UserControl
    {

        private Grid grid;
        private Project project;
        private Applicant applicant;
        private ModelInterview interview;

        public InterviewPanel(Grid grid, Project project, Applicant applicant)
        {
            this.grid = grid;
            this.project = project;
            this.applicant = applicant;
            this.interview = Interview.Data_InterviewById()[0];
            InitializeComponent();

            interviewLoader();
            SendButtonStatus(interview.sent);
            if (Session.UserData.kategoria < 1)
            {
                addPerson.Visibility = Visibility.Hidden;
                invitePerson.Visibility = Visibility.Hidden;
            }
        }

        protected void navigateBackFromInterview(object sender, RoutedEventArgs e)
        {
            if(Session.UserData.kategoria >= 1)
            {
                switch (Session.lastPage)
                {
                    case Utilities.Views.ApplicantDataSheet:
                        Utilities.NavigateTo(grid, new ApplicantDataSheet(grid, applicant));
                        break;
                    case Utilities.Views.ProjectJeloltDataSheet:
                        Utilities.NavigateTo(grid, new ProjektJeloltDataSheet(grid, project, applicant));
                        break;
                    case Utilities.Views.FavoritePanel:
                        Utilities.NavigateTo(grid, new FavoritesPanel(grid));
                        break;
                    default:
                        Utilities.NavigateTo(grid, new ProjektJeloltDataSheet(grid, project, applicant));
                        break;
                }
            }
            else
            {
                Utilities.NavigateTo(grid, new SzakmaiInterviewList(grid));
            }
        }

        protected void interviewLoader()
        {
            ModelFullProject li = project.data;
            List<ModelKompetenciak> li_k = Interview.Data_Kompetencia();

            foreach (var item in li_k)
            {
                if (item.id == li.kepesseg1)
                { kompetencia1.Text = item.kompetencia_megnevezes; }
                if (item.id == li.kepesseg2)
                { kompetencia2.Text = item.kompetencia_megnevezes; }
                if (item.id == li.kepesseg3)
                { kompetencia3.Text = item.kompetencia_megnevezes; }
                if (item.id == li.kepesseg4)
                { kompetencia4.Text = item.kompetencia_megnevezes; }
                if (item.id == li.kepesseg5)
                { kompetencia5.Text = item.kompetencia_megnevezes; }
            }

            interju_jelolt_tbl.Text = interview.jelolt_megnevezes;
            interju_projekt_tbl.Text = interview.projekt_megnevezes;
            interju_cim_tbl.Text = interview.interju_cim;
            interju_helye_tbl.Text = interview.helyszin;
            interju_idopont_tbl.Text = interview.date_start +"   "+ interview.time_start +" - "+ interview.time_end;
            interju_liras_tbl.Text = interview.interju_leiras;

            choose_editlist.ItemsSource = Interview.Data_ProjektErtesitendokKapcsolt();
            ertesitendok_editlist.ItemsSource = Interview.Data_InterjuErtesitendokKapcsolt();

            if (Interview.HasTest())
            {
                Panel.SetZIndex(kompetencia_border, 1);
                locked_title.Visibility = Visibility.Visible;
                //teszt_nyitas_btn.Visibility = Visibility.Visible;   //teszt megtekintéshez
            }
        }

        protected void kompetencia_send_btn(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            int type = Convert.ToInt32(button.Tag);
            List<ModelFullProject> li = Project.GetFullProject();
            List<int> list = new List<int>();

            list.Add(li[0].kepesseg1);
            list.Add(Convert.ToInt32(k1_slider.Value));
            list.Add(li[0].kepesseg2);
            list.Add(Convert.ToInt32(k2_slider.Value));
            list.Add(li[0].kepesseg3);
            list.Add(Convert.ToInt32(k3_slider.Value));
            list.Add(li[0].kepesseg4);
            list.Add(Convert.ToInt32(k4_slider.Value));
            list.Add(li[0].kepesseg5);
            list.Add(Convert.ToInt32(k5_slider.Value));
            list.Add(type);

            Interview.UpdateTest(list);
            Panel.SetZIndex(kompetencia_border,1);
            locked_title.Visibility = Visibility.Visible;
            //teszt_nyitas_btn.Visibility = Visibility.Visible;  //teszt megtekintéshez


        }

        protected void openColleaguePanelClick(object sender, RoutedEventArgs e)
        {
            resztvevo_felvetel_list.Visibility = Visibility.Visible;
            Blur_Grid.Visibility = Visibility.Visible;
            choose_editlist.ItemsSource = Interview.Data_ProjektErtesitendokKapcsolt();
        }
        
        protected void addColleagueToInterview(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            ModelErtesitendok items = btn.DataContext as ModelErtesitendok;

            Interview.Insert(items.id);
            choose_editlist.ItemsSource = Interview.Data_ProjektErtesitendokKapcsolt();
            ertesitendok_editlist.ItemsSource = Interview.Data_InterjuErtesitendokKapcsolt();
        }

        protected void removeColleague(object sender, RoutedEventArgs e)
        {
            if(Session.UserData.kategoria >= 1)
            {
                MenuItem menu = sender as MenuItem;
                ModelErtesitendok items = menu.DataContext as ModelErtesitendok;

                Interview.DeleteInvited(items.id);
                choose_editlist.ItemsSource = Interview.Data_ProjektErtesitendokKapcsolt();
                ertesitendok_editlist.ItemsSource = Interview.Data_InterjuErtesitendokKapcsolt();
            }
        }

        protected void closeColleaguePanelClick(object sender, RoutedEventArgs e)
        {
            resztvevo_felvetel_list.Visibility = Visibility.Hidden;
            Blur_Grid.Visibility = Visibility.Hidden;
        }

        protected void resztvevoSaveClick(object sender, RoutedEventArgs e)
        {
            ertesitendok_editlist.ItemsSource = Interview.Data_InterjuErtesitendokKapcsolt();
        }

        protected void SendInvitations(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Biztosan elküldi a meghívókat? \n\n", "HR Cloud", MessageBoxButton.YesNo);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    EmailTemplate et = new EmailTemplate();
                    Email email = new Email();
                    List<ModelErtesitendok> szemelyek = Interview.Data_InterjuErtesitendokKapcsolt();
                    List<string> resztvevok = new List<string>();

                    foreach (var item in szemelyek)
                    {
                        resztvevok.Add(item.name);
                    }
                    //foreach (var item in szemelyek)
                    //{
                    //    new Email().Send(item.email, et.Belsos_Meghivo_Email(item.name, interview.projekt_megnevezes, interview.date_start + " - " + interview.time_start, interview.helyszin, interview.jelolt_megnevezes));
                    //}
                    new Email().Send(interview.jelolt_email, et.Jelolt_Meghivo_Email(interview.jelolt_megnevezes, interview.projekt_megnevezes, interview.date_start + " - " + interview.time_start, resztvevok));

                    new Appointments().CreateMeeting(new AppointmentModel().TransformData(interview));
                    new Interview().madeSent(interview.id);

                    break;
                case MessageBoxResult.No:
                    break;
            }
        }
        private void SendButtonStatus(int id)
        {
            var bc = new BrushConverter();
            switch (id)
            {
                case 0:
                    invitePerson.Content = "Meeting kitűzése";
                    invitePerson.Background = (Brush)bc.ConvertFrom("#FFD2F9EF");
                    break;
                case 1:
                    invitePerson.Content = "Meeting kitűzve";
                    invitePerson.Background = (Brush)bc.ConvertFrom("#FFF9D2E2");
                    break;
                default:
                    invitePerson.Content = "Email kiküldése";
                    invitePerson.Background = (Brush)bc.ConvertFrom("#FFD2F9EF");
                    break;
            }
        }


    }
}
