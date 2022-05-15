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
        public string vb = "";
        public MainWindow()
        {
            InitializeComponent();
            
            UpdateGrid();
        }

        public void UpdateGrid()
        {
            dataStorageRecords.ItemsSource = StorageEntities.GetContext().StorageRecords.ToList();
        }


        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddWindow aw = new AddWindow();
            aw.Owner = this;
            aw.btnSave.Click += (awsender, awe) =>
            {
                int count = Convert.ToInt32(aw.tbCountAdd.Text);
                for (int i = 0; i < count; i++)
                {
                    Storage addStorage = new Storage();
                    addStorage.Name = aw.tbNameAdd.Text;
                    dataStorage.Items.Add(addStorage);
                }
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

        private void dataStorageRecords_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //StorageRecords selecteditem = (StorageRecords)dataStorageRecords.SelectedItem;
            //string name = selecteditem.Name;
            //int? count = selecteditem.Count;
            //dataStorage.ItemsSource = StorageEntities.GetContext().Storage.ToList().Where(p => p.Name == name);
            //IEnumerable<Storage> source = StorageEntities.GetContext().Storage.ToList().Where(p => p.Name == name);
            //
            //if ( count> dataStorage.Items.Count)
            //{
            //    StorageRecords namee = new StorageRecords();
            //    namee.Name = name;
            //    //source.Union();
            //    for (int i = 0; i < count-dataStorage.Items.Count; i++)
            //    {
            //        
            //    }
            //    dataStorage.ItemsSource = source;
            //}
        }

        private void dataStorage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void dataStorage_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            dataStorage.BeginEdit();
        }
    }
}
