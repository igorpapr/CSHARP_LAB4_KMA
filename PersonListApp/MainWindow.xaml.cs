using System.Windows;
using PersonListApp.Tools.DataStorage;
using PersonListApp.Tools.Managers;
using PersonListApp.ViewModels;

namespace PersonListApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            StationManager.Initialize(new SerializedDataStorage());
            DataContext = new PersonListViewModel();
        }
    }
}
