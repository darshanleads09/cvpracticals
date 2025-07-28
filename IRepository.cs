using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApplicationTest.DAL
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync(int pageIndex, int pageSize);
        Task<int> GetCountAsync();
        Task<T> GetByIdAsync(int id);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);

        Task<(IEnumerable<T> Data, int TotalCount)> GetPagedAsync(
    int pageIndex,
    int pageSize,
    string sortColumn,
    string sortDirection,
    string search,
    string[] searchableColumns);

    }
}
