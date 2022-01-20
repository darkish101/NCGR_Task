using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace task
{
    public partial class Items : System.Web.UI.Page
    {
        Orders_Object O = new Orders_Object();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;

            Fill();
        }
        public void Fill()
        {
            Clear();
            gvItems.DataSource = O.Get_All_Items();
            gvItems.DataBind();
        }
        public void Clear()
        {
            hdnCoustomer_ID.Value = txtName.Text = txtDescription1.InnerText = txtPrice.Text = string.Empty;
            btnAddItem.InnerText = "Add Item";
            btnSave.Text = "Save";
            btnCancel.Visible = false;
        }

        protected void gvItems_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gvItems.Rows[index];
            HiddenField hdn_Item_IN = (HiddenField)row.FindControl("hdn_Item_IN");
            hdnCoustomer_ID.Value = hdn_Item_IN.Value; // to use on update
            O.Item_ID = Convert.ToInt32(hdn_Item_IN.Value);
            if (e.CommandName == "coEdit")
            {
                DataTable dt = O.Get_Item_ByID(); // return selected user info 

                txtName.Text = dt.Rows[0]["Item_Name"].ToString();
                txtDescription1.InnerText = dt.Rows[0]["Item_Description"].ToString();
                txtPrice.Text = dt.Rows[0]["Item_Price"].ToString();
                btnAddItem.InnerText = "Update Item";
                btnSave.Text = "Update";
                btnCancel.Visible = true;
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Openbutton", "document.getElementById('btnAddItem').click()", true);
            }
            else
            {
                O.DeleteItem();
                Fill();
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtDescription1.InnerText) || string.IsNullOrEmpty(txtPrice.Text))
            {
                return;
            }
            O.Item_Name = txtName.Text;
            O.Item_Description = txtDescription1.InnerText;
            O.Item_Price = decimal.Parse(txtPrice.Text);
            if (string.IsNullOrEmpty(hdnCoustomer_ID.Value))
                O.AddItem();
            //else
            //{
            //    O.Item_ID = Convert.ToInt32(hdnCoustomer_ID.Value);
            //    O.UpdateItem();
            //}
            Fill();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Clear();
        }
    }
}