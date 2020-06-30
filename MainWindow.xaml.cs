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
    /// Interakcja logiczna dla MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        TasksCollection tasksCollection = new TasksCollection();

        public DateTime TodayDate;

        /// <summary>
        /// Ten konstruktor ładuje warstwę graficznę XAML, ustawia minimalną datę w kalendarzu na aktualną datę, ustawia kontext dla tabeli z zadaniami oraz ustawia ilość priorytetowych zdań
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            TodayDate = DateTime.Today;
            Calendarz.DisplayDateStart = TodayDate;
           
            GridPanel.DataContext = tasksCollection;
            ImportantCounter.Text = tasksCollection.getImportantTasksNumber().ToString();
        }



        /// <summary>
        /// Metoda ta wywoływana jest po naciśnięciu przycisku dodającego zadanie.
        /// </summary>
        /// <param name="sender"> Jest to przycisk dodający zadanie.</param>
        /// <param name="e"> J</param>
        public void AddBtn_Click(object sender, RoutedEventArgs e)
        {

            var TaskTitle = InputText.Text;

            var ComboBoxItem = ComboBoxDropdown.SelectionBoxItem.ToString();

            bool isImportant = Important.IsChecked==true? true : false;

            var selectedDate = Calendarz.SelectedDate.HasValue? Calendarz.SelectedDate.Value:TodayDate;

            if (TaskTitle == "")
            {
                MessageBox.Show("Dodaj tytuł zadania");
                return;
            }
            else if (string.IsNullOrWhiteSpace(TaskTitle))
            {
                MessageBox.Show("Podaj poprawną nazwę");
                return;
            }
            else if (!Calendarz.SelectedDate.HasValue)
            {
                MessageBox.Show("Dodaj datę");
                return;
            }
            else if (tasksCollection.checkIfTaskExists(TaskTitle, selectedDate))
            {
                MessageBox.Show("Zadanie o takim tytule już istnieje w wybranym dniu");
            }
            else
            {
                tasksCollection.ListWithTasks.Add(new Task(
                        TaskTitle,
                        ComboBoxItem,
                        isImportant,
                        selectedDate
                    )
                 );

                ImportantCounter.Text = tasksCollection.getImportantTasksNumber().ToString();

                ClearAll();
            }
        }


        /// <summary>
        /// Metoda ta zostaje wywołana po naciśnięciu na przycisk usuwania zadania. Usuwa ona zaznaczone zadanie.
        /// </summary>
        /// <param name="sender">Przycisk usuwający zadanie</param>
        /// <param name="e"> event</param>
        public void RemoveBtn_Click(object sender, RoutedEventArgs e)
        {
            if(listView.Items.Count < 1)
            {
                MessageBox.Show("Nie masz obecnie żadnych zadań do usunięcia");
                return;
            }
            if(tasksCollection.ChosenTask == null)
            {
                MessageBox.Show("Zaznacz zadanie do usunięcia");
            }
            else
            {
                tasksCollection.ListWithTasks.Remove(tasksCollection.ChosenTask);

                ImportantCounter.Text = tasksCollection.getImportantTasksNumber().ToString();
            }
        }




        /// <summary>
        /// Metoda ta ma za zadanie posortować zadania. Naciśnięcie kolejny raz tego samego naglówka odwraca jego kolejność sortowania.
        /// </summary>
        /// <param name="sender"> Jest to dany nagłówek w liście z zadaniami.</param>
        /// <param name="e"> Event</param>
        public void GridViewColumnHeader_Click(object sender, RoutedEventArgs e)
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



        /// <summary>
        /// Metoda ta czyści wszystkie wprowadzone przez użytkownika dane potrzebne do utworzenia nowego zadania
        /// </summary>
        public void ClearAll()
        {
            InputText.Text = "";
            Calendarz.SelectedDate = null;
            Important.IsChecked = false;
            ComboBoxDropdown.SelectedIndex = 0;
        }

        /// <summary>
        /// Metoda wywołuje się po naciśnięciu przycisku RESET i ma za zadanie wywołać inną metodę: ClearAll()  
        /// </summary>
        /// <param name="sender">Przycisk RESET</param>
        /// <param name="e"> Event</param>
        public void ClearBtn_Click(object sender, RoutedEventArgs e)
        {
            ClearAll();
        }


        /// <summary>
        /// Metoda ta wywołuje się po naciśnięciu przycisku zapisującego zadania i ma za zadanie zapisać zadania do pliu
        /// </summary>
        /// <param name="sender"> Przycisk zapisujacy zadania</param>
        /// <param name="e"> Event</param>
        public void saveButton_Click(object sender, RoutedEventArgs e)
        {
            Serializer.serializeCollection(tasksCollection.ListWithTasks);

            MessageBox.Show("Zapisano listę zadań!");
        }



        /// <summary>
        /// Metoda ta wywołuje się po naciśnięciu przycisku wczytującego zapisane zadania i ma za zadanie wyświetlić zapisane zadania po uprzednim skasowaniu widniejących i niezapisanych zadań.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void loadButton_Click(object sender, RoutedEventArgs e)
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
