using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BooksLib
{
	public class Author
	{
		public int ID { get; set; }
		[Required]
		[MaxLength(32)]
		public string Name { get; set; }
		[DataType(DataType.Date)]
		[Required]
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
		[Display(Name = "Birth Date")]
		public DateTime DateOfBirth { get; set; }
		[DataType(DataType.Date)]
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
		[Display(Name = "Date Of Death")]
		public DateTime? DateOfDeath { get; set; }
		public override string ToString()
		{
			return $"{Name } ";			
		}

	}
}