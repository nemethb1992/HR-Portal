﻿using HR_Portal.Source;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using HR_Portal.Source.Model;
using HR_Portal.Source.ViewModel;
using HR_Portal.Source.Model.Project;
using HR_Portal.Public.templates;
using HR_Portal.Utils;

namespace HR_Portal.View.Usercontrol.Panels
{
    /// <summary>
    /// Interaction logic for projekt_jelolt_DataView.xaml
    /// </summary>
    /// 
    public partial class ProjektJeloltDataSheet : UserControl
    {
    
        Utilities Utility = new Utilities();
        
        private Grid grid;
        private Project project;
        private Applicant applicant;
        

        public ProjektJeloltDataSheet(Grid grid, Project project, Applicant applicant)
        {
                this.grid = grid;
            this.applicant = applicant;
            this.project = project;
                InitializeComponent();
            projectFormLoader();
        }

        protected void projectFormLoader()
        {
            //List<ModelFullProject> project = Project.GetFullProject();
            List<ModelKompetenciak> kompetenciaList = Interview.Data_Kompetencia();
            List<ModelKompetenciaSummary> summaryList = Utility.Data_KompetenciaJeloltKapcs();

            projekt_jelolt_title_tbl.Text = project.data.megnevezes_projekt + " - " + applicant.data.nev;
            jelolt_telefon.Text = "( " + applicant.data.telefon + " )";
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
                if (item.id == project.data.kepesseg1)
                { kompetencia_1.Text = item.kompetencia_megnevezes; }
                if (item.id == project.data.kepesseg2)
                { kompetencia_2.Text = item.kompetencia_megnevezes; }
                if (item.id == project.data.kepesseg3)
                { kompetencia_3.Text = item.kompetencia_megnevezes; }
                if (item.id == project.data.kepesseg4)
                { kompetencia_4.Text = item.kompetencia_megnevezes; }
                if (item.id == project.data.kepesseg5)
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
            Utilities.NavigateTo(grid, new ProjectDataSheet(grid,project));
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
                    Utilities.NavigateTo(grid, new ProjectDataSheet(grid, project));
                    EmailTemplate email = new EmailTemplate();
                    new Email().Send(applicant.data.email, email.Elutasito_Email(applicant.data.nev));
                    break;
                case MessageBoxResult.No:
                    break;
                case MessageBoxResult.Cancel:
                    break;
            }
        }

        protected void interviewPanelOpen()
        {
            inter_date_year.Text = DateTime.Now.Year.ToString();
            inter_date_month.Text = DateTime.Now.Month.ToString();
            inter_date_day.Text = "";
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
            if (inter_helyszin.Text == "" || inter_idopont_hour.Text == "" || inter_idopont_hour_end.Text == "" || inter_date_year.Text == "" || inter_date_month.Text == "" || inter_date_day.Text == "")
            {
                InterviewInfo_tbx.Text = "Minden mező kitöltése kötelező";
                return;
            }
            if (Convert.ToUInt32(inter_date_year.Text) < 1900 ||
                Convert.ToUInt32(inter_date_year.Text) > 2100 ||
                Convert.ToUInt32(inter_date_day.Text) > 31 ||
                Convert.ToUInt32(inter_date_day.Text) < 1 ||
                Convert.ToUInt32(inter_date_month.Text) > 12 ||
                Convert.ToUInt32(inter_date_month.Text) < 1)
            {
                InterviewInfo_tbx.Text = "Dátum megadása hibás!";
                return;
            }
            int hour_start = Convert.ToInt32(inter_idopont_hour.Text);
            int minute_start = (inter_idopont_minute.Text != "" ? Convert.ToInt32(inter_idopont_minute.Text) : 0);
            int hour_end = Convert.ToInt32(inter_idopont_hour_end.Text);
            int minute_end = (inter_idopont_minute_end.Text != "" ? Convert.ToInt32(inter_idopont_minute_end.Text) : 0);
    
            if (hour_start == 0 || hour_start > 24 || minute_start > 59 || hour_end == 0 || hour_end > 24 || minute_end > 59)
            {
                InterviewInfo_tbx.Text = "Időpont megadása hibás!";
                return;
            }
            if ((hour_start == hour_end && minute_end < minute_start) || hour_end < hour_start)
            {
                InterviewInfo_tbx.Text = "Időpont megadása hibás!";
                return;
            }

            string datum = inter_date_year.Text+"."+DateHandler.NormalForm(Convert.ToUInt32(inter_date_month.Text).ToString()) + "."+ DateHandler.NormalForm(Convert.ToUInt32(inter_date_day.Text).ToString())+".";
            new Interview().addInterview(datum, inter_cim.SelectedItem.ToString(), inter_leiras.Text, inter_helyszin.Text, hour_start + ":" + (minute_start < 10 ? "0" + minute_start.ToString() : minute_start.ToString()), hour_end + ":" + (minute_end < 10 ? "0"+minute_end.ToString() : minute_end.ToString()));
            projectFormLoader();
            interviewPanelClose();

        }

        protected void navigateToInterviewPanel(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            ModelInterview items = btn.DataContext as ModelInterview;

            Session.InterViewID = items.id;
            Utilities.SetReturnPage(Utilities.Views.ProjectJeloltDataSheet);
            Utilities.NavigateTo(grid, new InterviewPanel(grid, new Project(items.projekt_id), new Applicant(items.jelolt_id)));
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
            Utilities.SetReturnPage(Utilities.Views.ProjectJeloltDataSheet);
            Utilities.NavigateTo(grid, new ApplicantDataSheet(grid,new Applicant()));
        }
    }

}
