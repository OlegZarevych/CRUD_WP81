using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Windows.Input;
using System.ComponentModel;
using System.Runtime.CompilerServices;




namespace CRUD_WP81
{
    public class ViewModel : INotifyPropertyChanged
    {
        private DataStorageModel _selecteDataStorageModel;

        public DataStorageModel SelecteDataStorageModel
        {
            get { return _selecteDataStorageModel; }
            set
            {
                _selecteDataStorageModel = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<DataStorageModel> Models { get; set; }
        public string NewName { get; set; }
        public string NewSurname { get; set; }
        public int NewAge { get; set; }

        //Event Handlers
        public ICommand CreateClickCommand { get; set; }
        public ICommand UpdateClickCommand { get; set; }
        public ICommand DeleteClickCommand { get; set; }
        public ViewModel()
        {
            CreateClickCommand = new RelayCommand(arg => CreateClickMethod());
            UpdateClickCommand = new RelayCommand(arg => UpdateClickMethod());
            DeleteClickCommand = new RelayCommand(arg => DeleteClickMethod());
            //Some mock data
            Models = new ObservableCollection<DataStorageModel>()
             {
                   new DataStorageModel {Name = "Honda", Surname="Civic" , Age = 30000}
                 //   new DataStorageModel {Name = "Ford", Surname="Mustang", Age= 15000},
                 //   new DataStorageModel {Name = "Lada", Surname="Kalina", Age= 5000}
             };
        }


        private void CreateClickMethod()
        {
            Models.Add(new DataStorageModel() { Name = NewName, Surname = NewSurname, Age = NewAge });

        }

        private void UpdateClickMethod()

        {
           SelecteDataStorageModel.Name = NewName;
           SelecteDataStorageModel.Surname = NewSurname;
           SelecteDataStorageModel.Age = NewAge;
         
        }

        private void DeleteClickMethod()
        {
        Models.RemoveAll(i => i.IsSelected);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    }

        public static class ExtensionMethods
    {
        public static int RemoveAll<T>(
            this ObservableCollection<T> coll, Func<T, bool> condition)
        {
            var itemsToRemove = coll.Where(condition).ToList();

            foreach (var itemToRemove in itemsToRemove)
            {
                coll.Remove(itemToRemove);
            }

            return itemsToRemove.Count;
         }
     }
