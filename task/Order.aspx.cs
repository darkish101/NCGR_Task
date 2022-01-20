using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Services;
using System.Web.UI.WebControls;

namespace task
{
    public partial class Order : System.Web.UI.Page
    {
            Orders_Object O = new Orders_Object();
        Customers C = new Customers();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;


            //List<Orders_Object> Items_List = new List<Orders_Object>();
            //Items_List.Add(new Orders_Object());
            rpItems.DataSource = O.Get_All_Items();
            rpItems.DataBind();
            ddlCustomers.DataSource = C.GetAllCustomers();
            ddlCustomers.DataTextField = "Customer_Name";
            ddlCustomers.DataValueField = "Customer_ID";
            ddlCustomers.DataBind();
        }


        protected void rpItems_ItemCommand(object source, System.Web.UI.WebControls.RepeaterCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.Item.ItemIndex);
            RepeaterItem item = rpItems.Items[index];
            HiddenField hdnItemID = (HiddenField)item.FindControl("hdnItemID");
            O.Item_ID = Convert.ToInt32(hdnItemID.Value);
            O.Item_Amount = 1;

            if (string.IsNullOrEmpty(hdnOrderID.Value))
                hdnOrderID.Value = O.CreateInvoice().ToString();//create invoice and set the hidden field to it to be used on the same order
            else
            {
                O.Invoice_ID = Convert.ToInt32(hdnOrderID.Value);
                O.Invoice_Add_Item();
            }
            FillOrder();
        }
        public void FillOrder()
        {
            dvHasOrderd.Visible = true;
            O.Invoice_ID = Convert.ToInt32(hdnOrderID.Value);
            rpOrder.DataSource = O.GetOrderByID();
            rpOrder.DataBind();
        }

        protected void rpOrder_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
//todo: add delete minus item from order
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            O.Invoice_ID = Convert.ToInt32(hdnOrderID.Value);
            O.Customer_ID = Convert.ToInt32(ddlCustomers.SelectedValue);
            O.UpdateOrder();
            Response.Redirect("PrintOrder?Invoice_ID=" + hdnOrderID.Value);
        }



        //[WebMethod(EnableSession = true)]
        //public static List<Orders_Object> GetItems()
        //{
        //    List<Orders_Object> Items_List = new List<Orders_Object>();
        //    DataTable dt = O.Get_All_Items();

        //    foreach (DataRow row in dt.Rows)
        //    {
        //        Items_List.Add(new Orders_Object
        //        {
        //            Item_ID = Convert.ToInt32(row["Item_ID"].ToString()),
        //            Item_Name = row["Item_Name"].ToString(),
        //            Item_Description = row["Item_Description"].ToString(),
        //            Item_Price = decimal.Parse(row["Item_Price"].ToString()),
        //        });
        //    }

        //    return Items_List;
        //}
    }
}
        
