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
    /// Interaction logic for KoupitHrace.xaml
    /// </summary>
    public partial class KoupitHrace : Window
    {
        private Collection<Tym> tymy;
        private Tym tym;
        private Hrac hrac;
        public KoupitHrace(Hrac hrac)
        {
            InitializeComponent();
            listing();
            this.hrac = hrac;
        }

        private void listing()
        {
            tymy = TymTable.SelectKlub(Uzivatel.uzivatel.klub_id);
            if (tymy.Count == 0)
            {

                MessageBox.Show("Nevlastníte žádný tým");
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

        private void listTymu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listTymu.SelectedItem != null)
            {
                tym = tymy.FirstOrDefault(t => t.tym_nazev == (string)(listTymu.SelectedItem as ListBoxItem)?.Content);

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (listTymu.SelectedItem == null)
            {
                MessageBox.Show("Zvolte tým");
                return;
            }
            if (KlubTable.Select(Uzivatel.uzivatel.klub_id).sponzoring < hrac.cena)
            {
                MessageBox.Show("Váš klub nemá dostatek financí");
                return;
            }
                
                HracTable.Koupit(hrac.hrac_id, tym.tym_id, Uzivatel.uzivatel.klub_id);
            MessageBox.Show("Hráč odkoupen");
            var w = new MainWindow();
            w.Show();
            this.Close();
        }
    }
}
