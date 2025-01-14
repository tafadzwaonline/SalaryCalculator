using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication4.Classes;

namespace WebApplication4
{
    public partial class tax_tables : System.Web.UI.Page
    {
        readonly LookUp lp = new LookUp("con", 1);
        protected void Page_Load(object sender, EventArgs e)
        {
            MaintainScrollPositionOnPostBack = true;
            if (!IsPostBack)
            {
                getSettings();
                //getRate();
            }
        }

        private void getSettings()
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
              

            }
            catch (Exception ex)
            {

            }
        }



        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            double rate;
            List<string> errorMessages = new List<string>();
            if (string.IsNullOrWhiteSpace(txtEffective.Text))
            {
                //lblError.Text = "effective date is required";
                //return;
                errorMessages.Add("effective date is required");
            }
            if (string.IsNullOrWhiteSpace(txtRate.Text))
            {
                //lblError.Text = "rate is required";
                //return;
                errorMessages.Add("rate is required");
            }

            if (!double.TryParse(txtRate.Text, out rate))
            {
                //lblError.Text = "rate must be a valid number.";
                //return;
                errorMessages.Add("rate must be a valid number.");
            }

         
            if (drpCurrency.SelectedValue == "0")
            {
                //lblError.Text = "rate is required";
                //return;
                errorMessages.Add("currency is required");
            }

            if (string.IsNullOrWhiteSpace(txtStartRate.Text))
            {
                //lblError.Text = "rate is required";
                //return;
                errorMessages.Add("start rate is required");
            }
            if (string.IsNullOrWhiteSpace(txtEndRate.Text))
            {
                //lblError.Text = "rate is required";
                //return;
                errorMessages.Add("end rate is required");
            }
            if (string.IsNullOrWhiteSpace(txtCummulative.Text))
            {
                //lblError.Text = "rate is required";
                //return;
                errorMessages.Add("cummulative is required");
            }

           


            if (errorMessages.Count > 0)
            {
                //AmberAlert();
                lblError.Text = $"{string.Join("<br>", errorMessages)}";
                return;
            }



            ///save to database
            lp.SaveTaxTables(drpCurrency.SelectedValue,Convert.ToDateTime(txtEffective.Text), Convert.ToDouble(txtRate.Text), Convert.ToDouble(txtStartRate.Text), Convert.ToDouble(txtEndRate.Text), Convert.ToDouble(txtCummulative.Text));
            //getRate();
            lblSuccess.Text = "tax rates successfully saved";
            Clear();
            drpCurrency.SelectedValue = "0";
        }


      
        private void Clear()
        {
            txtRate.Text = string.Empty;
            txtEffective.Text = string.Empty;
            lblError.Text = string.Empty;
        }
        protected void getSavedTaxBands()
        {
            try
            {
               
                if (lp.GetBands(drpCurrency.SelectedValue) != null)
                {
                    grdTax.DataSource = lp.GetBands(drpCurrency.SelectedValue);
                    grdTax.DataBind();
                 
                }
                else
                {
                    grdTax.DataSource = null;
                    grdTax.DataBind();
                 
                }

            }
            catch (Exception ex)
            {
                lblError.Text = "An error occured while retrieving data";
            }
        }

        protected void drpCurrency_SelectedIndexChanged(object sender, EventArgs e)
        {
            getSavedTaxBands();
        }
    }
}