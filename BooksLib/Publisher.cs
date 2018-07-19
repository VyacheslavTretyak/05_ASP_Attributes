using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BooksLib
{
	public class Publisher
	{
		public int ID { get; set; }		
		
		[Required]
		[MaxLength(32)]
		public string Name { get; set; }

		public override string ToString()
		{
			return Name;
		}
	}
}