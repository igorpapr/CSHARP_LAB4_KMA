using System;
using System.Windows;
using PersonListApp.Models;
using PersonListApp.ViewModels;

namespace PersonListApp
{
    /// <summary>
    /// Логика взаимодействия для AddEditWindow.xaml
    /// </summary>
    public partial class AddEditWindow : Window
    {
        //for adding
        public AddEditWindow()
        {
            InitializeComponent();
            
            AddEditViewModel vm = new AddEditViewModel();
            DataContext = vm;
            if (vm.CloseAction == null)
                vm.CloseAction = new Action(this.Close);
        }

        //for editing
        public AddEditWindow(Person person)
        {
            InitializeComponent();
            AddEditViewModel vm = new AddEditViewModel(person);
            DataContext = vm;
            if (vm.CloseAction == null)
                vm.CloseAction = new Action(this.Close);
        }
    }
}
