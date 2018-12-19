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

namespace HR_Portal.View.Usercontrol.Panels
{
    /// <summary>
    /// Interaction logic for ProfessionPage.xaml
    /// </summary>
    public partial class ProfessionPage : UserControl
    {
        private Grid grid;
        public ProfessionPage(Grid grid)
        {
            this.grid = grid;
            InitializeComponent();
            abstractApplicant_ListBox.ItemsSource = new Profession().GetAll();
        }
    }
}
