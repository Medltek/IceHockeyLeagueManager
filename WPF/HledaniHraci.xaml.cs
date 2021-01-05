using ProjektProjektProjekt.Database;
using ProjektProjektProjekt.Database.Tabulky;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;

namespace WPF
{
    /// <summary>
    /// Interaction logic for HledaniHraci.xaml
    /// </summary>
    public partial class HledaniHraci : Window
    {
        private Collection<Hrac> hraci;
        private Hrac hrac;
        
        String hrac_jmeno;
        bool fail = false;
        private Collection<Statistika> statistiky;
        private Statistika statistika;
        public HledaniHraci(String t)
        {
            InitializeComponent();
            hrac_jmeno = t;
            listing();
            setVisibility();
        }
        public void setVisibility()
        {
            buttSmazat.Visibility = Visibility.Hidden;
            buttKoupit.Visibility = Visibility.Hidden;
            buttVymenit.Visibility = Visibility.Hidden;
            buttSmazatStat.Visibility = Visibility.Hidden;
            buttVytvorStat.Visibility = Visibility.Hidden;

            var admin = Uzivatel.uzivatel.is_admin;
            if (admin == true)
            {
                buttSmazat.Visibility = Visibility.Visible;
                buttVytvorStat.Visibility = Visibility.Visible;
                buttSmazatStat.Visibility = Visibility.Visible;
            }
            var manager = Uzivatel.uzivatel.is_manager;
            if (manager == true)
            {
                buttKoupit.Visibility = Visibility.Visible;

                buttVymenit.Visibility = Visibility.Visible;
            }
            
        }
        private void listing()
        {
            hraci = HracTable.SelectJmeno(hrac_jmeno);
            if(hraci.Count == 0)
            {
                fail = true;
                MessageBox.Show("Zadaný hráč se nenachází v systému");
                
                return;
            }
            listHracu.Items.Clear();
            foreach (var hrac in hraci)
            {
                listHracu.Items.Add(new ListBoxItem()
                {
                    Content = hrac.hrac_jmeno
                });
                statistiky = StatistikaTable.SelectHrac(hrac.hrac_id);

            }

            
        }
        private void listHracu_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listHracu.SelectedItem != null)
            {
                
                hrac = hraci.FirstOrDefault(h => h.hrac_jmeno == (string)(listHracu.SelectedItem as ListBoxItem)?.Content);
                statistika = statistiky.FirstOrDefault(s => s.hrac_id == hrac.hrac_id);
                if (statistika == null)
                {
                    MessageBox.Show("Vybraný hráč nemá vytvořený záznam o statitistice");
                   
                }
                    Update();
            }
        }
        private void Update()
        {
            jmeno.Content = hrac.hrac_jmeno;
            vaha.Content = hrac.vaha;
            vyska.Content = hrac.vyska;
            post.Content = hrac.post;
            zahyb.Content = hrac.zahyb;
            if(statistika == null)
            {
                goly.Content = null;
                zapasy.Content = null;
                asistence.Content = null;
            }
            else
            {
                goly.Content = statistika.goly;
                zapasy.Content = statistika.zapasy;
                asistence.Content = statistika.asistence;
            }
            
            cena.Content = hrac.cena;
            tym_id.Content = TymTable.Select(hrac.tym_id).tym_nazev;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (listHracu.SelectedItem == null)
            {
                MessageBox.Show("Nejdříve vyberte hráče");
                return;
            }
            var w = new VymenaHrace(hrac);
            w.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (listHracu.SelectedItem == null)
            {
                MessageBox.Show("Nejdříve vyberte hráče");
                return;
            }
            var w = new KoupitHrace(hrac);
            w.Show();
            this.Close();
            
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (listHracu.SelectedItem == null)
            {
                MessageBox.Show("Nejdříve vyberte hráče");
                return;
            }
            HracTable.Smazat(hrac);
            MessageBox.Show("Hráč vymazán");
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (listHracu.SelectedItem == null)
            {
                MessageBox.Show("Nejdříve vyberte hráče");
                return;
            }
            if (statistika == null)
            {
                MessageBox.Show("Hráč nemá záznam o statistice");
                return;
            }
            StatistikaTable.Smazat(statistika);
            MessageBox.Show("Statistika vymazána");
        }

        private void buttVytvorStat_Click(object sender, RoutedEventArgs e)
        {
            if (listHracu.SelectedItem == null)
            {
                MessageBox.Show("Nejdříve vyberte hráče");
                return;
            }
            if (statistika != null)
            {
                MessageBox.Show("Hráč již má záznam o statistice");
                return;
            }
            Statistika s = new Statistika();
            s.asistence = 0;
            s.body = 0;
            s.goly = 0;
            s.hrac_id = hrac.hrac_id;
            s.smazano = false;
            s.zapasy = 0;
            StatistikaTable.Insert(s);
            MessageBox.Show("Statistika vytvořena");
        }
    }
}
