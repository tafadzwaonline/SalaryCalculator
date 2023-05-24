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
	public partial class salary_calculator : System.Web.UI.Page
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
            LookUp lp = new LookUp("con", 1);
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
            if (txtAllowance.Text == "")
            {
                txtAllowance.Text = "0";
            }

            double MemberContributionRate = 0;
            double NassaContributionRate = 0;
            double NecContributionRate = 0;
            double PayeeTax = 0;
            double BandRate = 0;
            double AidsLevyRate = 0;
            double AidsLevy = 0;
            double FinalTax = 0;
            double CummulativeBalance = 0;
            double Allowances = double.Parse(txtAllowance.Text);
            double GrossSalary = double.Parse(txtgrosssalary.Text);
            double TotalTax = 0;
            double NetSalary = 0;
            double TotalTaxableAmount = 0;

            if (CheckIsAidsLevy.Checked)
            {
                AidsLevyRate = .03;
            }
            if (CheckContributions.Checked)
            {
                MemberContributionRate = .05;
            }
            if (CheckNassa.Checked)
            {
                NassaContributionRate = .045;
            }
            if (CheckNec.Checked)
            {
                NecContributionRate = .02;
            }

            double NassaPension = Math.Round(GrossSalary * NassaContributionRate,2);
            double PensionFund =Math.Round(GrossSalary * MemberContributionRate,2);
            double Nec = Math.Round((GrossSalary * NecContributionRate)/2,2);
            double MedicalAid = Math.Round(double.Parse(txtmedicalaid.Text), 2);

            TotalTax = NassaPension + PensionFund + Nec + MedicalAid;
            GrossSalary = GrossSalary - TotalTax;
            TotalTaxableAmount = GrossSalary + Allowances;
            DataSet taxtable = lp.TaxTables(TotalTaxableAmount, dropdownCurrency.SelectedItem.Text, DateTime.Now);
            if (taxtable != null)
            {
                foreach (DataRow dt in taxtable.Tables[0].Rows)
                {
                    CummulativeBalance = Math.Round(double.Parse(dt["Cumulative"].ToString()), 2);
                    BandRate = Math.Round(double.Parse(dt["BandRate"].ToString()) / 100, 2);
                }
            }


            PayeeTax = (TotalTaxableAmount * BandRate) - CummulativeBalance;

            AidsLevy = PayeeTax * AidsLevyRate;

            FinalTax = Math.Round(PayeeTax + TotalTax + AidsLevy,2);
            txtTotalTax.Text = FinalTax.ToString();

            NetSalary = Math.Round(TotalTaxableAmount - PayeeTax,2);
            txtnetsalary.Text = NetSalary.ToString();

        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtgrosssalary.Text = "";
            txtmedicalaid.Text = "";
            txtmedicalaid.Text = "";
            lblError.Text = "";
            txtnetsalary.Text = "";
            txtTotalTax.Text = "";
            txtAllowance.Text = "";
            chkIsActive.Checked = false;
            CheckIsAidsLevy.Checked = false;
            CheckContributions.Checked = false;
            CheckAllowance.Checked = false;
            CheckNassa.Checked = false;
            ma.Visible = false;
            all.Visible = false;
            CheckNec.Checked = false;

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

            if (tax != null)
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

        protected void CheckAllowance_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckAllowance.Checked)
            {
                all.Visible = true;
            }
            else
            {
                all.Visible = false;
                txtAllowance.Text = "0";
            }
        }
    }

}