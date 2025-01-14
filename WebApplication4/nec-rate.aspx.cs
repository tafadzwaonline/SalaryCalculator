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
    public partial class nec_rate : System.Web.UI.Page
    {
        readonly LookUp lp = new LookUp("con", 1);
        protected void Page_Load(object sender, EventArgs e)
        {
            MaintainScrollPositionOnPostBack = true;
            if (!IsPostBack)
            {
                getRate();
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
            if (string.IsNullOrWhiteSpace(txtRate.Text))
            {
                lblError.Text = "rate is required";
                return;
            }

            if (!double.TryParse(txtRate.Text, out rate))
            {
                lblError.Text = "rate must be a valid number.";
                return;
            }

            if (double.Parse(txtRate.Text) <= 0)
            {
                lblError.Text = "rate cannot be less or equal to zero";
                return;
            }



            ///save to database
            lp.SaveNecRate(Convert.ToDateTime(txtEffective.Text), Convert.ToDouble(txtRate.Text));
            getRate();
            lblSuccess.Text = "rate successfully saved";
            Clear();
        }


        private void getRate()
        {
            DataSet rates = lp.getNecRate();
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
            txtRate.Text = string.Empty;
            txtEffective.Text = string.Empty;
            lblError.Text = string.Empty;
        }
    }
}