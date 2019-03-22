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

        protected List<ModelApplicantSearchBar> GetSearchValues()
        {
            List<ModelApplicantSearchBar> list = new List<ModelApplicantSearchBar>();

            ModelNem nemek_item = (nemek_srccbx as ComboBox).SelectedItem as ModelNem;
            ModelMunkakor munkakor_item = (munkakor_srccbx as ComboBox).SelectedItem as ModelMunkakor;
            ModelVegzettseg vegzettseg_item = (vegzettseg_srccbx as ComboBox).SelectedItem as ModelVegzettseg;

            double listSize = Math.Round(applicant_listBox.RenderSize.Height / 55);
            string munkakorStr = "";
            string vegzettsegStr = "";
            string nemekStr = "";

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


            list.Add(new ModelApplicantSearchBar
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
                cimke = cimke_srcinp.Text,
                szabad = szabad,
                allasbanBool = allasban_check.IsChecked.Value,
                szabadBool = szabad_check.IsChecked.Value,
                HeaderSelected = HeaderSelected,
                sorrend = sorrend,
                numberLimit = listSize
            });
            return list;
        }

        protected void SetSearchValues()
        {
            if (Session.ApplicantSearchValue == null)
                return;
            List<ModelApplicantSearchBar> values = Session.ApplicantSearchValue;
            munkakor_srccbx.SelectedIndex = values[0].munkakorIndex;
            vegzettseg_srccbx.SelectedIndex = values[0].vegzettsegIndex;
            nemek_srccbx.SelectedIndex = values[0].nemekIndex;
            szabad_check.IsChecked = values[0].szabadBool;
            allasban_check.IsChecked = values[0].allasbanBool;
            nev_srcinp.Text = values[0].nev;
            lakhely_srcinp.Text = values[0].lakhely;
            email_srcinp.Text = values[0].email;
            eletkor_srcinp.Text = values[0].eletkor;
            //tapsztalat_srcinp.Text = values[0].tapasztalat;
            regdate_srcinp.Text = values[0].regdate;
            interju_srcinp.Text = values[0].interjuk;
            cimke_srcinp.Text = values[0].cimke;

            if (values[0].nev.Length > 0)
                nev_label.Visibility = Visibility.Hidden;
            if (values[0].lakhely.Length > 0)
                lakhely_label.Visibility = Visibility.Hidden;
            if (values[0].email.Length > 0)
                email_label.Visibility = Visibility.Hidden;
            if (values[0].eletkor.Length > 0)
                eletkor_label.Visibility = Visibility.Hidden;
            if (values[0].regdate.Length > 0)
                regdate_label.Visibility = Visibility.Hidden;
            if (values[0].interjuk.Length > 0)
                interju_label.Visibility = Visibility.Hidden;
            if (values[0].cimke.Length > 0)
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
                    Utility.applicantArchiver(items.id, items.statusz);
                    applicantListLoader();
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
    }

}
