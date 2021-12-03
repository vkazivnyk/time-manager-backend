using System.Collections.Generic;
using System.Threading.Tasks;

namespace TimeManageData.Repositories
{
    public interface IRepository<T>
    {
        public Task SaveChangesAsync();
        public T Create(T item);
        public T Find(string id);
        public T Update(T item);
        public T Delete(string id);
        public List<T> GetAll();
    }
}