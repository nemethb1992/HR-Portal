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
            projectListLoader();
        }

        protected void StartUp()
        {
            SetSearchValues();
            nyelv_srccbx.ItemsSource = Utility.Data_Nyelv();
            vegzettseg_srccbx.ItemsSource = Utility.Data_Vegzettseg();
            projectListLoader();
        }

        protected List<ModelProjectSearchBar> GetSearchValues()
        {
            List<ModelProjectSearchBar> list = new List<ModelProjectSearchBar>();
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

            list.Add(new ModelProjectSearchBar
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
            });
            return list;
        }

        protected void SetSearchValues()
        {
            if (Session.ProjectSearchValue == null)
                return;
            List<ModelProjectSearchBar> values = Session.ProjectSearchValue;

            projektnev_srcinp.Text = values[0].projektnev;
            jeloltnev_srcinp.Text = values[0].jeloltnev;
            pc_srcinp.Text = values[0].pc;
            publikalva_srcinp.Text = values[0].publikalva;
            cimke_srcinp.Text = values[0].cimke;
            publikalt_check.IsChecked = values[0].publikaltBool;
            nyelv_srccbx.SelectedIndex = values[0].nyelvIndex;
            vegzettseg_srccbx.SelectedIndex = values[0].vegzettsegIndex;

            if (values[0].jeloltszam == "0")
            {
                values[0].jeloltszam = "";
                jeloltszam_label.Visibility = Visibility.Visible;
            }
            else
                jeloltszam_srcinp.Text = values[0].jeloltszam;
            if (values[0].interjuk == "0")
            {
                values[0].interjuk = "";
                interju_label.Visibility = Visibility.Visible;
            }
            else
                interju_srcinp.Text = values[0].interjuk;


            if (values[0].projektnev.Length > 0)
                projektnev_label.Visibility = Visibility.Hidden;
            if (values[0].jeloltnev.Length > 0)
                jeloltnev_label.Visibility = Visibility.Hidden;
            if (values[0].jeloltszam.Length > 0)
                jeloltszam_label.Visibility = Visibility.Hidden;
            if (values[0].pc.Length > 0)
                pc_label.Visibility = Visibility.Hidden;
            if (values[0].interjuk.Length > 0)
                interju_label.Visibility = Visibility.Hidden;
            if (values[0].publikalva.Length > 0)
                publikalva_label.Visibility = Visibility.Hidden;
            if (values[0].cimke.Length > 0)
                cimke_label.Visibility = Visibility.Hidden;

        }

        protected void projectListLoader()
        {
            Session.ProjectStatusz = 1;
            buttonColorChange();

            try{
                List<ModelProjectList> lista = Project.GetProjectList(GetSearchValues());
                project_listBox.ItemsSource = lista;
                talalat_tbl.Text = "Találatok:  " + lista.Count.ToString();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        //protected List<ProjectListItems> projectListLoader2()
        //{
        //    List<Projekt_Search_Memory> list = new List<Projekt_Search_Memory>();
        //    List<ProjectListItems> lista = new List<ProjectListItems>();

        //    list.Add(new Projekt_Search_Memory() { statusz = 1 });
        //    pControl.projectSearchMemory = list;
        //    //buttonColorChange();
        //    lista = pControl.Data_ProjectFull(getSearchData());
        //    return lista;
        //}

        protected void buttonColorChange()
        {
            var bc = new BrushConverter();
            if (Session.ProjectStatusz == 1)
            {
                projekt_aktiv_btn.Background = (Brush)bc.ConvertFrom("#bfbfbf");
                projekt_aktiv_btn.BorderBrush = (Brush)bc.ConvertFrom("#bfbfbf");
                projekt_aktiv_btn.Foreground = (Brush)bc.ConvertFrom("#ffffff");
                projekt_passziv_btn.Background = (Brush)bc.ConvertFrom("#ffffff");
                projekt_passziv_btn.Foreground = (Brush)bc.ConvertFrom("#404040");
            }
            else
            {
                projekt_aktiv_btn.Background = (Brush)bc.ConvertFrom("#ffffff");
                projekt_aktiv_btn.Foreground = (Brush)bc.ConvertFrom("#404040");
                projekt_passziv_btn.Background = (Brush)bc.ConvertFrom("#bfbfbf");
                projekt_passziv_btn.BorderBrush = (Brush)bc.ConvertFrom("#bfbfbf");
                projekt_passziv_btn.Foreground = (Brush)bc.ConvertFrom("#ffffff");
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
                    projectListLoader();
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
                    projectListLoader();
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
            string Tbx_name = ((TextBox)sender).Tag.ToString();
            var Tbx = (TextBox)this.FindName(Tbx_name);

            if (((TextBox)sender).Text == "")
                Tbx.Visibility = Visibility.Hidden;
        }

        protected void textBoxPlaceHolderLostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textbox = sender as TextBox;
            TextBox textboxOther = (TextBox)this.FindName(textbox.Tag.ToString());

            if (((TextBox)sender).Text == "")
            {
                textboxOther.Visibility = Visibility.Visible;
                textbox.BorderBrush = (SolidColorBrush)Application.Current.Resources["racs_light"];
            }
            else
            {
                textbox.BorderBrush = (SolidColorBrush)Application.Current.Resources["ThemeColor"];
                textbox.Foreground = Brushes.Black;
            }
        }

        protected async void searchInputTextChange(object sender, TextChangedEventArgs e)
        {
            TextBox textbox = (TextBox)sender;
            int fisrtLength = textbox.Text.Length;

            await Task.Delay(500);
            if (fisrtLength == textbox.Text.Length)
            {
                projectListLoader();
            }
        }

        protected void comboboxSelection(object sender, SelectionChangedEventArgs e)
        {
            projectListLoader();
        }

        protected void publikaltChecked(object sender, RoutedEventArgs e)
        {
            projectListLoader();
        }

        protected void publikaltUnchecked(object sender, RoutedEventArgs e)
        {
            projectListLoader();
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
            projectListLoader();
        }

        protected void sorrendChecked(object sender, RoutedEventArgs e)
        {
            projectListLoader();
        }

        protected void sorrendUnchecked(object sender, RoutedEventArgs e)
        {
            projectListLoader();
        }
    }
}