using HR_Portal.Source;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using HR_Portal.Source.Model;
using HR_Portal.Source.Model.Applicant;
using HR_Portal.Source.ViewModel;
using HR_Portal.Source.Model.Project;

namespace HR_Portal.View.Usercontrol.Panels.SzakmaiLayouts
{
    /// <summary>
    /// Interaction logic for SzakmaiProjekt_DataView.xaml
    /// </summary>
    public partial class SzakmaiProjektDataSheet : UserControl
    {
        
        //ControlApplicant aControl = new ControlApplicant();
        CommonUtility Utility = new CommonUtility();

        private SzakmaiApplicantDataView szakmaiApplicantDataView;
        private SzakmaiList szakmaiList;
        private Grid grid;

        public SzakmaiProjektDataSheet(Grid grid)
        {
            InitializeComponent();
            this.grid = grid;
            formLoader();
        }

        protected void navigateToSzakmaiList(object sender, RoutedEventArgs e)
        {
            grid.Children.Clear();
            grid.Children.Add(szakmaiList = new SzakmaiList(grid));
        }

        protected void formLoader()
        {
            List <ModelFullProject> list = Project.GetFullProject();
            projekt_profile_title.Text = list[0].megnevezes_projekt;
            projekt_input_1.Text = list[0].statusz.ToString();
            projekt_input_2.Text = list[0].megnevezes_munka;
            projekt_input_3.Text = list[0].megnevezes_pc;
            projekt_input_4.Text = list[0].megnevezes_vegzettseg;
            projekt_input_5.Text = list[0].megnevezes_nyelv;
            projekt_input_6.Text = list[0].jeloltek_db.ToString();
            projekt_input_7.Text = list[0].megnevezes_hr;
            projekt_input_8.Text = list[0].fel_datum.ToString();
            projekt_input_9.Text = list[0].ber.ToString() + " Ft";
            projekt_input_10.Text = list[0].tapasztalat_ev.ToString();

            List<ModelKompetenciak> listKompetencia = Interview.Data_Kompetencia();
            foreach (var item in listKompetencia)
            {
                if (item.id == list[0].kepesseg1)
                { kompetencia1.Text = item.kompetencia_megnevezes; }
                if (item.id == list[0].kepesseg2)
                { kompetencia2.Text = item.kompetencia_megnevezes; }
                if (item.id == list[0].kepesseg3)
                { kompetencia3.Text = item.kompetencia_megnevezes; }
                if (item.id == list[0].kepesseg4)
                { kompetencia4.Text = item.kompetencia_megnevezes; }
                if (item.id == list[0].kepesseg5)
                { kompetencia5.Text = item.kompetencia_megnevezes; }
            }

            megjegyzes_listBox.ItemsSource = Utility.Data_CommentProject();
            kapcs_jeloltek_listBox.ItemsSource = Utility.Data_JeloltKapcs();
        }

        protected void commentDeleteClick(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = sender as MenuItem;
            ModelComment items = menuItem.DataContext as ModelComment;

            Comment.Delete(items.id);
            megjegyzes_listBox.ItemsSource = Utility.Data_CommentProject();
        }

        protected void enterComment(object sender, KeyEventArgs e)
        {
            TextBox tbx = sender as TextBox;

            if (e.Key != System.Windows.Input.Key.Enter) return;
            e.Handled = true;
            Comment.Add(comment_tartalom.Text, Session.ProjektID, 0);
            megjegyzes_listBox.ItemsSource = Utility.Data_CommentProject();
            tbx.Text = "";
        }

        protected void commentGotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tbx = sender as TextBox;

            if (tbx.Text == "Új megjegyzés")
            {
                tbx.Text = "";
            }
        }

        protected void commentLostFocus(object sender, RoutedEventArgs e)
        {
            TextBox tbx = sender as TextBox;

            if (tbx.Text == "")
            {
                tbx.Text = "Új megjegyzés";
            }
        }

        protected void openApplicant(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            ModelApplicantList items = button.DataContext as ModelApplicantList;

            Session.ApplicantID = items.id;
            grid.Children.Clear();
            grid.Children.Add(szakmaiApplicantDataView = new SzakmaiApplicantDataView(grid));
        }
    }
}
