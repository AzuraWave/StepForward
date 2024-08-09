using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc.Html;
using System.Text.RegularExpressions;


namespace StepForward.Models
{
    public class SectionTypeService
    {
        private readonly StepForwardContext _context;

        public SectionTypeService()
        {
            _context = new StepForwardContext();
        }

        public IEnumerable<Section_Type> GetAllSectionTypes()
        {
            try
            {
                var list = _context.Section_Types.ToList();

                if (list == null || !list.Any())
                {
                    throw new Exception(list + " Is Empty");
                }

                return list;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public SectionTypeViewModel GetSectionTypeViewModel() {

            try
            {
                var viewModel = new SectionTypeViewModel
                {
                    NewSectionType = new Section_Type(),
                    SectionTypes = GetAllSectionTypes()

                };

                return viewModel;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void AddSectionType(Section_Type sectionType)
        {

            string name = sectionType.Name;
            try
            {
                if (string.IsNullOrEmpty(name))
                {
                    throw new Exception( "Section Type Name Can't be empty");
                }

                if (!Regex.IsMatch(name, "^[A-Za-z]+$")) {

                    throw new Exception("Section Type Name must not contain special characters");
                }

                if (name.Length > 10) {
                    throw new Exception("Section Type Name must be 10 or less characters");
                } 



                _context.Section_Types.InsertOnSubmit(sectionType);
                _context.SubmitChanges();
            }
            catch(Exception ex)
            {
                throw;
            }


            
        }
    }
}
