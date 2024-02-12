using Microsoft.AspNetCore.Mvc;
using THTDotNetCore.MvcApp.Models;

namespace THTDotNetCore.MvcApp.Controllers
{
    public class ApexChartController : Controller
    {
        public IActionResult PyramidChart()
        {
            var model = new PyramidChartModel()
            {
                categories = new List<string> { "sweet", "Processed Foods", "Healthy Fats", "Meat", "Beans & Legumes", "Dairy", "Fruits & Vegetables", "Grains" },
                data = new List<int>() { 200, 330, 548, 740, 880, 990, 1100, 1320 },
            };
            return View(model);
        }
    }
}

