using CurrencyExplorer.Core;
using CurrencyExplorer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CurrencyExplorer.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var CourseCurrency = new CurrencyAggregatedCourses();
            return View(CourseCurrency.GetCurrencCourses().OrderBy(c=> c.AggregateInterval).ThenBy(c => c.ValuePairName));
        }
    }
}