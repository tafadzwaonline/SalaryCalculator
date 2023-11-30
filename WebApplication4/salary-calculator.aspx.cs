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
        readonly LookUp lp = new LookUp("con", 1);
        protected void Page_Load(object sender, EventArgs e)
		{
            Page.MaintainScrollPositionOnPostBack = true;
            if (!IsPostBack)
            {

            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            
            if (dropdownCurrency.SelectedItem.Text == "Select Currency")
            {
                lblError.Text = "Please select currency";
                return;
            }
            if (txtgrosssalary.Text == "")
            {
                lblError.Text = "Please enter basic salary";
                return;
            }

            lblError.Text = string.Empty;

            if (txtmedicalaid.Text == "")
            {
                txtmedicalaid.Text = "0";
            }

            

            double MemberContributionRate = 0;
            double NassaContributionRate = 0;
            double NecContributionRate = 0;
            double BandRate = 0;
            double AidsLevyRate = 0;
            double CummulativeBalance = 0;
            double GrossSalary = Math.Round(double.Parse(txtgrosssalary.Text),2);
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
            double TotalAdditions = Math.Round(GrossSalary, 2);
            double NassaPension = Math.Round(GrossSalary * NassaContributionRate,2);
            double PensionFund =Math.Round(GrossSalary * MemberContributionRate,2);
            double Nec = Math.Round((GrossSalary * NecContributionRate)/2,2);
            double MedicalAid = Math.Round(double.Parse(txtmedicalaid.Text), 2);

            double TotalTax = NassaPension + PensionFund + Nec + MedicalAid;
            GrossSalary -= (NassaPension + PensionFund + Nec);
            double TotalTaxableAmount = GrossSalary;
            DataSet taxtable = lp.TaxTables(TotalTaxableAmount, dropdownCurrency.SelectedItem.Text, DateTime.Now);
            if (taxtable != null)
            {
                foreach (DataRow dt in taxtable.Tables[0].Rows)
                {
                    CummulativeBalance = Math.Round(double.Parse(dt["Cumulative"].ToString()), 2);
                    BandRate = Math.Round(double.Parse(dt["BandRate"].ToString()) / 100, 2);
                }
            }


            double PayeeTax = Math.Round(TotalTaxableAmount * BandRate - CummulativeBalance, 2);
            PayeeTax = Math.Round(PayeeTax - (.5 * MedicalAid),2);
            double AidsLevy = Math.Round(PayeeTax * AidsLevyRate, 2);

            double FinalTax = Math.Round(PayeeTax + TotalTax + AidsLevy, 2);
            txtTotalTax.Text = FinalTax.ToString();

            double NetSalary = Math.Round(TotalAdditions - FinalTax, 2);
            txtnetsalary.Text = NetSalary.ToString();



            lblgrosssalary.Text = txtgrosssalary.Text;
            lblPaye.Text = PayeeTax.ToString();
            lblMedicalAid.Text = MedicalAid.ToString();
            lblNec.Text = Nec.ToString();
            lblNassa.Text = NassaPension.ToString();
            lblAidsLevy.Text = AidsLevy.ToString();
            lblPension.Text = PensionFund.ToString();
            lblTotalGross.Text = TotalAdditions.ToString();
            lblTotalDeductions.Text = txtTotalTax.Text;

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

        
    }

}