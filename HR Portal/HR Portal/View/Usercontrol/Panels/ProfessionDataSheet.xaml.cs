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
    /// Interaction logic for ProfessionDataSheet.xaml
    /// </summary>
    public partial class ProfessionDataSheet : UserControl
    {
        private Grid grid;
        private ModelProfession data;
        private ProfessionPage professionPage;
        public ProfessionDataSheet(Grid grid, ModelProfession data)
        {
            this.data = data;
            this.grid = grid;
            InitializeComponent();
            LoadUp();
        }
        protected void LoadUp()
        {
            ProfessionName.Text = data.name;
            app_input_1.Text = data.email;
            app_input_2.Text = data.telephone;
            app_input_3.Text = data.reg_date;
            app_input_4.Text = data.projekt;
            ProfessionAttachmentListBox.ItemsSource = Files.ReadProfession(data.id);
        }

        private void BackButton(object sender, RoutedEventArgs e)
        {
            grid.Children.Clear();
            grid.Children.Add(professionPage = new ProfessionPage(grid));
            
        }

        private void ProfessionAttachemntOpen(object sender, RoutedEventArgs e)
        {
            ModelJeloltFile item = (sender as Button).DataContext as ModelJeloltFile;
            Process.Start(item.path);
        }

        private void SaveApplicant(object sender, RoutedEventArgs e)
        {
            Profession prof = new Profession();
            prof.Fullify(data);
        }
    }
}
