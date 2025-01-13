﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication4.Master
{
    public partial class Dashboard : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnGrossSalary_Click(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("~/gross-salary-calculator"));
        }

        protected void btnNetSalary_Click(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("~/net-salary-calculator"));
        }
    }
}