using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using HR_Portal.Source;
using HR_Portal.Source.Model;
using HR_Portal.Source.Model.Applicant;
using HR_Portal.Source.Model.Project;
using HR_Portal.Source.ViewModel;
using HR_Portal_Test.Source.Utility;

namespace HR_Portal.View.Usercontrol.Panels
{
    /// <summary>
    /// Interaction logic for project_panel.xaml
    /// </summary>
    public partial class ProjectList : UserControl
    {
        private static string HeaderSelecteds;
        protected string HeaderSelected { get { return HeaderSelecteds; } set { HeaderSelecteds = value; } }

        Utilities Utility = new Utilities();
        
        private Grid grid;

        public ProjectList(Grid grid)
        {
            this.grid = grid;
            InitializeComponent();
            StartUp();
        }

        protected void ClearSearchbar(object sender, RoutedEventArgs e)
        {
            projektnev_srcinp.Text = "";
            jeloltszam_srcinp.Text = "";
            jeloltnev_srcinp.Text = "";
            pc_srcinp.Text = "";
            interju_srcinp.Text = "";
            publikalva_srcinp.Text = "";
            cimke_srcinp.Text = "";
            publikalt_check.IsChecked = false;
            nyelv_srccbx.SelectedIndex = -1;
            vegzettseg_srccbx.SelectedIndex = -1;

            projektnev_label.Visibility = Visibility.Visible;
            jeloltnev_label.Visibility = Visibility.Visible;
            jeloltszam_label.Visibility = Visibility.Visible;
            pc_label.Visibility = Visibility.Visible;
            interju_label.Visibility = Visibility.Visible;
            publikalva_label.Visibility = Visibility.Visible;
            cimke_label.Visibility = Visibility.Visible;

            Session.ProjectSearchValue = null;
            ProjectListLoad();
        }

        protected void StartUp()
        {
            SetSearchValues();
            nyelv_srccbx.ItemsSource = Utility.Data_Nyelv();
            vegzettseg_srccbx.ItemsSource = Utility.Data_Vegzettseg();
            ProjectListLoad();
        }

        protected ModelProjectSearchBar GetSearchValues()
        {
            ModelProjectSearchBar value = new ModelProjectSearchBar();
            ModelNyelv nyelvItem = null;
            ModelVegzettseg vegzettsegItem = null;
            string nyelvkStr = "";
            string vegzettsegStr = "";

            try
            {
                nyelvItem = (nyelv_srccbx as ComboBox).SelectedItem as ModelNyelv;
                vegzettsegItem = (vegzettseg_srccbx as ComboBox).SelectedItem as ModelVegzettseg;
            }
            catch (Exception){
            }

            try  { if(vegzettsegItem.id !=-1) vegzettsegStr = vegzettsegItem.id.ToString(); } catch (Exception) { }

            try  { if (nyelvItem.id != -1) nyelvkStr = nyelvItem.id.ToString(); } catch (Exception)  {}

            string jeloltszam = jeloltszam_srcinp.Text;
            if (jeloltszam_srcinp.Text == "")
                jeloltszam = "0";

            string interjuk = interju_srcinp.Text;
            if (interju_srcinp.Text == "")
                interjuk = "0";

            string publikalt = "";
            if (publikalt_check.IsChecked == true)
                publikalt = "1";

            string sorrend = " ASC";
            if (sorrend_check.IsChecked == true)
                sorrend = " DESC";

            value = new ModelProjectSearchBar
            {
                projektnev = projektnev_srcinp.Text,
                jeloltszam = jeloltszam,
                publikalva = publikalva_srcinp.Text,
                interjuk = interjuk,
                pc = pc_srcinp.Text,
                nyelvkStr = nyelvkStr,
                nyelvIndex = nyelv_srccbx.SelectedIndex,
                vegzettsegStr = vegzettsegStr,
                vegzettsegIndex = vegzettseg_srccbx.SelectedIndex,
                cimke = cimke_srcinp.Text,
                jeloltnev  = jeloltnev_srcinp.Text,
                publikalt = publikalt,
                publikaltBool = publikalt_check.IsChecked.Value,
                HeaderSelected = HeaderSelected,
                sorrend = sorrend
            };
            return value;
        }

        protected void SetSearchValues()
        {
            if (Session.ProjectSearchValue == null)
                return;
            ModelProjectSearchBar value = Session.ProjectSearchValue;

            projektnev_srcinp.Text = value.projektnev;
            jeloltnev_srcinp.Text = value.jeloltnev;
            pc_srcinp.Text = value.pc;
            publikalva_srcinp.Text = value.publikalva;
            cimke_srcinp.Text = value.cimke;
            publikalt_check.IsChecked = value.publikaltBool;
            nyelv_srccbx.SelectedIndex = value.nyelvIndex;
            vegzettseg_srccbx.SelectedIndex = value.vegzettsegIndex;

            if (value.jeloltszam == "0")
            {
                value.jeloltszam = "";
                jeloltszam_label.Visibility = Visibility.Visible;
            }
            else
                jeloltszam_srcinp.Text = value.jeloltszam;
            if (value.interjuk == "0")
            {
                value.interjuk = "";
                interju_label.Visibility = Visibility.Visible;
            }
            else
                interju_srcinp.Text = value.interjuk;


            if (value.projektnev.Length > 0)
                projektnev_label.Visibility = Visibility.Hidden;
            if (value.jeloltnev.Length > 0)
                jeloltnev_label.Visibility = Visibility.Hidden;
            if (value.jeloltszam.Length > 0)
                jeloltszam_label.Visibility = Visibility.Hidden;
            if (value.pc.Length > 0)
                pc_label.Visibility = Visibility.Hidden;
            if (value.interjuk.Length > 0)
                interju_label.Visibility = Visibility.Hidden;
            if (value.publikalva.Length > 0)
                publikalva_label.Visibility = Visibility.Hidden;
            if (value.cimke.Length > 0)
                cimke_label.Visibility = Visibility.Hidden;

        }

