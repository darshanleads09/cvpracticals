using System.Threading.Tasks;
using WebApplicationTest.Models;

namespace WebApplicationTest.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public IRepository<RiskTheme> RiskTheme { get; private set; }
        public IRepository<RiskSubThemeLevel1> RiskSubThemeLevel1 { get; private set; }
        public IRepository<RiskSubThemeLevel2> RiskSubThemeLevel2 { get; private set; }
        public IRepository<RiskSubThemeLevel3> RiskSubThemeLevel3 { get; private set; }

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            RiskTheme = new Repository<RiskTheme>(_context);
            RiskSubThemeLevel1 = new Repository<RiskSubThemeLevel1>(_context);
            RiskSubThemeLevel2 = new Repository<RiskSubThemeLevel2>(_context);
            RiskSubThemeLevel3 = new Repository<RiskSubThemeLevel3>(_context);
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
