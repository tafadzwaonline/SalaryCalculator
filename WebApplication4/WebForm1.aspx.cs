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
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.MaintainScrollPositionOnPostBack = true;
            if (!IsPostBack)
            {
                
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            LookUp lp = new LookUp("con",1);
            if (dropdownCurrency.SelectedItem.Text == "Select Currency")
            {
                lblError.Text = "Please select currency";
                return;
            }
            lblError.Text = "";

            if (txtmedicalaid.Text == "")
            {
                txtmedicalaid.Text = "0";
            }

            double membercontributionrate = 0;
            double nassacontributionrate = 0;
            double neccontributionrate = 0;
            double MedicalAid = Math.Round(double.Parse(txtmedicalaid.Text), 2);
            double payeetax = 0;
            double BandRate = 0;
            double AidsLevy = 0;
            double FinalTax = 0;
            double cummulativebalance = 0;
            double GrossSalary = double.Parse(txtgrosssalary.Text);
            double TotalTax = 0;
            double NetSalary = 0;
          
            //TotalTax = (GrossSalary * tax) - cummulativebalance;
            //txtTotalTax.Text = TotalTax.ToString();
            if (CheckIsAidsLevy.Checked)
            {
                AidsLevy = .03;
            }
            if (CheckContributions.Checked)
            {
                membercontributionrate = .05;
            }
            if (CheckNassa.Checked)
            {
                nassacontributionrate = .045;
            }
            if (CheckNec.Checked)
            {
                neccontributionrate = .02;
            }
            TotalTax = (GrossSalary * AidsLevy) + (GrossSalary * membercontributionrate) + (GrossSalary * nassacontributionrate) + (GrossSalary * neccontributionrate)+ MedicalAid;
            GrossSalary = GrossSalary - TotalTax;
            DataSet taxtable = lp.TaxTables(GrossSalary, dropdownCurrency.SelectedItem.Text, DateTime.Now);
            if (taxtable != null)
            {
                foreach (DataRow dt in taxtable.Tables[0].Rows)
                {
                    cummulativebalance = Math.Round(double.Parse(dt["Cumulative"].ToString()), 2);
                    BandRate = Math.Round(double.Parse(dt["BandRate"].ToString()) / 100, 2);
                }
            }
            payeetax = (GrossSalary * BandRate) - cummulativebalance;
            FinalTax = payeetax + TotalTax;
            txtTotalTax.Text = FinalTax.ToString();



            NetSalary = (GrossSalary - payeetax);
            txtnetsalary.Text = NetSalary.ToString();

        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtgrosssalary.Text = "";
            txtmedicalaid.Text = "";
            chkIsActive.Checked = false;
            CheckIsAidsLevy.Checked = false;
            CheckContributions.Checked = false;
            ma.Visible = false;
            lblError.Text = "";
            txtnetsalary.Text = "";
            txtTotalTax.Text = "";

        }

        protected void chkIsActive_CheckedChanged(object sender, EventArgs e)
        {
            if (chkIsActive.Checked)
            {

                ma.Visible = true;
            }
            else
            {
                ma.Visible = false;
                txtmedicalaid.Text = "";
            }
        }

        protected void drpCurrency_SelectedIndexChanged(object sender, EventArgs e)
        {
            LookUp lp = new LookUp("con", 1);
            DataSet tax = lp.getTax(drpCurrency.SelectedItem.Text);

            if (tax!=null)
            {
                grdTax.DataSource = tax;
                grdTax.DataBind();
            }
            else
            {
                grdTax.DataSource = null;
                grdTax.DataBind();
            }

        }
    }
}