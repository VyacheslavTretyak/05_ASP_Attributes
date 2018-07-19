using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace BooksLib
{
	public class AuthorsRepository : IRepository<Author>
	{
		private static AuthorsRepository instance = null;
		public static AuthorsRepository Instance => instance ?? (instance = new AuthorsRepository());

		private List<Author> list = new List<Author>();
		private AuthorsRepository() { }

		public void Create(Author item)
		{
			item.ID = (list.Any()?list.Max(i=>i.ID) : 0) + 1;
			list?.Add(item);
		}
		public bool Delete(int id) => list?.Remove(Get(id)) ?? false;

		public Author Get(int id) => list?.Find(role => role.ID == id);

		public IEnumerable<Author> GetAll() => list;

		public void Update(Author editing)
		{
			Author author = Get(editing.ID);
			foreach (PropertyInfo propInfo in author.GetType().GetProperties().Where(p => p.Name != "ID"))
			{
				propInfo.SetValue(author, propInfo.GetValue(editing));
			}
		}
		public bool IsInit { get; set; } = false;
	}
}
