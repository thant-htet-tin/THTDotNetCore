using Microsoft.AspNetCore.Mvc;
using System.Reflection.Emit;
using THTDotNetCore.MvcApp.Models;

namespace THTDotNetCore.MvcApp.Controllers
{
    public class ChartJsController : Controller
    {
        public IActionResult StackedBarChartWithGroup()
        {
            string[] months = new string[]
             {
                "January",
                "February",
                "March",
                "April",
                "May",
                "June",
                "July",
                "August",
                "September",
                "October",
                "November",
                "December"
             };

            var model = new ChartJsModel
            {
                labels = months,
                datasets = new List<datasetsModel>()
                {
                     new datasetsModel
                     {
                        label = "Dataset 1",
                        data = new[] { 19, 29, 28, 59, 24, 28, 85 },
                        backgroundColor = "rgb(255, 99, 132)",
                        stack = "Stack 0"
                     },
                      new datasetsModel
                     {
                        label = "Dataset 2",
                        data = new[] { 45, 12, 45, 65, 87, 20, 46 },
                        backgroundColor = "rgb(75, 192, 192)",
                        stack = "Stack 0"
                     },
                       new datasetsModel
                     {
                        label = "Dataset 3",
                        data = new[] { 39, 49, 50, 94, 64, 82, 99 },
                        backgroundColor = "rgb(153, 102, 255)",
                        stack = "Stack 1"
                     },
                }
            };



            return View(model);
        }

        public IActionResult SubtitleChart()
        {
            string[] months = new string[]
             {
                "January",
                "February",
                "March",
                "April",
                "May",
                "June",
                "July",
                "August",
                "September",
                "October",
                "November",
                "December"
             };

            var model = new ChartJsModel
            {
                labels = months,
                datasets = new List<datasetsModel>()
                {
                     new datasetsModel
                     {
                        label = "Dataset 1",
                        data = new[] { 19, 29, 28, 59, 24, 28, 85 },
                        backgroundColor = "rgb(255, 99, 132)",
                        fill = false
                     }
                }
            };
            return View(model);
        }


    }
}
