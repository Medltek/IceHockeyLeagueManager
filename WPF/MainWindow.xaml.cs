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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            setVisibility();
        }
        public void setVisibility()
        {
            kluby.Visibility = Visibility.Hidden;
            tymy.Visibility = Visibility.Hidden;
            
            var admin = Uzivatel.uzivatel.is_admin;
            if (admin == true)
            {
                kluby.Visibility = Visibility.Visible;

                tymy.Visibility = Visibility.Visible;
            }
            var manager = Uzivatel.uzivatel.is_manager;
            if (manager == true)
            {
                kluby.Visibility = Visibility.Visible;

                tymy.Visibility = Visibility.Visible;
            }

        }
        private void zebricek_Click(object sender, RoutedEventArgs e)
        {
            var w = new Zebricky();
            w.Show();
            this.Close();
        }

        private void zapasy_Click(object sender, RoutedEventArgs e)
        {
            var w = new Zapasy();
            w.Show();
            this.Close();
        }

        private void hraci_Click(object sender, RoutedEventArgs e)
        {
            var w = new Hraci();
            w.Show();
            this.Close();
        }

        private void sponzori_Click(object sender, RoutedEventArgs e)
        {
            var w = new Sponzori();
            w.Show();
            this.Close();
        }

        private void kluby_Click(object sender, RoutedEventArgs e)
        {
            var w = new Kluby();
            w.Show();
            this.Close();
        }

        private void tymy_Click(object sender, RoutedEventArgs e)
        {
            var w = new Tymy();
            w.Show();
            this.Close();
        }
    }
}
