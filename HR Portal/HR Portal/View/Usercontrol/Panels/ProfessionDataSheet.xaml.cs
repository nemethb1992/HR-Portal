using HR_Portal.Public.templates;
using HR_Portal.Source;
using HR_Portal.Source.Model;
using HR_Portal.Source.Model.Applicant;
using HR_Portal.Source.Model.Project;
using HR_Portal.Source.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        Utility Utility = new Utility();
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

            cbx1.ItemsSource = Utility.Data_Nemek();
            cbx2.ItemsSource = Utility.Data_Vegzettseg();
            cbx3.ItemsSource = Utility.Data_Nyelv();
            cbx4.ItemsSource = Utility.Data_Ertesulesek();
            cbx_projekt.ItemsSource = new Applicant().Data_PorjectListSmall();
        }

        private void BackButton(object sender, RoutedEventArgs e)
        {
            Utility.NavigateTo(grid, new ProfessionPage(grid));
            
        }

        private void ProfessionAttachemntOpen(object sender, RoutedEventArgs e)
        {
            ModelJeloltFile item = (sender as Button).DataContext as ModelJeloltFile;
            Process.Start(item.path);
        }

        private void SaveApplicant(object sender, RoutedEventArgs e)
        {
            Profession prof = new Profession();
            data.szuldatum = input1.Text;
            data.lakhely = input2.Text;
            
            data.neme = (cbx1.SelectedIndex != -1 ? ((cbx1 as ComboBox).SelectedItem as ModelNem).id : 9999);
            data.vegzettseg = (cbx2.SelectedIndex != -1 ? ((cbx2 as ComboBox).SelectedItem as ModelVegzettseg).id : 9999);
            data.nyelvtudas = (cbx3.SelectedIndex != -1 ? ((cbx3 as ComboBox).SelectedItem as ModelNyelv).id : 9999);
            data.ertesult = (cbx4.SelectedIndex != -1 ? ((cbx4 as ComboBox).SelectedItem as ModelErtesulesek).id : 9999);
            data.projekt_id = (cbx_projekt.SelectedIndex != -1 ? ((cbx_projekt as ComboBox).SelectedItem as ModelSmallProject).id : 9999);
            int new_id = prof.Fullify(data);

            if (!data.projekt_id.Equals(9999))
            {
                new Applicant().AddToProject(new_id, data.projekt_id);
            }
            if (!new_id.Equals(0))
            {
                new Email().Send(data.email, new EmailTemplate().Udvozlo_Email(data.name));
                Utility.NavigateTo(grid, new ProfessionPage(grid));
            }
        }


        protected void numericTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
