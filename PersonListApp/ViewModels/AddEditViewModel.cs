using System;
using System.Windows;
using PersonListApp.Models;
using PersonListApp.Tools;
using PersonListApp.Tools.Managers;

namespace PersonListApp.ViewModels
{
    class AddEditViewModel: BaseViewModel
    {
        #region Fields
        private Person _person = null;
        private string _firstName;
        private string _lastName;
        private string _email;
        private DateTime _date = DateTime.Today;
        #endregion
        
        #region Commands
        private RelayCommand<object> _submitCommand;
        private RelayCommand<object> _cancelCommand;
        #endregion

        internal AddEditViewModel(Person person)
        {
            _person = person;
            FirstName = person.FirstName;
            LastName = person.LastName;
            Email = person.Email;
            Date = person.Date;
        }

        internal AddEditViewModel()
        {
        }

        public DateTime Date
        {
            get { return _date; }
            set
            {
                _date = value;
                OnPropertyChanged();
            }
        }

        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                OnPropertyChanged();
            }
        }

        public Action CloseAction { get; set; }

        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                OnPropertyChanged();
            }
        }

        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }


        public RelayCommand<object> Submit
        {
            get
            {
                return _submitCommand ?? (_submitCommand = new RelayCommand<object>(
                    SubmitImplementation, o => CanExecuteCommand()));
            }
        }

        public RelayCommand<object> Close
        {
            get
            {
                return _cancelCommand ?? (_cancelCommand = new RelayCommand<object>(
                           CancelImplementation, o => CanExecuteCommand()));
            }
        }

        private void SubmitImplementation(object obj)
        {
            try
            {
                if (_person != null)
                {

                    if (MessageBox.Show("Submit changes?", "Edit?",
                            MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                    {
                        StationManager.DataStorage.EditPerson(_person, new Person(FirstName, LastName, Email, Date));
                        CloseAction();
                    }
                }
                else
                {
                    StationManager.DataStorage.AddPerson(new Person(FirstName, LastName, Email, Date));
                    CloseAction();
                }

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }


        private void CancelImplementation(object obj)
        {
            try
            {
                if (MessageBox.Show("Are you sure?", "Cancel?",
                            MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                   CloseAction();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        private bool CanExecuteCommand()
        {
            return !string.IsNullOrWhiteSpace(_firstName) && !string.IsNullOrWhiteSpace(_lastName) && !string.IsNullOrWhiteSpace(_email);
        }
    }
}
