using HR_Portal.Source;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using HR_Portal.Source.Model;
using HR_Portal.Source.Model.Project;
using HR_Portal.Source.ViewModel;

namespace HR_Portal.View.Usercontrol.Panels
{
    /// <summary>
    /// Interaction logic for NewProjectPanel.xaml
    /// </summary>
    public partial class NewProjectPanel : UserControl
    {
        CommonUtility Utility = new CommonUtility();

        private Grid grid;
        private ProjectDataSheet projectDataSheet;
        private ProjectList projectList;

        public NewProjectPanel(Grid grid)
        {
            this.grid = grid;
            InitializeComponent();
            checkboxLoader();
        }

        protected void checkboxLoader()
        {
            pc_cbx.ItemsSource = Utility.Data_Pc();
            vegzettseg_cbx.ItemsSource = Utility.Data_Vegzettseg();
            nyelv_cbx.ItemsSource = Utility.Data_Nyelv();
            munkakor_cbx.ItemsSource = Utility.Data_Munkakor();
            k1_cbx.ItemsSource = Interview.Data_Kompetencia();
            k2_cbx.ItemsSource = Interview.Data_Kompetencia();
            k3_cbx.ItemsSource = Interview.Data_Kompetencia();
            k4_cbx.ItemsSource = Interview.Data_Kompetencia();
            k5_cbx.ItemsSource = Interview.Data_Kompetencia();

            if (Session.isUpdate == true)
            {
                uj_cim.Visibility = Visibility.Hidden;
                projekt_INSERT_btn.Visibility = Visibility.Hidden;
                modositas_cim.Visibility = Visibility.Visible;
                projekt_UPDATE_btn.Visibility = Visibility.Visible;
                modifyFormLoader();
            }
        }

        protected void modifyFormLoader()
        {
            List<ModelFullProject> list = Project.GetFullProject();
            nev_tbx.Text = list[0].megnevezes_projekt;
            tapasztalat_tbx.Text = list[0].tapasztalat_ev.ToString();
            ber_tbx.Text = list[0].ber.ToString();
            pc_cbx.SelectedIndex = CommonUtility.ComboBoxValueSetter(Utility.Data_Pc().ConvertAll(x => new ModelId { id = x.id, }), list.ConvertAll(x => new ModelId { id = x.pc, }));
            vegzettseg_cbx.SelectedIndex = CommonUtility.ComboBoxValueSetter(Utility.Data_Vegzettseg().ConvertAll(x => new ModelId { id = x.id, }), list.ConvertAll(x => new ModelId { id = x.vegzettseg, }));
            nyelv_cbx.SelectedIndex = CommonUtility.ComboBoxValueSetter(Utility.Data_Nyelv().ConvertAll(x => new ModelId { id = x.id, }), list.ConvertAll(x => new ModelId { id = x.nyelvtudas, }));
            munkakor_cbx.SelectedIndex = CommonUtility.ComboBoxValueSetter(Utility.Data_Munkakor().ConvertAll(x => new ModelId { id = x.id, }), list.ConvertAll(x => new ModelId { id = x.munkakor, }));
            k1_cbx.SelectedIndex = CommonUtility.ComboBoxValueSetter(Interview.Data_Kompetencia().ConvertAll(x => new ModelId { id = x.id, }), list.ConvertAll(x => new ModelId { id = x.kepesseg1, }));
            k2_cbx.SelectedIndex = CommonUtility.ComboBoxValueSetter(Interview.Data_Kompetencia().ConvertAll(x => new ModelId { id = x.id, }), list.ConvertAll(x => new ModelId { id = x.kepesseg2, }));
            k3_cbx.SelectedIndex = CommonUtility.ComboBoxValueSetter(Interview.Data_Kompetencia().ConvertAll(x => new ModelId { id = x.id, }), list.ConvertAll(x => new ModelId { id = x.kepesseg3, }));
            k4_cbx.SelectedIndex = CommonUtility.ComboBoxValueSetter(Interview.Data_Kompetencia().ConvertAll(x => new ModelId { id = x.id, }), list.ConvertAll(x => new ModelId { id = x.kepesseg4, }));
            k5_cbx.SelectedIndex = CommonUtility.ComboBoxValueSetter(Interview.Data_Kompetencia().ConvertAll(x => new ModelId { id = x.id, }), list.ConvertAll(x => new ModelId { id = x.kepesseg5, }));
        }


