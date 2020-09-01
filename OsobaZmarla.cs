using System;
using System.Collections.Generic;
using System.Text;

namespace Person
{
    class OsobaZmarla : Person
    {
        DateTime _dataSmierci;
        public OsobaZmarla(string name, string surname, char gen, string birthdate) : base(name, surname, gen, birthdate)
        {
        }
        public void setDataSmierci(string data)
        {
            try
            {
                this._dataSmierci = DateTime.Parse(data);
            }
            catch (System.FormatException ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Podaj date w formacie <dd-mm-yyyy> - m<13");
            }
        }
        public string getDataSmierci()
        {
            return this._dataSmierci.ToString();
        }
    }
}
