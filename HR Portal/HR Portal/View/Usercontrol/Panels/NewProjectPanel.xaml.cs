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
        Utilities Utility = new Utilities();

        private Grid grid;
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
            pc_cbx.SelectedIndex = Utilities.ComboBoxValueSetter(Utility.Data_Pc().ConvertAll(x => new ModelId { id = x.id, }), list.ConvertAll(x => new ModelId { id = x.pc, }));
            vegzettseg_cbx.SelectedIndex = Utilities.ComboBoxValueSetter(Utility.Data_Vegzettseg().ConvertAll(x => new ModelId { id = x.id, }), list.ConvertAll(x => new ModelId { id = x.vegzettseg, }));
            nyelv_cbx.SelectedIndex = Utilities.ComboBoxValueSetter(Utility.Data_Nyelv().ConvertAll(x => new ModelId { id = x.id, }), list.ConvertAll(x => new ModelId { id = x.nyelvtudas, }));
            munkakor_cbx.SelectedIndex = Utilities.ComboBoxValueSetter(Utility.Data_Munkakor().ConvertAll(x => new ModelId { id = x.id, }), list.ConvertAll(x => new ModelId { id = x.munkakor, }));
            k1_cbx.SelectedIndex = Utilities.ComboBoxValueSetter(Interview.Data_Kompetencia().ConvertAll(x => new ModelId { id = x.id, }), list.ConvertAll(x => new ModelId { id = x.kepesseg1, }));
            k2_cbx.SelectedIndex = Utilities.ComboBoxValueSetter(Interview.Data_Kompetencia().ConvertAll(x => new ModelId { id = x.id, }), list.ConvertAll(x => new ModelId { id = x.kepesseg2, }));
            k3_cbx.SelectedIndex = Utilities.ComboBoxValueSetter(Interview.Data_Kompetencia().ConvertAll(x => new ModelId { id = x.id, }), list.ConvertAll(x => new ModelId { id = x.kepesseg3, }));
            k4_cbx.SelectedIndex = Utilities.ComboBoxValueSetter(Interview.Data_Kompetencia().ConvertAll(x => new ModelId { id = x.id, }), list.ConvertAll(x => new ModelId { id = x.kepesseg4, }));
            k5_cbx.SelectedIndex = Utilities.ComboBoxValueSetter(Interview.Data_Kompetencia().ConvertAll(x => new ModelId { id = x.id, }), list.ConvertAll(x => new ModelId { id = x.kepesseg5, }));
        }


        protected bool isFulfilled()
        {
            if ( pc_cbx.SelectedIndex.Equals(-1) || nev_tbx.Text.Length == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        protected ModelInsertProject getData()
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
            ModelInsertProject data;

            data = new ModelInsertProject
            {

                id = 0,
                fel_datum = localDate.ToString("yyyy.MM.dd"),
                le_datum = "-",
                statusz = 1,
                szuldatum = 0,

                megnevezes_projekt = nev_tbx.Text,
                hr_id = Session.UserData.id,
                tapasztalat_ev = Convert.ToInt32((tapasztalat_tbx.Text.Length > 0 ? tapasztalat_tbx.Text : "0")),
                ber = Convert.ToInt32((ber_tbx.Text.Length > 0 ? ber_tbx.Text : "0")),

                pc = pcComboBox.id,

                nyelvtudas = Convert.ToInt32(nyelv_cbx.SelectedIndex.Equals(-1) ? "0" : nyelvComboBox.id.ToString()),
                munkakor = Convert.ToInt32(munkakor_cbx.SelectedIndex.Equals(-1) ? "0" : munkakorComboBox.id.ToString()),
                vegzettseg = Convert.ToInt32(vegzettseg_cbx.SelectedIndex.Equals(-1) ? "0" : vegzettsegComboBox.id.ToString()),
                kepesseg1 = Convert.ToInt32(k1_cbx.SelectedIndex.Equals(-1) ? "0" : kepzettseg1ComboBox.id.ToString()),
                kepesseg2 = Convert.ToInt32(k2_cbx.SelectedIndex.Equals(-1) ? "0" : kepzettseg2ComboBox.id.ToString()),
                kepesseg3 = Convert.ToInt32(k3_cbx.SelectedIndex.Equals(-1) ? "0" : kepzettseg3ComboBox.id.ToString()),
                kepesseg4 = Convert.ToInt32(k4_cbx.SelectedIndex.Equals(-1) ? "0" : kepzettseg4ComboBox.id.ToString()),
                kepesseg5 = Convert.ToInt32(k5_cbx.SelectedIndex.Equals(-1) ? "0" : kepzettseg5ComboBox.id.ToString()),

            };
            return data;
        }

        protected void projektInsertClick(object sender, RoutedEventArgs e)
        {

            Session.isUpdate = false;

                if(isFulfilled())
            {
                ModelInsertProject data = getData();
                Project.Insert(data);
                Utilities.NavigateTo(grid, new ProjectDataSheet(grid, new Project(0)));
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
                ModelInsertProject data = getData();
                Project.Update(data);
                Utilities.NavigateTo(grid,new ProjectDataSheet(grid, new Project(0)));
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
