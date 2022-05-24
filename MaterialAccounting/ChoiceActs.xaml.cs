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
    /// Логика взаимодействия для ChoiceActs.xaml
    /// </summary>
    public partial class ChoiceActs : Window
    {
        public ChoiceActs()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnActDeletion_Click(object sender, RoutedEventArgs e)
        {
            ActDeletionWindow adt = new ActDeletionWindow();
            adt.Show();
            adt.btnActDeletion.Click += (awsender, awe) =>
            {
                MainWindow.SelfRef.UpdateGrid();
            };
            this.Close();
        }

        private void btnActMoving_Click(object sender, RoutedEventArgs e)
        {
            ActMovingWindow amw = new ActMovingWindow();
            amw.Show();
            amw.btnActMoving.Click += (awsender, awe) =>
            {
                MainWindow.SelfRef.UpdateGrid();
            };
            this.Close();
        }
    }
}
