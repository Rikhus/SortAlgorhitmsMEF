using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;

namespace TryMEFApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const int ELEMENTS_COUNT = 50;
        const int MAX_VALUE = 100;
        const int DELAY_MS = 20;

        List<int> list = new List<int>();

        MainViewModel viewModel;

        public MainWindow()
        {
            InitializeComponent();

            viewModel = DataContext as MainViewModel;
            foreach(var sort in Import.LoadAndGetSortAlgorhytms())
            {
                viewModel.Sorts.Add(sort);
            }
            
            CreateRandomList();
            viewModel.SortedList = new ObservableCollection<int>(list);
        }

        void CreateRandomList()
        {
            var random = new Random();
            for(int i = 0; i < ELEMENTS_COUNT; i++)
            {
                list.Add(random.Next(MAX_VALUE));
            }
        }

        bool requireStop = false;

        private void SortButton_Click(object sender, RoutedEventArgs e)
        {
            var sort = viewModel.SelectedSort.Sort(list.ToList());
            viewModel.IsComplete = false;

            var dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler((object sender, EventArgs e)=>
            {
                if (sort.MoveNext() && !requireStop)
                {
                    viewModel.SortedList = new ObservableCollection<int>(sort.Current);
                }
                else
                {
                    viewModel.SortedList = new ObservableCollection<int>(sort.Current);
                    viewModel.IsComplete = true;
                    (sender as DispatcherTimer).Stop();
                    requireStop = false;
                    viewModel.IsComplete = true;
                }
            });
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, DELAY_MS);
            dispatcherTimer.Start();
           
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            requireStop= true;
        }
    }
}
