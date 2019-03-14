using System;
using System.Windows;
using System.Windows.Input;
using HR_Portal.View.Usercontrol;
using System.Deployment;

namespace HR_Portal
{
    public partial class MainWindow : Window
    {
        private Login login;

        public MainWindow()
        {
            InitializeComponent();
            sgrid.Children.Add(login = new Login(sgrid));

        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            this.DragMove();
        }

        private void exit_btn_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
        
    }
}
