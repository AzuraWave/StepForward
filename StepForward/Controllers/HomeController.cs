
using System;
using System.Collections.Generic;
using System.Data.Linq;
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
        public ActionResult EditSectionType(int id )
        {
            using (var db = new StepForwardContext())
            {
                var sectionType = db.Section_Types.SingleOrDefault(st => st.Id == id);

                if (sectionType == null)
                {
                    return HttpNotFound();
                }

                var viewModel = new SectionTypeViewModel
                {
                    NewSectionType = sectionType
                };

                return View(viewModel);
            }
        }

        [HttpPost]
        public ActionResult EditSectionType(SectionTypeViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (var db = new StepForwardContext())
                {
                    var sectionType = db.Section_Types.SingleOrDefault(x => x.Id == model.NewSectionType.Id);

                    if (sectionType == null)
                    {
                        return HttpNotFound();
                    }

                    sectionType.Name = model.NewSectionType.Name;

                    db.SubmitChanges();

                    return RedirectToAction("Index");
                }
            }

            // If the model state is invalid, redisplay the form with validation errors
            return View(model);
        }

    }
}