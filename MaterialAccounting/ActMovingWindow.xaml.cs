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
    /// Логика взаимодействия для ActMovingWindow.xaml
    /// </summary>
    public partial class ActMovingWindow : Window
    {
        public string vb;
        public ActMovingWindow()
        {
            InitializeComponent();
            UpdateGrid();
        }

        public void UpdateGrid()
        {
            dataActMoving.ItemsSource = StorageEntities.GetContext().Storage.ToList().Where((p => p.editingWorkshop==true));
            
        }

        private void dataActMoving_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnCloseActMoving_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnAddActMoving_Click(object sender, RoutedEventArgs e)
        {
            Storage movingEntity = new Storage();
            foreach (Storage entity in StorageEntities.GetContext().Storage)
            {
                if ((tbInventoryMoving.Text == entity.InventoryNumber)&&(entity.editingWorkshop==null || entity.editingWorkshop==false) && (entity.status==true))
                {
                    entity.editingWorkshop = true;
                    movingEntity = entity;
                }
            }
            UpdateGrid();
        }

        private void btnActMoving_Click(object sender, RoutedEventArgs e)
        {
            
            MessageBoxResult result = MessageBox.Show("Сохранить изменения?", "", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Storage movingEntity = new Storage();
                foreach (Storage entity in StorageEntities.GetContext().Storage)
                {
                    if (tbInventoryMoving.Text == entity.InventoryNumber)
                    {
                        entity.WorkshopNumber = tbWorkshopNumber.Text;
                        entity.DateMoving = DateTime.Today;
                        movingEntity = entity;
                    }
                }
                StorageEntities.GetContext().SaveChanges();
            }
            else
            {
                
            }

            UpdateGrid();
            
        }
    }
}
