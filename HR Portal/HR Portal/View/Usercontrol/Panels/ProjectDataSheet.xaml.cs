using HR_Portal.Source;
using HR_Portal.Public.templates;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using HR_Portal.Source.Model;
using HR_Portal.Source.Model.Applicant;
using HR_Portal.Source.Model.Project;
using HR_Portal.Source.ViewModel;

namespace HR_Portal.View.Usercontrol.Panels
{
    public partial class ProjectDataSheet : UserControl
    {
        CommonUtility Utility = new CommonUtility();
        ApplicantImplementation Applicant = new ApplicantImplementation();
        EmailTemplate emailTemplate = new EmailTemplate();

        private Grid grid;
        private ProjektJeloltDataSheet projektJeloltDataSheet;
        private ApplicantDataSheet applicantDataSheet;
        private ProjectList projectList;

        public ProjectDataSheet(Grid grid)
        {
            this.grid = grid;
            InitializeComponent();
            try
            {
                formLoader();
            }
            catch (Exception ext)
            {
                MessageBox.Show(ext.ToString());
                grid.Children.Clear();
                grid.Children.Add(projectList = new ProjectList(grid));
            }
        }

        protected void formLoader()
        {
            listLoader();
            List<ModelFullProject> li = Project.GetFullProject();
            projekt_profile_title.Text = li[0].megnevezes_projekt;
            projekt_input_1.Text = li[0].statusz.ToString();
            projekt_input_2.Text = li[0].megnevezes_munka;
            projekt_input_3.Text = li[0].megnevezes_pc;
            projekt_input_4.Text = li[0].megnevezes_vegzettseg;
            projekt_input_5.Text = li[0].megnevezes_nyelv;
            projekt_input_6.Text = li[0].jeloltek_db.ToString();
            projekt_input_7.Text = li[0].megnevezes_hr;
            projekt_input_8.Text = li[0].fel_datum.ToString();
            projekt_input_9.Text = li[0].ber.ToString() + " Ft";
            projekt_input_10.Text = li[0].tapasztalat_ev.ToString();

            List<ModelKompetenciak> li_k = Interview.Data_Kompetencia();
            foreach (var item in li_k)
            {
                if(item.id == li[0].kepesseg1)
                { kompetencia1.Text = item.kompetencia_megnevezes; }
                if (item.id == li[0].kepesseg2)
                { kompetencia2.Text = item.kompetencia_megnevezes; }
                if (item.id == li[0].kepesseg3)
                { kompetencia3.Text = item.kompetencia_megnevezes; }
                if (item.id == li[0].kepesseg4)
                { kompetencia4.Text = item.kompetencia_megnevezes; }
                if (item.id == li[0].kepesseg5)
                { kompetencia5.Text = item.kompetencia_megnevezes; }
            }

            feladatok_tbx.Text = li[0].feladatok;
            elvarasok_tbx.Text = li[0].elvarasok;
            kinalunk_tbx.Text = li[0].kinalunk;
            elonyok_tbx.Text = li[0].elonyok;
            projectCost();
        }

        protected void listLoader()
        {
            megjegyzes_listBox.ItemsSource = Utility.Data_CommentProject();
            kapcs_jeloltek_listBox.ItemsSource = Utility.Data_JeloltKapcs();
            kapcs_ertesitendo_listBox.ItemsSource = Utility.Data_ErtesitendokKapcs();
            koltseg_listBox.ItemsSource = Project.Data_ProjectCost();
        }

        protected void openApplicantClick(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            ModelApplicantList items = button.DataContext as ModelApplicantList;

            Session.ApplicantID = items.id;

            if(items.allapota >= 1)
            {
                Session.TelefonSzurt = 1;
            }
            else
            {
                Session.TelefonSzurt = 0;
            }
            grid.Children.Clear();
            grid.Children.Add(projektJeloltDataSheet = new ProjektJeloltDataSheet(grid));
        }

        //protected void jeloltTabClick(object sender, RoutedEventArgs e)
        //{
        //    kapcs_jeloltek_listBox.Visibility = System.Windows.Visibility.Visible;
        //    kapcs_ertesitendo_listBox.Visibility = System.Windows.Visibility.Hidden;

        //    jeloltek_addbtn.Visibility = System.Windows.Visibility.Visible;
        //    ertesitendok_addbtn.Visibility = System.Windows.Visibility.Hidden;
        //}

        //protected void ertesitendokTabClick(object sender, RoutedEventArgs e)
        //{
        //    kapcs_jeloltek_listBox.Visibility = System.Windows.Visibility.Hidden;
        //    kapcs_ertesitendo_listBox.Visibility = System.Windows.Visibility.Visible;

        //    ertesitendok_addbtn.Visibility = System.Windows.Visibility.Visible;
        //    jeloltek_addbtn.Visibility = System.Windows.Visibility.Hidden;
        //}

