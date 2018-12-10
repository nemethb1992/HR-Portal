using HR_Portal.Source;
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
    /// Interaction logic for HomePanel.xaml
    /// </summary>
    public partial class HomePanel : UserControl
    {
        private Grid grid;
        private ApplicantList applicant_p;
        private ProjectList project_p;
        private SettingsPanel settings_p;
        private AdminPage adminPage;


        public HomePanel(Grid grid)
        {
            this.grid = grid;
            InitializeComponent();
            ButtonInfoLoad();
            //if (Session.UserData.kategoria >= 2)
            //    admin_btn.Visibility = Visibility.Visible;

        }

        private void ButtonInfoLoad()
        {
            uj_jelolt_result.Text = Source.MySql.Count("SELECT count(jeloltek.id) FROM jeloltek WHERE friss=1 AND jeloltek.reg_date > '"+Session.UserData.belepve+"'").ToString() + " db";
            nem_megnyitott_result.Text = Source.MySql.Count("SELECT count(jeloltek.id) FROM jeloltek WHERE friss = 1").ToString() +" db";
            osszes_jelolt_result.Text = Source.MySql.Count("SELECT count(jeloltek.id) FROM jeloltek").ToString() + " db";
            projektben_jelolt_result.Text = Source.MySql.Count("SELECT COUNT(DISTINCT jelolt_id) FROM projekt_jelolt_kapcs;").ToString() + " db";
            aktiv_projekt_result.Text = Source.MySql.Count("SELECT count(id) FROM projektek WHERE statusz = 1;").ToString() + " db";


        }

        private void ToProject(object sender, MouseButtonEventArgs e)
        {
            grid.Children.Clear();
            grid.Children.Add(project_p = new ProjectList(grid));
        }
        private void ToApplicant(object sender, MouseButtonEventArgs e)
        {
            grid.Children.Clear();
            grid.Children.Add(applicant_p = new ApplicantList(grid));
        }
        private void ToSettings(object sender, MouseButtonEventArgs e)
        {
            grid.Children.Clear();
            grid.Children.Add(settings_p = new SettingsPanel(grid));
        }
        private void ToAdmin(object sender, MouseButtonEventArgs e)
        {
            grid.Children.Clear();
            grid.Children.Add(adminPage = new AdminPage(grid));
        }
    }
}
