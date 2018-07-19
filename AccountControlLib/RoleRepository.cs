using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AccountControlLib
{
	public class RoleRepository : IRepository<Role>
	{
		private static RoleRepository instance = null;
		public static RoleRepository Instance => instance ?? (instance = new RoleRepository());

		private List<Role> list = new List<Role>();
		private RoleRepository() { }

		public void Create(Role item)
		{
			item.ID = (list.Any() ? list.Max(i => i.ID) : 0) + 1;
			list?.Add(item);
		}


		public bool Delete(int id) => list?.Remove(Get(id)) ?? false;

		public Role Get(int id) => list?.Find(role => role.ID == id);

		public IEnumerable<Role> GetAll() => list;

		public void Update(Role editing)
		{
			Role role = Get(editing.ID);
			foreach (PropertyInfo propInfo in role.GetType().GetProperties().Where(p => p.Name != "ID"))
			{				
				propInfo.SetValue(role, propInfo.GetValue(editing));
			}
		}

		public bool IsInit { get; set; } = false;
	}
}
