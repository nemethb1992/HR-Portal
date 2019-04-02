using HR_Portal.Source;
using HR_Portal.Source.Model;
using HR_Portal.Source.Model.Applicant;
using HR_Portal.Source.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Interaction logic for StatisticsPage.xaml
    /// </summary>
    public partial class StatisticsPage : UserControl
    {
        private Grid grid;
        public StatisticsPage(Grid grid)
        {
            this.grid = grid;
            InitializeComponent();
            LoadUp();
        }

        private void LoadUp()
        {
            automaticStatisticsList.ItemsSource = Files.ReadStatistics("JeloltEloszlas");
            //StatisticTypeCbx.Items.Add("Jelentkezők Projektenkénti eloszlása");
            StatisticTypeCbx.Items.Add(new ModelStatList { type = "JeloltEloszlas", title = "Jelentkezők Projektenkénti eloszlása" });
            StatisticTypeCbx.SelectedIndex = 0;
        }

        protected void attachmentOpenClick(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            ModelFile item = btn.DataContext as ModelFile;
            Process.Start(item.path);
        }

    }
}
