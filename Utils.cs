using LittleLogBook.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace LittleLogBook.Data
{
	public static class Utils
	{
		public static string PopulateTemplateFromEntity(string Template, object Entity)
		{
			if (String.IsNullOrEmpty(Template) || Entity == null) throw new ArgumentNullException("The template argument or the entity object was not specified");

			string returnValue = Template;

			Type entityType = Entity.GetType();

			foreach(PropertyInfo propertyInfo in entityType.GetProperties())
			{
				string propertyValue = propertyInfo.GetValue(Entity, null) + "";

				returnValue = returnValue.Replace("{" + propertyInfo.Name + "}", propertyValue);
			}

			return returnValue;
		}

		public static string GetFileSizeText(long FileSize)
		{
			string returnValue = FileSize < 1000000000 ?
				string.Format("{0} MB", Math.Round((double) FileSize / 1000000, 2)) :
				string.Format("{0:F2} GB", Math.Round((double) FileSize / 1000000000, 2));

			return returnValue;
		}
	}
}