using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace task
{
    /// <summary>
    /// Summary description for WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
     [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod(EnableSession = true)]
        public List<Orders_Object> GetItems()
        {
            Orders_Object O = new Orders_Object();
            List<Orders_Object> Items_List = new List<Orders_Object>();
            DataTable dt = O.Get_All_Items();

            foreach (DataRow row in dt.Rows)
            {
                Items_List.Add(new Orders_Object
                {
                    Item_ID = Convert.ToInt32( row["Item_ID"].ToString()),
                    Item_Name = row["Item_Name"].ToString(),
                    Item_Description = row["Item_Description"].ToString(),
                    Item_Price = decimal.Parse(row["Item_Price"].ToString()),
                });
            }

            return Items_List;
        }
    }
}
