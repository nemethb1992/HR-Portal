using HR_Portal.Source;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using HR_Portal.Source.Model;
using HR_Portal.Source.Model.Applicant;
using HR_Portal.Source.ViewModel;
using HR_Portal.Source.Model.Project;
using HR_Portal_Test.Source.Model.Applicant;
using System;
using HR_Portal.Public.templates;
using System.Text.RegularExpressions;
using HR_Portal_Test.Source.Model.Other;

namespace HR_Portal.View.Usercontrol.Panels.SzakmaiLayouts
{
    /// <summary>
    /// Interaction logic for SzakmaiProjekt_DataView.xaml
    /// </summary>
    public partial class SzakmaiProjektDataSheet : UserControl
    {
        
        //ControlApplicant aControl = new ControlApplicant();
        Utilities Utility = new Utilities();
        private ModelFullProject projekt;
        private SzakmaiApplicantDataView szakmaiApplicantDataView;
        private SzakmaiList szakmaiList;
        private Grid grid;

        public SzakmaiProjektDataSheet(Grid grid)
        {
            InitializeComponent();
            this.grid = grid;
            formLoader();
        }

        protected void navigateToSzakmaiList(object sender, RoutedEventArgs e)
        {
            grid.Children.Clear();
            grid.Children.Add(szakmaiList = new SzakmaiList(grid));
        }

        protected void formLoader()
        {
            projekt = Project.GetFullProject()[0];
            projekt_profile_title.Text = projekt.megnevezes_projekt;
            projekt_input_1.Text = projekt.statusz.ToString();
            projekt_input_2.Text = projekt.megnevezes_munka;
            projekt_input_3.Text = projekt.megnevezes_pc;
            projekt_input_4.Text = projekt.megnevezes_vegzettseg;
            projekt_input_5.Text = projekt.megnevezes_nyelv;
            projekt_input_6.Text = projekt.jeloltek_db.ToString();
            projekt_input_7.Text = projekt.megnevezes_hr;
            projekt_input_8.Text = projekt.fel_datum.ToString();
            projekt_input_9.Text = projekt.ber.ToString() + " Ft";
            projekt_input_10.Text = projekt.tapasztalat_ev.ToString();

            List<ModelKompetenciak> listKompetencia = Interview.Data_Kompetencia();
            foreach (var item in listKompetencia)
            {
                if (item.id == projekt.kepesseg1)
                { kompetencia1.Text = item.kompetencia_megnevezes; }
                if (item.id == projekt.kepesseg2)
                { kompetencia2.Text = item.kompetencia_megnevezes; }
                if (item.id == projekt.kepesseg3)
                { kompetencia3.Text = item.kompetencia_megnevezes; }
                if (item.id == projekt.kepesseg4)
                { kompetencia4.Text = item.kompetencia_megnevezes; }
                if (item.id == projekt.kepesseg5)
                { kompetencia5.Text = item.kompetencia_megnevezes; }
            }

            megjegyzes_listBox.ItemsSource = Utility.Data_CommentProject();
            kapcs_jeloltek_listBox.ItemsSource = new Szakmai().Data_JeloltKapcsSzakmai(projekt.id);
        }

        protected void commentDeleteClick(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = sender as MenuItem;
            ModelComment items = menuItem.DataContext as ModelComment;

            Comment.Delete(items.id);
            megjegyzes_listBox.ItemsSource = Utility.Data_CommentProject();
        }

        protected void enterComment(object sender, KeyEventArgs e)
        {
            TextBox tbx = sender as TextBox;

            if (e.Key != System.Windows.Input.Key.Enter) return;
            e.Handled = true;
            Comment.Add(comment_tartalom.Text, Session.ProjektID, 0);
            megjegyzes_listBox.ItemsSource = Utility.Data_CommentProject();
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
            TextBox tbx = sender as TextBox;

            if (tbx.Text == "")
            {
                tbx.Text = "Új megjegyzés";
            }
        }

        protected void openApplicant(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            ModelApplicantSzakmaiList items = button.DataContext as ModelApplicantSzakmaiList;

            Session.ApplicantID = items.id;
            grid.Children.Clear();
            grid.Children.Add(szakmaiApplicantDataView = new SzakmaiApplicantDataView(grid));
        }

        private void Uj_interju_megsem_btn_Click(object sender, RoutedEventArgs e)
        {
            Interview_Panel_Close();
        }

        public void Interview_Panel_Close()
        {
            var grid = (Grid)this.FindName("Interview_request_panel");
            inter_jelolt.Text = "";
            inter_helyszin.Text = "";
            inter_idopont_hour.Text = "";
            inter_idopont_minute.Text = "";
            inter_idopont_hour_end.Text = "";
            inter_idopont_minute_end.Text = "";
            inter_leiras.Text = "";
            grid.Visibility = Visibility.Hidden;
        }
        protected void numericTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void Uj_interju_mentes_btn_Click(object sender, RoutedEventArgs e)
        {
            if (inter_helyszin.Text == "" || inter_idopont_hour.Text == "" || inter_idopont_hour_end.Text == "" || inter_date.SelectedDate.ToString() == "")
            {
                InterviewInfo_tbx.Text = "Minden mező kitöltése kötelező";
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

            string datum = "";
            string[] seged = inter_date.SelectedDate.ToString().Split(' ');
            try
            {
                datum = seged[0] + seged[1] + seged[2];
            }
            catch { }
            ModelUserData user = UserData.GetById(projekt.hr_id);
            string idopont = hour_start + ":" + (minute_start < 10 ? "0" + minute_start.ToString() : minute_start.ToString()) +" - "+ hour_end +":" + (minute_end < 10 ? "0" + minute_end.ToString() : minute_end.ToString());
            new Email().Send(user.email, new EmailTemplate().Meeting_igenyles(user.name, Session.UserData.name, inter_jelolt.Text, projekt.megnevezes_projekt, datum, idopont, inter_helyszin.Text, inter_leiras.Text));
            new ModelSzakmaiInterviewIgeny().Insert(new ModelSzakmaiInterviewIgeny { jelolt_id = Convert.ToInt32(inter_jelolt_id.Text), projekt_id = projekt.id, user_id = Session.UserData.id, state = 1 });
            formLoader();
            Interview_Panel_Close();
        }

        private void Interju_igenyles_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            ModelApplicantSzakmaiList items = button.DataContext as ModelApplicantSzakmaiList;

            var grid = (Grid)this.FindName("Interview_request_panel");
            grid.Visibility = Visibility.Visible;
            inter_jelolt.Text = items.nev;
            inter_jelolt_id.Text = items.id.ToString();
        }
    }
}
