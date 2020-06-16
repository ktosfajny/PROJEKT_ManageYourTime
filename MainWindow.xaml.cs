using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using System.Runtime.CompilerServices;
namespace ManageYourTime
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        TasksCollection tasksCollection = new TasksCollection();

        public DateTime TodayDate;

        public MainWindow()
        {
            InitializeComponent();

            TodayDate = DateTime.Today;
            Calendarz.DisplayDateStart = TodayDate;

            GridPanel.DataContext = tasksCollection;
            ImportantCounter.Text = tasksCollection.getImportantTasksNumber().ToString();
        }

        
        
        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {

            var TaskTitle = InputText;

            var ComboBoxItem = ComboBoxDropdown.SelectionBoxItem;

            bool isImportant = Important.IsChecked==true? true : false;

            var selectedDate = Calendarz.SelectedDate.HasValue? Calendarz.SelectedDate.Value:TodayDate;

            if (TaskTitle.Text == "")
            {
                MessageBox.Show("Dodaj tytuł zadania");
                return;
            }
            else if (string.IsNullOrWhiteSpace(TaskTitle.Text))
            {
                MessageBox.Show("Podaj poprawną nazwę");
                return;
            }
            else if (!Calendarz.SelectedDate.HasValue)
            {
                MessageBox.Show("dodaj datę");
                return;
            }
            else if (tasksCollection.checkIfTaskExists(TaskTitle.Text, selectedDate, isImportant))
            {
                MessageBox.Show("Takie zadanie już istnieje");
            }
            else
            {
                tasksCollection.ListWithTasks.Add(new Task(
                        TaskTitle.Text,
                        ComboBoxItem.ToString(),
                        isImportant,
                        selectedDate.ToShortDateString()
                    )
                 );

                ImportantCounter.Text = tasksCollection.getImportantTasksNumber().ToString();

                ClearAll();
            }
        }



        private void RemoveBtn_Click(object sender, RoutedEventArgs e)
        {
            if(listView.Items.Count < 1)
            {
                MessageBox.Show("Nie masz obecnie żadnych zadań do usunięcia");
                return;
            }
            if(tasksCollection.ChoosenTask == null)
            {
                MessageBox.Show("Zaznacz zadanie do usunięcia");
            }
            else
            {
                tasksCollection.ListWithTasks.Remove(tasksCollection.ChoosenTask);

                ImportantCounter.Text = tasksCollection.getImportantTasksNumber().ToString();
            }
        }





        private void GridViewColumnHeader_Click(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader headerType = sender as GridViewColumnHeader;

            string headerName = headerType.Content.ToString();

            CollectionView columnView = CollectionViewSource.GetDefaultView(listView.ItemsSource) as CollectionView;

            ListSortDirection sortDirection = ListSortDirection.Ascending;

            if (columnView.SortDescriptions.Count > 0)
            {
                SortDescription sorting = columnView.SortDescriptions.FirstOrDefault();

                if (sorting.Direction == ListSortDirection.Descending)
                {
                    sortDirection = ListSortDirection.Ascending;
                }
                else
                {
                    sortDirection = ListSortDirection.Descending;
                }


            }

            columnView.SortDescriptions.Clear();

            columnView.SortDescriptions.Add(new SortDescription(headerName, sortDirection));
        }




        private void ClearAll()
        {
            InputText.Text = "";
            Calendarz.SelectedDate = null;
            Important.IsChecked = false;
            ComboBoxDropdown.SelectedIndex = 0;
        }

        private void ClearBtn_Click(object sender, RoutedEventArgs e)
        {
            ClearAll();
        }


        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            Serializer.serializeCollection(tasksCollection.ListWithTasks);

            MessageBox.Show("Zapisano listę zadań!");
        }



        private void loadButton_Click(object sender, RoutedEventArgs e)
        {
            tasksCollection.ListWithTasks.Clear();

            ObservableCollection<Task> savedList = Serializer.deserializeCollection();

            foreach (Task oldTask in savedList)
            {
                tasksCollection.ListWithTasks.Add(oldTask);
            }

            ImportantCounter.Text = tasksCollection.getImportantTasksNumber().ToString();

            MessageBox.Show("wczytano listę zadań!");
        }

 
    }
}
