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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class ZmenaTymu : Window
    {
        private Tym tym;
        public ZmenaTymu(Tym tym)
        {
            InitializeComponent();
            this.tym = tym;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            if (nazev == null)
            {
                MessageBox.Show("Vyplňte název týmu");
                return;
            }
            Tym t = new Tym();
            t.tym_nazev = nazev.Text;
            t.klub_id = tym.klub_id;
            t.smazano = false;

            TymTable.Update(t);
            MessageBox.Show("Tým vložen");
            var w = new MainWindow();
            w.Show();
            this.Close();
        }
    }
}
