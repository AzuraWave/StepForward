
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
                 string errormessage = null; 

                var viewModel = _sectionTypeService.GetSectionTypeViewModel(out errormessage);



                return View(viewModel);
            }
        }

        [HttpPost]
        public ActionResult CreateSectionType(Section_Type sectionType)
        {

            string errormessage = null;

            _sectionTypeService.AddSectionType(sectionType ,out errormessage);

            if (!string.IsNullOrEmpty(errormessage))
            {
                ModelState.AddModelError("CreateSectionTypes", errormessage);
            }



            var viewModel = _sectionTypeService.GetSectionTypeViewModel(out errormessage);
            return View("Index", viewModel);
        }

        [HttpPost]
        public ActionResult DeleteSectionTypes(int[] selectedItems)
        {
            string errormessage = null;

            _sectionTypeService.DeleteSectionTypes(selectedItems, out errormessage);
            if (!string.IsNullOrEmpty(errormessage))
            {
                ModelState.AddModelError("DeleteSectionTypes", errormessage);
            }


            var viewModel = _sectionTypeService.GetSectionTypeViewModel(out errormessage);
            return View("Index", viewModel);
        }

        [HttpGet]
        public ActionResult EditSectionType(int id)
        {
            
            
        }

        [HttpPost]
        public ActionResult EditSectionType(Section_Type sectionType)
        {
            
        }

    }
}