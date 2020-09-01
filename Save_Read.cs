using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace Person
{
    class Save_Read
    {
		public static void serializableTree(Person person,string path)
		{
			IFormatter formatter = new BinaryFormatter();

			Stream stream = new FileStream(path, FileMode.Create, FileAccess.Write);
			formatter.Serialize(stream, person);
			stream.Close();
		}
		public static Person DeserializableTree(string path)
		{
			IFormatter formatter = new BinaryFormatter();

			Stream stream = new FileStream(path, FileMode.Open, FileAccess.Read);
			Person temp = (Person)formatter.Deserialize(stream);
			stream.Close();
			return temp;
		}
	}
}
