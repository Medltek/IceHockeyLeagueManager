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
    /// Interaction logic for Sponzori.xaml
    /// </summary>
    public partial class Sponzori : Window
    {
        private Collection<Klub> kluby;
        private Klub klub;
        private static readonly Regex _regex = new Regex("[^0-9]+");

        private Collection<Sponzor> sponzori;
        private Sponzor sponzor;
        public Sponzori()
        {
            InitializeComponent();
            GetSponzors();
            listing();
            setVisibility();
        }
        public void setVisibility()
        {
            var admin = Uzivatel.uzivatel.is_admin;

            buttSmazat.Visibility = admin ? Visibility.Visible : Visibility.Hidden;
            buttVlozit.Visibility = admin ? Visibility.Visible : Visibility.Hidden;
        }
        private void listing()
        {
            
            kluby = KlubTable.SelectAll();
            if (kluby.Count == 0)
            {

                MessageBox.Show("V systému nejsou žádné kluby");

                return;
            }
            
            listKlub.Items.Clear();
            
            foreach (var klub in kluby)
            {
                listKlub.Items.Add(new ListBoxItem()
                {
                    Content = klub.klub_nazev
                });
               
            }

            sponzori = SponzorTable.SelectAll();
            if (sponzori.Count == 0)
            {

                MessageBox.Show("V systému nejsou žádní sponzoři");

                return;
            }

            listSponzoru.Items.Clear();

            foreach (var sponzor in sponzori)
            {
                listSponzoru.Items.Add(new ListBoxItem()
                {
                    Content = sponzor.sponzor_nazev
                }) ;

            }

        }

        private void GetSponzors()
        {
            var s = SponzorTable.SelectAll();
            dataGrids.ItemsSource = s;
        }

        private void listKlub_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listKlub.SelectedItem != null)
            {
                klub = kluby.FirstOrDefault(k => k.klub_nazev == (string)(listKlub.SelectedItem as ListBoxItem)?.Content);
                
            }
        }
        private static bool IsTextAllowed(string text)
        {
            return !_regex.IsMatch(text);
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            /* Klub k = new Klub();
             k.klub_nazev = klub.klub_nazev;
             k.smazano = false;
             k.sponzoring = 0;
             KlubTable.Insert(k);*/

            if (castka.Text == null)
            {
                MessageBox.Show("Vyplňte částku");
                return;
            }
            
            if (listSponzoru.SelectedItem == null)
            {
                MessageBox.Show("Vyberte klub");

                return;
            }
            if (nazev.Text == null)
            {
                MessageBox.Show("Vyplňte název");
                return;
            }
            if (IsTextAllowed(castka.Text) == false)
            {
                MessageBox.Show("Pro částku použijte číslo");
                return;
            }
            Sponzor s = new Sponzor();
            s.klub_id = klub.klub_id;
            s.smazano = false;
            s.castka = Int32.Parse(castka.Text);
            s.sponzor_nazev = nazev.Text;
            SponzorTable.Insert(s);
        }

        private void listSponzoru_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listSponzoru.SelectedItem != null)
            {
                sponzor = sponzori.FirstOrDefault(k => k.sponzor_nazev == (string)(listSponzoru.SelectedItem as ListBoxItem)?.Content);

            }
        }
        
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (listSponzoru.SelectedItem == null)
            {
                MessageBox.Show("Vyberte sponzora");

                return;
            }
            SponzorTable.Smazat(sponzor);
        }
    }
}
