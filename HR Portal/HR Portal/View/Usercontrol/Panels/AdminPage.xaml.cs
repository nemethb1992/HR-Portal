using HR_Portal.Source;
using HR_Portal.Source.Model;
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
    /// Interaction logic for AdminPage.xaml
    /// </summary>
    public partial class AdminPage : UserControl
    {
        private Grid grid; 
        private UserDataSheet userDataSheet; 

        public AdminPage(Grid grid)
        {
            this.grid = grid;
            InitializeComponent();
            StartUp();
        }

        protected void StartUp()
        {
            UserListLoadup();
        }

        protected void UserListLoadup()
        {
            UserListBox.ItemsSource = UserData.GetAll();
        }

        private void UserValidationUnchecked(object sender, RoutedEventArgs e)
        {
            ModelUserData item = (sender as CheckBox).DataContext as ModelUserData;
            UserData.ModifyValidation(item.id,false);
        }

        private void UserValidationChecked(object sender, RoutedEventArgs e)
        {
            ModelUserData item = (sender as CheckBox).DataContext as ModelUserData;
            UserData.ModifyValidation(item.id, true);
        }

        private void applicant_open_btn_Click(object sender, RoutedEventArgs e)
        {
            ModelUserData item = (sender as Button).DataContext as ModelUserData;
            grid.Children.Clear();
            grid.Children.Add(userDataSheet = new UserDataSheet(grid, item.id));
        }
    }
}
