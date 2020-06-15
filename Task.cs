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
        public int Nr { get; set; }

        public string Tytul { get; set; }

        public string Rodzaj { get; set; }

        public string Data { get; set; }


        public Task()
        {
            Nr = 1;
        }

        public Task(int nr, string tytul, string rodzaj, string data)
        {
            this.Nr = nr;
            this.Tytul = tytul;
            this.Rodzaj = rodzaj;
            this.Data = data;
        }




        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Nr", Nr);
            info.AddValue("Tytul", Tytul);
            info.AddValue("Rodzaj", Rodzaj);
            info.AddValue("Data", Data);
        }



        public Task(SerializationInfo info, StreamingContext ctxt)
        {
            Nr = (int)info.GetValue("Nr", typeof(int));
            Tytul = (string)info.GetValue("Tytul", typeof(string));
            Rodzaj = (string)info.GetValue("Rodzaj", typeof(string));
            Data = (string)info.GetValue("Data", typeof(string));
        }
    }
}
