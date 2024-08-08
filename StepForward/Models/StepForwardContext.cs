using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Linq;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace StepForward.Models
{
    public class StepForwardContext : DataContext
    {
        public StepForwardContext() : base(ConfigurationManager.ConnectionStrings["StepForwardConnectionString"].ConnectionString)
        {
        }

        public Table<Section_Type> Section_Types;
    }
}