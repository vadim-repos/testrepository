using System;
using System.ComponentModel;
using System.Windows;
using ToDoApp.Models;
using ToDoApp.Services;

namespace ToDoApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BindingList<ToDoModel> _toDoData;
        private readonly string PATH = $"{Environment.CurrentDirectory}\\toDoDataList.json";
        private FileIOService _fileIOService;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _fileIOService = new FileIOService(PATH);

            try
            {
                _toDoData = _fileIOService.LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Close();
            }
            
            dgToDoList.ItemsSource = _toDoData;
            _toDoData.ListChanged += ToDoDataListChanged;
        }

        private void ToDoDataListChanged (object sender, ListChangedEventArgs e)
        {
            if(e.ListChangedType == ListChangedType.ItemAdded || e.ListChangedType == ListChangedType.ItemChanged || e.ListChangedType == ListChangedType.ItemDeleted)
            {
                try
                {
                    _fileIOService.SaveData(sender);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    Close();
                }                
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < _toDoData.Count;)
            {
                if (_toDoData[i].IsSelected)
                {
                    _toDoData.RemoveAt(i);
                }
                else i++;
            }
        }
    }
}
