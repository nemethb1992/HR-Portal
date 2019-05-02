using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using HR_Portal.Public.templates;
using HR_Portal.Source;
using HR_Portal.Source.Model;
using HR_Portal.Source.Model.Applicant;
using HR_Portal.Source.ViewModel;
using HR_Portal.Test;

namespace HR_Portal.View.Usercontrol.Panels
{
    /// <summary>
    /// Interaction logic for applicant_panel.xaml
    /// </summary>
    public partial class ApplicantList : UserControl
    {
        private ModelFullApplicant selectedApplicant;

        private static string HeaderSelecteds;
        public string HeaderSelected { get { return HeaderSelecteds; } set { HeaderSelecteds = value; } }

        Utilities Utility = new Utilities();

        
        private Grid grid;

        public ApplicantList(Grid grid)
        {
            this.grid = grid;
            InitializeComponent();
            startMethods();
        }

        protected void startMethods()
        {
            Session.ApplicantStatusz = 1;
            checkBoxLoader();
            applicantListLoader();
            if (Session.ApplicantSearchValue != null)
                SetSearchValues();
        }

        protected ModelApplicantSearchBar GetSearchValues()
        {
            ModelApplicantSearchBar list = new ModelApplicantSearchBar();

            ModelNem nemek_item = (nemek_srccbx as ComboBox).SelectedItem as ModelNem;
            ModelMunkakor munkakor_item = (munkakor_srccbx as ComboBox).SelectedItem as ModelMunkakor;
            ModelVegzettseg vegzettseg_item = (vegzettseg_srccbx as ComboBox).SelectedItem as ModelVegzettseg;
            ModelCimkek cimke_item = (cimke_srccbx as ComboBox).SelectedItem as ModelCimkek;

            double listSize = Math.Round(applicant_listBox.RenderSize.Height / 55);
            string munkakorStr = "";
            string vegzettsegStr = "";
            string nemekStr = "";
            string cimkeStr = "";

            if (munkakor_srccbx.SelectedIndex != -1)
            {
                munkakorStr = munkakor_item.id.ToString();
            }
            if (vegzettseg_srccbx.SelectedIndex != -1)
            {
                vegzettsegStr = vegzettseg_item.id.ToString();
            }
            if (nemek_srccbx.SelectedIndex != -1)
            {
                nemekStr = nemek_item.id.ToString();
            }
            if (cimke_srccbx.SelectedIndex != -1)
            {
                cimkeStr = cimke_item.cimke_megnevezes;
            }
            else {
                cimkeStr = cimke_srcinp.Text;
            }

            //string tapasztalat = tapsztalat_srcinp.Text;
            //if (tapsztalat_srcinp.Text == "")
            //    tapasztalat = "";

            string interjuk = interju_srcinp.Text;
            if (interju_srcinp.Text == "")
                interjuk = "";

            string szabad = "";
            if (szabad_check.IsChecked == true)
                szabad = "1";

            string sorrend = " ASC";


            list = new ModelApplicantSearchBar
            {
                nev = nev_srcinp.Text,
                lakhely = lakhely_srcinp.Text,
                email = email_srcinp.Text,
                eletkor = eletkor_srcinp.Text,
                //tapasztalat = tapasztalat,
                regdate = regdate_srcinp.Text,
                interjuk = interjuk,
                nemekStr = nemekStr,
                nemekIndex = nemek_srccbx.SelectedIndex,
                munkakorStr = munkakorStr,
                munkakorIndex = munkakor_srccbx.SelectedIndex,
                vegzettsegStr = vegzettsegStr,
                vegzettsegIndex = nemek_srccbx.SelectedIndex,
                cimke = cimkeStr,
                szabad = szabad,
                allasbanBool = allasban_check.IsChecked.Value,
                szabadBool = szabad_check.IsChecked.Value,
                HeaderSelected = HeaderSelected,
                sorrend = sorrend,
                numberLimit = listSize
            };
            return list;
        }

