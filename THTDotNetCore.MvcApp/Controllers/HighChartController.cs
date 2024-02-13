using Microsoft.AspNetCore.Mvc;
using THTDotNetCore.MvcApp.Models;

namespace THTDotNetCore.MvcApp.Controllers
{
    public class HighChartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult PieWithLegend()
        {
            var model = new HighChartModel()
            {
                name = "Brands",
                datas = new List<Data>()
                {
                    new Data
                    {
                        name = "Chrome",
                        y = 74.77,
                        sliced = true,
                        selected = true,
                    },
                    new Data
                    {
                        name = "Edge",
                        y = 12.82,
                       
                    },
                     new Data
                    {
                        name = "Firefox",
                        y = 4.63,

                    }, new Data
                    {
                        name = "Safari",
                        y = 2.44,

                    },
                      new Data
                    {
                        name = "Internet Explorer",
                        y = 2.02,

                    },
                       new Data
                    {
                        name = "Other",
                        y = 3.28,

                    },


                }
               
            };

            return View(model);
        }
    }
}
