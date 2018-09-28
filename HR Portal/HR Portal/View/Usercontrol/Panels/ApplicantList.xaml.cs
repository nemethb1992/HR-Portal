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
        
        ControlApplicant aControl = new ControlApplicant();

        private ApplicantDataSheet applicantDataSheet;
        private NewApplicantPanel newApplicantPanel;
        private Grid grid;

        public ApplicantList(Grid grid)
        {
            this.grid = grid;
            InitializeComponent();
            startMethods();
        }

        public ApplicantList()
        {

        }

        protected void startMethods()
        {
            checkBoxLoader();
            applicantListLoader();
        }

        protected List<string> searchValues()
        {
            List<string> list = new List<string>();
            
            ModelMunkakor munkakor_item = (munkakor_srccbx as ComboBox).SelectedItem as ModelMunkakor;
            ModelVegzettseg vegzettseg_item = (vegzettseg_srccbx as ComboBox).SelectedItem as ModelVegzettseg;
            ModelNem nemek_item = (nemek_srccbx as ComboBox).SelectedItem as ModelNem;

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

            string tapasztalat = tapsztalat_srcinp.Text;
            if (tapsztalat_srcinp.Text == "")
                tapasztalat = "";

            string interjuk = interju_srcinp.Text;
            if (interju_srcinp.Text == "")
                interjuk = "";

            string szabad = "";
            if (szabad_check.IsChecked == true)
                szabad = "1";

            string sorrend = " ASC";

            list.Add(nev_srcinp.Text);
            list.Add(lakhely_srcinp.Text);
            list.Add(email_srcinp.Text);
            list.Add(eletkor_srcinp.Text);
            list.Add(tapasztalat);
            list.Add(regdate_srcinp.Text);
            list.Add(interjuk);
            list.Add(nemekStr);
            list.Add(munkakorStr);
            list.Add(vegzettsegStr);
            list.Add(cimke_srcinp.Text);
            list.Add(szabad);
            list.Add(HeaderSelected);
            list.Add(sorrend);

            return list;
        }

        public void applicantListLoader()
        {
                List<ModelApplicantList> list = VMApplicant.GetApplicantList(searchValues());
                applicant_listBox.ItemsSource = list;
                talalat_tbl.Text = "Találatok:  " + list.Count.ToString();
            
        }

        protected void checkBoxLoader()
        {
            vegzettseg_srccbx.ItemsSource = aControl.Data_Vegzettseg();
            munkakor_srccbx.ItemsSource = aControl.Data_Munkakor();
            nemek_srccbx.ItemsSource = aControl.Data_Nemek();
        }

        protected void applicantOpenClick(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            ModelApplicantList items = button.DataContext as ModelApplicantList;
            Session.ApplicantID = items.id;
            grid.Children.Clear();
            grid.Children.Add(applicantDataSheet = new ApplicantDataSheet(grid));
        }

        protected void applicantDeleteClick(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Biztosan törölni szeretnéd? \n", "HR Cloud", MessageBoxButton.YesNoCancel);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    MenuItem menuItem = sender as MenuItem;
                    ModelApplicantList items = menuItem.DataContext as ModelApplicantList;
                    VMApplicant.Delete(items.id);
                    applicantListLoader();
                    break;
                case MessageBoxResult.No:
                    break;
                case MessageBoxResult.Cancel:
                    break;
            }

        }

        protected void navigateToNewApplicantPanel(object sender, RoutedEventArgs e)
        {
            grid.Children.Clear();
            grid.Children.Add(newApplicantPanel = new NewApplicantPanel(grid));

        }

        protected async void searchInputTextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textbox = (TextBox)sender;
            int fisrtLength = textbox.Text.Length;

            await Task.Delay(250);
            if (fisrtLength == textbox.Text.Length)
                applicantListLoader();
        }

        protected void numericPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        protected void searchCbxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
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

        protected void searchBarRefresh(object sender, RoutedEventArgs e)
        {
            TextBox nev_tbx = nev_srcinp as TextBox;  // TODO: <- ez kell?
            munkakor_srccbx.SelectedIndex = 0;
            vegzettseg_srccbx.SelectedIndex = 0;
            nemek_srccbx.SelectedIndex = 0;
            nev_srcinp.Text = "";
            lakhely_srcinp.Text = "";
            email_srcinp.Text = "";
            eletkor_srcinp.Text = "";
            tapsztalat_srcinp.Text = "";
            regdate_srcinp.Text = "";
            interju_srcinp.Text = "";
        }

        protected void szabadChecked(object sender, RoutedEventArgs e)
        {
            applicantListLoader();
        }

        protected void szabadUnchecked(object sender, RoutedEventArgs e)
        {
            applicantListLoader();
        }

        protected void modositasClick(object sender, RoutedEventArgs e)
        {
            MenuItem item = sender as MenuItem;
            ModelApplicantList itemSource = item.DataContext as ModelApplicantList;
            Session.isUpdate = true;
            Session.ApplicantID = itemSource.id;
            grid.Children.Clear();
            grid.Children.Add(newApplicantPanel = new NewApplicantPanel(grid));
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
    }

}
