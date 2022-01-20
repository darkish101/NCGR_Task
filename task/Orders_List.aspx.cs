using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace task
{
    public partial class Orders_List : System.Web.UI.Page
    {
        Orders_Object O = new Orders_Object();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            gvOrders.DataSource = O.GetAll_Orders();
            gvOrders.DataBind();

        }
    }
}