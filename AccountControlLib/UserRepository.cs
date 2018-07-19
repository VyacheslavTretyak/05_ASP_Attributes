using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AccountControlLib
{
	public class UserRepository : IRepository<User>
	{
		private static UserRepository instance = null;
		public static UserRepository Instance => instance ?? (instance = new UserRepository());

		private List<User> list = new List<User>();
		private UserRepository() { }

		public void Create(User item)
		{
			item.ID = (list.Any() ? list.Max(i => i.ID) : 0) + 1;
			list?.Add(item);
		}

		public bool Delete(int id) => list?.Remove(Get(id)) ?? false;

		public User Get(int id) => list?.Find(user => user.ID == id);

		public IEnumerable<User> GetAll() => list;

		public void Update(User editing)
		{
			User user = Get(editing.ID);
			foreach(PropertyInfo propInfo in user.GetType().GetProperties().Where(p => p.Name != "ID"))
			{
				//PropertyInfo editingProp = editing.GetType().GetProperty(propInfo.Name);
				propInfo.SetValue(user, propInfo.GetValue(editing));
			}
		}

		public bool IsInit { get; set; } = false;
	}
}

