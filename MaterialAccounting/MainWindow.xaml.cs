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

namespace MaterialAccounting
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        StorageEntities db = new StorageEntities();

        public string vb = "";


        public static MainWindow SelfRef { get; set; }
        
        public MainWindow()
        {
            InitializeComponent();
            SelfRef = this;
            UpdateGrid();

        }
        
        public void UpdateGrid()
        {
            dataStorageRecords.ItemsSource = null;
            dataStorageRecords.ItemsSource = StorageEntities.GetContext().StorageRecords.ToList().Where(p=>p.status == true);
            var storageRecords = StorageEntities.GetContext().StorageRecords.ToList().Where(p => p.status == true);
            var storage = StorageEntities.GetContext().Storage.ToList().Where(p => p.status == true);
            foreach (var item in storageRecords)
            {
                int price = 0;
                foreach (var storageItem in storage)
                {
                    if (storageItem.Name == item.Name)
                    {
                        price += Convert.ToInt32(storageItem.Price);
                    }
                    
                }
                item.generalPrice = price.ToString();
            }
        }

        //Добавление оснащения
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddWindow aw = new AddWindow();
            aw.Owner = this;
            aw.btnSave.Click += (awsender, awe) =>
            {
                //int count = Convert.ToInt32(aw.tbCountAdd.Text);
                //for (int i = 0; i < count; i++)
                //{
                //    Storage addStorage = new Storage();
                //    addStorage.Name = aw.tbNameAdd.Text;
                //    dataStorage.Items.Add(addStorage);
                //}
                UpdateGrid();
            };
            aw.editing = false;
            aw.Show();
        }

        private void btnChanged_Click(object sender, RoutedEventArgs e)
        {
            AddWindow aw = new AddWindow();
            aw.Owner = this;
            aw.btnSave.Click += (awsender, awe) =>
             {
                 
                 UpdateGrid();
             };
            try
            {
                object item = dataStorageRecords.SelectedItem;
                vb = Convert.ToString((dataStorageRecords.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text);
                StorageRecords editingRecord = StorageEntities.GetContext().StorageRecords.FirstOrDefault(s => s.Name.Equals(vb));
                aw.tbCategoryAdd.Text = editingRecord.Category;
                aw.tbCountAdd.Text = editingRecord.Count.ToString();
                aw.tbNameAdd.Text = editingRecord.Name;
                aw.vb = vb;
            }
            catch
            {
                MessageBox.Show("Выберите оснащение!");
            }
            aw.editing = true;
            aw.Show();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            StorageEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p=>p.Reload());
            StorageEntities.GetContext().SaveChanges();
        }


        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                object item = dataStorageRecords.SelectedItem;
                vb = (dataStorageRecords.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text;
               
                foreach (var entity in StorageEntities.GetContext().Storage)
                {
                    if (vb == entity.Name)
                    {
                        entity.status = false;
                        entity.DateDeletion = DateTime.Today;
                    }
                }
                foreach (var entity in StorageEntities.GetContext().StorageRecords)
                {
                    if (vb == entity.Name)
                    {
                        entity.status = false;
                    }
                }

                StorageEntities.GetContext().SaveChanges();
                UpdateGrid();
                MessageBox.Show($"{vb} полностью удалено.");
                
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message.ToString());
            }
        }

        private void Category_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedCategory = cbCategory.SelectedItem.ToString();
            if(selectedCategory == "Все")
            {
                dataStorageRecords.ItemsSource = StorageEntities.GetContext().StorageRecords.ToList().Where(p=>p.status==true);
            }
            else
            {
                dataStorageRecords.ItemsSource = StorageEntities.GetContext().StorageRecords.ToList().Where(p => p.Category == selectedCategory && p.status==true);
            }
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateCategories();
        }

        void UpdateCategories()
        {
            var categories = from i in StorageEntities.GetContext().StorageRecords
                             select i.Category;
            var uniqueCategories = categories.Distinct();
            string[] all = { "Все" };
            uniqueCategories = uniqueCategories.Concat(all);
            cbCategory.ItemsSource = uniqueCategories.ToArray();
            
        }

        private void btnWatch_Click(object sender, RoutedEventArgs e)
        {
            string senderstring = sender.ToString();
            StorageRecords selecteditem = (StorageRecords)dataStorageRecords.SelectedItem;
            string name = selecteditem.Name;
            int? count = selecteditem.Count;
            WindowStorage ws = new WindowStorage(name,count);
            ws.btnSave.Click += (awsender, awe) =>
            {

                UpdateGrid();
            };
            // ws.source = StorageEntities.GetContext().Storage.ToList().Where(p => p.Name == name);
            ws.Show();
        }

        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchName = tbSearch.Text;
            if (searchName != "")
            {
                var names = from n in StorageEntities.GetContext().StorageRecords
                            select n.Name;
                var selectedStrings = StorageEntities.GetContext().StorageRecords.ToList().Where(p => p.Name.StartsWith(searchName) && p.status == true);
                if (selectedStrings.Count()>0)
                {
                    dataStorageRecords.ItemsSource = selectedStrings;
                }
                else
                {
                    dataStorageRecords.ItemsSource = StorageEntities.GetContext().StorageRecords.ToList().Where(p=>p.status==true);
                    
                }
            }
            else
            {
                dataStorageRecords.ItemsSource = StorageEntities.GetContext().StorageRecords.ToList().Where(p => p.status == true);
            }
        }

        private void btnActs_Click(object sender, RoutedEventArgs e)
        {
            /* AddWindow aw = new AddWindow();
             aw.Owner = this;
             aw.btnSave.Click += (awsender, awe) =>
             aw.editing = false;
             aw.Show();*/

            ChoiceActs ca = new ChoiceActs();
            ca.Owner = this;
            
            ca.Show();
        }

        private void StorageRecordsPrint_MouseDown(object sender, MouseButtonEventArgs e)
        {
         // Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
         // excel.Visible = true;
         // Workbook workbook = excel.Workbooks.Add(System.Reflection.Missing.Value);
         // Worksheet sheet1 = (Worksheet)workbook.Sheets[1];
         //
         //
         //  for (int j = 0; j < dataStorageRecords.Columns.Count; j++)
         // {
         //     Range myRange = (Range)sheet1.Cells[1, j + 1];
         //     sheet1.Cells[1, j + 1].Font.Bold = true;
         //     sheet1.Columns[j + 1].ColumnWidth = 15;
         //     myRange.Value2 = dataStorageRecords.Columns[j].Header;
         // }
         // for (int i = 0; i < dataStorageRecords.Columns.Count; i++)
         // {
         //     for (int j = 0; j < dataStorageRecords.Items.Count; j++)
         //     {
         //         TextBlock b = dataStorageRecords.Columns[i].GetCellContent(dataStorageRecords.Items[j]) as TextBlock;
         //         Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[j + 2, i + 1];
         //         myRange.Value2 = b.Text;
         //
         //     }
         //      
         // }
         //
        }
    
    }
}