        protected void ProjectListLoad()
        {
            Session.ProjectStatusz = 1;
            buttonColorChange();

            try{
                List<ModelProjectList> lista = Project.GetProjectList(GetSearchValues());
                project_listBox.ItemsSource = lista;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        protected void buttonColorChange()
        {
            var bc = new BrushConverter();
            if (Session.ProjectStatusz == 1)
            {
                projekt_aktiv_btn.Background = (Brush)bc.ConvertFrom("#ffe6e6");
                projekt_passziv_btn.Background = (Brush)bc.ConvertFrom("#ffffff");
            }
            else
            {
                projekt_aktiv_btn.Background = (Brush)bc.ConvertFrom("#ffffff");
                projekt_passziv_btn.Background = (Brush)bc.ConvertFrom("#ffe6e6");
            }
        }

        protected void projectOpenClick(object sender, RoutedEventArgs e)
        {
            ModelProjectList items = (sender as Button).DataContext as ModelProjectList;
            Session.ProjektID = items.id;
            Utilities.SetReturnPage(Utilities.Views.ProjectList);
            Session.ProjectSearchValue = GetSearchValues();
            Utilities.NavigateTo(grid, new ProjectDataSheet(grid, new Project(items.id)));
        }

        protected void Numeric(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        protected void projectDeleteClick(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Biztosan törölni szeretnéd? \n", "HR Cloud", MessageBoxButton.YesNoCancel);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    ModelProjectList items = (sender as MenuItem).DataContext as ModelProjectList;
                    Project.Delete(items.id);
                    ProjectListLoad();
                    break;
                case MessageBoxResult.No:
                    break;
                case MessageBoxResult.Cancel:
                    break;
            }
        }

        protected void projectArchivateClick(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Biztosan módosítani szeretnéd?", "HR Cloud", MessageBoxButton.YesNoCancel);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    ModelProjectList items = (sender as MenuItem).DataContext as ModelProjectList;
                    new Project(items.id).projectArchiver(items.id, items.statusz);
                    ProjectListLoad();
                    break;
                case MessageBoxResult.No:
                    break;
                case MessageBoxResult.Cancel:
                    break;
            }
        }

        protected void projectPassivateClick(object sender, RoutedEventArgs e)
        {
            Project.ProjectStatusChange(0);
            project_listBox.ItemsSource = Project.GetProjectList(GetSearchValues());
            buttonColorChange();
        }

        protected void projectActivateClick(object sender, RoutedEventArgs e)
        {
            Project.ProjectStatusChange(1);
            project_listBox.ItemsSource = Project.GetProjectList(GetSearchValues());
            buttonColorChange();
        }

        protected void textBoxPlaceHolderGotFocus(object sender, RoutedEventArgs e)
        {
            ViewTools.SearchInputGotFocus(sender as TextBox, (TextBox)this.FindName((sender as TextBox).Tag.ToString()));
        }

        protected void SearchLostFocus(object sender, RoutedEventArgs e)
        {
            ViewTools.SearchInputLostFocus(sender as TextBox, (TextBox)this.FindName((sender as TextBox).Tag.ToString()));
        }

        protected async void searchInputTextChange(object sender, TextChangedEventArgs e)
        {
            int length = ViewTools.TextBoxLength(sender as TextBox);

            await Task.Delay(500);
            if (length == ViewTools.TextBoxLength(sender as TextBox))
            {
                ProjectListLoad();
            }
        }

        protected void comboboxSelection(object sender, SelectionChangedEventArgs e)
        {
            ProjectListLoad();
        }

        protected void publikaltChecked(object sender, RoutedEventArgs e)
        {
            ProjectListLoad();
        }

        protected void publikaltUnchecked(object sender, RoutedEventArgs e)
        {
            ProjectListLoad();
        }

        protected void New_projekt_btn_Click(object sender, RoutedEventArgs e)
        {
            Session.isUpdate = false;
            Utilities.SetReturnPage(Utilities.Views.ProjectList);
            Session.ProjectSearchValue = GetSearchValues();
            Utilities.NavigateTo(grid, new NewProjectPanel(grid));
        }

        protected void modositasClick(object sender, RoutedEventArgs e)
        {
            Session.isUpdate = true;
            ModelProjectList itemSource = (sender as MenuItem).DataContext as ModelProjectList;

            Session.ProjektID = itemSource.id;
            Utilities.NavigateTo(grid, new NewProjectPanel(grid));
        }

        protected void headerClick(object sender, MouseButtonEventArgs e)
        {
            HeaderSelected = (sender as Label).Tag.ToString();
            ProjectListLoad();
        }

        protected void sorrendChecked(object sender, RoutedEventArgs e)
        {
            ProjectListLoad();
        }

        protected void sorrendUnchecked(object sender, RoutedEventArgs e)
        {
            ProjectListLoad();
        }
    }
}