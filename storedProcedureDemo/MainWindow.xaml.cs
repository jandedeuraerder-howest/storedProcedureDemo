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
using Databank;


namespace storedProcedureDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            vuldgrCursussen("cursusnaam", Enumeraties.sorteervolgorde.ASC);
        }


        private void vuldgrCursussen(string sorteerveld, Enumeraties.sorteervolgorde sortorder)
        {
            dgrCursussen.ItemsSource = Cursus.GetCursussen("cursusnaam", sortorder).DefaultView;
        }
        private void vuldgrCursussen()
        {
            dgrCursussen.ItemsSource = Cursus.GetCursussen("cursusnaam", Enumeraties.sorteervolgorde.ASC).DefaultView;

        }
        Cursus actieveCursus;
        private void DgrCursussen_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lblCursusNummer.Content = "";
            txtCursusNaam.Text = "";
            txtInschrijvingsgeld.Text = "";
            actieveCursus = null;
            if (dgrCursussen.SelectedValue == null) return;

            string cursusnr = dgrCursussen.SelectedValue.ToString();
            lblCursusNummer.Content = cursusnr;
            actieveCursus = Cursus.GetCursusByID(int.Parse(cursusnr));
            txtCursusNaam.Text = actieveCursus.CursusNaam;
            txtInschrijvingsgeld.Text = actieveCursus.InschrijvingsGeld.ToString();
        }

        private void BtnBijwerken_Click(object sender, RoutedEventArgs e)
        {
            if (actieveCursus == null) return;

            int actiefcursusnr = actieveCursus.Cursusnr;
            actieveCursus.CursusNaam = txtCursusNaam.Text;
            actieveCursus.InschrijvingsGeld = int.Parse(txtInschrijvingsgeld.Text);
            if (actieveCursus.UpdateCursus() == 1)
            {
                vuldgrCursussen();
                dgrCursussen.SelectedValue = actiefcursusnr;
                DgrCursussen_SelectionChanged(null, null);
                dgrCursussen.Focus();

            }
            else
                MessageBox.Show("Wijziging niet geslaagd", "Error");
        }

        private void BtnToevoegen_Click(object sender, RoutedEventArgs e)
        {
            string cursusnaam = txtCursusNaam.Text;
            int inschrijvingsgeld = int.Parse(txtInschrijvingsgeld.Text);
            Cursus nieuwecursus = new Cursus(cursusnaam, inschrijvingsgeld);
            int cursusnr = nieuwecursus.InsertCursus();

            vuldgrCursussen();
            dgrCursussen.SelectedValue = cursusnr;
            DgrCursussen_SelectionChanged(null, null);
            dgrCursussen.Focus();

        }

        private void BtnVerwijderen_Click(object sender, RoutedEventArgs e)
        {
            if (actieveCursus == null) return;

            if (actieveCursus.DeleteCursus() == 1)
            {
                vuldgrCursussen();
                lblCursusNummer.Content = "";
                txtCursusNaam.Text = "";
                txtInschrijvingsgeld.Text = "";
                actieveCursus = null;
            }
            else
                MessageBox.Show("Wissen niet geslaagd", "Error");

        }
    }
}
