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
    /// Interaction logic for VymenaHrace.xaml
    /// </summary>
    public partial class VymenaHrace : Window
    {
        private Collection<Hrac> hraci;
        private Hrac hrac;
        private Hrac hrac2;

        public VymenaHrace(Hrac hrac)
        {
            InitializeComponent();
            this.hrac = hrac;
            listing();
        }

        private void listing()
        {
            hraci = HracTable.SelectAll();
            if (hraci.Count == 0)
            {
                
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

            }


        }

        private void listHracu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listHracu.SelectedItem != null)
            {

                hrac2 = hraci.FirstOrDefault(h => h.hrac_jmeno == (string)(listHracu.SelectedItem as ListBoxItem)?.Content);
                
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (listHracu.SelectedItem == null)
            {
                MessageBox.Show("Vyberte hráče");
                return;
            }
           HracTable.Vymenit(hrac.hrac_id, hrac2.hrac_id);
           MessageBox.Show("Hráč vyměněn");
            var w = new MainWindow();
            w.Show();
            this.Close();

        }
    }
}
