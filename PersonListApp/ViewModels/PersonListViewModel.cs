using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using PersonListApp.Models;
using PersonListApp.Tools;
using System.Linq;
using PersonListApp.Tools.Managers;

namespace PersonListApp.ViewModels
{
    internal class PersonListViewModel : BaseViewModel, ILoaderOwner
    {
        #region Fields

        private ObservableCollection<Person> _persons;
        private Person _selectedPerson;

        #region Commands
        private RelayCommand<object> _editPerson;
        private RelayCommand<object> _addPerson;
        private RelayCommand<object> _deletePerson;
        private RelayCommand<object> _saveAll;
        private RelayCommand<object> _sortFirstName;
        private RelayCommand<object> _sortLastName;
        private RelayCommand<object> _sortEmail;
        private RelayCommand<object> _sortBirthday;
        private RelayCommand<object> _sortWestern;
        private RelayCommand<object> _sortChinese;
        private RelayCommand<object> _sortIsAdult;
        private RelayCommand<object> _sortIsBirthday;
        #endregion

        private Visibility _loaderVisibility = Visibility.Hidden;
        private bool _isControlEnabled = true;


        #endregion

        #region Properties

        public Visibility LoaderVisibility
        {
            get { return _loaderVisibility; }
            set
            {
                _loaderVisibility = value;
                OnPropertyChanged();
            }
        }
        public bool IsControlEnabled
        {
            get { return _isControlEnabled; }
            set
            {
                _isControlEnabled = value;
                OnPropertyChanged();
            }
        }

        public Person SelectedPerson
        {
            get { return _selectedPerson; }
            set
            {
                _selectedPerson = value;
            }
        }

        public ObservableCollection<Person> Persons
        {
            get { return _persons;}
            private set
            {
                _persons = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand<object> Add
        {
            get
            {
                return _addPerson ?? (_addPerson = new RelayCommand<object>(
                           AddPersonImplementation));
            }
        }

        public RelayCommand<object> Edit
        {
            get
            {
                return _editPerson ?? (_editPerson = new RelayCommand<object>(
                           EditImplementation,o=> CanExecuteCommand()));
            }
        }

        


        public RelayCommand<object> Delete
        {
            get
            {
                return _deletePerson ?? (_deletePerson = new RelayCommand<object>(
                           DeleteImplementation,o=>CanExecuteCommand()));
            }
        }

        private async void DeleteImplementation(object obj)
        {
            LoaderManager.Instance.ShowLoader();
            await Task.Run(() =>
            {
                if (MessageBox.Show($"Delete {_selectedPerson}?",
                "Delete?",
                    MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                StationManager.DataStorage.DeletePerson(_selectedPerson);
                _selectedPerson = null;
                Persons = new ObservableCollection<Person>(StationManager.DataStorage.PersonsList);
                }
            });
            Thread.Sleep(400);
            LoaderManager.Instance.HideLoader();
            
        }

        public RelayCommand<object> Save
        {
            get
            {
                return _saveAll ?? (_saveAll = new RelayCommand<object>(
                           SaveImplementation));
            }
        }

        public RelayCommand<object> SortFirstName
        {
            get
            {
                return _sortFirstName ?? (_sortFirstName = new RelayCommand<object>(o =>
                    SortImplementation(o,1)));
            }
        }
        public RelayCommand<object> SortLastName
        {
            get
            {
                return _sortLastName ?? (_sortLastName = new RelayCommand<object>(o =>
                           SortImplementation(o, 2)));
            }
        }
        public RelayCommand<object> SortEmail
        {
            get
            {
                return _sortEmail ?? (_sortEmail = new RelayCommand<object>(o =>
                           SortImplementation(o, 3)));
            }
        }
        public RelayCommand<object> SortBirthday
        {
            get
            {
                return _sortBirthday ?? (_sortBirthday = new RelayCommand<object>(o =>
                           SortImplementation(o, 4)));
            }
        }
        public RelayCommand<object> SortWestern
        {
            get
            {
                return _sortWestern ?? (_sortWestern = new RelayCommand<object>(o =>
                           SortImplementation(o, 5)));
            }
        }
        public RelayCommand<object> SortChinese
        {
            get
            {
                return _sortChinese ?? (_sortChinese = new RelayCommand<object>(o =>
                           SortImplementation(o, 6)));
            }
        }
        public RelayCommand<object> SortIsAdult
        {
            get
            {
                return _sortIsAdult ?? (_sortIsAdult = new RelayCommand<object>(o =>
                           SortImplementation(o, 7)));
            }
        }
        public RelayCommand<object> SortIsBirthday
        {
            get
            {
                return _sortIsBirthday ?? (_sortIsBirthday = new RelayCommand<object>(o =>
                           SortImplementation(o, 8)));
            }
        }
        #endregion


        private async void SaveImplementation(object obj)
        {
            LoaderManager.Instance.ShowLoader();
            await Task.Run(() =>
            {
                StationManager.DataStorage.SaveChanges();
                Thread.Sleep(150);
            });
            LoaderManager.Instance.HideLoader();
        }


        private async void SortImplementation(object obj,int i)
        {
            LoaderManager.Instance.ShowLoader();
            await Task.Run(() =>
            {
                IOrderedEnumerable<Person> sortedPersons;
                switch (i)
                {
                    case 1:
                        sortedPersons = from u in _persons
                            orderby u.FirstName
                            select u;
                        break;
                    case 2:
                        sortedPersons = from u in _persons
                            orderby u.LastName
                            select u;
                        break;
                    case 3:
                        sortedPersons = from u in _persons
                            orderby u.Email
                            select u;
                        break;
                    case 4:
                        sortedPersons = from u in _persons
                            orderby u.Date
                            select u;
                        break;
                    case 5:
                        sortedPersons = from u in _persons
                            orderby u.WesternZodiac
                            select u;
                        break;
                    case 6:
                        sortedPersons = from u in _persons
                            orderby u.ChineseZodiac
                            select u;
                        break;
                    case 7:
                        sortedPersons = from u in _persons
                            orderby u.IsAdult
                            select u;
                        break;
                    default:
                        sortedPersons = from u in _persons
                            orderby u.IsBirthday
                            select u;
                        break;
                }
                Persons = new ObservableCollection<Person>(sortedPersons);
                StationManager.DataStorage.PersonsList = Persons.ToList();
                Thread.Sleep(300);
            });
            LoaderManager.Instance.HideLoader();
        }

        private void AddPersonImplementation(object obj)
        {
            IsControlEnabled = false;
            
            AddEditWindow window = new AddEditWindow();
            window.ShowDialog();
            
            IsControlEnabled = true;
            Persons = new ObservableCollection<Person>(StationManager.DataStorage.PersonsList);
        }

        private void EditImplementation(object obj)
        {
            IsControlEnabled = false;
            
            AddEditWindow window = new AddEditWindow(_selectedPerson);
            window.ShowDialog();
            
            IsControlEnabled = true;
            Persons = new ObservableCollection<Person>(StationManager.DataStorage.PersonsList);
        }



        internal PersonListViewModel()
        {
            LoaderManager.Instance.Initialize(this);
            LoaderManager.Instance.ShowLoader();
            _persons = new ObservableCollection<Person>(StationManager.DataStorage.PersonsList);
            LoaderManager.Instance.HideLoader();
        }

        private bool CanExecuteCommand()
        {
            return _selectedPerson != null;
        }
    }
}
