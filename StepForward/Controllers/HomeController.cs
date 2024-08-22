
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
        public JsonResult CreateSectionType(Section_Type sectionType)
        {
            var viewModel = _sectionTypeService.GetSectionTypeViewModel();
            viewModel.SourceAction = "CreateSectionType";

            string errormessage = null;
            viewModel.IsSuccess = _sectionTypeService.AddSectionType(sectionType, out errormessage);
            viewModel.Message = errormessage;
            viewModel.NewSectionType = sectionType; 

            return Json(new { viewModel});
        }

        [HttpPost]
        public JsonResult DeleteSectionTypes(int[] selectedItems)
        {
            var viewModel = _sectionTypeService.GetSectionTypeViewModel();
            viewModel.SourceAction = "DeleteSectionType";

            string errormessage = null;
            viewModel.IsSuccess = _sectionTypeService.DeleteSectionTypes(selectedItems, out errormessage);

            viewModel.Message = errormessage;

            return Json(new { viewModel });
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
        public JsonResult EditSectionType(SectionTypeViewModel model)
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
                return Json(new { viewModel });
                }

            return Json( viewModel );
        }

        [HttpGet]

        public ActionResult GetPartialView () {

            var viewModel = _sectionTypeService.GetSectionTypeViewModel();
            return PartialView("CRUD", viewModel);
        }


    }
}