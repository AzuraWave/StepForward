
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using StepForward.Models;

namespace StepForward.Controllers
{
    public class HomeController : Controller
    {
        private readonly SectionTypeService _sectionTypeService;
        public HomeController()
        {
            _sectionTypeService = new SectionTypeService();
        }

        public ActionResult Index()
        {
            using (var context = new StepForwardContext())
            {

                var viewModel = _sectionTypeService.GetSectionTypeViewModel();

                return View(viewModel);
            }
        }

        [HttpPost]
        public ActionResult CreateSectionType(Section_Type sectionType)
        {
            _sectionTypeService.AddSectionType(sectionType);

            return RedirectToAction("Index");
        }


    }
}