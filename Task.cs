using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;


namespace ManageYourTime
{
    [Serializable]
    /// <summary>
    /// Instancją tej klasy jest obiekt, który jest pojedynczym zadaniem w liście zadań.
    /// </summary>
    public class Task : ISerializable
    {

        public string Tytul { get; set; }

        public string Rodzaj { get; set; }

        public bool Priorytet { get; set; }

        public string Data { get; set; }


        /// <summary>
        /// Opis konstruktora przypisujące podane jako argument własności do właściwości obiektu
        /// </summary>
        /// <param name="tytul">To pole przyjmuje string jako tytuł zadania</param>
        /// <param name="rodzaj">To pole przyjmuje string jako rodzaj zadania</param>
        /// <param name="priorytet">to pole przyjmuje bool aby oznaczyć (lub nie) zadanie jako piorytetowe</param>
        /// <param name="data">To pole przyjmuje obiekt DateTime lecz zamienia go na string i obcina do samej daty</param>
        public Task(string tytul, string rodzaj, bool priorytet, DateTime data)
        {
            
            this.Tytul = tytul;
            this.Rodzaj = rodzaj;
            this.Priorytet = priorytet;
            this.Data = data.ToShortDateString();
        }



        /// <summary>
        /// To jest metoda wykorzystująca serializer.
        /// </summary>
        /// <param name="info">Do info dodawane są wartości pobrane z właściwości obiektu utworoznego na bazie tej klasy Task.</param>
        /// <param name="context">Ustawia context danych na odpowiadającą instancję klasy Task</param>
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Tytul", Tytul);
            info.AddValue("Rodzaj", Rodzaj);
            info.AddValue("Priorytet", Priorytet);
            info.AddValue("Data", Data);
        }


        /// <summary>
        /// Ten konstruktor wykorzystuje Serializer z jego pomocą przypisuje właściwościom odpowiadającego obiektu wartości przechowane w info
        /// </summary>
        /// <param name="info">Info przehowuje wartości danych właściwości obiektów zapisanych za pomocą serializera</param>
        /// <param name="ctxt">Ustawia context na odpowiadający obiekt przechowywany w pliku</param>
        public Task(SerializationInfo info, StreamingContext ctxt)
        {
            Tytul = (string)info.GetValue("Tytul", typeof(string));
            Rodzaj = (string)info.GetValue("Rodzaj", typeof(string));
            Priorytet = (bool)info.GetValue("Priorytet", typeof(bool));
            Data = (string)info.GetValue("Data", typeof(string));
        }
    }
}
