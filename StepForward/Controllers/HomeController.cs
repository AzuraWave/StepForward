using StepForward.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StepForward.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            using (var context = new StepForwardContext())
            {
                var sectionTypes = context.Section_Types.ToList();
                if (sectionTypes == null || !sectionTypes.Any())
                {
                    ViewBag.Message = "No data found.";
                }
                return View(sectionTypes);
            }
        }
    }
}