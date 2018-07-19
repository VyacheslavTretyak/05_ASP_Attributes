using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BooksLib
{
	public class Book
	{		
		public int ID { get; set; }
		[Required]		
		[DataType(DataType.Text)]
		[MaxLength (32)]
		public string Name { get; set; }		
		[DisplayList]
		[Display(Name = "Authors")]
		public IEnumerable<Author> Author { get; set; }		
		public Publisher Publisher { get; set; }
		[DataType(DataType.Date)]
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
		[Required]
		[Display(Name = "Publish date")]
		public DateTime PublishDate { get; set; }
		[Required]
		[Display(Name = "Page count")]
		public int PageCount { get; set; }
		[Required]		
		[StringLength(18,MinimumLength =18)]
		public string ISBN { get; set; }
	}
}