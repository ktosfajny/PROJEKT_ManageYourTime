using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using System.Collections.ObjectModel;
using System.Windows;




namespace ManageYourTime
{
    static class Serializer
    {
        /// <summary>
        /// Ta metoda ma za zadanie zapisać zadania do pliku aby można było je potem wczytać.
        /// </summary>
        /// <param name="taskList"> Ten argument przyjmuje listę z zadaniami będącą ObservableCollection składającą się z obiektów klasy Task </param>
        public static void serializeCollection(ObservableCollection<Task> taskList)
        {
            Stream stream = File.Open("TaskListData.dat", FileMode.Create);

            BinaryFormatter binaryFile = new BinaryFormatter();

            binaryFile.Serialize(stream, taskList);

            stream.Close();
        }

        /// <summary>
        /// Ta metoda ma za zadanie odczytać zapisane zadania.
        /// </summary>
        /// <returns>Zwraca zapisane zadania jako ObservableCollection obiektów z klasy Task</returns>
        public static ObservableCollection<Task> deserializeCollection()
        {
            Stream stream = File.Open("TaskListData.dat", FileMode.Open);

            BinaryFormatter binaryFile = new BinaryFormatter();

            ObservableCollection<Task> taskList = (ObservableCollection<Task>)binaryFile.Deserialize(stream);

            stream.Close();

            return taskList;
        }
    }
}
