﻿using System;
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
            this.Calendarz.DisplayDateStart = this.TodayDate;

            GridPanel.DataContext = tasksCollection;
        }

        
        
        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {

            var TaskTitle = this.InputText;

            var ComboBoxItem = this.ComboBoxDropdown.SelectionBoxItem;


            if (TaskTitle.Text == "")
            {
                MessageBox.Show("dodaj tytuł zadania");
                return;
            }
            else if (string.IsNullOrWhiteSpace(TaskTitle.Text))
            {
                MessageBox.Show("podaj poprawną nazwę");
                return;
            }
            else if (!this.Calendarz.SelectedDate.HasValue)
            {
                MessageBox.Show("dodaj datę");
                return;
            }
            else
            {
                tasksCollection.ListwithTasks.Add(new Task(
                        tasksCollection.ListwithTasks.Count + 1,
                        TaskTitle.Text,
                        ComboBoxItem.ToString(),
                        this.Calendarz.SelectedDate.Value.ToString("yyyy/MM/dd")
                    )
                 );

                this.Calendarz.SelectedDate = null;

                TaskTitle.Text = "";
            }
        }



        private void RemoveBtn_Click(object sender, RoutedEventArgs e)
        {
            if(this.listView.Items.Count < 1)
            {
                MessageBox.Show("Nie masz obecnie żadnych zadań do usunięcia");
                return;
            }
            if(tasksCollection.WybranyTask == null)
            {
                MessageBox.Show("Zaznacz zadanie do usunięcia");
            }
            else
            {
                tasksCollection.ListwithTasks.Remove(tasksCollection.WybranyTask);
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






        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            Serializer.serializeCollection(tasksCollection.ListwithTasks);

            MessageBox.Show("Zapisano listę zadań!");
        }



        private void loadButton_Click(object sender, RoutedEventArgs e)
        {
            tasksCollection.ListwithTasks.Clear();

            ObservableCollection<Task> savedList = Serializer.deserializeCollection();

            foreach (Task oldTask in savedList)
            {
                tasksCollection.ListwithTasks.Add(oldTask);
            }

            MessageBox.Show("wczytano listę zadań!");
        }
    }
}
