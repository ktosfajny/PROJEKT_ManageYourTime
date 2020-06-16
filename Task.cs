using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;


namespace ManageYourTime
{
    [Serializable]
    public class Task : ISerializable
    {

        public string Tytul { get; set; }

        public string Rodzaj { get; set; }

        public bool Priorytet { get; set; }

        public string Data { get; set; }



        public Task(string tytul, string rodzaj, bool priorytet, DateTime data)
        {
            
            this.Tytul = tytul;
            this.Rodzaj = rodzaj;
            this.Priorytet = priorytet;
            this.Data = data.ToShortDateString();
        }




        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Tytul", Tytul);
            info.AddValue("Rodzaj", Rodzaj);
            info.AddValue("Priorytet", Priorytet);
            info.AddValue("Data", Data);
        }



        public Task(SerializationInfo info, StreamingContext ctxt)
        {
            Tytul = (string)info.GetValue("Tytul", typeof(string));
            Rodzaj = (string)info.GetValue("Rodzaj", typeof(string));
            Priorytet = (bool)info.GetValue("Priorytet", typeof(bool));
            Data = (string)info.GetValue("Data", typeof(string));
        }
    }
}
