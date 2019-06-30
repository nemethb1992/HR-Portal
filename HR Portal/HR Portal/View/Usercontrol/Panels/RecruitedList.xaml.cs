using HR_Portal.Source;
using HR_Portal.Source.Model;
using HR_Portal.Source.Model.Applicant;
using HR_Portal.Source.ViewModel;
using HR_Portal_Test.Source.Model.Applicant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HR_Portal_Test.View.Usercontrol.Panels
{
    /// <summary>
    /// Interaction logic for RecruitedList.xaml
    /// </summary>
    public partial class RecruitedList : UserControl
    {
        private Grid grid;

        private static string HeaderSelecteds;
        public string HeaderSelected { get { return HeaderSelecteds; } set { HeaderSelecteds = value; } }
        public RecruitedList(Grid grid)
        {
            this.grid = grid;
            InitializeComponent();
            ResourceLoader();
        }

        protected void ResourceLoader()
        {
            toborzo_srccbx.ItemsSource = ModelFreelancerList.getFreelancerList("SELECT * FROM freelancer_list");
        }
        public void applicantListLoader()
        {
            try
            {
                List<ModelFreelancerApplicant> list = Applicant.GetRecruitedList(GetSearchValues());
                applicant_listBox.ItemsSource = list;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
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

        private void PreviousPageButton_Click(object sender, RoutedEventArgs e)
        {
            int value = Session.ApplicantSearchPage;
            if (!value.Equals(0))
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
        protected void searchCbxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SetPageNull();
            applicantListLoader();
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
            (toborzo_srccbx as ComboBox).SelectedIndex = -1;
            nev_srcinp.Text = "";
            lakhely_srcinp.Text = "";
            regdate_srcinp.Text = "";

            nev_label.Visibility = Visibility.Visible;
            lakhely_label.Visibility = Visibility.Visible;
            regdate_label.Visibility = Visibility.Visible;
            SetPageNull();
            applicantListLoader();

        }

        private void SetPageNull()
        {
            PageSwitchEventHandler();
        }

        private void PageSwitchEventHandler()
        {
            applicantListLoader();
            actualPageTbl.Text = (Session.ApplicantSearchPage + 1).ToString() + "."; ;
        }

        protected ModelApplicantSearchBar GetSearchValues()
        {
            ModelApplicantSearchBar data = new ModelApplicantSearchBar();
            ModelFreelancerList cimke_item = (toborzo_srccbx as ComboBox).SelectedItem as ModelFreelancerList;

            double listSize = Math.Round(applicant_listBox.RenderSize.Height / 55);


            string sorrend = " ASC";


            data = new ModelApplicantSearchBar
            {
                nev = nev_srcinp.Text,
                lakhely = lakhely_srcinp.Text,
                //tapasztalat = tapasztalat,
                bekuldo = (cimke_item != null ? cimke_item.id : 0),
                regdate = regdate_srcinp.Text,
                sorrend = sorrend,
                numberLimit = listSize
            };
            return data;
        }

        private void RefuseRecruitedApplicant(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Biztosan törölni szeretné? \n", "HR Cloud", MessageBoxButton.YesNo);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    ModelFreelancerApplicant applicant = (sender as Button).DataContext as ModelFreelancerApplicant;
                    Applicant.DeleteApplicant(applicant.id, true);
                    applicantListLoader();
                    break;
                case MessageBoxResult.No:
                    break;
            }
        }

        private void AcceptRecruitedApplicant(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Biztosan elfogadja a jelöltet? \n", "HR Cloud", MessageBoxButton.YesNo);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    ModelFreelancerApplicant applicant = (sender as Button).DataContext as ModelFreelancerApplicant;
                    Applicant.AcceptRecruited(applicant);
                    applicantListLoader();
                    break;
                case MessageBoxResult.No:
                    break;
            }
        }
    }
}
