using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace task
{
    public class Orders_Object
    {
        //item table
        public int Item_ID { get; set; }
        public string Item_Name { get; set; }
        public string Item_Description { get; set; }
        public decimal Item_Price { get; set; }

        //order tabel
        public int Order_ID { get; set; }
        public int Item_Amount { get; set; }

        //inoice table
        public int Invoice_ID { get; set; }
        public int Customer_ID { get; set; }

        // common colums
        public DateTime Created_On { get; set; }
        public char is_Deleted { get; set; }
        #region Items
        public void AddItem()
        {
            DataConnectivity _db = new DataConnectivity();
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Add("@p_Item_Name", SqlDbType.NVarChar).Value = Item_Name;
            cmd.Parameters.Add("@p_Item_Description", SqlDbType.NVarChar).Value = Item_Description;
            cmd.Parameters.Add("@p_Item_Price", SqlDbType.Decimal).Value = Item_Price;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[dbo].[sp_Add_Items]";
            _db.ExecuteSP(cmd);
        }

        public DataTable Get_All_Items()
        {
            DataConnectivity _db = new DataConnectivity();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "sp_Get_From_Item_Table";
            return _db.ExecuteSP(cmd).Tables[0];
        }
        public DataTable Get_Item_ByID()
        {
            DataConnectivity _db = new DataConnectivity();
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Add("@p_Item_ID", SqlDbType.Int).Value = Item_ID;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "sp_Get_From_Item_Table";
            return _db.ExecuteSP(cmd).Tables[0];
        }
         public void DeleteItem()
        {
            DataConnectivity _db = new DataConnectivity();
            string query = @"UPDATE Items_Table SET 
                                    is_Deleted = 1
                                WHERE Item_ID = @Item_ID ";
            SqlCommand cmd = new SqlCommand(query);
            cmd.Parameters.Add("@Item_ID", SqlDbType.Int).Value = Item_ID;
            _db.ExecuteSql(cmd);
        }



        #endregion

        #region order
        public int CreateInvoice()
        {
            DataConnectivity _db = new DataConnectivity();
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Add("@p_Item_ID", SqlDbType.Int).Value = Item_ID;
            cmd.Parameters.Add("@p_Amount", SqlDbType.Int).Value = Item_Amount;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "CreateInvoice";
            return Convert.ToInt32(_db.ExecuteSP(cmd, CommandType.StoredProcedure).Tables[0].Rows[0][0]);
        }
         public void Invoice_Add_Item()
        {
            DataConnectivity _db = new DataConnectivity();
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Add("@p_Invoice_ID", SqlDbType.Int).Value = Invoice_ID;
            cmd.Parameters.Add("@p_Item_ID", SqlDbType.Int).Value = Item_ID;
            cmd.Parameters.Add("@p_Amount", SqlDbType.Decimal).Value = Item_Amount;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "sp_Invoice_Add_Item";
            _db.ExecuteSP(cmd);
        }



        public DataTable GetOrderByID()
        {
            DataConnectivity _db = new DataConnectivity();
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Add("@p_Invoice_ID", SqlDbType.Int).Value = Invoice_ID;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "sp_GetOrder";
            return _db.ExecuteSP(cmd).Tables[0];
        }

        public DataTable Get_Order_Print()
        {
            DataConnectivity _db = new DataConnectivity();
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Add("@p_Invoice_ID", SqlDbType.Int).Value = Invoice_ID;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "sp_Get_Order_By_ID";
            return _db.ExecuteSP(cmd).Tables[0];
        }

        public void UpdateOrder()
        {
            DataConnectivity _db = new DataConnectivity();

            SqlCommand cmd = new SqlCommand("UPDATE Invoice_Table SET Customer_ID = @Customer_ID WHERE Invoice_ID = @Invoice_ID");
            cmd.Parameters.Add("@Invoice_ID", SqlDbType.Int).Value = Invoice_ID;
            cmd.Parameters.Add("@Customer_ID", SqlDbType.Int).Value = Customer_ID;
           
            _db.ExecuteSql(cmd);
        }
        public DataTable GetAll_Orders()
        {
            DataConnectivity _db = new DataConnectivity();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "sp_Get_All_Orders";
            return _db.ExecuteSP(cmd).Tables[0];
        }
        #endregion

    }
}