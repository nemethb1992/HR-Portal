using HR_Portal.Source;
using HR_Portal.Source.Model.Applicant;
using HR_Portal.Source.Model.Project;
using HR_Portal.Source.ViewModel;
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

namespace HR_Portal.View.Usercontrol.Panels
{
    /// <summary>
    /// Interaction logic for favorites_panel.xaml
    /// </summary>
    public partial class FavoritesPanel : UserControl
    {
        private Grid grid;

        public FavoritesPanel(Grid grid)
        {
            this.grid = grid;
            InitializeComponent();
            LoadLists();
        }

        protected void LoadLists()
        {
            Interview interv = new Interview();
            list1.ItemsSource = interv.Data_MyPreviousInterviews();
            list2.ItemsSource = interv.Data_MyPastInterviews();
            list3.ItemsSource = Applicant.Data_FavoriteApplicants();
        }

        private void BackButton(object sender, RoutedEventArgs e)
        {
            Utilities.NavigateTo(grid, new HomePanel(grid));
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
            Utilities.SetReturnPage(Utilities.Views.FavoritePanel);
            Utilities.NavigateTo(grid, new ApplicantDataSheet(grid, new Applicant(items.id)));
        }

        private void Remove_from_favorit_Click(object sender, RoutedEventArgs e)
        {
            ModelApplicantList applicant = (sender as MenuItem).DataContext as ModelApplicantList;
            Applicant.DeleteFromFavorite(applicant.id);
            LoadLists();
        }

        protected void navigateToInterviewPanel(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            ModelInterview items = btn.DataContext as ModelInterview;

            Session.InterViewID = items.id;
            Utilities.SetReturnPage(Utilities.Views.FavoritePanel);
            Utilities.NavigateTo(grid, new InterviewPanel(grid, new Project(items.projekt_id), new Applicant(items.jelolt_id)));
        }
    }
}
