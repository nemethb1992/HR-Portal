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
        Utility Utility = new Utility();

        private Grid grid;

        public SettingsPanel(Grid grid)
        {
            this.grid = grid;
            InitializeComponent();
            listLoader();
        }
        protected void listLoader()
        {
            ertesitendok_editlist.ItemsSource = Utility.Data_Ertesitendok();
            vegzettsegek_editlist.ItemsSource = Utility.Data_Vegzettseg();
            munkakorok_editlist.ItemsSource = Utility.Data_Munkakorok();
            pc_editlist.ItemsSource = Utility.Data_Pc();
            ertesules_editlist.ItemsSource = Utility.Data_Ertesulesek();
            nyelv_editlist.ItemsSource = Utility.Data_Nyelv();
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
        
        protected void vegzettseg(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Biztosan törölni szeretnéd? \n", "HR Cloud", MessageBoxButton.YesNoCancel);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    MenuItem menuItem = sender as MenuItem;
                    ModelVegzettseg items = menuItem.DataContext as ModelVegzettseg;
                    if (items.megnevezes_vegzettseg != "Összes")
                        Utility.Delete(items.id, "vegzettsegek");
                    listLoader();
                    break;
                case MessageBoxResult.No:
                    break;
                case MessageBoxResult.Cancel:
                    break;
            }
        }

        protected void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Biztosan törölni szeretnéd? \n", "HR Cloud", MessageBoxButton.YesNoCancel);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    MenuItem menuItem = sender as MenuItem;
                    ModelMunkakor items = menuItem.DataContext as ModelMunkakor;
                    if (items.munkakor != "Összes")
                        Utility.Delete(items.id, "munkakor");
                    listLoader();
                    break;
                case MessageBoxResult.No:
                    break;
                case MessageBoxResult.Cancel:
                    break;
            }
        }

        protected void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Biztosan törölni szeretnéd? \n", "HR Cloud", MessageBoxButton.YesNoCancel);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    MenuItem menuItem = sender as MenuItem;
                    ModelPc items = menuItem.DataContext as ModelPc;
                    if (items.megnevezes_pc != "Összes")
                        Utility.Delete(items.id, "pc");
                    listLoader();
                    break;
                case MessageBoxResult.No:
                    break;
                case MessageBoxResult.Cancel:
                    break;
            }
        }

        protected void MenuItem_Click_4(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Biztosan törölni szeretnéd? \n", "HR Cloud", MessageBoxButton.YesNoCancel);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    MenuItem menuItem = sender as MenuItem;
                    ModelErtesulesek items = menuItem.DataContext as ModelErtesulesek;
                    if (items.ertesules_megnevezes != "Összes")
                        Utility.Delete(items.id, "ertesulesek");
                    listLoader();
                    break;
                case MessageBoxResult.No:
                    break;
                case MessageBoxResult.Cancel:
                    break;
            }
        }

        protected void MenuItem_Click_5(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Biztosan törölni szeretnéd? \n", "HR Cloud", MessageBoxButton.YesNoCancel);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    MenuItem menuItem = sender as MenuItem;
                    ModelNyelv items = menuItem.DataContext as ModelNyelv;
                    if (items.nyelv != "Összes")
                        Utility.Delete(items.id, "nyelv");
                    listLoader();
                    break;
                case MessageBoxResult.No:
                    break;
                case MessageBoxResult.Cancel:
                    break;
            }
        }

        protected void kompetenciaDelete(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Biztosan törölni szeretnéd? \n", "HR Cloud", MessageBoxButton.YesNoCancel);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    MenuItem menuItem = sender as MenuItem;
                    ModelKompetenciak items = menuItem.DataContext as ModelKompetenciak;
                    if (items.kompetencia_megnevezes != "Összes")
                        Utility.Delete(items.id, "kompetenciak");
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
                Utility.SettingsInsert(vegzettsegek_new_tbx.Text, "vegzettsegek");
                vegzettsegek_new_tbx.Text = "Új hozzáadása";
                listLoader();
            }
        }

        protected void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (munkakorok_new_tbx.Text != "" && munkakorok_new_tbx.Text != "Új hozzáadása")
            {
                Utility.SettingsInsert(munkakorok_new_tbx.Text, "munkakor");
                munkakorok_new_tbx.Text = "Új hozzáadása";
                listLoader();
            }
        }

        protected void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (pc_new_tbx.Text != "" && pc_new_tbx.Text != "Új hozzáadása")
            {
                Utility.SettingsInsert(pc_new_tbx.Text, "pc");
                pc_new_tbx.Text = "Új hozzáadása";
                listLoader();
            }
        }

        protected void Button_Click_4(object sender, RoutedEventArgs e)
        {
            if (ertesules_new_tbx.Text != "" && ertesules_new_tbx.Text != "Új hozzáadása")
            {
                Utility.SettingsInsert(ertesules_new_tbx.Text, "ertesulesek");
                ertesules_new_tbx.Text = "Új hozzáadása";
                listLoader();
            }
        }

        protected void Button_Click_5(object sender, RoutedEventArgs e)
        {
            if (nyelv_new_tbx.Text != "" && nyelv_new_tbx.Text != "Új hozzáadása")
            {
                Utility.SettingsInsert(nyelv_new_tbx.Text, "nyelv");
                nyelv_new_tbx.Text = "Új hozzáadása";
                listLoader();
            }

        
        }

        protected void Button_Click_6(object sender, RoutedEventArgs e)
        {
            if (kompetencia_new_tbx.Text != "" && kompetencia_new_tbx.Text != "Új hozzáadása")
            {
                Utility.SettingsInsert(kompetencia_new_tbx.Text, "kompetenciak");
                kompetencia_new_tbx.Text = "Új hozzáadása";
                listLoader();
            }
        }


    }
}
