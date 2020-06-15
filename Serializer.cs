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
        public static void serializeCollection(ObservableCollection<Task> taskList)
        {
            Stream stream = File.Open("TaskListData.dat", FileMode.Create);

            BinaryFormatter binaryFile = new BinaryFormatter();

            binaryFile.Serialize(stream, taskList);

            stream.Close();
        }


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
