using ProjektProjektProjekt.Database;
using ProjektProjektProjekt.Database.Tabulky;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for Zapasy.xaml
    /// </summary>
    public partial class Zapasy : Window
    {

        private Collection<Tym> tymy;
        private Tym tym1;
        private Tym tym2;
        private Zapas zapas;
        private Tymy_Zapasy tym_zapas1;
        private Tymy_Zapasy tym_zapas2;
        private static readonly Regex _regex = new Regex("[^0-9]+");
        public Zapasy()
        {
            InitializeComponent();
            listing();
            setVisibility();
        }

        public void setVisibility()
        {
            var admin = Uzivatel.uzivatel.is_admin;

            
            buttVlozit.Visibility = admin ? Visibility.Visible : Visibility.Hidden;
        }

        private void listing()
        {
            tymy = TymTable.SelectAll();
            if (tymy.Count == 0)
            {

                MessageBox.Show("Žádné týmy v systému");

                return;
            }
            listTymu1.Items.Clear();
            listTymu2.Items.Clear();
            foreach (var tym in tymy)
            {
                listTymu1.Items.Add(new ListBoxItem()
                {
                    Content = tym.tym_nazev
                });

                listTymu2.Items.Add(new ListBoxItem()
                {
                    Content = tym.tym_nazev
                });


            }
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (tym_nazev_hledat == null)
            {
                MessageBox.Show("Vyplňte název týmu");
                return;
            }
            var s = Tymy_ZapasyTable.SelectNazev(tym_nazev_hledat.Text);
            tymyNazev.ItemsSource = s;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (pocatek.SelectedDate == null)
            {
                MessageBox.Show("Počátek intervalu musí být zvolen");
                return;
            }
            if (konec.SelectedDate == null)
            {
                MessageBox.Show("Konec intervalu musí být zvolen");
                return;
            }
            var s = Tymy_ZapasyTable.SelectInterval((DateTime)pocatek.SelectedDate, (DateTime)konec.SelectedDate);
            tymyDatum.ItemsSource = s;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (datum.SelectedDate == null)
            {
                MessageBox.Show("datum zápasu musí být zvoleno");
                return;
            }
            if (listTymu2.SelectedItem == null)
            {
                MessageBox.Show("Oba týmy musí být zvoleny");
                return;
            }
            if (listTymu1.SelectedItem == null)
            {
                MessageBox.Show("Oba týmy musí být zvoleny");
                return;
            }
            if (listTymu2.SelectedItem == listTymu1.SelectedItem)
            {
                MessageBox.Show("Zvolte prosím dva rozdílné týmy");
                return;
            }
            if (string.IsNullOrWhiteSpace(skore1.Text) || string.IsNullOrWhiteSpace(skore2.Text))
            {
                MessageBox.Show("Vyplňte počet vstřelených branek obou týmů");
                return;
            }
            if (IsTextAllowed(skore1.Text) == false || IsTextAllowed(skore2.Text) == false)
            {
                MessageBox.Show("Pro Počet vstřelených branek použijte pouze číslo");
                return;
            }

            zapas = new Zapas();
            zapas.datum = (DateTime)datum.SelectedDate;
            if (Int32.Parse(skore1.Text) == Int32.Parse(skore2.Text))
            {
                MessageBox.Show("Zápas nemůže skončit nerozhodně");
                return;
            }
            if (Int32.Parse(skore1.Text) > Int32.Parse(skore2.Text))
            {
                zapas.vyherce_id = tym1.tym_id;
            }
            if (Int32.Parse(skore1.Text) < Int32.Parse(skore2.Text))
            {
                zapas.vyherce_id = tym2.tym_id;
            }
            zapas.goly_tym1 = Int32.Parse(skore1.Text);
            zapas.goly_tym2 = Int32.Parse(skore2.Text);
            zapas.smazano = false;
            int zid = ZapasTable.Insert(zapas);

            tym_zapas1 = new Tymy_Zapasy();
            tym_zapas2 = new Tymy_Zapasy();
            tym_zapas1.tym_id = tym1.tym_id;
            tym_zapas1.zapas_id = zid;
            tym_zapas2.tym_id = tym2.tym_id;
            tym_zapas2.zapas_id = zid;
            Tymy_ZapasyTable.Insert(tym_zapas1);
            Tymy_ZapasyTable.Insert(tym_zapas2);

            var w = new Bodovani(tym1, tym2, Int32.Parse(skore1.Text), Int32.Parse(skore2.Text));
            w.Show();
            this.Close();


        }

        private static bool IsTextAllowed(string text)
        {
            return !_regex.IsMatch(text);
        }

        private void listTymu2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listTymu2.SelectedItem != null)
            {
                tym2 = tymy.FirstOrDefault(t => t.tym_nazev == (string)(listTymu2.SelectedItem as ListBoxItem)?.Content);
                
            }
        }

        private void listTymu1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listTymu1.SelectedItem != null)
            {
                tym1 = tymy.FirstOrDefault(t => t.tym_nazev == (string)(listTymu1.SelectedItem as ListBoxItem)?.Content);
                
            }
        }
    }
}
