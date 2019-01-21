using HR_Portal.Public.templates;
using HR_Portal.Source;
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
            Source.MySql mySql = new Source.MySql();
            uj_jelolt_result.Text = mySql.Count("SELECT count(jeloltek.id) FROM jeloltek WHERE friss=1 AND jeloltek.reg_date > '"+Session.UserData.belepve+"'").ToString() + " db";
            nem_megnyitott_result.Text = mySql.Count("SELECT count(jeloltek.id) FROM jeloltek WHERE friss = 1").ToString() +" db";
            osszes_jelolt_result.Text = mySql.Count("SELECT count(jeloltek.id) FROM jeloltek").ToString() + " db";
            projektben_jelolt_result.Text = mySql.Count("SELECT COUNT(DISTINCT jelolt_id) FROM projekt_jelolt_kapcs;").ToString() + " db";
            aktiv_projekt_result.Text = mySql.Count("SELECT count(id) FROM projektek WHERE statusz = 1;").ToString() + " db";
            mySql.Close();

        }

        private void ToProject(object sender, MouseButtonEventArgs e)
        {
            Utility.NavigateTo(grid, new ProjectList(grid));
        }
        private void ToApplicant(object sender, MouseButtonEventArgs e)
        {
            Utility.NavigateTo(grid, new ApplicantList(grid));
        }
        private void ToProfession(object sender, MouseButtonEventArgs e)
        {
            Utility.NavigateTo(grid, new ProfessionPage(grid));
        }
        private void ToSettings(object sender, MouseButtonEventArgs e)
        {
            Utility.NavigateTo(grid, new SettingsPanel(grid));
        }
        private void ToAdmin(object sender, MouseButtonEventArgs e)
        {
            Utility.NavigateTo(grid, new AdminPage(grid));
        }
    }
}
