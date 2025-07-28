using DAL;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApplicationTest.DAL;
using WebApplicationTest.Models;

namespace WebApplicationTest.Controllers
{
    public class RiskSubThemeLevel1Controller : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDataTableService _dataTableService;
        public RiskSubThemeLevel1Controller(IUnitOfWork unitOfWork, IDataTableService dataTableService)
        {
            _unitOfWork = unitOfWork;
            _dataTableService = dataTableService;
        }

        [HttpPost]
        public async Task<JsonResult> GetData()
        {
            return Json(await _dataTableService.GetData<RiskSubThemeLevel1>(Request, new[] { "RiskSubThemeLevel1Code", "RiskSubThemeLevel1Name" }));
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.RiskTheme = await _unitOfWork.RiskTheme.GetAllAsync(1, 100);
            return PartialView("~/Views/RiskManagement/_Level1Form.cshtml", new RiskSubThemeLevel1());
        }

        public async Task<IActionResult> Edit(int id)
        {
            var model = await _unitOfWork.RiskSubThemeLevel1.GetByIdAsync(id);
            ViewBag.RiskTheme = await _unitOfWork.RiskTheme.GetAllAsync(1, 100);
            return PartialView("~/Views/RiskManagement/_Level1Form.cshtml", model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrUpdate(RiskSubThemeLevel1 model)
        {
            if (model.RiskSubThemeLevel1Id == 0)
            {
                model.CreatedDate = System.DateTime.Now;
                model.UpdatedDate = System.DateTime.Now;
                await _unitOfWork.RiskSubThemeLevel1.AddAsync(model);
            }
            else
            {
                model.UpdatedDate = System.DateTime.Now;
                await _unitOfWork.RiskSubThemeLevel1.UpdateAsync(model);
            }
            await _unitOfWork.CompleteAsync();
            return Json(new { success = true });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _unitOfWork.RiskSubThemeLevel1.DeleteAsync(id);
            await _unitOfWork.CompleteAsync();
            return Json(new { success = true });
        }
    }
}
