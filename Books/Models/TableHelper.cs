using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using BooksLib;

namespace Books.Controllers
{
	public static class TableHelper
	{
		public static MvcHtmlString TableGenerator<T>(this HtmlHelper htmlHelper, 
								IEnumerable<T> collection, 														
								Func<T, string> additionalColumn = null)
		{
			TagBuilder table = new TagBuilder("table");
			TagBuilder tr = new TagBuilder("tr");
			foreach(PropertyInfo info in typeof(T).GetProperties())
			{
				if(!info.GetCustomAttribute<ScaffoldColumnAttribute>()?.Scaffold ?? false)
				{
					continue;
				}
				var display = info.GetCustomAttribute<DisplayAttribute>();
				if (display != null)
				{
					tr.InnerHtml += new TagBuilder("th") { InnerHtml = display.Name}.ToString();
				}
				else
				{
					tr.InnerHtml += new TagBuilder("th") { InnerHtml = info.Name }.ToString();
				}				
			}
			if(additionalColumn != null)
			{
				tr.InnerHtml += new TagBuilder("th").ToString();
			}
			table.InnerHtml += tr.ToString();


			foreach (T item in collection)
			{
				TagBuilder row = new TagBuilder("tr");
				foreach (PropertyInfo info in typeof(T).GetProperties())
				{
					if (!info.GetCustomAttribute<ScaffoldColumnAttribute>()?.Scaffold ?? false)
					{
						continue;
					}
					if (info.GetCustomAttribute<DisplayListAttribute>() == null)
					{
						row.InnerHtml += new TagBuilder("td") { InnerHtml = info.GetValue(item)?.ToString() }.ToString();
					}
					else
					{
						StringBuilder str = new StringBuilder();
						IEnumerable<object> list = info.GetValue(item) as IEnumerable<object>;
						if (list != null)
						{
							foreach (var el in list)
							{
								str.Append(el.ToString());
							}
						}
						row.InnerHtml += new TagBuilder("td") { InnerHtml = str.ToString() }.ToString();
					}
				}
				if (additionalColumn != null)
					row.InnerHtml += new TagBuilder("td") { InnerHtml = additionalColumn(item) }.ToString();

				table.InnerHtml += row.ToString();
			}
			return new MvcHtmlString(table.ToString());
		}
	}
}