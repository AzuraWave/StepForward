
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
                var viewModel = _sectionTypeService.GetSectionTypeViewModel();
                return View(viewModel);
  
        }

        [HttpPost]
        public ActionResult CreateSectionType(Section_Type sectionType)
        {
            var viewModel = _sectionTypeService.GetSectionTypeViewModel();
            viewModel.SourceAction = "CreateSectionType";

            string errormessage = null;
            bool isSuccessful = _sectionTypeService.AddSectionType(sectionType, out errormessage);

            if (!isSuccessful)
            {
                viewModel.IsSuccess = false;
                viewModel.Message = errormessage;
                return View("Index", viewModel); 
            }

            return RedirectToAction("Index"); 
        }

        [HttpPost]
        public ActionResult DeleteSectionTypes(int[] selectedItems)
        {
            var viewModel = _sectionTypeService.GetSectionTypeViewModel();
            viewModel.SourceAction = "DeleteSectionType";

            string errormessage = null;
            bool isSuccessful = _sectionTypeService.DeleteSectionTypes(selectedItems, out errormessage);

            if (!isSuccessful)
            {
                viewModel.IsSuccess = false;
                viewModel.Message = errormessage;
                return View("Index", viewModel); 
            }

            return RedirectToAction("Index"); 
        }


        [HttpGet]
        public ActionResult EditSectionType(int id )
        {
            
                var sectionType = _sectionTypeService.GetSection_Type(id);
                
                var viewmodel = _sectionTypeService.GetSectionTypeViewModel();
                viewmodel.NewSectionType = sectionType;
                viewmodel.SourceAction = "GetEditSectionType";

                return View(viewmodel);
            
        }

        [HttpPost]
        public ActionResult EditSectionType(SectionTypeViewModel model)
        {
            var viewModel = _sectionTypeService.GetSectionTypeViewModel();
            viewModel.NewSectionType = model.NewSectionType;
            viewModel.SourceAction = "PostEditSectionType";

            string error = null;
            
            bool isSuccessful =  _sectionTypeService.EditSectionType(model.NewSectionType, out error);
                
                if (!isSuccessful)
                { 
                    viewModel.IsSuccess = false;
                    viewModel.Message = error;
                    return View(viewModel);
                }

            return RedirectToAction("Index");
        }

    }
}