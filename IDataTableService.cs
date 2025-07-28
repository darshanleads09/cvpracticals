using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace WebApplicationTest.DAL
{
    public interface IDataTableService
    {
        Task<object> GetData<T>(HttpRequest request, string[] searchableColumns) where T : class;
    }
}
