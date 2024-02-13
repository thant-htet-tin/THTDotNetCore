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

        public IActionResult StackedAndGroupedColumn()
        {
            var model = new HighChartModel()
            {
                //name = "Board",
                stackedData = new List<StackedData>
                {
                    new StackedData
                    {
                        name = "Norway",
                        data = new List<int>()
                        {
                            148, 133, 124
                        },
                        stack = "Europe"

                    },
                    new StackedData
                    {
                        name = "Germany",
                        data = new List<int>()
                        {
                            102, 98, 65
                        },
                        stack = "Europe"

                    },new StackedData
                    {
                        name = "United States",
                        data = new List<int>()
                        {
                            113, 122, 95
                        },
                        stack = "North America"

                    },new StackedData
                    {
                        name = "Canada",
                        data = new List<int>()
                        {
                            77, 72, 80
                        },
                        stack = "North America"

                    },
                }
            };
            return View(model);
        }
    }
}
