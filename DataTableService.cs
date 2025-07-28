using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System;
using WebApplicationTest.Models;
using WebApplicationTest.DAL;
namespace DAL
{
    public class DataTableService : IDataTableService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DataTableService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<object> GetData<T>(HttpRequest request, string[] searchableColumns) where T : class
        {
            // ✅ Extract DataTables parameters from Request.Form
            var form = request.Form;

            int pageIndex = (Convert.ToInt32(form["start"]) / Convert.ToInt32(form["length"])) + 1;
            int pageSize = Convert.ToInt32(form["length"]);
            string sortColumn = form[$"columns[{form["order[0][column]"]}][data]"];
            string sortDirection = form["order[0][dir]"];
            string searchValue = form["search[value]"];

            // ✅ Resolve Repository for T dynamically
            var repo = GetRepository<T>();

            // ✅ Call generic GetPagedAsync from Repository
            var (data, total) = await repo.GetPagedAsync(pageIndex, pageSize, sortColumn, sortDirection, searchValue, searchableColumns);

            return new
            {
                draw = Convert.ToInt32(form["draw"]),
                recordsTotal = total,
                recordsFiltered = total,
                data
            };
        }

        // ✅ Helper: Resolve Repository for any entity
        private IRepository<T> GetRepository<T>() where T : class
        {
            if (typeof(T) == typeof(RiskTheme))
                return (IRepository<T>)_unitOfWork.RiskTheme;

            if (typeof(T) == typeof(RiskSubThemeLevel1))
                return (IRepository<T>)_unitOfWork.RiskSubThemeLevel1;

            if (typeof(T) == typeof(RiskSubThemeLevel2))
                return (IRepository<T>)_unitOfWork.RiskSubThemeLevel2;

            if (typeof(T) == typeof(RiskSubThemeLevel3))
                return (IRepository<T>)_unitOfWork.RiskSubThemeLevel3;

            throw new InvalidOperationException($"Repository for type {typeof(T).Name} not found.");
        }
    }
}
