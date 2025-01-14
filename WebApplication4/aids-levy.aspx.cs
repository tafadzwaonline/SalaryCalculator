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
    public partial class aids_levy : System.Web.UI.Page
    {
        readonly LookUp lp = new LookUp("con", 1);
        protected void Page_Load(object sender, EventArgs e)
        {
            MaintainScrollPositionOnPostBack = true;
            if (!IsPostBack)
            {
                getLevy();
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
            if (string.IsNullOrWhiteSpace(txtLevy.Text))
            {
                lblError.Text = "rate is required";
                return;
            }

            if (!double.TryParse(txtLevy.Text, out rate))
            {
                lblError.Text = "rate must be a valid number.";
                return;
            }

            if (double.Parse(txtLevy.Text) <= 0)
            {
                lblError.Text = "rate cannot be less or equal to zero";
                return;
            }
            ///save to database
            lp.SaveAIDSLevy(Convert.ToDateTime(txtEffective.Text), Convert.ToDouble(txtLevy.Text));
            getLevy();
            lblSuccess.Text = "levy successfully saved";
            Clear();
        }


        private void getLevy()
        {
            DataSet rates = lp.getLevy();
            if (rates != null)
            {
                grdLevy.DataSource = rates;
                grdLevy.DataBind();
            }
            else
            {
                grdLevy.DataSource = null;
                grdLevy.DataBind();
            }
        }
        private void Clear()
        {
            txtLevy.Text = string.Empty;
            txtEffective.Text = string.Empty;
            lblError.Text = string.Empty;
        }
    }
}