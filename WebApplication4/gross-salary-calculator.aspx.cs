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
    public partial class gross_salary_calculator : System.Web.UI.Page
    {
        readonly LookUp lp = new LookUp("con", 1);
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.MaintainScrollPositionOnPostBack = true;
            if (!IsPostBack)
            {
                try
                {

                    if (lp.getCurrency() != null)
                    {
                        ListItem li = new ListItem("Select a currency", "0");
                        drpCurrency.DataSource = lp.getCurrency();
                        drpCurrency.DataValueField = "CODE";
                        drpCurrency.DataTextField = "Name";
                        drpCurrency.DataBind();
                        drpCurrency.Items.Insert(0, li);
                    }
                    else
                    {
                        ListItem li = new ListItem("There are no currencies", "0");
                        drpCurrency.DataSource = null;
                        drpCurrency.DataBind();
                        drpCurrency.Items.Insert(0, li);
                    }
                    if (lp.getCurrency() != null)
                    {
                        ListItem li = new ListItem("Select a currency", "0");
                        dropdownCurrency.DataSource = lp.getCurrency();
                        dropdownCurrency.DataValueField = "CODE";
                        dropdownCurrency.DataTextField = "Name";
                        dropdownCurrency.DataBind();
                        dropdownCurrency.Items.Insert(0, li);
                    }
                    else
                    {
                        ListItem li = new ListItem("There are no currencies", "0");
                        dropdownCurrency.DataSource = null;
                        dropdownCurrency.DataBind();
                        dropdownCurrency.Items.Insert(0, li);
                    }

                }
                catch (Exception ex)
                {

                }
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txtnetsalary.Text))
            {
                lblError.Text = "Please enter net salary";
                return;
            }

            double NetSalary = Math.Round(double.Parse(txtnetsalary.Text), 2);
            double NassaLimit = (dropdownCurrency.SelectedValue == "USD") ? 31.5 : (31.5 * 26.0579);
            double MemberContributionRate = CheckContributions.Checked ? 0.05 : 0;
            double NassaContributionRate = CheckNassa.Checked ? 0.045 : 0;
            double NecContributionRate = CheckNec.Checked ? 0.02 : 0;
            double AidsLevyRate = CheckIsAidsLevy.Checked ? 0.03 : 0;

            // Estimate Gross Salary
            double EstimatedGrossSalary = NetSalary;
            double TotalDeductions = 0, FinalTax = 0;

            do
            {
                EstimatedGrossSalary += 0.01; // Increment gross salary
                double NassaPension = Math.Min(Math.Round(EstimatedGrossSalary * NassaContributionRate, 2), NassaLimit);
                double PensionFund = Math.Round(EstimatedGrossSalary * MemberContributionRate, 2);
                double Nec = Math.Round((EstimatedGrossSalary * NecContributionRate) / 2, 2);

                TotalDeductions = NassaPension + PensionFund + Nec;
                double TotalTaxableAmount = EstimatedGrossSalary - TotalDeductions;

                // Fetch tax data from the database
                double CummulativeBalance = 0, BandRate = 0;
                DataSet taxtable = lp.TaxTables(TotalTaxableAmount, dropdownCurrency.SelectedValue, DateTime.Now);
                if (taxtable != null)
                {
                    DataRow dt = taxtable.Tables[0].Rows[0];
                    CummulativeBalance = Math.Round(double.Parse(dt["Cumulative"].ToString()), 2);
                    BandRate = Math.Round(double.Parse(dt["BandRate"].ToString()) / 100, 2);
                }

                double PayeeTax = Math.Round(TotalTaxableAmount * BandRate - CummulativeBalance, 2);
                double AidsLevy = Math.Round(PayeeTax * AidsLevyRate, 2);

                FinalTax = Math.Round(PayeeTax + TotalDeductions + AidsLevy, 2);
            }
            while (Math.Round(EstimatedGrossSalary - FinalTax, 2) < NetSalary);

            // Display results
            txtgrosssalary.Text = EstimatedGrossSalary.ToString("F2");
            txtTotalTax.Text = FinalTax.ToString("F2");
            lblError.Text = string.Empty;

        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtgrosssalary.Text = string.Empty;
            txtmedicalaid.Text = string.Empty;
            txtmedicalaid.Text = string.Empty;
            lblError.Text = string.Empty;

            txtnetsalary.Text = string.Empty;
            txtTotalTax.Text = string.Empty;
            CheckContributions.Checked = false;
            chkIsActive.Checked = false;
            ma.Visible = false;
            CheckNec.Checked = false;
            lblgrosssalary.Text = "0";
            lblPaye.Text = "0";
            lblMedicalAid.Text = "0";
            lblNec.Text = "0";
            lblNassa.Text = "0";
            lblAidsLevy.Text = "0";
            lblPension.Text = "0";
            lblTotalGross.Text = "0";
            lblTotalDeductions.Text = "0";
            drpCurrency.SelectedValue = "0";
            dropdownCurrency.SelectedValue = "0";

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
                txtmedicalaid.Text = string.Empty;
            }
        }

        protected void drpCurrency_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet tax = lp.getTax(drpCurrency.SelectedValue);
            if (drpCurrency.SelectedValue == "0")
            {
                lblError2.Text = "currency is required";
                grdTax.DataSource = null;
                grdTax.DataBind();
                return;
            }

            lblError2.Text = string.Empty;


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


    }

}