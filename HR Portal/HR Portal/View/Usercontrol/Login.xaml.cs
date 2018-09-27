using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using HR_Portal.Control;
using HR_Portal.Source;
using HR_Portal.View.Windows;

namespace HR_Portal.View.Usercontrol
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : UserControl
    {
        ControlLogin lcontrol = new ControlLogin();

        private Grid grid;

        public Login(Grid sgrid)
        {
            this.grid = sgrid;
            InitializeComponent();

            setartUp();
        }

        private bool dbConnectionOpener()
        {
            Source.MySql mySql = new Source.MySql();
            bool conn = Source.MySql.isConnected();
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
            string user = lcontrol.getRememberedUser();
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
                lcontrol.writeRememberedUser(Luser_tbx.Text);
            }
            else
            {
                lcontrol.deleteRememberedUser();
            }
        }

        private void enter()
        {
            if (dbConnectionOpener())
            {
                //if (ActiveDirecotry.Bind(Luser_tbx.Text, Lpass_pwd.Password))
                //{
                if (lcontrol.mySqlUserValidation(Luser_tbx.Text))
                {
                    Session.ActiveDirectoryDomain = Luser_tbx.Text;
                    Session.UserData = VMUserData.getUserSession();
                    Main mw = new Main();
                    var window = Window.GetWindow(this);
                    usernameRemember();
                    mw.Show();
                    window.Close();
                }
                //    else
                //    {
                //        LoginSign.Text = "Kérem regisztráljon!";
                //    }
                //}
                //    else
                //    {
                //        LoginSign.Text = "Sikertelen hitelesítés!";
                //    }
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
