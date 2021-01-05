using ProjektProjektProjekt.Database;
using ProjektProjektProjekt.Database.Tabulky;
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
using System.Windows.Shapes;

namespace WPF
{
    /// <summary>
    /// Interaction logic for ZmenaKlubu.xaml
    /// </summary>
    public partial class ZmenaKlubu : Window
    {
        private Klub klub;
        public ZmenaKlubu(Klub klub)
        {
            InitializeComponent();
            this.klub = klub;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (nazev.Text == null)
            {
                MessageBox.Show("Vyplňte název");
                return;
            }
            klub.klub_nazev = nazev.Text;
            KlubTable.Update(klub);
            MessageBox.Show("Název změněn");
            var w = new MainWindow();
            w.Show();
            this.Close();
        }
    }
}