        protected bool isFulfilled()
        {
            if (
                vegzettseg_cbx.SelectedItem == null ||
                munkakor_cbx.SelectedItem == null ||
                nyelv_cbx.SelectedItem == null ||
                k1_cbx.SelectedItem == null ||
                k2_cbx.SelectedItem == null ||
                k3_cbx.SelectedItem == null ||
                k4_cbx.SelectedItem == null ||
                k5_cbx.SelectedItem == null ||
                pc_cbx.SelectedItem == null ||
                nev_tbx.Text.Length == 0 ||
                tapasztalat_tbx.Text.Length == 0 ||
                ber_tbx.Text.Length == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        protected List<ModelInsertProject> getData()
        {
            ModelVegzettseg vegzettsegComboBox = (vegzettseg_cbx as ComboBox).SelectedItem as ModelVegzettseg;
            ModelMunkakor munkakorComboBox = (munkakor_cbx as ComboBox).SelectedItem as ModelMunkakor;
            ModelNyelv nyelvComboBox = (nyelv_cbx as ComboBox).SelectedItem as ModelNyelv;
            ModelKompetenciak kepzettseg1ComboBox = (k1_cbx as ComboBox).SelectedItem as ModelKompetenciak;
            ModelKompetenciak kepzettseg2ComboBox = (k2_cbx as ComboBox).SelectedItem as ModelKompetenciak;
            ModelKompetenciak kepzettseg3ComboBox = (k3_cbx as ComboBox).SelectedItem as ModelKompetenciak;
            ModelKompetenciak kepzettseg4ComboBox = (k4_cbx as ComboBox).SelectedItem as ModelKompetenciak;
            ModelKompetenciak kepzettseg5ComboBox = (k5_cbx as ComboBox).SelectedItem as ModelKompetenciak;
            ModelPc pcComboBox = (pc_cbx as ComboBox).SelectedItem as ModelPc;
            DateTime localDate = DateTime.Now;
            List<ModelInsertProject> list = new List<ModelInsertProject>();

            list.Add(new ModelInsertProject
            {
                id = 0,
                hr_id = Session.UserData.id,
                megnevezes_projekt = nev_tbx.Text,
                pc = pcComboBox.id,
                vegzettseg = vegzettsegComboBox.id,
                tapasztalat_ev = Convert.ToInt32(tapasztalat_tbx.Text),
                statusz = 1,
                fel_datum = localDate.ToString("yyyy.MM.dd."),
                le_datum = "-",
                nyelvtudas = nyelvComboBox.id,
                munkakor = munkakorComboBox.id,
                szuldatum = 0,
                ber = Convert.ToInt32(ber_tbx.Text),
                kepesseg1 = Convert.ToInt32(kepzettseg1ComboBox.id),
                kepesseg2 = Convert.ToInt32(kepzettseg2ComboBox.id),
                kepesseg3 = Convert.ToInt32(kepzettseg3ComboBox.id),
                kepesseg4 = Convert.ToInt32(kepzettseg4ComboBox.id),
                kepesseg5 = Convert.ToInt32(kepzettseg5ComboBox.id)

            });
            return list;
        }

        protected void projektInsertClick(object sender, RoutedEventArgs e)
        {

            Session.isUpdate = false;

                if(isFulfilled())
                {
                    Project.Insert(getData());
                    grid.Children.Clear();
                    grid.Children.Add(projectDataSheet = new ProjectDataSheet(grid));
                }
                else
                {
                    showInfo.Text = "Nem lehet kitöltetlen mező!";
                }
        }

        protected void projektUpdateClick(object sender, RoutedEventArgs e)
        {
            Session.isUpdate = false;

            if (isFulfilled())
            {
                Project.Update(getData());
                grid.Children.Clear();
                grid.Children.Add(projectDataSheet = new ProjectDataSheet(grid));
            }
            else
            {
                showInfo.Text = "Nem lehet kitöltetlen mező!";
            }
        }

        protected void numericTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9-]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void BackButton(object sender, RoutedEventArgs e)
        {
            grid.Children.Clear();
            grid.Children.Add(projectList = new ProjectList(grid));
        }
    }
}
