using DAL;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApplicationTest.DAL;
using WebApplicationTest.Models;

namespace WebApplicationTest.Controllers
{
    public class RiskSubThemeLevel3Controller : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDataTableService _dataTableService;
        public RiskSubThemeLevel3Controller(IUnitOfWork unitOfWork, IDataTableService dataTableService)
        {
            _unitOfWork = unitOfWork;
            _dataTableService = dataTableService;
        }

        [HttpPost]
        public async Task<JsonResult> GetData()
        {
            return Json(await _dataTableService.GetData<RiskSubThemeLevel3>(Request, new[] { "RiskSubThemeLevel3Code", "RiskSubThemeLevel3Name" }));
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.RiskTheme = await _unitOfWork.RiskTheme.GetAllAsync(1, 100);
            ViewBag.Level2s = await _unitOfWork.RiskSubThemeLevel2.GetAllAsync(1, 100);
            return PartialView("~/Views/RiskManagement/_Level3Form.cshtml", new RiskSubThemeLevel3());
        }

        public async Task<IActionResult> Edit(int id)
        {
            var model = await _unitOfWork.RiskSubThemeLevel3.GetByIdAsync(id);
            ViewBag.RiskTheme = await _unitOfWork.RiskTheme.GetAllAsync(1, 100);
            ViewBag.Level2s = await _unitOfWork.RiskSubThemeLevel2.GetAllAsync(1, 100);
            return PartialView("~/Views/RiskManagement/_Level3Form.cshtml", model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrUpdate(RiskSubThemeLevel3 model)
        {
            if (model.RiskSubThemeLevel3Id == 0)
            {
                model.CreatedDate = System.DateTime.Now;
                model.UpdatedDate = System.DateTime.Now;
                await _unitOfWork.RiskSubThemeLevel3.AddAsync(model);
            }
            else
            {
                model.UpdatedDate = System.DateTime.Now;
                await _unitOfWork.RiskSubThemeLevel3.UpdateAsync(model);
            }
            await _unitOfWork.CompleteAsync();
            return Json(new { success = true });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _unitOfWork.RiskSubThemeLevel3.DeleteAsync(id);
            await _unitOfWork.CompleteAsync();
            return Json(new { success = true });
        }
    }
}
