using HR_Portal.Source;
using HR_Portal.View.Usercontrol.Panels;
using System.Windows;
using System.Windows.Input;
using HR_Portal.View.Usercontrol.Panels.SzakmaiLayouts;

namespace HR_Portal.View.Windows
{
    /// <summary>
    /// Interaction logic for Main.xaml
    /// </summary>
    public partial class Main : Window
    {

        private SzakmaiKezdolap szakmai_Kezdolap;
        private HomePanel homePanel;
        //private applicant_DataView applicant_dv;
        //private project_DataView project_dv;
        public Main()
        {
            InitializeComponent();
            if (Session.UserData.kategoria >= 1)
            {
                grid.Children.Add(homePanel = new HomePanel(grid));
                HR_navigation_Grid.Visibility = Visibility.Visible;
            }
            else
            {
                grid.Children.Add(szakmai_Kezdolap = new SzakmaiKezdolap(grid));
                Szakmai_navigation_Grid.Visibility = Visibility.Visible;
                HR_navigation_Grid.Visibility = Visibility.Hidden;
            }
            //f_control.Applicant_Folder_Structure_Creator();
            //f_control.Projekt_Folder_Structure_Creator();
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
        private void logout_btn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            var window = Window.GetWindow(this);
            window.Close();
            mw.Show();
        }

        private void szakmai_mainpage_btn_Click(object sender, RoutedEventArgs e)
        {
            grid.Children.Clear();
            grid.Children.Add(szakmai_Kezdolap = new SzakmaiKezdolap(grid));
        }



        private void Maximize_Window_Click(object sender, RoutedEventArgs e)
        {
            Window window = Window.GetWindow(this);
            if (window.WindowState == WindowState.Normal)
            {
                window.WindowState = WindowState.Maximized;
            }
            else
            {
                window.WindowState = WindowState.Normal;
            }

        }

        private void HomeButtonClick(object sender, RoutedEventArgs e)
        {
            grid.Children.Clear();
            grid.Children.Add(homePanel = new HomePanel(grid));
        }

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    login_cont lcont = new login_cont();
        //    string user = testtbx.Text;
        //    string pass = testtbx2.Text;
        //    bool valid = lcont.ActiveDirectoryValidation(user,pass);
        //    if (valid == true)
        //    {
        //        info.Text = "Siker";
        //    }
        //    else
        //    {
        //        info.Text = "Nem";
        //    }
        //}
    }
}
