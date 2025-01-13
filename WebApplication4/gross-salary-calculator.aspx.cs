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
            double gSalary;
            if (string.IsNullOrWhiteSpace(txtnetsalary.Text))
            {
                lblError.Text = "Please enter net salary";
                return;
            }

            if (!double.TryParse(txtnetsalary.Text, out gSalary))
            {
                lblError.Text = "net salary must be a valid number.";
                return;
            }

            if (double.Parse(txtnetsalary.Text) <= 0)
            {
                lblError.Text = "net salary cannot be less or equal to zero";
                return;
            }

            double NetSalary = Math.Round(double.Parse(txtnetsalary.Text), 2);
            double NassaLimit = (dropdownCurrency.SelectedValue == "USD") ? 31.5 : (31.5 * 26.0579);
            double MemberContributionRate = CheckContributions.Checked ? 0.05 : 0;
            double NassaContributionRate = CheckNassa.Checked ? 0.045 : 0;
            double NecContributionRate = CheckNec.Checked ? 0.02 : 0;
            double AidsLevyRate = CheckIsAidsLevy.Checked ? 0.03 : 0;

            //// Validate Medical Aid input
            //if (string.IsNullOrWhiteSpace(txtmedicalaid.Text))
            //{
            //    txtmedicalaid.Text = "0";
            //}
            //double MedicalAid = chkIsActive.Checked ?  Math.Round(Convert.ToDouble(txtmedicalaid.Text), 2) : 0;

            double low = NetSalary; // Minimum possible gross salary
            double high = NetSalary * 2; // Arbitrarily set an upper limit
            double EstimatedGrossSalary = 0;
            double TotalDeductions = 0, FinalTax = 0;

            while (high - low > 0.01) // Precision up to 0.01
            {
                EstimatedGrossSalary = (low + high) / 2; // Midpoint of the range
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

                FinalTax = Math.Round(PayeeTax + AidsLevy, 2); // Removed TotalDeductions here
                double calculatedNetSalary = Math.Round(EstimatedGrossSalary - (FinalTax + TotalDeductions), 2);
             
                // Adjust the search range based on the calculated net salary
                if (calculatedNetSalary < NetSalary)
                {
                    low = EstimatedGrossSalary; // Increase the lower bound
                }
                else
                {
                    high = EstimatedGrossSalary; // Decrease the upper bound
                }
            }
           
            // Final estimated gross salary
            EstimatedGrossSalary = Math.Round(EstimatedGrossSalary, 2);

            // Display results
            txtgrosssalary.Text = Math.Round(EstimatedGrossSalary, 0, MidpointRounding.AwayFromZero).ToString();
            lblEstimatedGross.Text = txtgrosssalary.Text;
            lblNetsalary.Text = txtnetsalary.Text;
            lblError.Text = string.Empty;

            lblGrossCode.Text = dropdownCurrency.SelectedValue;
            lblNetCode.Text = lblGrossCode.Text;
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtgrosssalary.Text = string.Empty;
            lblError.Text = string.Empty;
            lblNetCode.Text= string.Empty;
            lblGrossCode.Text=string.Empty;
            txtnetsalary.Text = string.Empty;
            CheckContributions.Checked = false;
            CheckNec.Checked = false;
            lblEstimatedGross.Text = "0";
            lblNetsalary.Text = "0";
            drpCurrency.SelectedValue = "0";
            dropdownCurrency.SelectedValue = "0";

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

        protected void btnNetSalary_Click(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("~/net-salary-calculator"));
        }
    }

}