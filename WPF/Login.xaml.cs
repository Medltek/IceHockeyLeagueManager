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
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(username.Text))
            {
                MessageBox.Show("Vyplňte uživatelské jméno");
                return;
            }

            if (string.IsNullOrWhiteSpace(password.Text))
            {
                MessageBox.Show("Vyplňte heslo");
                return;
            }

            var uzi = UziTable.Login(username.Text, password.Text);

            if (uzi == null)
            {
                MessageBox.Show("Špatné přihlašovací údaje");
            }
            else
            {
                Uzivatel.uzivatel = uzi;

                var w = new MainWindow();
                w.Show();
                this.Close();
            }
            
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var uzi = UziTable.Login("uzivatel", "uzivatel");

            Uzivatel.uzivatel = uzi;

            var w = new MainWindow();
            w.Show();
            this.Close();
        }
    }
}
