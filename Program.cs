using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Person
{
    class Program
    {
        static void Main(string[] args)
        {
            Person Dawid = new Person("Dawid","Kowa#lski",'M',"10-10-2001");
            Person Ada = new Person("Ada", "Nowak-Tomczyk", 'K', "10-10-1996");
            Person Zbigniew = new Person("Zbigniew", "KoWal", 'M', "10-10-1976");
            Person Grundwalda = new Person("Grundwalda", "Tomasz", 'K', "10-10-1976");
            Person Gerald = new Person("Gerald", "Tomasz", 'M', "10-10-1976");
            Person Zbigniewa = new Person("Zbigniewa", "Ktostam", 'K', "10-10-1976");
            //dzialania fullName
            Console.WriteLine(Dawid.fullName("Imie: <name> Nazwisko: <surname>"));
            Console.WriteLine(Ada.fullName("Nazwisko: <surname>Imie: <name> "));
            Console.WriteLine(Zbigniew.fullName("Im<name>ie:  Nazw<surname>isko: "));
            //dzialanie isValid
            Console.WriteLine(Dawid.isValid());
            Console.WriteLine(Ada.isValid());
            Console.WriteLine(Zbigniew.isValid());
            //dzialanie age
            Console.WriteLine(Dawid.age('d'));
            Console.WriteLine(Ada.age('m'));
            Console.WriteLine(Zbigniew.age('y'));
            //dzialanie addChild
            Zbigniew.AddChild(Dawid);
            Zbigniew.AddChild(Ada);
            //dzialanie PrintChildren
            Zbigniew.PrintChildren();
            //dzialanie AssignSpouse
            Zbigniew.AssignSpouse(Gerald);
            Zbigniew.AssignSpouse(Zbigniewa);
            Zbigniew.AssignSpouse(Grundwalda);
            Console.WriteLine(Zbigniew.getHasSpouse());
            //dzialanie PrintGenealogicalTree
            Zbigniew.PrintGenealogicalTree();
            //dzialanie zapisu
            string path="test.txt";
            Save_Read.serializableTree(Zbigniew, path);
            Zbigniew = null;
            //dzialanie wczytania i metod typu get 
            Zbigniew = Save_Read.DeserializableTree(path);
            Console.WriteLine(Zbigniew.getName());
            Console.WriteLine(Zbigniew.getSurname());
            Console.WriteLine(Zbigniew.getHasChildren());
            Console.WriteLine(Zbigniew.getHasSpouse());
            Zbigniew.PrintChildren();
            Zbigniew.PrintGenealogicalTree();
            //Lapanie wyjatkow
            Zbigniew.setBirthdate("dasd-23123-dasds");
            Zbigniew.setName("Coostam123@#!");
            Zbigniew.setSurname("Coostam123@#!");
            //blednie wprowadzone dane nie podmieniaja poprawnych danych!
            Console.WriteLine(Zbigniew.getName());
            Console.WriteLine(Zbigniew.getSurname());
            Console.WriteLine(Zbigniew.getBirthdate());
            //dziedziczenie...
            OsobaZyjaca test1 = new OsobaZyjaca("test1","test2",'K',"23-12-1985");
            Console.WriteLine(test1.getName());
            Console.WriteLine(test1.getSurname());
            Console.WriteLine(test1.age('d'));
            Console.WriteLine(test1.age('m'));
            Console.WriteLine(test1.age('y'));
            test1.AddChild(Ada);
            test1.AddChild(Dawid);
            test1.PrintChildren();
            test1.AssignSpouse(Gerald);
            test1.PrintGenealogicalTree();
            test1.setTelefon("+48 123 456 789");
            Console.WriteLine(test1.getTelefon());
            OsobaZmarla test2 = new OsobaZmarla("test3", "test4", 'M', "23-12-1985");
            test2.setDataSmierci("10-10-2010");
            Console.WriteLine(test2.getName());
            Console.WriteLine(test2.getSurname());
            Console.WriteLine(test2.getBirthdate());
            Console.WriteLine(test2.getDataSmierci());



        }
    }
}
