using HR_Portal.Source;
using HR_Portal.Source.Model;
using HR_Portal.Source.Model.Other;
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
    /// Interaction logic for SettingsPanel.xaml
    /// </summary>
    public partial class SettingsPanel : UserControl
    {
        Utilities util = new Utilities();

        private Grid grid;

        public SettingsPanel(Grid grid)
        {
            this.grid = grid;
            InitializeComponent();
            listLoader();
        }
        protected void listLoader()
        {
            freelancer_editlist.ItemsSource = util.Data_Freelancer();
            ertesitendok_editlist.ItemsSource = util.Data_Ertesitendok();
            vegzettsegek_editlist.ItemsSource = util.Data_Vegzettseg();
            munkakorok_editlist.ItemsSource = util.Data_Munkakorok();
            pc_editlist.ItemsSource = util.Data_Pc();
            ertesules_editlist.ItemsSource = util.Data_Ertesulesek();
            nyelv_editlist.ItemsSource = util.Data_Nyelv();
            cimkek_editlist.ItemsSource = util.Data_Cimkek();
            kompetencia_editlist.ItemsSource = Interview.Data_Kompetencia();
        }

        protected void settingsInputGotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tbx = sender as TextBox;

            if (tbx.Text == tbx.Tag.ToString())
            {
                tbx.Text = "";
            }
        }

        protected void settingsInputLostFocus(object sender, RoutedEventArgs e)
        {
            TextBox tbx = sender as TextBox;

            if (tbx.Text == "")
            {
                tbx.Text = tbx.Tag.ToString();
            }
        }
        
        protected void DeleteListItem(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Biztosan törölni szeretnéd? \n", "HR Cloud", MessageBoxButton.YesNoCancel);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    MenuItem menuItem = sender as MenuItem;
                    ModelVegzettseg items = menuItem.DataContext as ModelVegzettseg;
                    int id = 9999;
                    switch (menuItem.Tag.ToString())
                    {
                        case "munkakor":
                            id = (menuItem.DataContext as ModelMunkakor).id;
                            break;
                        case "pc":
                            id = (menuItem.DataContext as ModelPc).id;
                            break;
                        case "ertesulesek":
                            id = (menuItem.DataContext as ModelErtesulesek).id;
                            break;
                        case "vegzettsegek":
                            id = (menuItem.DataContext as ModelVegzettseg).id;
                            break;
                        case "nyelv":
                            id = (menuItem.DataContext as ModelNyelv).id;
                            break;
                        case "kompetenciak":
                            id = (menuItem.DataContext as ModelKompetenciak).id;
                            break;
                        case "jelolt_cimkek":
                            id = (menuItem.DataContext as ModelCimkek).id;
                            break;
                        case "freelancer_list":
                            id = (menuItem.DataContext as ModelFreelancerList).id;
                            break;
                        default:
                            break;
                    }
                    if (id != 9999)
                        util.Delete(id, menuItem.Tag.ToString());
                    listLoader();
                    break;
                case MessageBoxResult.No:
                    break;
                case MessageBoxResult.Cancel:
                    break;
            }
        }

        protected void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (vegzettsegek_new_tbx.Text != "" && vegzettsegek_new_tbx.Text != "Új hozzáadása")
            {
                util.SettingsInsert(vegzettsegek_new_tbx.Text, "vegzettsegek");
                vegzettsegek_new_tbx.Text = "Új hozzáadása";
                listLoader();
            }
        }

        protected void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (munkakorok_new_tbx.Text != "" && munkakorok_new_tbx.Text != "Új hozzáadása")
            {
                util.SettingsInsert(munkakorok_new_tbx.Text, "munkakor");
                munkakorok_new_tbx.Text = "Új hozzáadása";
                listLoader();
            }
        }

        protected void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (pc_new_tbx.Text != "" && pc_new_tbx.Text != "Új hozzáadása")
            {
                util.SettingsInsert(pc_new_tbx.Text, "pc");
                pc_new_tbx.Text = "Új hozzáadása";
                listLoader();
            }
        }

        protected void Button_Click_4(object sender, RoutedEventArgs e)
        {
            if (ertesules_new_tbx.Text != "" && ertesules_new_tbx.Text != "Új hozzáadása")
            {
                util.SettingsInsert(ertesules_new_tbx.Text, "ertesulesek");
                ertesules_new_tbx.Text = "Új hozzáadása";
                listLoader();
            }
        }

        protected void Button_Click_5(object sender, RoutedEventArgs e)
        {
            if (nyelv_new_tbx.Text != "" && nyelv_new_tbx.Text != "Új hozzáadása")
            {
                util.SettingsInsert(nyelv_new_tbx.Text, "nyelv");
                nyelv_new_tbx.Text = "Új hozzáadása";
                listLoader();
            }
        }

        protected void Button_Click_6(object sender, RoutedEventArgs e)
        {
            if (kompetencia_new_tbx.Text != "" && kompetencia_new_tbx.Text != "Új hozzáadása")
            {
                util.SettingsInsert(kompetencia_new_tbx.Text, "kompetenciak");
                kompetencia_new_tbx.Text = "Új hozzáadása";
                listLoader();
            }
        }
        private void Add_cimke_click(object sender, RoutedEventArgs e)
        {
            if (cimkek_new_tbx.Text != "" && cimkek_new_tbx.Text != cimkek_new_tbx.Tag.ToString())
            {
                util.SettingsInsert(cimkek_new_tbx.Text, "cimkek");
                cimkek_new_tbx.Text = cimkek_new_tbx.Tag.ToString();
                listLoader();
            }
        }
        protected void Add_freelancer_click(object sender, RoutedEventArgs e)
        {
            if (freelancer_name_tbx.Text != "" && freelancer_name_tbx.Text != freelancer_name_tbx.Tag.ToString() &&
                freelancer_email_tbx.Text != "" && freelancer_email_tbx.Text != freelancer_email_tbx.Tag.ToString())
            {
                util.SettingsInsert(freelancer_name_tbx.Text, "freelancer" , freelancer_email_tbx.Text);
                freelancer_name_tbx.Text = freelancer_name_tbx.Tag.ToString();
                freelancer_email_tbx.Text = freelancer_email_tbx.Tag.ToString();
                listLoader();
            }
        }

        private void PressStatButton(object sender, RoutedEventArgs e)
        {
            
            string button = (sender as Button).Tag.ToString();
            switch (button)
            {
                case "1":
                    {
                        //new ExcelMethod().Stat1((stat_week_cbx.SelectedIndex.Equals(-1)? 5 : Convert.ToInt32(stat_week_cbx.SelectedIndex.ToString())+1));
                        break;
                    }
                default:
                    break;
            }
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Grid grid = sender as Grid;
            ModelFreelancerList data = grid.DataContext as ModelFreelancerList;
            freel_name.Text = data.name;
            freel_link.Text = "https://web.phoenix-mecano.hu/recruitment?rid="+ data.rid;
        }
    }
}
