using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;

namespace Person
{
	[Serializable]
	public class Person
	{
		//czy ma dziecki
		private bool hasChild;
		//czy ma malzonka
		private bool hasSpouse;
		//lista dzieci
		private List<Person> children = new List<Person>();
		//maloznek
		private Person spouse;

		private string name;
		private string surname;
		public enum Gender
		{
			Male,
			Female
		}
		private Gender gen;
		private DateTime birthdate;

		//konstruktor podstawowy
		public Person()
		{
			this.name = "Jan";
			this.surname = "Kowalski";
			this.birthdate = new DateTime(2000, 1, 1);
		}
		public Person(string name, string surname, char gen, string birthdate)
		{
			this.name = name;
			this.surname = surname;
			if (gen.Equals('M'))
			{
				this.gen = Gender.Male;
			}
			else if (gen.Equals('K'))
			{
				this.gen = Gender.Female;
			}
			this.birthdate = DateTime.Parse(birthdate);
		}
		public string getName()
		{
			return this.name;
		}
		public string getSurname()
		{
			return this.surname;
		}
		public Gender getGender()
		{
			return this.gen;
		}
		public DateTime getBirthdate()
		{
			return this.birthdate;
		}
		public bool getHasChildren()
		{
			if (this.hasChild)
			{
				Console.WriteLine("Tak");
				return true;
			}
			else
			{
				Console.WriteLine("Nie");
				return false;
			}
		}
		
