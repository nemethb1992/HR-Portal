﻿using System;
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
        
        CommonUtility Utility = new CommonUtility();
        ApplicantImplementation Applicant = new ApplicantImplementation();
        

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
            Session.ApplicantStatusz = 1;
            buttonColorChange();
            try
            {
                List<ModelApplicantList> list = Applicant.GetApplicantList(searchValues());
                applicant_listBox.ItemsSource = list;
                talalat_tbl.Text = "Találatok:  " + list.Count.ToString();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        protected void projectPassivateClick(object sender, RoutedEventArgs e)
        {
            Utility.ApplicantStatusChange(0);
            applicant_listBox.ItemsSource = Applicant.GetApplicantList(searchValues());
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
            applicant_listBox.ItemsSource = Applicant.GetApplicantList(searchValues());
            buttonColorChange();
        }

        protected void buttonColorChange()
        {
            var bc = new BrushConverter();
            if (Session.ApplicantStatusz == 1)
            {
                applicant_aktiv_btn.Background = (Brush)bc.ConvertFrom("#bfbfbf");
                applicant_aktiv_btn.BorderBrush = (Brush)bc.ConvertFrom("#bfbfbf");
                applicant_aktiv_btn.Foreground = (Brush)bc.ConvertFrom("#ffffff");
                applicant_passziv_btn.Background = (Brush)bc.ConvertFrom("#ffffff");
                applicant_passziv_btn.Foreground = (Brush)bc.ConvertFrom("#404040");
            }
            else
            {
                applicant_aktiv_btn.Background = (Brush)bc.ConvertFrom("#ffffff");
                applicant_aktiv_btn.Foreground = (Brush)bc.ConvertFrom("#404040");
                applicant_passziv_btn.Background = (Brush)bc.ConvertFrom("#bfbfbf");
                applicant_passziv_btn.BorderBrush = (Brush)bc.ConvertFrom("#bfbfbf");
                applicant_passziv_btn.Foreground = (Brush)bc.ConvertFrom("#ffffff");
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
                Applicant.ChangeFirstOpen();
            }
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
                    Applicant.DeleteApplicant(items.id);
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

        protected void ClearSearchbar(object sender, RoutedEventArgs e)
        {
            munkakor_srccbx.SelectedIndex = -1;
            vegzettseg_srccbx.SelectedIndex = -1;
            nemek_srccbx.SelectedIndex = -1;
            szabad_check.IsChecked = false;
            nev_srcinp.Text = "";
            lakhely_srcinp.Text = "";
            email_srcinp.Text = "";
            eletkor_srcinp.Text = "";
            tapsztalat_srcinp.Text = "";
            regdate_srcinp.Text = "";
            interju_srcinp.Text = "";
            cimke_srcinp.Text = "";
            applicantListLoader();
        }

        protected void szabadChecked(object sender, RoutedEventArgs e)
        {
            applicantListLoader();
        }

        protected void szabadUnchecked(object sender, RoutedEventArgs e)
        {
            applicantListLoader();
        }

        protected void navigateToNewApplicantPanel(object sender, RoutedEventArgs e)
        {

            Session.isUpdate = false;
            grid.Children.Clear();
            grid.Children.Add(newApplicantPanel = new NewApplicantPanel(grid));
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
