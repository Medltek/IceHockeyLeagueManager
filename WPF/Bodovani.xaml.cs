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
    /// Interaction logic for Bodovani.xaml
    /// </summary>
    public partial class Bodovani : Window
    {
        private Collection<Hrac> hraci1;
        private Collection<Hrac> hraci2;
        private Hrac hrac1;
        private Hrac hrac2;
        private Tym tym1;
        private Tym tym2;
        private Statistika statistika;
        private Collection<Statistika> statistiky1;
        private Statistika statistika1;
        private Collection<Statistika> statistiky2;
        private Statistika statistika2;
        private int goly1;
        private int goly2;
        private int g1 = 0;
        private int g2 = 0;
        public Bodovani(Tym tym1, Tym tym2, int goly1, int goly2)
        {
            InitializeComponent();
            this.tym1 = tym1;
            this.tym2 = tym2;
            this.goly1 = goly1;
            this.goly2 = goly2;
            listing();
        }

        private void listing()
        {
            hraci1 = HracTable.SelectTym(tym1.tym_id);
            hraci2 = HracTable.SelectTym(tym2.tym_id);
            if (hraci1.Count == 0)
            {

                MessageBox.Show("tým1 nevlastní žádné hráče");

                return;
            }
            if (hraci2.Count == 0)
            {

                MessageBox.Show("tým2 nevlastní žádné hráče");

                return;
            }
            listHracu1.Items.Clear();
            listHracu2.Items.Clear();
            foreach (var hrac in hraci1)
            {
                listHracu1.Items.Add(new ListBoxItem()
                {
                    Content = hrac.hrac_jmeno
                });
                statistiky1 = StatistikaTable.SelectHrac(hrac.hrac_id);
            }
            foreach (var hrac in hraci2)
            {
                listHracu2.Items.Add(new ListBoxItem()
                {
                    Content = hrac.hrac_jmeno
                });
                statistiky2 = StatistikaTable.SelectHrac(hrac.hrac_id);
            }
        }

        private void listHracu1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listHracu1.SelectedItem != null)
            {
                hrac1 = hraci1.FirstOrDefault(h => h.hrac_jmeno == (string)(listHracu1.SelectedItem as ListBoxItem)?.Content);
                statistika1 = statistiky1.FirstOrDefault(s => s.hrac_id == hrac1.hrac_id);
                if (statistika1 == null)
                {
                    MessageBox.Show("Vybraný hráč nemá vytvořený záznam o statitistice");
                    return;
                }
            }
        }

        private void listHracu2_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (listHracu2.SelectedItem != null)
            {
                hrac2 = hraci2.FirstOrDefault(h => h.hrac_jmeno == (string)(listHracu2.SelectedItem as ListBoxItem)?.Content);
                statistika2 = statistiky2.FirstOrDefault(s => s.hrac_id == hrac2.hrac_id);
                if (statistika2 == null)
                {
                    MessageBox.Show("Vybraný hráč nemá vytvořený záznam o statitistice");
                    return;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (g1 < goly1)
            {
                statistika = new Statistika();
                statistika.stat_id = statistika1.stat_id;
                statistika.asistence = statistika1.asistence;
                statistika.body = statistika1.body++;
                statistika.goly = statistika1.goly++;
                statistika.hrac_id = statistika1.hrac_id;
                statistika.zapasy = statistika1.zapasy++;
                statistika.smazano = false;
                StatistikaTable.Update(statistika);
                g1++;
            }
            else MessageBox.Show("Bylo vloženo maximum gólů pro tým1");
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            
            statistika = new Statistika();
            statistika.stat_id = statistika1.stat_id;
            statistika.asistence = statistika1.asistence++;
            statistika.body = statistika1.body++;
            statistika.goly = statistika1.goly;
            statistika.hrac_id = statistika1.hrac_id;
            statistika.zapasy = statistika1.zapasy++;
            statistika.smazano = false;
            StatistikaTable.Update(statistika);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (g2 < goly2)
            {
                statistika = new Statistika();
                statistika.stat_id = statistika2.stat_id;
                statistika.asistence = statistika2.asistence;
                statistika.body = statistika2.body++;
                statistika.goly = statistika2.goly++;
                statistika.hrac_id = statistika2.hrac_id;
                statistika.zapasy = statistika2.zapasy++;
                statistika.smazano = false;
                StatistikaTable.Update(statistika);
                g2++;
            }
            else MessageBox.Show("Bylo vloženo maximum gólů pro tým2");
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            statistika = new Statistika();
            statistika.stat_id = statistika2.stat_id;
            statistika.asistence = statistika2.asistence++;
            statistika.body = statistika2.body++;
            statistika.goly = statistika2.goly;
            statistika.hrac_id = statistika2.hrac_id;
            statistika.zapasy = statistika2.zapasy++;
            statistika.smazano = false;
            StatistikaTable.Update(statistika);
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            if(g1<goly1)
            {
                int i = goly1 - g1;
                MessageBox.Show("chybí vložit ještě: " + i + " gólů pro tým1");
                return;
            }
            if (g2 < goly2)
            {
                int i = goly2 - g2;
                MessageBox.Show("chybí vložit ještě: " + i + " gólů pro tým2");
                return;
            }
            if (g1 == goly1 && g2 == goly2)
            {
                MessageBox.Show("Úspěšně dokončeno!");
                var w = new MainWindow();
                w.Show();
                this.Close();
            }

            
        }
    }
}
