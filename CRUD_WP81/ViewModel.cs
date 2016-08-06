using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using CRUD_WP81.Annotations;

namespace CRUD_WP81
{
    public class ViewModel : INotifyPropertyChanged
    {
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
                new DataStorageModel {Name = "Honda", Surname="Civic" , Age = 30000},
                new DataStorageModel {Name = "Ford", Surname="Mustang", Age= 15000},
                new DataStorageModel {Name = "Lada", Surname="Kalina", Age= 5000}
             };
        }


        private void CreateClickMethod()
        {
            Models.Add(new DataStorageModel() { Name = NewName, Surname = NewSurname, Age = NewAge });

        }

        private void UpdateClickMethod()
        {

        }

        private void DeleteClickMethod()
        {
            Models.RemoveAll(i => i.IsSelected);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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
}
