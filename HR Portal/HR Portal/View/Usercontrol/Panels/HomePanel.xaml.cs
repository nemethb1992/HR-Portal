using HR_Portal.Public.templates;
using HR_Portal.Source;
using HR_Portal.Source.Model.Project;
using HR_Portal_Test.View.Usercontrol.Panels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HR_Portal.View.Usercontrol.Panels
{
    /// <summary>
    /// Interaction logic for HomePanel.xaml
    /// </summary>
    public partial class HomePanel : UserControl
    {
        private Grid grid;

        public HomePanel(Grid grid)
        {
            this.grid = grid;
            InitializeComponent();
            ButtonInfoLoad();
            //if (Session.UserData.kategoria >= 2)
            //    admin_btn.Visibility = Visibility.Visible;

            //new Email().Send("hrportal@pm-hungaria.com", new EmailTemplate().Teszt());
        }

        private void ButtonInfoLoad()
        {
            if (Session.UserData.kategoria == 2)
            {
                AdminBtn.Visibility = Visibility.Visible;
            }
            else
            {
                AdminBtn.Visibility = Visibility.Hidden;
            }
            Source.MySqlDB mySql = new Source.MySqlDB();
            projektek_list.ItemsSource = new Utilities().Data_PorjectListSmall();
            uj_jelolt_result.Text = mySql.Count("SELECT count(jeloltek.id) FROM jeloltek WHERE friss=1 AND jeloltek.reg_date > '"+Session.UserData.belepve+"'").ToString() + " db";
            nem_megnyitott_result.Text = mySql.Count("SELECT count(*) FROM jeloltek WHERE friss = 1 AND statusz = 0").ToString() +" db";
            osszes_jelolt_result.Text = mySql.Count("SELECT count(jeloltek.id) FROM jeloltek").ToString() + " db";
            projektben_jelolt_result.Text = mySql.Count("SELECT COUNT(DISTINCT jelolt_id) FROM projekt_jelolt_kapcs;").ToString() + " db";
            aktiv_projekt_result.Text = mySql.Count("SELECT count(id) FROM projektek WHERE statusz = 1;").ToString() + " db";
            //passziv_projekt_result.Text = mySql.Count("SELECT count(id) FROM projektek WHERE statusz = 0;").ToString() + " db";
            kapcsolt_projekt_result.Text = mySql.Count("SELECT count(projekt_id) FROM projekt_ertesitendok_kapcs LEFT JOIN projektek ON projekt_id = projektek.id WHERE statusz = 1 AND ertesitendok_id = " + Session.UserData.id + ";").ToString() + " db";
            //profession_list_result.Text = mySql.Count("SELECT count(id) FROM pmkcvtest.profession_jeloltek").ToString() + " db";
            mySql.Close(); 

        }

        private void ToProject(object sender, MouseButtonEventArgs e)
        {
            Utilities.NavigateTo(grid, new ProjectList(grid));
        }
        private void ToApplicant(object sender, MouseButtonEventArgs e)
        {
            Utilities.NavigateTo(grid, new ApplicantList(grid));
        }
        private void ToStatistics(object sender, MouseButtonEventArgs e)
        {
            Utilities.NavigateTo(grid, new StatisticsPage(grid));
        }
        private void ToSettings(object sender, MouseButtonEventArgs e)
        {
            Utilities.NavigateTo(grid, new SettingsPanel(grid));
        }
        private void ToAdmin(object sender, MouseButtonEventArgs e)
        {
            if(Session.UserData.kategoria == 2)
            Utilities.NavigateTo(grid, new AdminPage(grid));
        }
        private void ToFavorites(object sender, MouseButtonEventArgs e)
        {
            Utilities.NavigateTo(grid, new FavoritesPanel(grid));
        }

        private void OpenProjectClick(object sender, MouseButtonEventArgs e)
        {
            ModelSmallProject project = (sender as Grid).DataContext as ModelSmallProject;
            Session.ProjektID = project.id;
            Utilities.NavigateTo(grid, new ProjectDataSheet(grid, new Source.ViewModel.Project(project.id)));
        }

        private void ToRecruitment(object sender, RoutedEventArgs e)
        {
            Utilities.NavigateTo(grid, new RecruitedList(grid));
        }
    }
}
