using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication4.Classes;

namespace WebApplication4
{
    public partial class nassa_limit : System.Web.UI.Page
    {
        readonly LookUp lp = new LookUp("con", 1);
        protected void Page_Load(object sender, EventArgs e)
        {
            MaintainScrollPositionOnPostBack = true;
            if (!IsPostBack)
            {
                getLimit();
            }
        }



        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            double rate;
            if (string.IsNullOrWhiteSpace(txtEffective.Text))
            {
                lblError.Text = "effective date is required";
                return;
            }
            if (string.IsNullOrWhiteSpace(txtLimit.Text))
            {
                lblError.Text = "limit is required";
                return;
            }

            if (!double.TryParse(txtLimit.Text, out rate))
            {
                lblError.Text = "limit must be a valid number.";
                return;
            }

            if (double.Parse(txtLimit.Text) <= 0)
            {
                lblError.Text = "limit cannot be less or equal to zero";
                return;
            }



            ///save to database
            lp.SaveLimit(Convert.ToDateTime(txtEffective.Text), Convert.ToDouble(txtLimit.Text));
            getLimit();
            lblSuccess.Text = "limit successfully saved";
            Clear();
        }


        private void getLimit()
        {
            DataSet rates = lp.getLimit();
            if (rates != null)
            {
                grdLimit.DataSource = rates;
                grdLimit.DataBind();
            }
            else
            {
                grdLimit.DataSource = null;
                grdLimit.DataBind();
            }
        }
        private void Clear()
        {
            txtLimit.Text = string.Empty;
            txtEffective.Text = string.Empty;
            lblError.Text = string.Empty;
        }
    }
}