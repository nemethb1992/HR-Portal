using HR_Portal.Source;
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

namespace HR_Portal.View.Usercontrol.Panels.SzakmaiLayouts
{
    /// <summary>
    /// Interaction logic for Szakmai_Kezdolap.xaml
    /// </summary>
    public partial class SzakmaiKezdolap : UserControl
    {
        ControlProject pControl = new ControlProject();

        private SzakmaiInterviewList szakmaiInterviewList;
        private SzakmaiList szakmaiList;
        private Grid grid;

        public SzakmaiKezdolap(Grid grid)
        {
            InitializeComponent();
            this.grid = grid;
            StartUp();
        }
        protected void StartUp()
        {
            interju_no.Text = VMSzakmai.Data_SzakmaiInterview().Count.ToString() + " db";
            projekt_no.Text = VMSzakmai.Data_SzakmaiProject().Count.ToString() + " db";
        }

        protected void navigateToSzakmaiList(object sender, RoutedEventArgs e)
        {
            grid.Children.Clear();
            grid.Children.Add(szakmaiList = new SzakmaiList(grid));
        }

        protected void navigateToSzakmaiInterviewList(object sender, RoutedEventArgs e)
        {
            grid.Children.Clear();
            grid.Children.Add(szakmaiInterviewList = new SzakmaiInterviewList(grid));
        }
    }
}
