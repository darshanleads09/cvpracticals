using System.Threading.Tasks;
using System;
using WebApplicationTest.Models;

namespace WebApplicationTest.DAL
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<RiskTheme> RiskTheme { get; }
        IRepository<RiskSubThemeLevel1> RiskSubThemeLevel1 { get; }
        IRepository<RiskSubThemeLevel2> RiskSubThemeLevel2 { get; }
        IRepository<RiskSubThemeLevel3> RiskSubThemeLevel3 { get; }
        Task<int> CompleteAsync();
    }
}
