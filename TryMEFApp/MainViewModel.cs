using Protocol;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TryMEFApp
{
    internal class MainViewModel: INotifyPropertyChanged
    {
        private bool isComplete = true;
        public bool IsComplete
        {
            get => isComplete;
            set
            {
                isComplete = value;
                NotifyPropertyChanged();
            }
        }

        private ObservableCollection<int> sortedList = new ObservableCollection<int>();
        public ObservableCollection<int> SortedList
        {
            get => sortedList;
            set
            {
                sortedList = value;
                NotifyPropertyChanged();
            }
        }

        private ISort selectedSort;
        public ISort SelectedSort
        {
            get { return selectedSort; }
            set
            {
                selectedSort = value;
                NotifyPropertyChanged();
            }
        }

        public ObservableCollection<ISort> Sorts { get; set; } = new ObservableCollection<ISort>();

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
