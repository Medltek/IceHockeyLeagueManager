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
    /// Interaction logic for Kluby.xaml
    /// </summary>
    public partial class Kluby : Window
    {
        private Collection<Klub> kluby;
        private Klub klub;
        public Kluby()
        {
            InitializeComponent();
            listing();
        }
        private void listing()
        {

            kluby = KlubTable.SelectAll();
            if (kluby.Count == 0)
            {

                MessageBox.Show("V systému nejsou žádné kluby");

                return;
            }

            listKlubu.Items.Clear();

            foreach (var klub in kluby)
            {
                listKlubu.Items.Add(new ListBoxItem()
                {
                    Content = klub.klub_nazev
                });

            }
        }
            private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (nazev.Text == null)
            {
                MessageBox.Show("Vyplňte název klubu");
                return;
            }
            Klub k = new Klub();
            k.klub_nazev = nazev.Text;
            k.smazano = false;
            k.sponzoring = 0;
            KlubTable.Insert(k);
            MessageBox.Show("Klub vytvořen");
        }

        private void listKlubu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listKlubu.SelectedItem != null)
            {
                klub = kluby.FirstOrDefault(k => k.klub_nazev == (string)(listKlubu.SelectedItem as ListBoxItem)?.Content);

            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (listKlubu.SelectedItem == null)
            {
                MessageBox.Show("Nejdříve zvolte klub");
                return;
            }

            KlubTable.Smazat(klub);
            MessageBox.Show("Klub smazán");
            var w = new MainWindow();
            w.Show();
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (listKlubu.SelectedItem == null)
            {
                MessageBox.Show("Nejdříve zvolte klub");
                return;
            }
            var w = new ZmenaKlubu(klub);
            w.Show();
            this.Close();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (listKlubu.SelectedItem == null)
            {
                MessageBox.Show("Nejdříve zvolte klub");
                return;
            }
            KlubTable.Sponzoring(klub.klub_id);
            MessageBox.Show("Prostředky přičteny");
        }
    }
}
