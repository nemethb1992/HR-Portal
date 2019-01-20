using HR_Portal.Source;
using HR_Portal.Source.Model;
using HR_Portal.Source.Model.Applicant;
using HR_Portal.Source.ViewModel;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HR_Portal.View.Usercontrol.Panels
{
    /// <summary>
    /// Interaction logic for applicant_new_panel.xaml
    /// </summary>
    public partial class NewApplicantPanel : UserControl
    {
        Utility Utility = new Utility();
        private Applicant applicant;
        private Grid grid;

        public NewApplicantPanel(Grid grid, Applicant applicant = null)
        {
            this.grid = grid;
            this.applicant = applicant;
            InitializeComponent();
            checkboxLoader();
        }

        protected void checkboxLoader()
        {
            munkakor_cbx.ItemsSource = Utility.Data_Munkakor();
            munkakor2_cbx.ItemsSource = Utility.Data_Munkakor();
            munkakor3_cbx.ItemsSource = Utility.Data_Munkakor();
            vegzettseg_cbx.ItemsSource = Utility.Data_Vegzettseg();
            nyelv_cbx.ItemsSource = Utility.Data_Nyelv();
            nyelv2_cbx.ItemsSource = Utility.Data_Nyelv();
            ertesules_cbx.ItemsSource = Utility.Data_Ertesulesek();
            neme_cbx.ItemsSource = Utility.Data_Nemek();

            if (Session.isUpdate == true)
            {
                uj_cim.Visibility = Visibility.Hidden;
                applicant_INSERT_btn.Visibility = Visibility.Hidden;
                modositas_cim.Visibility = Visibility.Visible;
                applicant_UPDATE_btn.Visibility = Visibility.Visible;
                modifyFormLoader();
            }
        }

        protected void modifyFormLoader()
        {
            
            nev_tbx.Text = applicant.data.nev;
            email_tbx.Text = applicant.data.email;
            lakhely_tbx.Text = applicant.data.lakhely;
            telefon_tbx.Text = applicant.data.telefon;
            eletkor_tbx.Text = applicant.data.szuldatum.ToString();
            tapasztalat_tbx.Text = applicant.data.tapasztalat_ev.ToString();

            munkakor_cbx.SelectedIndex = Utility.ComboBoxValueSetter(Utility.Data_Munkakor().ConvertAll(x => new ModelId { id = x.id, }), applicant.list.ConvertAll(x => new ModelId { id = x.id_munkakor, }));
            munkakor2_cbx.SelectedIndex = Utility.ComboBoxValueSetter(Utility.Data_Munkakor().ConvertAll(x => new ModelId { id = x.id, }), applicant.list.ConvertAll(x => new ModelId { id = x.id_munkakor2, }));
            munkakor3_cbx.SelectedIndex = Utility.ComboBoxValueSetter(Utility.Data_Munkakor().ConvertAll(x => new ModelId { id = x.id, }), applicant.list.ConvertAll(x => new ModelId { id = x.id_munkakor3, }));
            nyelv_cbx.SelectedIndex = Utility.ComboBoxValueSetter(Utility.Data_Nyelv().ConvertAll(x => new ModelId { id = x.id, }), applicant.list.ConvertAll(x => new ModelId { id = x.id_nyelvtudas, }));
            nyelv2_cbx.SelectedIndex = Utility.ComboBoxValueSetter(Utility.Data_Nyelv().ConvertAll(x => new ModelId { id = x.id, }), applicant.list.ConvertAll(x => new ModelId { id = x.id_nyelvtudas2, }));
            ertesules_cbx.SelectedIndex = Utility.ComboBoxValueSetter(Utility.Data_Ertesulesek().ConvertAll(x => new ModelId { id = x.id, }), applicant.list.ConvertAll(x => new ModelId { id = x.id_ertesult, }));
            vegzettseg_cbx.SelectedIndex = Utility.ComboBoxValueSetter(Utility.Data_Vegzettseg().ConvertAll(x => new ModelId { id = x.id, }), applicant.list.ConvertAll(x => new ModelId { id = x.id_vegz_terulet, }));
            neme_cbx.SelectedIndex = Utility.ComboBoxValueSetter(Utility.Data_Nemek().ConvertAll(x => new ModelId { id = x.id, }), applicant.list.ConvertAll(x => new ModelId { id = x.id_neme, }));
        }

        protected bool isFulfilled()
        {
            if (
                nev_tbx.Text.Length == 0 ||
                email_tbx.Text.Length == 0

                )
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        protected ModelFullApplicant getFormData()
        {
            DateTime localDate = DateTime.Now;
            ModelFullApplicant items;

            ModelNem nemeComboBoxItem = (neme_cbx as ComboBox).SelectedItem as ModelNem;
            ModelNyelv nyelvComboBoxItem = (nyelv_cbx as ComboBox).SelectedItem as ModelNyelv;
            ModelNyelv nyelv2ComboBoxItem = (nyelv2_cbx as ComboBox).SelectedItem as ModelNyelv;
            ModelErtesulesek ertesulesComboBoxItem = (ertesules_cbx as ComboBox).SelectedItem as ModelErtesulesek;
            ModelMunkakor munkakorComboBoxItem = (munkakor_cbx as ComboBox).SelectedItem as ModelMunkakor;
            ModelMunkakor munkakor2ComboBoxItem = (munkakor2_cbx as ComboBox).SelectedItem as ModelMunkakor;
            ModelMunkakor munkakor3ComboBoxItem = (munkakor3_cbx as ComboBox).SelectedItem as ModelMunkakor;
            ModelVegzettseg vegzettsegComboBoxItem = (vegzettseg_cbx as ComboBox).SelectedItem as ModelVegzettseg;
            
            items = new ModelFullApplicant
            {
                id = 0,
                nev = nev_tbx.Text,
                email = email_tbx.Text,
                telefon = telefon_tbx.Text,
                lakhely = lakhely_tbx.Text,
                tapasztalat_ev = Convert.ToInt32((tapasztalat_tbx.Text.Length > 0? tapasztalat_tbx.Text : "0")),
                szuldatum = Convert.ToInt32(eletkor_tbx.Text.Length > 0 ? eletkor_tbx.Text : "0"),
                reg_date = localDate.ToString("yyyy.MM.dd."),

                neme = (neme_cbx.SelectedIndex.Equals(-1) ? "0" : nemeComboBoxItem.id.ToString()),
                munkakor = (munkakor_cbx.SelectedIndex.Equals(-1) ? "1" : munkakorComboBoxItem.id.ToString()),
                munkakor2 = (munkakor2_cbx.SelectedIndex.Equals(-1) ? "1" : munkakor2ComboBoxItem.id.ToString()),
                munkakor3 = (munkakor3_cbx.SelectedIndex.Equals(-1) ? "1" : munkakor3ComboBoxItem.id.ToString()),
                vegz_terulet = (vegzettseg_cbx.SelectedIndex.Equals(-1) ? "1" : vegzettsegComboBoxItem.id.ToString()),
                nyelvtudas = (nyelv_cbx.SelectedIndex.Equals(-1) ? "0" : nyelvComboBoxItem.id.ToString()),
                nyelvtudas2 = (nyelv2_cbx.SelectedIndex.Equals(-1) ? "0" : nyelv2ComboBoxItem.id.ToString()),
                ertesult = (ertesules_cbx.SelectedIndex.Equals(-1) ? "0" : ertesulesComboBoxItem.id.ToString())
            };
            return items;
        }

        protected void applicantInsertClick(object sender, RoutedEventArgs e)
        {
            if (isFulfilled())
            {
                ModelFullApplicant applicant = getFormData();
                Applicant.Insert(applicant);
                Utility.NavigateTo(grid, new ApplicantDataSheet(grid, new Applicant(applicant.id)));
            }
            else
            {
                showInfo.Text = "Nem lehet kitöltetlen mező!";
            }
        }

        protected void applicantModifyClick(object sender, RoutedEventArgs e)
        {
            if (isFulfilled())
            {
                ModelFullApplicant applicant = getFormData();
                Applicant.Update(getFormData());
                Utility.NavigateTo(grid, new ApplicantDataSheet(grid, new Applicant(applicant.id)));
            }
            else
            {
                showInfo.Text = "Nem lehet kitöltetlen mező!";
            }

        }

        protected void numericTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void BackButton(object sender, RoutedEventArgs e)
        {
            Utility.NavigateTo(grid, new ApplicantList(grid));
        }
    }
}
