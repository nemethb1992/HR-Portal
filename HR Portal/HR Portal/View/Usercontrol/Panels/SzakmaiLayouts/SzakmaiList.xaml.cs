using HR_Portal.Source;
using HR_Portal.Source.ViewModel;
using HR_Portal.View.Usercontrol.Panels.SzakmaiLayouts;
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
    /// Interaction logic for Szakmai_panel.xaml
    /// </summary>
    public partial class SzakmaiList : UserControl
    {

        private SzakmaiProjektDataSheet szakmaiProjektDataSheet;
        private SzakmaiKezdolap szakmaiKezdolap;
        private Grid grid;

        public SzakmaiList(Grid grid)
        {
            InitializeComponent();
            this.grid = grid;
            settingUp();
        }

        protected void openProject(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            ModelSzakmaiBevont items = button.DataContext as ModelSzakmaiBevont;

            Session.ProjektID = items.id;
            grid.Children.Clear();
            grid.Children.Add(szakmaiProjektDataSheet = new SzakmaiProjektDataSheet(grid));
        }

        protected void settingUp()
        {
            bevont_projekt_lista.ItemsSource = Szakmai.Data_SzakmaiProject();
        }

        protected void navigateToSzakmaiKezdolap(object sender, RoutedEventArgs e)
        {
            grid.Children.Clear();
            grid.Children.Add(szakmaiKezdolap = new SzakmaiKezdolap(grid));
        }
    }
}
