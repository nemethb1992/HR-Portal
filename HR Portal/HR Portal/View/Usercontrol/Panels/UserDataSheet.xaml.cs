using HR_Portal.Source;
using HR_Portal.Source.Model;
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
    /// Interaction logic for UserDataSheet.xaml
    /// </summary>
    public partial class UserDataSheet : UserControl
    {
        private Grid grid;
        private AdminPage adminPage;
        int userId;

        public UserDataSheet(Grid grid, int userId)
        {
            this.grid = grid;
            this.userId = userId;
            InitializeComponent();
            StartUp(userId);
        }

        protected void StartUp(int userId)
        {
            List<ModelUserData> list = GetUserData(userId);




            NameTitleTextBlock.Text = list[0].name;
            Input_username.Text = list[0].username;
            Input_email.Text = list[0].email;
            //Input_kategoriaMegnevezes.Text = list[0].kategoriaMegnevezes;
            //Input_jogosultsag.Text = (list[0].validitasBool) ? "Jogosult" : "Megvonva";
            Input_reg_datum.Text = list[0].reg_datum;
            Input_belepve.Text = list[0].belepve;

            KategoriaCbx.SelectedIndex = CommonUtility.ComboBoxValueSetter(KategoriaLoadUp().ConvertAll(x => new ModelId { id = x.id, }), list.ConvertAll(x => new ModelId { id = x.kategoria, }));
            JogosultsagCbx.SelectedIndex = CommonUtility.ComboBoxValueSetter(ValiditasLoadUp().ConvertAll(x => new ModelId { id = x.id, }), list.ConvertAll(x => new ModelId { id = x.validitas, })); 

        }

        protected List<ModelList> KategoriaLoadUp()
        { 
            List<ModelList> cbxList = new List<ModelList>();

            cbxList.Add(new ModelList { id = 0, value = "Szakmai" });
            cbxList.Add(new ModelList { id = 1, value = "HR" });
            cbxList.Add(new ModelList { id = 2, value = "Admin" });

            KategoriaCbx.ItemsSource = cbxList;

            return cbxList;
        }

        protected List<ModelList> ValiditasLoadUp()
        {
            List<ModelList> cbxList = new List<ModelList>();

            cbxList.Add(new ModelList { id = 0, value = "Megvonva" });
            cbxList.Add(new ModelList { id = 1, value = "Jogosult" });

            JogosultsagCbx.ItemsSource = cbxList;

            return cbxList;
        }

        protected List<ModelUserData> GetUserData(int userId)
        {
            return UserData.GetById(userId);
        }

        private void BackButton(object sender, RoutedEventArgs e)
        {
            grid.Children.Clear();
            grid.Children.Add(adminPage = new AdminPage(grid));
        }

        private void ModifyEmailAddress(object sender, TextChangedEventArgs e)
        {
            UserData.Modify(userId,"email", (sender as TextBox).Text);
        }

        private void ModifyKategoria(object sender, SelectionChangedEventArgs e)
        {
            UserData.Modify(userId, "kategoria", ((sender as ComboBox).SelectedItem as ModelList).id.ToString());
        }

        private void ModifyValiditas(object sender, SelectionChangedEventArgs e)
        {
            UserData.Modify(userId, "validitas", ((sender as ComboBox).SelectedItem as ModelList).id.ToString());
        }
    }
}
