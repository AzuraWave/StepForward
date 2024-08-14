using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc.Html;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Serialization;
using System.Xml.Linq;
using StepForward.Models;
using System.Web.Mvc;

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

                var list = _context.Section_Types.ToList();
                return list;
       
        }

        public SectionTypeViewModel GetSectionTypeViewModel(string errorMessage = "")
        {
                return new SectionTypeViewModel
                {
                    NewSectionType = new Section_Type(),
                    SectionTypes = GetAllSectionTypes(),
                    IsSuccess = string.IsNullOrEmpty(errorMessage),
                    Message = errorMessage
                };
   
        }


        public bool NameValidation(string name, out string errorMessage)
        {
            errorMessage = null;
            if (string.IsNullOrEmpty(name))
            {
                errorMessage = "Section Type Name can't be empty.";
                return false;
               
            }

            if (!Regex.IsMatch(name, "^[A-Za-z]+$"))
            {
                 
                errorMessage = "Section Type Name must not contain special characters.";
                return false;
            }

            if (name.Length > 10)
            {
                errorMessage = "Section Type Name must be 10 or fewer characters.";
                return false; 
            }
            return true ;
        }

        public bool AddSectionType(Section_Type sectionType, out string errorMessage)
        {
            errorMessage = null;
            bool IsSuccessful = false;
            try
            {
                 IsSuccessful = NameValidation(sectionType.Name, out errorMessage);
                if (!IsSuccessful)
                {
                    return false;
                }

                _context.Section_Types.InsertOnSubmit(sectionType);
                _context.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }



        }

        public bool DeleteSectionTypes(int[] selectedItems, out string errorMessage)
        {
            errorMessage = null;
            try
            {
                if (selectedItems == null || !selectedItems.Any())
                {
                    errorMessage = "No Section Types Selected";
                    return false;
                }

                var sectionTypes = _context.Section_Types.Where(x => selectedItems.Contains(x.Id)).ToList();
                _context.Section_Types.DeleteAllOnSubmit(sectionTypes);
                _context.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }

        public Section_Type GetSection_Type(int id)
        {
            var SectionType = _context.Section_Types.SingleOrDefault(st => st.Id == id);
            return SectionType ;

        }

        public bool EditSectionType(Section_Type sectionType, out string errorMessage)
        {
            errorMessage = null;
            
                try
            {
                Section_Type original = _context.Section_Types.SingleOrDefault(st => st.Id == sectionType.Id);

                bool IsSuccessful = NameValidation(sectionType.Name, out errorMessage);
                if (!IsSuccessful)
                {
                    return false;
                }

                original.Name = sectionType.Name;
                _context.SubmitChanges();

            }
            catch(Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
            
            return true;
        }



    }
}
