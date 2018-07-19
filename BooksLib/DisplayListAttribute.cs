using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BooksLib
{	
	[AttributeUsage(AttributeTargets.Property)]	
	public class DisplayListAttribute : Attribute
	{	
	}
}