        protected void jeloltDeleteClick(object sender, MouseButtonEventArgs e)
        {
            Image delete = sender as Image;
            ModelApplicantList items = delete.DataContext as ModelApplicantList;

            Utility.jeloltKapcsDelete(items.id);
            kapcs_jeloltek_listBox.ItemsSource = Utility.Data_JeloltKapcs();
        }

        protected void ertesitendoDeleteClick(object sender, RoutedEventArgs e)
        {
            Button delete = sender as Button;
            ModelErtesitendok items = delete.DataContext as ModelErtesitendok;

            Utility.ertesitendokKapcsDelete(items.id);
            kapcs_ertesitendo_listBox.ItemsSource = Utility.Data_ErtesitendokKapcs();
        }
        
        protected void commentDeleteClick(object sender, RoutedEventArgs e)
        {
            MenuItem delete = sender as MenuItem;
            ModelComment items = delete.DataContext as ModelComment;

            Comment.Delete(items.id);
            listLoader();
        }

        protected void jeloltRightClick(object sender, RoutedEventArgs e)
        {
            Email email = new Email();
            MenuItem mitem = sender as MenuItem;
            ModelApplicantList items = mitem.DataContext as ModelApplicantList;

            switch (mitem.Tag.ToString())
            {
                case "delete":
                    {
                        MessageBoxResult result = MessageBox.Show("Elutasító E-Mail kiküldésre kerüljön?", "My App", MessageBoxButton.YesNoCancel);
                        switch (result)
                        {
                            case MessageBoxResult.Yes:
                                Utility.jeloltKapcsDelete(items.id);
                                new Email().Send(items.email, emailTemplate.Elutasito_Email(items.nev));
                                break;
                            case MessageBoxResult.No:
                                Utility.jeloltKapcsDelete(items.id);
                                break;
                            case MessageBoxResult.Cancel:
                                break;
                        }
                        break;
                    }
                case "1":
                    Utility.jeloltKapcsUpdate(items.id, Convert.ToInt32(mitem.Tag));
                    break;
                case "2":
                    Utility.jeloltKapcsUpdate(items.id, Convert.ToInt32(mitem.Tag));
                    break;
                case "3":
                    Utility.jeloltKapcsUpdate(items.id, Convert.ToInt32(mitem.Tag));
                    new Email().Send(items.email, emailTemplate.Elutasito_Email(items.nev));
                    break;
            }
            listLoader();
            formLoader();
        }

        protected void enterComment(object sender, KeyEventArgs e)
        {
            TextBox tbx = sender as TextBox;

            if (e.Key != System.Windows.Input.Key.Enter) return;
            e.Handled = true;
            Comment.Add(comment_tartalom.Text, Session.ProjektID, 0);
            listLoader();
            tbx.Text = "";
        }

        protected void commentGotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textbox = sender as TextBox;

            if (textbox.Text == "Új megjegyzés")
            {
                textbox.Text = "";
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

        protected void emailGotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textbox = sender as TextBox;

            if (textbox.Text == "E-mail cím")
            {
                textbox.Text = "";
            }
        }

        protected void emailLostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textbox = sender as TextBox;

            if (textbox.Text == "")
            {
                textbox.Text = "E-mail cím";
            }
        }
        protected void addPanelClose(object sender, RoutedEventArgs e)
        {
            Ember_hozzaadas_Grid.Visibility = System.Windows.Visibility.Hidden;
            formLoader();
        }

        private static int SelectedTabCode;
        public int selectedTabCode { get { return SelectedTabCode; } set { SelectedTabCode = value; } }

        protected void addJeloltToProject(object sender, RoutedEventArgs e)
        {
            Blur_Grid.Visibility = System.Windows.Visibility.Visible;
            projekt_kapcsolodo_grid.Visibility = System.Windows.Visibility.Visible;
            selectedTabCode = 1;
            projekt_kapcsolodo_list.ItemsSource = Utility.Data_JeloltForCheckbox(Ember_Search_tbx.Text);
            formLoader();
        }

        protected void addUserToProject(object sender, RoutedEventArgs e)
        {
            Blur_Grid.Visibility = System.Windows.Visibility.Visible;
            projekt_kapcsolodo_grid.Visibility = System.Windows.Visibility.Visible;
            selectedTabCode = 2;
            projekt_kapcsolodo_list.ItemsSource = Utility.Data_ErtesitendokCheckbox(Ember_Search_tbx.Text);
            formLoader();
        }

        protected void personPanelClose(object sender, RoutedEventArgs e)
        {
            Blur_Grid.Visibility = Visibility.Hidden;
            projekt_kapcsolodo_grid.Visibility = Visibility.Hidden;
        }

