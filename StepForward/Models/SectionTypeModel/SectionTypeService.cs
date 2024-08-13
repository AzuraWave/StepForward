using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc.Html;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Serialization;
using System.Xml.Linq;


namespace StepForward.Models
{
    public class SectionTypeService
    {
        private readonly StepForwardContext _context;

        public SectionTypeService()
        {
            _context = new StepForwardContext();
        }

        public IEnumerable<Section_Type> GetAllSectionTypes(out string errormessage)
        {
            errormessage = null;
            try
            {

                var list = _context.Section_Types.ToList();

                if (list == null || !list.Any())
                {
                    errormessage = (list + " Is Empty");
                }

                return list;
            }
            catch (Exception ex)
            {
                errormessage = ex.Message;
                return null;
            }
        }

        public SectionTypeViewModel GetSectionTypeViewModel(out string errormessage1)
        {

            string errormessage = null;
            errormessage1 = null;
            try
            {
                var viewModel = new SectionTypeViewModel
                {
                    NewSectionType = new Section_Type(),
                    SectionTypes = GetAllSectionTypes(out errormessage),

                };
                errormessage1 = errormessage;
                return viewModel;
            }
            catch (Exception ex)
            {
                errormessage1 = ex.Message;
                return null;
            }
        }

        public void AddSectionType(Section_Type sectionType, out string ADDerrormessage)
        {
            ADDerrormessage = null;
            string name = sectionType.Name;
            try
            {
                if (string.IsNullOrEmpty(name))
                {
                    ADDerrormessage = "Section Type Name can't be empty.";
                    return;
                }

                if (!Regex.IsMatch(name, "^[A-Za-z]+$"))
                {
                    ADDerrormessage = "Section Type Name must not contain special characters.";
                    return;
                }

                if (name.Length > 10)
                {
                    ADDerrormessage = "Section Type Name must be 10 or fewer characters.";
                    return;
                }



                _context.Section_Types.InsertOnSubmit(sectionType);
                _context.SubmitChanges();
            }
            catch (Exception ex)
            {
                ADDerrormessage = ex.Message;
            }



        }

        public void DeleteSectionTypes(int[] selectedItems, out string DELETEerrormessage)
        {
            DELETEerrormessage = null;
            try
            {
                if (selectedItems == null || !selectedItems.Any())
                {
                    DELETEerrormessage = "No Section Types Selected";
                    return;
                }

                var sectionTypes = _context.Section_Types.Where(x => selectedItems.Contains(x.Id)).ToList();

                if (sectionTypes == null || !sectionTypes.Any())
                {
                    DELETEerrormessage = "No Section Types Found";
                    return;
                }
                _context.Section_Types.DeleteAllOnSubmit(sectionTypes);
                _context.SubmitChanges();
            }
            catch (Exception ex)
            {
                DELETEerrormessage = ex.Message;
            }
        }


        
    }
}
