using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using HR_Portal.Public.templates;
using HR_Portal.Source;
using HR_Portal.Source.ViewModel;
using HR_Portal.View.Windows;

namespace HR_Portal.View.Usercontrol
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : UserControl
    {

        private Grid grid;

        public Login(Grid sgrid)
        {
            this.grid = sgrid;
            InitializeComponent();
            //setartUp();
        }

        private bool dbConnectionOpener()
        {
            bool conn = Source.MySql.IsConnected();
            return conn;
        }

        private void loginEnterClick(object sender, RoutedEventArgs e)
        {
            enter();
        }
        
        private void usernameEnterKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter) return;
            e.Handled = true;
            enter();
        }

        private void passwordEnterKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter) return;
            e.Handled = true;
            enter();
        }

        private void setartUp()
        {
            string user = Source.ViewModel.Login.GetSavedUser();
            if (user != "")
            {
                Luser_tbx.Text = user;
                login_cbx.IsChecked = true;
            }
            else
            {
                login_cbx.IsChecked = false;
            }
        }

        private void usernameRemember()
        {
            if (login_cbx.IsChecked == true)
            {
                Source.ViewModel.Login.SaveUser(Luser_tbx.Text);
            }
            else
            {
                Source.ViewModel.Login.DeleteSavedUser();
            }
        }

        private void enter()
        {
            if (dbConnectionOpener())
            {
                if (ActiveDirecotry.Bind(Luser_tbx.Text, Lpass_pwd.Password))
                {
                    Session.ActiveDirectoryDomain = Luser_tbx.Text;
                 if (Source.ViewModel.Login.Authentication(Luser_tbx.Text))
                    {
                    Main mw = new Main();
                    var window = Window.GetWindow(this);
                    //usernameRemember();
                    mw.Show();
                    window.Close();
                    }
                    else
                    {
                        LoginSign.Text = "Kérem regisztráljon!";
                    }
            }
            else
            {
                LoginSign.Text = "Sikertelen hitelesítés!";
            }
        }
            else
            {
                LoginSign.Text = "Nincs adatkapcsolat!";
            }
        }

        private void navigateToSurveyWindow(object sender, MouseButtonEventArgs e)
        {
            SurveyWindow popup = new SurveyWindow();
            var window = Window.GetWindow(this);
            popup.Show();
            window.Close();
        }       
    }
}
