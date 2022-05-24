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
    /// Логика взаимодействия для ActDeletionWindow.xaml
    /// </summary>
    public partial class ActDeletionWindow : Window
    {
        public string vb = "";
        public ActDeletionWindow()
        {
            InitializeComponent();
            UpdateGrid();
        }
        public void UpdateGrid()
        {
            //dataActDeletion.ItemsSource = null;
            dataActDeletion.ItemsSource = StorageEntities.GetContext().Storage.ToList().Where(p => p.status == false);

            
        }

        private void btnAddActDeletion_Click(object sender, RoutedEventArgs e)
        {
            bool isExist = false;
            Storage deletedEntity = new Storage();
            foreach (Storage entity in StorageEntities.GetContext().Storage)
            {
                if (tbInventory.Text == entity.InventoryNumber)
                {
                    isExist = true;
                    deletedEntity = entity;
                }    
            }
            if (isExist)
            {
                if(deletedEntity.DateDeletion != null)
                {
                    MessageBox.Show("Предмет с таким инвентарным номером уже списан.");
                }
                else
                {
                    deletedEntity.status = false;
                    deletedEntity.DateDeletion = DateTime.Today;
                    UpdateGrid();
                }
                
            }
            else
            {
                MessageBox.Show("Такого инвентарного номера не существует."); 
            }
            isExist = false;
        }


        private void btnActDeletion_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Сохранено!");
            
            StorageEntities.GetContext().SaveChanges();
            UpdateGrid();
        }

        private void btnCloseActDeletion_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

        }
    }
}
