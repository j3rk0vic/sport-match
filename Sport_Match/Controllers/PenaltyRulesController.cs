using Microsoft.AspNetCore.Mvc;
using Sport_Match.Dtos;
using Sport_Match.Services;
using System.Threading.Tasks;

namespace SportMatch.Controllers
{
    public class PenaltyRulesController : Controller
    {
        private readonly IPenaltyRuleService _penaltyRuleService;

        public PenaltyRulesController(IPenaltyRuleService penaltyRuleService)
        {
            _penaltyRuleService = penaltyRuleService;
        }

        public async Task<IActionResult> Index()
        {
            var rule = await _penaltyRuleService.GetAsync();

            var dto = new PenaltyRuleDto
            {
                LateCancellationPenalty = rule.LateCancellationPenalty,
                NoShowEnabled = rule.NoShowEnabled
            };

            return View("~/Views/PenaltyRules/Index.cshtml", dto);
        }


        [HttpPost]
        public async Task<IActionResult> Save(PenaltyRuleDto dto)
        {
            await _penaltyRuleService.UpdateAsync(dto);
            return RedirectToAction("Index");
        }
    }
}
