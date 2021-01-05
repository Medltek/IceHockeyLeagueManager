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
    /// Interaction logic for Tymy.xaml
    /// </summary>
    public partial class Tymy : Window
    {
        private Collection<Klub> kluby;
        private Klub klub;

        private Collection<Tym> tymy;
        private Tym tym;
        public Tymy()
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
        private void listKlubu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listKlubu.SelectedItem != null)
            {
                klub = kluby.FirstOrDefault(k => k.klub_nazev == (string)(listKlubu.SelectedItem as ListBoxItem)?.Content);

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (listKlubu.SelectedItem == null)
            {
                MessageBox.Show("Zvolte klub");
                return;
            }
            if (nazev == null)
            {
                MessageBox.Show("Vyplňte název týmu");
                return;
            }
            Tym t = new Tym();
            t.tym_nazev = nazev.Text;
            t.klub_id = klub.klub_id;
            t.smazano = false;

            TymTable.Insert(t);
            MessageBox.Show("Tým vložen");
        }

        private void listTymu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listTymu.SelectedItem != null)
            {
                tym = tymy.FirstOrDefault(t => t.tym_nazev == (string)(listTymu.SelectedItem as ListBoxItem)?.Content);

            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (listTymu.SelectedItem == null)
            {
                MessageBox.Show("Zvolte tým");
                return;
            }
            TymTable.Smazat(tym);
            MessageBox.Show("Tým smazán");
            var w = new MainWindow();
            w.Show();
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var w = new ZmenaTymu(tym);
            w.Show();
            this.Close();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (listTymu.SelectedItem == null)
            {
                MessageBox.Show("Zvolte tým");
                return;
            }
            if (tym.klub_id == 1)
            {
                MessageBox.Show("Tento tým již vlastníte");
                return;
            }
            TymTable.Koupit(tym.tym_id, 1);
            MessageBox.Show("Tým koupen");
            var w = new MainWindow();
            w.Show();
            this.Close();
        }
    }
}
