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
        Applicant Applicant = new Applicant();

        private Grid grid;
        private ApplicantDataSheet applicantDataSheet;
        private ApplicantList applicantList;

        public NewApplicantPanel(Grid grid)
        {
            this.grid = grid;
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
            List<ModelFullApplicant> li = Applicant.GetFullApplicant();
            nev_tbx.Text = li[0].nev;
            email_tbx.Text = li[0].email;
            lakhely_tbx.Text = li[0].lakhely;
            telefon_tbx.Text = li[0].telefon;
            eletkor_tbx.Text = li[0].szuldatum.ToString();
            tapasztalat_tbx.Text = li[0].tapasztalat_ev.ToString();

            munkakor_cbx.SelectedIndex = Utility.ComboBoxValueSetter(Utility.Data_Munkakor().ConvertAll(x => new ModelId { id = x.id, }), li.ConvertAll(x => new ModelId { id = x.id_munkakor, }));
            munkakor2_cbx.SelectedIndex = Utility.ComboBoxValueSetter(Utility.Data_Munkakor().ConvertAll(x => new ModelId { id = x.id, }), li.ConvertAll(x => new ModelId { id = x.id_munkakor2, }));
            munkakor3_cbx.SelectedIndex = Utility.ComboBoxValueSetter(Utility.Data_Munkakor().ConvertAll(x => new ModelId { id = x.id, }), li.ConvertAll(x => new ModelId { id = x.id_munkakor3, }));
            nyelv_cbx.SelectedIndex = Utility.ComboBoxValueSetter(Utility.Data_Nyelv().ConvertAll(x => new ModelId { id = x.id, }), li.ConvertAll(x => new ModelId { id = x.id_nyelvtudas, }));
            nyelv2_cbx.SelectedIndex = Utility.ComboBoxValueSetter(Utility.Data_Nyelv().ConvertAll(x => new ModelId { id = x.id, }), li.ConvertAll(x => new ModelId { id = x.id_nyelvtudas2, }));
            ertesules_cbx.SelectedIndex = Utility.ComboBoxValueSetter(Utility.Data_Ertesulesek().ConvertAll(x => new ModelId { id = x.id, }), li.ConvertAll(x => new ModelId { id = x.id_ertesult, }));
            vegzettseg_cbx.SelectedIndex = Utility.ComboBoxValueSetter(Utility.Data_Vegzettseg().ConvertAll(x => new ModelId { id = x.id, }), li.ConvertAll(x => new ModelId { id = x.id_vegz_terulet, }));
            neme_cbx.SelectedIndex = Utility.ComboBoxValueSetter(Utility.Data_Nemek().ConvertAll(x => new ModelId { id = x.id, }), li.ConvertAll(x => new ModelId { id = x.id_neme, }));
        }

        protected bool isFulfilled()
        {
            if (
                munkakor_cbx.SelectedItem == null ||
                nyelv_cbx.SelectedItem == null ||
                neme_cbx.SelectedItem == null ||
                nyelv2_cbx.SelectedItem == null ||
                ertesules_cbx.SelectedItem == null ||
                munkakor_cbx.SelectedItem == null ||
                munkakor2_cbx.SelectedItem == null ||
                munkakor3_cbx.SelectedItem == null ||
                vegzettseg_cbx.SelectedItem == null ||
                nev_tbx.Text.Length == 0 ||
                email_tbx.Text.Length == 0 ||
                telefon_tbx.Text.Length == 0 ||
                lakhely_tbx.Text.Length == 0 ||
                eletkor_tbx.Text.Length == 0 ||
                tapasztalat_tbx.Text.Length == 0
                )
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        protected List<ModelFullApplicant> getFormData()
        {
            DateTime localDate = DateTime.Now;
            List<ModelFullApplicant> items = new List<ModelFullApplicant>();

            ModelNem nemeComboBoxItem = (neme_cbx as ComboBox).SelectedItem as ModelNem;
            ModelNyelv nyelvComboBoxItem = (nyelv_cbx as ComboBox).SelectedItem as ModelNyelv;
            ModelNyelv nyelv2ComboBoxItem = (nyelv2_cbx as ComboBox).SelectedItem as ModelNyelv;
            ModelErtesulesek ertesulesComboBoxItem = (ertesules_cbx as ComboBox).SelectedItem as ModelErtesulesek;
            ModelMunkakor munkakorComboBoxItem = (munkakor_cbx as ComboBox).SelectedItem as ModelMunkakor;
            ModelMunkakor munkakor2ComboBoxItem = (munkakor2_cbx as ComboBox).SelectedItem as ModelMunkakor;
            ModelMunkakor munkakor3ComboBoxItem = (munkakor3_cbx as ComboBox).SelectedItem as ModelMunkakor;
            ModelVegzettseg vegzettsegComboBoxItem = (vegzettseg_cbx as ComboBox).SelectedItem as ModelVegzettseg;
            
            items.Add(new ModelFullApplicant
            {
                id = 0,
                nev = nev_tbx.Text,
                email = email_tbx.Text,
                telefon = telefon_tbx.Text,
                lakhely = lakhely_tbx.Text,
                ertesult = ertesulesComboBoxItem.id.ToString(),
                szuldatum = Convert.ToInt32(eletkor_tbx.Text),
                neme = nemeComboBoxItem.id.ToString(),
                tapasztalat_ev = Convert.ToInt32(tapasztalat_tbx.Text),
                munkakor = munkakorComboBoxItem.id.ToString(),
                munkakor2 = munkakor2ComboBoxItem.id.ToString(),
                munkakor3 = munkakor3ComboBoxItem.id.ToString(),
                vegz_terulet = vegzettsegComboBoxItem.id.ToString(),
                nyelvtudas = nyelvComboBoxItem.id.ToString(),
                nyelvtudas2 = nyelv2ComboBoxItem.id.ToString(),
                reg_date = localDate.ToString("yyyy.MM.dd."),
            });
            return items;
        }

        protected void applicantInsertClick(object sender, RoutedEventArgs e)
        {
            if (isFulfilled())
            {
                Applicant.Insert(getFormData());
                grid.Children.Clear();
                grid.Children.Add(applicantDataSheet = new ApplicantDataSheet(grid));
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
                Applicant.Update(getFormData());
                grid.Children.Clear();
                grid.Children.Add(applicantDataSheet = new ApplicantDataSheet(grid));
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
            grid.Children.Clear();
            grid.Children.Add(applicantList = new ApplicantList(grid));
        }
    }
}
