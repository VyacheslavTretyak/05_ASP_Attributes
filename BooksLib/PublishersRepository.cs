using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace BooksLib
{
	public class PublishersRepository:IRepository<Publisher>
    {
		private static PublishersRepository instance = null;
		public static PublishersRepository Instance => instance ?? (instance = new PublishersRepository());

		private List<Publisher> list = new List<Publisher>();
		private PublishersRepository() { }

		public void Create(Publisher item)
		{
			item.ID = (list.Any() ? list.Max(i => i.ID) : 0) + 1;
			list?.Add(item);
		}
		public bool Delete(int id) => list?.Remove(Get(id)) ?? false;

		public Publisher Get(int id) => list?.Find(role => role.ID == id);

		public IEnumerable<Publisher> GetAll() => list;

		public void Update(Publisher editing)
		{
			Publisher publisher = Get(editing.ID);
			foreach (PropertyInfo propInfo in publisher.GetType().GetProperties().Where(p => p.Name != "ID"))
			{
				propInfo.SetValue(publisher, propInfo.GetValue(editing));
			}
		}
		public bool IsInit { get; set; } = false;
	}
}
