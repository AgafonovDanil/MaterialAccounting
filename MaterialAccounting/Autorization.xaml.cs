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

namespace MaterialAccounting
{
    /// <summary>
    /// Логика взаимодействия для Autorization.xaml
    /// </summary>
    public partial class Autorization : Window
    {
        public Autorization()
        {
            InitializeComponent();
            tbLogin.Text = "admin";
            tbPassword.Text = "admin";
        }

        private void btnOpen_Click(object sender, RoutedEventArgs e)
        {
            string login = tbLogin.Text;
            string password = tbPassword.Text;
            bool Connected = false;
            foreach (var item in StorageEntities.GetContext().Users)
            {
                
                if (item.Login==login&&item.Password==password)
                {
                    Connected = true;
                }
            }
            if (Connected)
            {
                MainWindow MW = new MainWindow();
                MW.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Не правильный логин или пароль!","Ошибка!");
            }
        }
    }
}