        protected void SetSearchValues()
        {
            if (Session.ApplicantSearchValue == null)
                return;
            ModelApplicantSearchBar srcValue = Session.ApplicantSearchValue;
            munkakor_srccbx.SelectedIndex = srcValue.munkakorIndex;
            vegzettseg_srccbx.SelectedIndex = srcValue.vegzettsegIndex;
            nemek_srccbx.SelectedIndex = srcValue.nemekIndex;
            szabad_check.IsChecked = srcValue.szabadBool;
            allasban_check.IsChecked = srcValue.allasbanBool;
            nev_srcinp.Text = srcValue.nev;
            lakhely_srcinp.Text = srcValue.lakhely;
            email_srcinp.Text = srcValue.email;
            eletkor_srcinp.Text = srcValue.eletkor;
            //tapsztalat_srcinp.Text = srcValue.tapasztalat;
            regdate_srcinp.Text = srcValue.regdate;
            interju_srcinp.Text = srcValue.interjuk;
            cimke_srcinp.Text = srcValue.cimke;

            if (srcValue.nev.Length > 0)
                nev_label.Visibility = Visibility.Hidden;
            if (srcValue.lakhely.Length > 0)
                lakhely_label.Visibility = Visibility.Hidden;
            if (srcValue.email.Length > 0)
                email_label.Visibility = Visibility.Hidden;
            if (srcValue.eletkor.Length > 0)
                eletkor_label.Visibility = Visibility.Hidden;
            if (srcValue.regdate.Length > 0)
                regdate_label.Visibility = Visibility.Hidden;
            if (srcValue.interjuk.Length > 0)
                interju_label.Visibility = Visibility.Hidden;
            if (srcValue.cimke.Length > 0)
                cimke_label.Visibility = Visibility.Hidden;

        }

        public void applicantListLoader()
        {
            buttonColorChange();
            try
            {
                List<ModelApplicantList> list = Applicant.GetApplicantList(GetSearchValues());
                applicant_listBox.ItemsSource = list;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        protected void projectPassivateClick(object sender, RoutedEventArgs e)
        {
            Utility.ApplicantStatusChange(0);
            applicant_listBox.ItemsSource = Applicant.GetApplicantList(GetSearchValues());
            buttonColorChange();
        }

        protected void applicantArchivateClick(object sender, RoutedEventArgs e)
        {

            MessageBoxResult result = MessageBox.Show("Biztosan megváltoztatod? \n\n", "HR Cloud", MessageBoxButton.YesNoCancel);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    ModelApplicantList items = (sender as MenuItem).DataContext as ModelApplicantList;
                    selectedApplicant = new Applicant(items.id).data;
                    if(selectedApplicant.statusz == 1)
                    {
                        Cimke_Grid.Visibility = Visibility.Visible;
                        cimke_related_list.ItemsSource = new ModelCimkek().GetRelated(selectedApplicant.id);
                        cimke_title.Text = "Cimkék (" + selectedApplicant.nev + ")";
                    }
                    else
                    {
                        Utility.applicantArchiver(selectedApplicant.id, selectedApplicant.statusz);
                        applicantListLoader();
                    }
                    break;
                case MessageBoxResult.No:
                    break;
                case MessageBoxResult.Cancel:
                    break;
            }
        }

        protected void projectActivateClick(object sender, RoutedEventArgs e)
        {
            Utility.ApplicantStatusChange(1);
            applicant_listBox.ItemsSource = Applicant.GetApplicantList(GetSearchValues());
            buttonColorChange();
        }

        protected void buttonColorChange()
        {
            var bc = new BrushConverter();
            if (Session.ApplicantStatusz == 1)
            {
                applicant_aktiv_btn.Background = (Brush)bc.ConvertFrom("#ffe6e6");
                applicant_passziv_btn.Background = (Brush)bc.ConvertFrom("#ffffff");
            }
            else
            {
                applicant_aktiv_btn.Background = (Brush)bc.ConvertFrom("#ffffff");
                applicant_passziv_btn.Background = (Brush)bc.ConvertFrom("#ffe6e6");
            }
        }

        protected void checkBoxLoader()
        {
            vegzettseg_srccbx.ItemsSource = Utility.Data_Vegzettseg();
            munkakor_srccbx.ItemsSource = Utility.Data_Munkakor();
            nemek_srccbx.ItemsSource = Utility.Data_Nemek();
            cimke_srccbx.ItemsSource = Utility.Data_Cimkek();
        }

        protected void applicantOpenClick(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            ModelApplicantList items = button.DataContext as ModelApplicantList;
            Session.ApplicantID = items.id;
            if (items.frissValue)
            {
                MessageBoxResult result = MessageBox.Show("Üdvözlő üzenet küldése? \n", "HR Cloud", MessageBoxButton.YesNo);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        Applicant.FirstOpen(items.id);
                        break;
                    case MessageBoxResult.No:
                        break;
                }
            }
            Utilities.SetReturnPage(Utilities.Views.ApplicantList);
            Session.ApplicantSearchValue = GetSearchValues();
            Utilities.NavigateTo(grid,new ApplicantDataSheet(grid,new Applicant(items.id)));
        }

