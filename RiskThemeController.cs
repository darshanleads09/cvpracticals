using DAL;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApplicationTest.DAL;
using WebApplicationTest.Models;

namespace WebApplicationTest.Controllers
{
    public class RiskThemeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDataTableService _dataTableService;
        public RiskThemeController(IUnitOfWork unitOfWork, IDataTableService dataTableService)
        {
            _unitOfWork = unitOfWork;
            _dataTableService = dataTableService;
        }

        public IActionResult Index() => View("~/Views/RiskManagement/Index.cshtml");

        [HttpPost]
        public async Task<JsonResult> GetData()
        {
            return Json(await _dataTableService.GetData<RiskTheme>(Request, new[] { "RiskThemeCode", "RiskThemeName" }));
        }

        // Load Create Partial
        public IActionResult Create() => PartialView("~/Views/RiskManagement/_RiskThemeForm.cshtml", new RiskTheme());

        // Load Edit Partial
        public async Task<IActionResult> Edit(int id)
        {
            var model = await _unitOfWork.RiskTheme.GetByIdAsync(id);
            return PartialView("~/Views/RiskManagement/_RiskThemeForm.cshtml", model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrUpdate(RiskTheme model)
        {
            if (model.RiskThemeId == 0)
            {
                model.CreatedDate = System.DateTime.Now;
                model.UpdatedDate = System.DateTime.Now;
                await _unitOfWork.RiskTheme.AddAsync(model);
            }
            else
            {
                model.UpdatedDate = System.DateTime.Now;
                await _unitOfWork.RiskTheme.UpdateAsync(model);
            }
            await _unitOfWork.CompleteAsync();
            return Json(new { success = true });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _unitOfWork.RiskTheme.DeleteAsync(id);
            await _unitOfWork.CompleteAsync();
            return Json(new { success = true });
        }
    }
}
