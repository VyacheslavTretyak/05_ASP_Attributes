using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountControlLib
{
	public interface IRepository<T>
	{
		void Create(T item);
		void Update(T item);
		bool Delete(int id);
		T Get(int id);
		IEnumerable<T> GetAll();
		bool IsInit { get; set; }
	}
}
