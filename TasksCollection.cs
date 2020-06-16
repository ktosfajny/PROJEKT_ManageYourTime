using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;


namespace ManageYourTime
{
    public class TasksCollection
    {

        public TasksCollection()
        {
            InitListWithTasks();
        }


        public ObservableCollection<Task> ListWithTasks { get; set; }

        public Task ChosenTask { get; set; }

        public void InitListWithTasks()
        {
            ListWithTasks = new ObservableCollection<Task>();
        }

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
