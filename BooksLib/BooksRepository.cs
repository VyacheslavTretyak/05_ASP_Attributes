using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace BooksLib
{
	public class BooksRepository:IRepository<Book>
    {
		private static BooksRepository instance = null;
		public static BooksRepository Instance => instance ?? (instance = new BooksRepository());

		private List<Book> list = new List<Book>();
		private BooksRepository() { }

		public void Create(Book item)
		{
			item.ID = (list.Any() ? list.Max(i => i.ID) : 0) + 1;
			list?.Add(item);
		}
		public bool Delete(int id) => list?.Remove(Get(id)) ?? false;

		public Book Get(int id) => list?.Find(role => role.ID == id);

		public IEnumerable<Book> GetAll() => list;

		public void Update(Book editing)
		{
			Book book = Get(editing.ID);
			foreach (PropertyInfo propInfo in book.GetType().GetProperties().Where(p => p.Name != "ID"))
			{
				propInfo.SetValue(book, propInfo.GetValue(editing));
			}
		}
		public bool IsInit { get; set; } = false;
	}
}
