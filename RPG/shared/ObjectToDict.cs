using Godot;
using Godot.Collections;
using System.Reflection;

public class MainHelper
{
	public static Dictionary<string, T> ObjectToDictionary<T>(object obj)
	{
		Dictionary<string, T> dict = new Dictionary<string, T>();

		PropertyInfo[] properties = obj.GetType().GetProperties();

		foreach (PropertyInfo property in properties)
		{
			GD.Print(property.Name);
			dict.Add(property.Name, (T)property.GetValue(obj));
		}

		return dict;
	}
}
