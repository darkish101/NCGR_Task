using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace task
{
    public partial class PrintOrder : System.Web.UI.Page
    {
        Orders_Object O = new Orders_Object();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;

            if (string.IsNullOrEmpty(Request.QueryString["Invoice_ID"]))
                Response.Redirect("Default");
            O.Invoice_ID = Convert.ToInt32(Request.QueryString["Invoice_ID"]);
            DataTable dt = O.Get_Order_Print();

            lblDate.Text = dt.Rows[0]["Created_On"].ToString();
            lblAddress.Text = dt.Rows[0]["Customer_Address"].ToString();
            lblName.Text = dt.Rows[0]["Customer_Name"].ToString();
            lblMobileNum.Text = dt.Rows[0]["Customer_Phone"].ToString();
            lblInvoiceNum.Text = Request.QueryString["Invoice_ID"];

            decimal totalPrice=0;
            foreach (DataRow row in dt.Rows)
            {
                totalPrice += decimal.Parse(row["Item_Price"].ToString()) * Convert.ToInt32(row["Item_Amount"].ToString());
            }
            lbltotal.Text = totalPrice.ToString();
            //dt.Rows.Add("", "Total: " + totalPrice, "", "", "", "", "", "", "");
            gvOrder.DataSource = dt;
            gvOrder.DataBind();
        }

        protected void gvOrder_DataBinding(object sender, EventArgs e)
        {

        }
    }
}