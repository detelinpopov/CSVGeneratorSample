using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace CSVGenerator.Core
{
	public static class CSVGenerator
	{
		public static string GenerateToCsv<T>(this IList<T> items, char seperator, bool includeHeader = true)
		{
			StringBuilder csvContent = new StringBuilder();
			if (includeHeader)
			{
				string csvHeader = CreateHeader<T>(seperator);
				csvContent.Append(csvHeader);
			}

			string csvBody = CreateRows(items, seperator);
			csvContent.Append(csvBody);

			return csvContent.ToString();
		}

		private static string CreateHeader<T>(char seperator)
		{
			StringBuilder header = new StringBuilder();
			PropertyInfo[] properties = typeof(T).GetProperties();
			for (int i = 0; i < properties.Length - 1; i++)
			{
				header.Append(properties[i].Name + seperator);
			}
			string lastPropertyName = properties[properties.Length - 1].Name;
			header.Append(lastPropertyName).Append('\n');
			return header.ToString();
		}

		private static string CreateRows<T>(IList<T> list, char seperator)
		{
			StringBuilder rows = new StringBuilder();
			foreach (T item in list)
			{
				PropertyInfo[] properties = typeof(T).GetProperties();
				for (int i = 0; i < properties.Length - 1; i++)
				{
					PropertyInfo prop = properties[i];
					rows.Append(prop.GetValue(item) + seperator.ToString());
				}
				PropertyInfo lastProperty = properties[properties.Length - 1];
				rows.Append(lastProperty.GetValue(item)).Append('\n');
			}
			return rows.ToString();
		}
	}
}
