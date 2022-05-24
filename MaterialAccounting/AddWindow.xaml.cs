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
    /// Логика взаимодействия для AddWindow.xaml
    /// </summary>
    public partial class AddWindow : Window
    {
        public string vb="";
        public bool editing = false;
        public AddWindow()
        {
            InitializeComponent();
            
        }

        private void tbNameAdd_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        public void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (editing == true)
            {
                MainWindow mw = new MainWindow();
                StorageRecords editingRecord = StorageEntities.GetContext().StorageRecords.FirstOrDefault(s => s.Name.Equals(vb));
                editingRecord.Category = tbCategoryAdd.Text;
                editingRecord.Count = Convert.ToInt32(tbCountAdd.Text);
                editingRecord.Name = tbNameAdd.Text;
                StorageEntities.GetContext().SaveChanges();
                
                this.Close();
            }
            else
            {
                StorageRecords record = new StorageRecords();
                record.Category = tbCategoryAdd.Text;
                int count = 0;
                if (Int32.TryParse(tbCountAdd.Text, out count))
                {
                    record.Count = count;
                }
                else
                {
                    MessageBox.Show("Введите число!");
                    return;
                }
                record.Name = tbNameAdd.Text;
                record.status = true;
                StorageEntities.GetContext().StorageRecords.Add(record);
                StorageEntities.GetContext().SaveChanges();
                this.Close();
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