        protected void applicantDeleteClick(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Biztosan törölni szeretnéd? \n", "HR Cloud", MessageBoxButton.YesNoCancel);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    MenuItem menuItem = sender as MenuItem;
                    ModelApplicantList items = menuItem.DataContext as ModelApplicantList;
                    Applicant.DeleteApplicant(items.id);
                    Files.DeleteFolder(items.id);
                    applicantListLoader();
                    break;
                case MessageBoxResult.No:
                    break;
                case MessageBoxResult.Cancel:
                    break;
            }
        }



        protected async void searchInputTextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textbox = (TextBox)sender;
            int fisrtLength = textbox.Text.Length;

            await Task.Delay(250);
            if (fisrtLength == textbox.Text.Length)
            {
                SetPageNull();
                applicantListLoader();
            }

        }

        protected void Numeric(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        protected void searchCbxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SetPageNull();
            applicantListLoader();
        }

        protected void textBoxPlaceHolderGotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textbox = sender as TextBox;
            string textboxName = ((TextBox)sender).Tag.ToString();
            var textboxElement = (TextBox)this.FindName(textboxName);

            if (((TextBox)sender).Text == "")
                textboxElement.Visibility = Visibility.Hidden;
        }

        protected void textBoxPlaceHolderLostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textbox = sender as TextBox;
            string textboxName = ((TextBox)sender).Tag.ToString();
            var Tbx = (TextBox)this.FindName(textboxName);

            if (((TextBox)sender).Text == "")
            {
                Tbx.Visibility = Visibility.Visible;
                textbox.BorderBrush = (SolidColorBrush)Application.Current.Resources["racs_light"];
            }
            else
            {
                textbox.BorderBrush = (SolidColorBrush)Application.Current.Resources["ThemeColor"];
                textbox.Foreground = Brushes.Black;
            }
        }

        protected void ClearSearchbar(object sender, RoutedEventArgs e)
        {
            munkakor_srccbx.SelectedIndex = -1;
            vegzettseg_srccbx.SelectedIndex = -1;
            nemek_srccbx.SelectedIndex = -1;
            cimke_srccbx.SelectedIndex = -1;
            szabad_check.IsChecked = false;
            allasban_check.IsChecked = false;
            nev_srcinp.Text = "";
            lakhely_srcinp.Text = "";
            email_srcinp.Text = "";
            eletkor_srcinp.Text = "";
            regdate_srcinp.Text = "";
            interju_srcinp.Text = "";
            cimke_srcinp.Text = "";
            
            nev_label.Visibility = Visibility.Visible;
            lakhely_label.Visibility = Visibility.Visible;
            email_label.Visibility = Visibility.Visible;
            eletkor_label.Visibility = Visibility.Visible;
            regdate_label.Visibility = Visibility.Visible;
            interju_label.Visibility = Visibility.Visible;
            cimke_label.Visibility = Visibility.Visible;

            Session.ApplicantSearchValue = null;
            SetPageNull();
            applicantListLoader();

        }

        protected void szabadChecked(object sender, RoutedEventArgs e)
        {
            SetPageNull();
            applicantListLoader();
        }

        protected void szabadUnchecked(object sender, RoutedEventArgs e)
        {
            SetPageNull();
            applicantListLoader();
        }

        protected void navigateToNewApplicantPanel(object sender, RoutedEventArgs e)
        {
            Utilities.SetReturnPage(Utilities.Views.ApplicantList);
            Session.isUpdate = false;
            Session.ApplicantSearchValue = GetSearchValues();
            Utilities.NavigateTo(grid, new NewApplicantPanel(grid));
        }

        protected void modositasClick(object sender, RoutedEventArgs e)
        {
            MenuItem item = sender as MenuItem;
            ModelApplicantList itemSource = item.DataContext as ModelApplicantList;
            Session.isUpdate = true;
            Session.ApplicantID = itemSource.id;
            Session.ApplicantSearchValue = GetSearchValues();
            Utilities.NavigateTo(grid, new NewApplicantPanel(grid,new Applicant(itemSource.id)));
        }

        protected void headerClick(object sender, MouseButtonEventArgs e)
        {
            Label item = sender as Label;
            HeaderSelected = item.Tag.ToString();
            applicantListLoader();
        }

        protected void sorrendChecked(object sender, RoutedEventArgs e)
        {
            applicantListLoader();
        }

        protected void sorrendUnchecked(object sender, RoutedEventArgs e)
        {
            applicantListLoader();
        }

        private void VisszautasitIdeiglenes(object sender, RoutedEventArgs e)
        {
            ModelApplicantList applicant = (sender as MenuItem).DataContext as ModelApplicantList;
            new Email().Send(applicant.email, new EmailTemplate().NincsPozicioElutasito(applicant.nev));
        }

        private void MegfigyeltekhezAd(object sender, RoutedEventArgs e)
        {
            ModelApplicantList applicant = (sender as MenuItem).DataContext as ModelApplicantList;
            Applicant.AddToFavorite(applicant.id);
        }

        private void PageSwitchEventHandler()
        {
            applicantListLoader();
            actualPageTbl.Text = (Session.ApplicantSearchPage + 1).ToString() + "."; ;
        }

        private void SetPageNull()
        {
            Session.ApplicantSearchPage = 0;
            PageSwitchEventHandler();
        }

        private void PreviousPageButton_Click(object sender, RoutedEventArgs e)
        {
            int value = Session.ApplicantSearchPage;
            if(!value.Equals(0))
            {
                Session.ApplicantSearchPage--;
                PageSwitchEventHandler();
            }
        }

        private void NextPageButton_Click(object sender, RoutedEventArgs e)
        {
            Session.ApplicantSearchPage++;
            PageSwitchEventHandler();
        }

        private void FirstPageButton_Click(object sender, RoutedEventArgs e)
        {
            SetPageNull();
            PageSwitchEventHandler();
        }

        private async void Applicant_listBox_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            await Task.Delay(400);
            PageSwitchEventHandler();
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
            new ModelCimkek().AddRelation(selectedApplicant.id, item.id);
            cimke_searched_list.ItemsSource = new ModelCimkek().GetAll();
            cimke_related_list.ItemsSource = new ModelCimkek().GetRelated(selectedApplicant.id);
        }

        private void Remove_cimke_relation_btn_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            ModelCimkek item = btn.DataContext as ModelCimkek;
            new ModelCimkek().DeleteRelation(selectedApplicant.id, item.id);
            cimke_related_list.ItemsSource = new ModelCimkek().GetRelated(selectedApplicant.id);
        }

        private async void Cimke_search_tbx_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textbox = (TextBox)sender;
            int fisrtLength = textbox.Text.Length;

            await Task.Delay(250);
            if (fisrtLength == textbox.Text.Length)
            {
                if (textbox.Text == textbox.Tag.ToString())
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
            Cimke_Grid.Visibility = Visibility.Hidden;
        }

        private void CimkePanel_Open(object sender, RoutedEventArgs e)
        {
            Cimke_Grid.Visibility = Visibility.Visible;
            cimke_searched_list.ItemsSource = new ModelCimkek().GetAll();
            cimke_related_list.ItemsSource = new ModelCimkek().GetRelated(selectedApplicant.id);
        }

        private void ArchivateAndClose_Click(object sender, RoutedEventArgs e)
        {
            Cimke_Grid.Visibility = Visibility.Hidden;
            Utility.applicantArchiver(selectedApplicant.id, selectedApplicant.statusz);
            applicantListLoader();
        }
    }

}