        protected void addPerson(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if(selectedTabCode == 1)
            {
                ModelApplicantListbox items = btn.DataContext as ModelApplicantListbox;
                Applicant.AddToProject(items.id, Session.ProjektID);
                projekt_kapcsolodo_list.ItemsSource = Utility.Data_JeloltForCheckbox(Ember_Search_tbx.Text);
                kapcs_jeloltek_listBox.ItemsSource = Utility.Data_JeloltKapcs();
            }
            if (selectedTabCode == 2)
            {
                ModelErtesitendok items = btn.DataContext as ModelErtesitendok;
                Utility.addErtesitendokInsert(items.id);
                projekt_kapcsolodo_list.ItemsSource = Utility.Data_ErtesitendokCheckbox(Ember_Search_tbx.Text);
                kapcs_ertesitendo_listBox.ItemsSource = Utility.Data_ErtesitendokKapcs();
            }
        }

        protected void personSearchTextChanged(object sender, TextChangedEventArgs e)
        {
            Ember_Search_Listbox.ItemsSource = Utility.Data_JeloltForCheckbox(Ember_Search_tbx.Text);
        }

        protected void descriptionLostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textbox = sender as TextBox;

            string type = textbox.Tag.ToString();
            Utility.projectDescriptionUpdate(type, textbox.Text);
            formLoader();
        }

        protected void projectCost()
        {
            int sum = 0;
            List<ModelKoltsegek> list = Project.Data_ProjectCost();

            foreach (var item in list)
            {
                sum += item.osszeg;
            }
            ossz_koltseg.Text = sum.ToString()+" ft";
        }

        protected void addCost(object sender, RoutedEventArgs e)
        {
            Utility.projectCostInsert(k_megnevezes_tbx.Text, k_osszeg_tbx.Text);
            koltseg_listBox.ItemsSource = Project.Data_ProjectCost();
            projectCost();
            Koltseg_insert_grid.Visibility = Visibility.Hidden;
        }

        protected void costPanelClose(object sender, RoutedEventArgs e)
        {
            Koltseg_insert_grid.Visibility = Visibility.Hidden;
            k_megnevezes_tbx.Text = "";
            k_osszeg_tbx.Text = "";
        }

        protected void numericTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        protected void costPanelOpen(object sender, RoutedEventArgs e)
        {
            Koltseg_insert_grid.Visibility = Visibility.Visible;
        }

        protected void deleteCost(object sender, RoutedEventArgs e)
        {
            MenuItem menu = sender as MenuItem;
            ModelKoltsegek item = menu.DataContext as ModelKoltsegek;
            Utility.projectCostDelete(item.id);
            koltseg_listBox.ItemsSource = Project.Data_ProjectCost();
            projectCost();
        }

        protected void publishChecked(object sender, RoutedEventArgs e)
        {
            Utility.publishProject(1);
        }

        protected void publishUnchecked(object sender, RoutedEventArgs e)
        {
            Utility.publishProject(0);
        }

        protected void gridMouseDown(object sender, MouseButtonEventArgs e)
        {
            Grid grid = sender as Grid;
            ModelApplicantList item = grid.DataContext as ModelApplicantList;

            if (item.checkbox == false)
            {
                item.checkbox = true;
            }
            else
            {
                item.checkbox = false;
            }
            kapcs_jeloltek_listBox.Items.Refresh();
        }

        private void projektSendInEmail(object sender, RoutedEventArgs e)
        {
            try
            {
                new Email().Send(email_tbx.Text, emailTemplate.ProjektPublikalo(projekt_profile_title.Text, hirdetes(), szoveg()));
            }
            catch (Exception)
            {
                MessageBox.Show("Sikertelen e-mail küldés!");
            }
        }
        protected List<string> hirdetes()
        {
            List<string> list = new List<string>(); 

            if (check_1.IsChecked == true)
                list.Add("Céges Honlap");
            if (check_2.IsChecked == true)
                list.Add("Facebook");
            if (check_3.IsChecked == true)
                list.Add("Profession");
            if (check_4.IsChecked == true)
                list.Add("allasstart.hu");
            if (check_5.IsChecked == true)
                list.Add("keol.hu");
            if (check_6.IsChecked == true)
                list.Add("hiros.hu");
            if (check_7.IsChecked == true)
                list.Add("Kecskeméti Lapok");
            
            return list;
        }
        protected List<string> szoveg()
        {
            List<string> list = new List<string>();
            list.Add(feladatok_tbx.Text);
            list.Add(elvarasok_tbx.Text);
            list.Add(kinalunk_tbx.Text);
            list.Add(elonyok_tbx.Text);

            return list;
        }
        private void BackButton(object sender, RoutedEventArgs e)
        {
            grid.Children.Clear();
            if (Session.lastPage == CommonUtility.Views.ApplicantDataSheet)
            {
                grid.Children.Add(applicantDataSheet = new ApplicantDataSheet(grid));
            }
            else
            {
                grid.Children.Add(projectList = new ProjectList(grid));
            }
        }
    }
}
