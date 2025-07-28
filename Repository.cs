using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationTest.Models;
using System.Linq.Expressions;
using System;

namespace WebApplicationTest.DAL
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync(int pageIndex, int pageSize)
        {
            return await _dbSet.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<int> GetCountAsync()
        {
            return await _dbSet.CountAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
            }
        }

        /// <summary>
        /// Retrieves a paginated, searchable, and sortable list of entities from the database using Expression Trees.
        /// </summary>
        /// <param name="pageIndex">The current page index (1-based).</param>
        /// <param name="pageSize">The number of records per page.</param>
        /// <param name="sortColumn">The name of the column to sort by. Must match a property name of the entity.</param>
        /// <param name="sortDirection">The sort direction: "asc" for ascending, "desc" for descending.</param>
        /// <param name="search">The search keyword to filter the data. If empty or null, no filtering is applied.</param>
        /// <param name="searchableColumns">An array of property names (columns) to search in.</param>
        /// <returns>
        /// A tuple containing:
        /// <list type="bullet">
        /// <item>
        /// <description><c>Data</c>: The list of entities for the current page, filtered and sorted.</description>
        /// </item>
        /// <item>
        /// <description><c>TotalCount</c>: The total number of records after filtering (used for pagination).</description>
        /// </item>
        /// </list>
        /// </returns>
        /// <remarks>
        /// This method dynamically builds LINQ expressions to apply search and sorting,
        /// ensuring the operations are executed in the database (SQL) for performance.
        /// </remarks>
        public async Task<(IEnumerable<T> Data, int TotalCount)> GetPagedAsync(
            int pageIndex,
            int pageSize,
            string sortColumn,
            string sortDirection,
            string search,
            string[] searchableColumns)
        {
            IQueryable<T> query = _dbSet;

            // ✅ Search Filter
            if (!string.IsNullOrWhiteSpace(search) && searchableColumns?.Length > 0)
            {
                var parameter = Expression.Parameter(typeof(T), "x");
                Expression orExpression = null;

                foreach (var column in searchableColumns)
                {
                    var property = Expression.Property(parameter, column);
                    var toStringCall = Expression.Call(property, "ToString", Type.EmptyTypes);
                    var toLowerCall = Expression.Call(toStringCall, "ToLower", null);
                    var containsCall = Expression.Call(
                        toLowerCall,
                        typeof(string).GetMethod("Contains", new[] { typeof(string) }),
                        Expression.Constant(search.ToLower())
                    );

                    orExpression = orExpression == null
                        ? (Expression)containsCall
                        : Expression.OrElse(orExpression, containsCall);
                }

                if (orExpression != null)
                {
                    var lambda = Expression.Lambda<Func<T, bool>>(orExpression, parameter);
                    query = query.Where(lambda);
                }
            }

            // ✅ Total Count
            var totalCount = await query.CountAsync();

            // ✅ Sorting
            if (!string.IsNullOrEmpty(sortColumn))
            {
                var propertyInfo = typeof(T).GetProperty(sortColumn);
                if (propertyInfo != null)
                {
                    var parameter = Expression.Parameter(typeof(T), "x");
                    var property = Expression.Property(parameter, propertyInfo);
                    var conversion = Expression.Convert(property, typeof(object));
                    var lambda = Expression.Lambda<Func<T, object>>(conversion, parameter);

                    query = sortDirection == "asc"
                        ? query.OrderBy(lambda)
                        : query.OrderByDescending(lambda);
                }
            }

            // ✅ Paging
            var data = await query.Skip((pageIndex - 1) * pageSize)
                                  .Take(pageSize)
                                  .ToListAsync();

            return (data, totalCount);
        }


    }
}
