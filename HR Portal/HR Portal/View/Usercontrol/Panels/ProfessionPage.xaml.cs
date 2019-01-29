using HR_Portal.Source;
using HR_Portal.Source.Model.Applicant;
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

        private void OpenProfessionApplicant(object sender, RoutedEventArgs e)
        {
            ModelProfession item = (sender as Button).DataContext as ModelProfession;
            Utilities.NavigateTo(grid, new ProfessionDataSheet(grid,item));
        }

        private void DiscardProfessionApplicant(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("  Biztosan törli a jelentkezőt?", "HR Portal", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
            {
                ModelProfession item = (sender as Button).DataContext as ModelProfession;
                Files.DeleteProfessionFolder(item.id);
                Profession prof = new Profession();
                prof.Delete(item.id);
                abstractApplicant_ListBox.ItemsSource = prof.GetAll();
            }
        }
    }
}