		public bool getHasSpouse()
		{
			if (this.hasSpouse)
			{
				Console.WriteLine("Tak");
				return true;
			}
			else
			{
				Console.WriteLine("Nie");
				return false;
			}
		}
		public void setName(string name)
		{
			try
			{
				if(!exeptionNameValid(name))
				{
					throw new InvalidFormatData("Podane imie ma niepoprawny format!");
				}
				if(exeptionNameValid(name))
				{
					this.name = name;
				}
				
				
			}
			catch(InvalidFormatData ex)
			{
				Console.WriteLine(ex.Message);
			}
		}
		public void setSurname(string surname)
		{
			try
			{
				if (!exeptionSurnameValid(surname))
				{
					throw new InvalidFormatData("Podane nazwisko ma niepoprawny format!");
				}
				if (exeptionSurnameValid(surname))
				{
					this.surname = surname;
				}


			}
			catch (InvalidFormatData ex)
			{
				Console.WriteLine(ex.Message);
			}
		}
		public void setBirthdate(string birthdate)
		{
			try
			{
				
				this.birthdate = DateTime.Parse(birthdate);

			}
			catch (System.FormatException ex)
			{
				Console.WriteLine(ex.Message);
				Console.WriteLine("Podaj date w formacie <dd-mm-yyyy> - d<32 m<13");
			}
		}
		public void PrintSpouse()
		{
			if (this.hasSpouse)
			{
				Console.WriteLine(spouse.name + "   " + spouse.surname);
			}
			else
			{
				Console.WriteLine("Przepraszam nie masz jeszcze malzonka!");
			}
		}
		public string age(char c)
		{
			TimeSpan wiek = DateTime.Now - birthdate;
			string wynik = "Niepoprawny parametr";
			if (c.Equals('d'))
			{
				int days = (int)wiek.TotalDays;
				wynik = days.ToString();
				return wynik;
			}
			if (c.Equals('m'))
			{
				int days;
				days = (int)wiek.TotalDays;

				float a = days / 30;

				int months = (int)Math.Round(a);
				wynik = months.ToString();
				return wynik;
			}
			if (c.Equals('y'))
			{
				int days;
				days = (int)wiek.TotalDays;

				float a = days / 365;

				int years = (int)Math.Round(a);
				wynik = years.ToString();
				return wynik;
			}
			return wynik;
		}
		//metoda zamieniajaca <name> <surname> na rzeczywiste imie i nazwisko
		public string fullName(string format)
		{
			//przykald fullName("Imie: <name>, Nazwisko: <surname>") -> output Imie: imie, Nazwisko: nazwisko
			string name = "<name>";
			string surname = "<surname>";
			if (format.Contains(name) && format.Contains(surname))
			{
				int indeksN;
				int indeksS;
				indeksN = format.IndexOf(name, 0);
				indeksS = format.IndexOf(surname, 0);
				//uwzglednienie ze najpierw jest <name> pozniej <surname>
				if (indeksN < indeksS)
				{
					string part1 = "";
					part1 += format.Substring(0, indeksN);
					part1 += this.name;
					part1 += format.Substring(indeksN + 6, indeksS - indeksN - 6);
					part1 += this.surname;
					part1 += format.Substring(indeksS + 9);
					return part1;
				}
				//uwzglednienie ze najpierw jest <surname> pozniej <name>
				if (indeksN > indeksS)
				{
					string part1 = "";
					part1 += format.Substring(0, indeksS);
					part1 += this.surname;
					part1 += format.Substring(indeksS + 9, indeksN - indeksS - 9);
					part1 += this.name;
					part1 += format.Substring(indeksN + 6);
					return part1;
				}
			}
			if (format.Contains(name))
			{
				int indeks;
				string part1 = "";
				indeks = format.IndexOf(name, 0);
				part1 += format.Substring(0, indeks);
				part1 += this.name;
				part1 += format.Substring(format.IndexOf(">") + 1);
				return part1;
			}
			if (format.Contains(surname))
			{
				int indeks;
				string part1 = "";
				indeks = format.IndexOf(surname, 0);
				part1 += format.Substring(0, indeks);
				part1 += this.surname;
				part1 += format.Substring(format.IndexOf(">") + 1);
				return part1;
			}


			return format;


		}
		public bool exeptionNameValid(string name)
		{
			bool nameContainValidate = Regex.IsMatch(name, @"^[a-zA-Z]+$");
			char nameChar = name[0];
			bool firstCharNameCheck;
			firstCharNameCheck = Char.IsUpper(nameChar);

			bool checkIfNameContainsUpper = false;

			for (int i = 1; i < name.Length; i++)
			{
				if (Char.IsUpper(name[i]))
				{
					checkIfNameContainsUpper = false;
					break;
				}
				else
				{
					checkIfNameContainsUpper = true;
				}
			}
			if(nameContainValidate && firstCharNameCheck && checkIfNameContainsUpper)
			{
				return true;
			}
			return false;
		}
		public bool exeptionSurnameValid(string surname)
		{
			bool surnameContainValidate = Regex.IsMatch(surname, @"^[a-zA-Z-]+$");
			char surnameChar = surname[0];
			bool firstCharSurnameCheck=true;
			firstCharSurnameCheck = Char.IsUpper(surnameChar);
			bool checkIfSurameContainsUpper = true;
			bool checkIfSurnameContainsUpperPart1 = true;
			bool checkIfSurnameContainsUpperPart2 = true;
			bool secoundCharSurnameCheck = true;
			if (surname.Contains('-'))
			{
				int index = surname.IndexOf('-');
				for (int i = 1; i < index; i++)
				{
					if (Char.IsUpper(surname[i]))
					{
						checkIfSurnameContainsUpperPart1 = false;
						break;
					}
					else
					{
						checkIfSurnameContainsUpperPart1 = true;
					}
				}

				secoundCharSurnameCheck = Char.IsUpper(surname[index + 1]);
				for (int i = index + 2; i < surname.Length; i++)
				{
					if (Char.IsUpper(surname[i]))
					{
						checkIfSurnameContainsUpperPart2 = false;
						break;
					}
					else
					{
						checkIfSurnameContainsUpperPart2 = true;
					}
				}
			}
			else
			{
				for (int i = 1; i < surname.Length; i++)
				{
					if (Char.IsUpper(surname[i]))
					{
						checkIfSurameContainsUpper = false;
						break;
					}
					else
					{
						checkIfSurameContainsUpper = true;
					}
				}
			}
			if(surnameContainValidate && firstCharSurnameCheck && checkIfSurameContainsUpper && checkIfSurnameContainsUpperPart1 && checkIfSurnameContainsUpperPart2 && secoundCharSurnameCheck)
			{
				return true;
			}
			return false;
		}
		public bool isValid()
		{
			bool nameContainValidate = Regex.IsMatch(this.name, @"^[a-zA-Z]+$");
			char nameChar = this.name[0];
			bool firstCharNameCheck;
			firstCharNameCheck = Char.IsUpper(nameChar);

			bool checkIfNameContainsUpper = false;

			for (int i = 1; i < this.name.Length; i++)
			{
				if (Char.IsUpper(this.name[i]))
				{
					checkIfNameContainsUpper = false;
					break;
				}
				else
				{
					checkIfNameContainsUpper = true;
				}
			}

			bool surnameContainValidate = Regex.IsMatch(this.surname, @"^[a-zA-Z-]+$");
			char surnameChar = this.surname[0];
			bool firstCharSurnameCheck;
			firstCharSurnameCheck = Char.IsUpper(surnameChar);
			bool checkIfSurameContainsUpper = true;
			bool checkIfSurnameContainsUpperPart1 = true;
			bool checkIfSurnameContainsUpperPart2 = true;
			bool secoundCharSurnameCheck = true;
			if (this.surname.Contains('-'))
			{
				int index = this.surname.IndexOf('-');
				for (int i = 1; i < index; i++)
				{
					if (Char.IsUpper(this.surname[i]))
					{
						checkIfSurnameContainsUpperPart1 = false;
						break;
					}
					else
					{
						checkIfSurnameContainsUpperPart1 = true;
					}
				}

				secoundCharSurnameCheck = Char.IsUpper(this.surname[index + 1]);
				for (int i = index + 2; i < this.surname.Length; i++)
				{
					if (Char.IsUpper(this.surname[i]))
					{
						checkIfSurnameContainsUpperPart2 = false;
						break;
					}
					else
					{
						checkIfSurnameContainsUpperPart2 = true;
					}
				}
			}
			else
			{
				for (int i = 1; i < this.surname.Length; i++)
				{
					if (Char.IsUpper(this.surname[i]))
					{
						checkIfSurameContainsUpper = false;
						break;
					}
					else
					{
						checkIfSurameContainsUpper = true;
					}
				}
			}

			if (nameContainValidate && firstCharNameCheck && checkIfNameContainsUpper && surnameContainValidate && firstCharSurnameCheck && checkIfSurameContainsUpper
				&& checkIfSurnameContainsUpperPart1 && checkIfSurnameContainsUpperPart2 && secoundCharSurnameCheck)
			{
				return true;
			}
			return false;
		}
		public void AddChild(Person child)
		{
			if (this.hasSpouse)
			{
				spouse.AddChild(child);
				spouse.hasChild = true;
			}
			hasChild = true;
			children.Add(child);
		}
		public void AssignSpouse(Person spouse)
		{
			if (this.hasSpouse)
			{
				Console.WriteLine("Masz juz malzonka!");
				return;
			}
			if (spouse.hasSpouse)
			{
				Console.WriteLine("Ta osoba ma juz maloznka!");
				return;
			}
			if (this.gen == spouse.gen)
			{
				Console.WriteLine("Przepraszam zwiazki homosexualne nie sa mozliwe");
				return;
			}

			this.hasSpouse = true;
			this.spouse = spouse;
			spouse.spouse = this;
		
			if (this.hasChild)
			{
				spouse.hasChild = true;
				for(int i=0;i<children.Count;i++)
				{
					spouse.AddChild(children[i]);
				}
			}
		}
		public void PrintChildren()
		{
			if (!this.hasChild)
			{
				Console.WriteLine("Przepraszam nie masz dzieci!");
				return;
			}
			Console.WriteLine("Lista dzieci");
			for (int i = 0; i < this.children.Count; i++)
			{
				Console.WriteLine((i + 1) + "   " + children[i].name + "   " + children[i].surname + "   " + children[i].gen + "   " + children[i].birthdate);
			}

		}
		public void PrintGenealogicalTree()
		{
			
			Console.WriteLine("Imie i nazwisko:   " + this.name + "   " + this.surname);
			if (!this.hasChild && !this.hasSpouse)
			{
				return;
			}
			if (this.hasSpouse)
			{
				if (this.gen.Equals(Gender.Male))
					Console.WriteLine("Imie i nazwisko zony:   " + spouse.name + "   " + spouse.surname);
				if (this.gen.Equals(Gender.Female))
					Console.WriteLine("Imie i nazwisko meza:   " + spouse.name + "   " + spouse.surname);
			}
			if(this.hasChild)
			{
				Console.WriteLine("Imiona i nazwiska dzieci:");
				for (int i=0;i<children.Count;i++)
				{
					Console.WriteLine(children[i].name + "   " + children[i].surname);
				}
			}
		}								
		
	}
}