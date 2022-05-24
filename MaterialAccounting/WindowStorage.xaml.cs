using System;
using System.Collections.Generic;
using System.Data;
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
    /// Логика взаимодействия для WindowStorage.xaml
    /// </summary>
    public partial class WindowStorage : Window
    {
        public WindowStorage(string Name, int? count)
        {
            InitializeComponent();
            var source = StorageEntities.GetContext().Storage.ToList().Where(p => p.Name == Name && p.status==true);
            var kolvo = StorageEntities.GetContext().Storage.ToList().Where(p => p.Name == Name && p.status==true).Count();
            if (count > dataStorage.Items.Count)
            {
                Storage namee = new Storage();
                namee.Name = Name;
                namee.DateMoving = null;
                namee.DateReceive = null;
                namee.Garanty = "";
                namee.InventoryNumber = "";
                namee.Price = "";
                namee.WorkshopNumber = "";
                
                for (int i = 0; i < count - dataStorage.Items.Count-kolvo; i++)
                {
                    source = source.Append(namee);
                }
                
            }
            
            List<Storage> calls = new List<Storage>();
            calls.AddRange(source);
            dataStorage.ItemsSource = calls.ToArray();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            dataStorage.BeginEdit();
            dataStorage.CommitEdit();
            dataStorage.CancelEdit();
            int count = 0;
            Storage newEntrie = new Storage();
            for (int i = 0; i < dataStorage.Items.Count; i++)
            {
                bool isEmpty = false;
                DataGridRow row = (DataGridRow)dataStorage.ItemContainerGenerator.ContainerFromIndex(i);
                for (int j = 0; j < dataStorage.Columns.Count; j++)
                {
                    TextBlock tbl = dataStorage.Columns[j].GetCellContent(row) as TextBlock;
                    string inf = tbl.Text;
                    
                    if(inf == "")
                    {
                        isEmpty = true;
                    }
                    var storage = from p in StorageEntities.GetContext().Storage
                                  select p.InventoryNumber;
                    if (storage.Contains((dataStorage.Columns[1].GetCellContent(row) as TextBlock).Text))
                    {
                        isEmpty=true;
                    }
                }
                if(isEmpty == false)
                {
                    newEntrie.Name = (dataStorage.Columns[0].GetCellContent(row) as TextBlock).Text;
                    newEntrie.InventoryNumber = (dataStorage.Columns[1].GetCellContent(row) as TextBlock).Text;
                    newEntrie.DateReceive = Convert.ToDateTime((dataStorage.Columns[2].GetCellContent(row) as TextBlock).Text);
                    newEntrie.DateMoving = Convert.ToDateTime((dataStorage.Columns[2].GetCellContent(row) as TextBlock).Text);
                    newEntrie.Garanty = (dataStorage.Columns[4].GetCellContent(row) as TextBlock).Text;
                    newEntrie.Price = (dataStorage.Columns[5].GetCellContent(row) as TextBlock).Text;
                    newEntrie.WorkshopNumber = (dataStorage.Columns[6].GetCellContent(row) as TextBlock).Text;
                    newEntrie.status = true;
                    var storage = from p in StorageEntities.GetContext().Storage
                                  select p.InventoryNumber;
                    if (storage.Contains(newEntrie.InventoryNumber))
                    {
                        MessageBox.Show("Один из элементов уже существует и не был добавлен (одинаковый инвентарный номер)");
                    }
                    else
                    {
                        StorageEntities.GetContext().Storage.Add(newEntrie);
                        StorageEntities.GetContext().SaveChanges();
                        count++;
                    }
                        
                } 
            }
            MessageBox.Show($"В базу успешно добавлено {count} строк");
            this.Close();

        }

        private void dataStorage_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                e.Handled = true;
                dataStorage.BeginEdit();
                dataStorage.CommitEdit();
                //dataStorage.CancelEdit();
            }
        }  
    }
}
