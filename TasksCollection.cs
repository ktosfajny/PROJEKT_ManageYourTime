using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ManageYourTime
{
    public class TasksCollection
    {

        public TasksCollection()
        {
            InitListWithTasks();
        }


        public ObservableCollection<Task> ListwithTasks { get; set; }

        public Task WybranyTask { get; set; }

        public void InitListWithTasks()
        {
            ListwithTasks = new ObservableCollection<Task>();
            ListwithTasks.Add(new Task(1, "zrobić zakupy", "Spotkanie", "28-07-2020"));
        }


    }
}
