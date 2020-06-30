using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;


namespace ManageYourTime
{
    /// <summary>
    /// Instancją tej klasy jest lista z zadaniami.
    /// </summary>
    public class TasksCollection
    {

        /// <summary>
        /// Ten konsturktor wywołuje metodę, która inicjalizuje nową listę zadań będącą ObservableCollection
        /// </summary>
        public TasksCollection()
        {
            InitListWithTasks();
        }

        /// <summary>
        /// tworzona jest tutaj lista ObservableCollection z zadaniami. Składa się ona z obiektów tworzonych w oparciu o klasę Task
        /// </summary>
        public ObservableCollection<Task> ListWithTasks { get; set; }

        /// <summary>
        /// Tworozna jest  tutaj instancja wybranego przez użytkownika zadania
        /// </summary>
        public Task ChosenTask { get; set; }

        /// <summary>
        /// Metoda ta inicjalizuje nową listę ObservableCollection opartą na obiektach z klasy Task. Metoda ta jest wywoływana w konstruktorze.
        /// </summary>
        public void InitListWithTasks()
        {
            ListWithTasks = new ObservableCollection<Task>();
        }

        /// <summary>
        /// Metoda ta sprawdza ilość zadań oznaczonych jako zadania z priorytetem wykonania.
        /// </summary>
        /// <returns>Metoda zwraca liczbę (int) oznaczającą ilość prorytetowych zadań.</returns>
        public int getImportantTasksNumber()
        {
            int ImportantTasksNumber = 0;

            foreach (Task task in ListWithTasks)
            {
                if (task.Priorytet)
                {
                    ImportantTasksNumber++;
                }
            }

            return ImportantTasksNumber;

        }

        /// <summary>
        /// Metoda ta sprawdza w liście z zadaniami czy nowo wprowadzane zadanie nie istnieje już w bazie danych
        /// </summary>
        /// <param name="TaskTitle">Przyjmuje wartość tekstową wprowadzoną przez użytkownika jako nazwa zadania</param>
        /// <param name="SelectedDate">Przyjmuje datę wprowadzaoną przez użytkownika jako data do końca której należy wykonać zadanie</param>
        /// <returns>Zwraca true/false w zależności od tego czy wprowadane przez użytkownika zadanie istnieje już w liscie czy nie.</returns>
        public bool checkIfTaskExists(string TaskTitle, DateTime SelectedDate)
        {
            bool verdict = false;

            foreach (Task task in ListWithTasks)
            {
                if (task.Tytul == TaskTitle && task.Data==SelectedDate.ToShortDateString())
                {
                    verdict = true;
                }
            }

            return verdict;
        }
    }
}
