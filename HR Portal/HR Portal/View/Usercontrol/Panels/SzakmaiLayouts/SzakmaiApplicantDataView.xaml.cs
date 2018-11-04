using HR_Portal.Source;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using HR_Portal.Source.Model;
using HR_Portal.Source.Model.Applicant;
using HR_Portal.Source.ViewModel;
using System.Diagnostics;

namespace HR_Portal.View.Usercontrol.Panels.SzakmaiLayouts
{
    /// <summary>
    /// Interaction logic for Szakmai_applicant_DataView.xaml
    /// </summary>
    public partial class SzakmaiApplicantDataView : UserControl
    {
        Session session = new Session();
        ApplicantImplementation Applicant = new ApplicantImplementation();
        CommonUtility Utility = new CommonUtility();

        private SzakmaiProjektDataSheet szakmaiProjektDataSheet;
        private Grid grid;

        public SzakmaiApplicantDataView(Grid grid)
        {
            InitializeComponent();
            this.grid = grid;
            formLoader();
        }

        protected void formLoader()
        {
            List<ModelFullApplicant> list = Applicant.GetFullApplicant();
            applicant_profile_title.Text = list[0].nev;
            app_input_1.Text = list[0].email;
            app_input_2.Text = list[0].telefon.ToString();
            app_input_3.Text = list[0].lakhely;
            app_input_5.Text = list[0].nyelvtudas.ToString();
            app_input_8.Text = list[0].munkakor;
            app_input_9.Text = list[0].ertesult.ToString();
            app_input_10.Text = list[0].szuldatum.ToString();
            commentLoader(megjegyzes_listBox);
            csatolmany_listBox.ItemsSource = Files.Read(Session.ApplicantID);
        }

        protected void commentLoader(ListBox lb)
        {
            lb.ItemsSource = Utility.Data_CommentApplicant();
        }

        protected void commentDeleteClick(object sender, RoutedEventArgs e)
        {
            MenuItem item = sender as MenuItem;
            ModelComment items = item.DataContext as ModelComment;

            Comment.Delete(items.id);
            commentLoader(megjegyzes_listBox);
        }

        protected void enterComment(object sender, KeyEventArgs e)
        {
            TextBox textbox = sender as TextBox;

            if (e.Key != System.Windows.Input.Key.Enter) return;
            e.Handled = true;
            Comment.Add(comment_tartalom.Text, 0, Session.ApplicantID);
            commentLoader(megjegyzes_listBox);
            textbox.Text = "";
        }

        protected void commentGotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textbox = sender as TextBox;

            if (textbox.Text == "Új megjegyzés")
            {
                textbox.Text = "";
            }
        }

        protected void commentLostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textbox = sender as TextBox;

            if (textbox.Text == "")
            {
                textbox.Text = "Új megjegyzés";
            }
        }

        protected void navigateToSzakmaiProjektDataSheet(object sender, RoutedEventArgs e)
        {
            grid.Children.Clear();
            grid.Children.Add(szakmaiProjektDataSheet = new SzakmaiProjektDataSheet(grid));
        }

        protected void attachmentOpenClick(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            ModelJeloltFile item = btn.DataContext as ModelJeloltFile;
            Process.Start(item.path);
        }
    }
}
