using System.Collections.Generic;
using System.Threading.Tasks;

namespace TimeManageData.Repositories
{
    public interface IRepository<T>
    {
        public Task SaveChangesAsync();
        public void Create(T item);
        public T Find(string id);
        public bool Update(string id, T item);
        public bool Delete(string id);
        public List<T> GetAll();
    }
}