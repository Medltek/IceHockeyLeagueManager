using ProjektProjektProjekt.Database;
using ProjektProjektProjekt.Database.Tabulky;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace WPF
{
    /// <summary>
    /// Interaction logic for Hraci.xaml
    /// </summary>
    public partial class Hraci : Window
    {
        private Collection<Tym> tymy;
        private Tym tym;
        private static readonly Regex _regex = new Regex("[^0-9]+");
        //public Hrac hrac { get; set; }
        private bool fail;
        public Hraci()
        {
            InitializeComponent();
            fail = false;
            listing();
            setVisibility();
        }

        public Hraci(bool fail)
        {
            InitializeComponent();
            this.fail = fail;
            listing();
        }

        private void listing()
        {
            tymy = TymTable.SelectAll();
            if (tymy.Count == 0)
            {

                MessageBox.Show("Žádné týmy v systému");

                return;
            }
            listTymu.Items.Clear();
            
            foreach (var tym in tymy)
            {
                listTymu.Items.Add(new ListBoxItem()
                {
                    Content = tym.tym_nazev
                });

            }
        }

        private static bool IsTextAllowed(string text)
        {
            return !_regex.IsMatch(text);
        }

        //hledat hráče
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(hrac_jmeno_hledat.Text))
            {
                MessageBox.Show("Vyplňte jméno hráče");
                return;
            }

            var w = new HledaniHraci(hrac_jmeno_hledat.Text);
            
            w.Show();
            /*if (w.IsLoaded)
            { w.Show(); }*/
            
            
        }
        public void setVisibility()
        {
            var admin = Uzivatel.uzivatel.is_admin;
            
            buttOcenit.Visibility = admin ? Visibility.Visible : Visibility.Hidden;
            buttVlozit.Visibility = admin ? Visibility.Visible : Visibility.Hidden;
        }
        //vlozit hrace
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(hrac_vaha.Text))
            {
                MessageBox.Show("Vyplňte váhu hráče");
                return;
            }
            if (string.IsNullOrWhiteSpace(hrac_vyska.Text))
            {
                MessageBox.Show("Vyplňte výšku hráče");
                return;
            }
            if (string.IsNullOrWhiteSpace(hrac_cena.Text))
            {
                MessageBox.Show("Vyplňte cenu hráče");
                return;
            }
            if (string.IsNullOrWhiteSpace(hrac_jmeno.Text))
            {
                MessageBox.Show("Vyplňte jméno hráče");
                return;
            }
            if (string.IsNullOrWhiteSpace(hrac_post.Text))
            {
                MessageBox.Show("Vyplňte post hráče");
                return;
            }
            if (string.IsNullOrWhiteSpace(hrac_zahyb.Text))
            {
                MessageBox.Show("Vyplňte záhyb hráče");
                return;
            }
            if (hrac_datum_narozeni.SelectedDate == null)
            {
                MessageBox.Show("Vyplňte datum narození hráče");
                return;
            }

            if(IsTextAllowed(hrac_vaha.Text) == false || IsTextAllowed(hrac_vyska.Text)==false || IsTextAllowed(hrac_cena.Text)==false)
            {
                MessageBox.Show("Pro Váhu, Výšku a Cenu hráče použijte pouze číslo");
                return;
            }

            Hrac hrac = new Hrac();
            hrac.hrac_jmeno = hrac_jmeno.Text;
            hrac.cena = Int32.Parse(hrac_cena.Text);
            hrac.vyska = Int32.Parse(hrac_vyska.Text);
            hrac.vaha = Int32.Parse(hrac_vaha.Text);
            hrac.zahyb = hrac_zahyb.Text;
            hrac.post = hrac_post.Text;
            hrac.tym_id = tym.tym_id;
            hrac.smazano = false;
            hrac.datum_narozeni = (DateTime)hrac_datum_narozeni.SelectedDate;

            var vytvor = HracTable.Insert(hrac);

            if (vytvor >= 0)
            {
                MessageBox.Show("hráč vložen");
            }

            else
            {
                MessageBox.Show("chyba, hráč nevložen");
            }
            
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            HracTable.Ocenit();
            MessageBox.Show("hráči oceněni");
        }

        private void listTymu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listTymu.SelectedItem != null)
            {
                tym = tymy.FirstOrDefault(t => t.tym_nazev == (string)(listTymu.SelectedItem as ListBoxItem)?.Content);

            }
        }
    }
}
