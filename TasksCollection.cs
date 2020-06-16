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

        public Task ChoosenTask { get; set; }

        public void InitListWithTasks()
        {
            ListWithTasks = new ObservableCollection<Task>();
            ListWithTasks.Add(new Task("zrobić zakupy", "Spotkanie", false, "28-07-2020"));
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


    }
}
