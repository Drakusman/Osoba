using System;
using System.Collections.Generic;
using System.Text;

namespace Person
{
    class OsobaZyjaca : Person
    {
        string _telefon;
        public OsobaZyjaca(string name, string surname, char gen, string birthdate) : base(name, surname, gen, birthdate)
        {
        }
        public void setTelefon(string tel)
        {
            this._telefon = tel;
        }
        public string getTelefon()
        {
            return this._telefon;
        }
    }
}
