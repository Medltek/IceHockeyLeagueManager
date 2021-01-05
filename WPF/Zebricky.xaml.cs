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
    /// Interaction logic for Zebricky.xaml
    /// </summary>
    public partial class Zebricky : Window
    {
        public Zebricky()
        {
            InitializeComponent();
            GetZebr();
        }

        private void GetZebr()
        {
            
            var zebr = ZebricekTable.SelectAll();
            dataGridZebr.ItemsSource = zebr;
        }

       
    }

}
