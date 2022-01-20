using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace task
{
    public partial class Customers_List : System.Web.UI.Page
    {
        Customers C = new Customers();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;

            Fill();
        }
        public void Fill()
        {
            Clear();
            gvCustomers.DataSource = C.GetAllCustomers();
            gvCustomers.DataBind();
        }
        public void Clear()
        {
            hdnCoustomer_ID.Value = txtName.Text = txtPhone.Text = txtAddress.Text = string.Empty;
            btnAddCustomer.InnerText = "Add User";
            btnSave.Text = "Save";
            btnCancel.Visible = false;
        }

        protected void gvCustomers_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gvCustomers.Rows[index];
            HiddenField hdn_Customer_IN = (HiddenField)row.FindControl("hdn_Customer_IN");
            hdnCoustomer_ID.Value = hdn_Customer_IN.Value; // to use on update
            C.Customer_ID = Convert.ToInt32(hdn_Customer_IN.Value);
            if (e.CommandName == "coEdit")
            {
                DataTable dt = C.GetCustomerByID(); // return selected user info 
                txtName.Text = dt.Rows[0]["Customer_Name"].ToString();
                txtPhone.Text = dt.Rows[0]["Customer_Phone"].ToString();
                txtAddress.Text = dt.Rows[0]["Customer_Address"].ToString();
                btnAddCustomer.InnerText = "Update User";
                btnSave.Text = "Update";
                btnCancel.Visible = true;
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Openbutton", "document.getElementById('btnAddCustomer').click()", true);
            }
            else
            {
                C.DeleteCustomer();
                Fill();
            }

        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtPhone.Text) || string.IsNullOrEmpty(txtAddress.Text))
            {
                return;
            }
            C.Customer_Name = txtName.Text;
            C.Customer_Phone = txtPhone.Text;
            C.Customer_Address = txtAddress.Text;
            if (string.IsNullOrEmpty(hdnCoustomer_ID.Value))
                C.AddCustomer();
            else
            {
                C.Customer_ID = Convert.ToInt32(hdnCoustomer_ID.Value);
                C.UpdateCustomer();
            }
            Fill();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Clear();
        }
    }
}