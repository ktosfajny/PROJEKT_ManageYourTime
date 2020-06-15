using System;
using System.Collections.Generic;
using System.Text;

namespace ManageYourTime
{
    public class Task
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
    }
}
