using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace task
{
    public class Customers
    {
        public int Customer_ID { get; set; }
        public string Customer_Name { get; set; }
        public string Customer_Phone { get; set; }
        public string Customer_Address { get; set; }
        public DateTime Created_On { get; set; }
        public char is_Deleted { get; set; }

        public DataTable GetAllCustomers()
        {
            DataConnectivity _db = new DataConnectivity();
            string query = @"SELECT * FROM Customer_Table WHERE is_Deleted = 0";
            SqlCommand cmd = new SqlCommand(query);
            return _db.ExecuteSql(cmd).Tables[0];
        } 
        public DataTable GetCustomerByID()
        {
            DataConnectivity _db = new DataConnectivity();
            string query = @"SELECT * FROM Customer_Table WHERE Customer_ID = @Customer_ID";
            SqlCommand cmd = new SqlCommand(query);
            cmd.Parameters.Add("@Customer_ID", SqlDbType.Int).Value = Customer_ID;
            return _db.ExecuteSql(cmd).Tables[0];
        }
        public void AddCustomer()
        {
            DataConnectivity _db = new DataConnectivity();
            string query = @"INSERT INTO Customer_Table (Customer_Name, Customer_Phone, Customer_Address) 
                                                VALUES (@Customer_Name, @Customer_Phone, @Customer_Address)";
            SqlCommand cmd = new SqlCommand(query);
            cmd.Parameters.Add("@Customer_Name", SqlDbType.NVarChar).Value = Customer_Name;
            cmd.Parameters.Add("@Customer_Phone", SqlDbType.NVarChar).Value = Customer_Phone;
            cmd.Parameters.Add("@Customer_Address", SqlDbType.NVarChar).Value = Customer_Address;
            _db.ExecuteSql(cmd);
        } 
        public void UpdateCustomer()
        {
            DataConnectivity _db = new DataConnectivity();
            string query = @"UPDATE Customer_Table SET 
                                    Customer_Name = @Customer_Name
                                  , Customer_Phone = @Customer_Phone
                                  , Customer_Address = @Customer_Address 
                                WHERE Customer_ID = @Customer_ID ";
            SqlCommand cmd = new SqlCommand(query);
            cmd.Parameters.Add("@Customer_ID", SqlDbType.Int).Value = Customer_ID;
            cmd.Parameters.Add("@Customer_Name", SqlDbType.NVarChar).Value = Customer_Name;
            cmd.Parameters.Add("@Customer_Phone", SqlDbType.NVarChar).Value = Customer_Phone;
            cmd.Parameters.Add("@Customer_Address", SqlDbType.NVarChar).Value = Customer_Address;
            _db.ExecuteSql(cmd);
        }
        public void DeleteCustomer()
        {
            DataConnectivity _db = new DataConnectivity();
            string query = @"UPDATE Customer_Table SET 
                                    is_Deleted = 1
                                WHERE Customer_ID = @Customer_ID ";
            SqlCommand cmd = new SqlCommand(query);
            cmd.Parameters.Add("@Customer_ID", SqlDbType.Int).Value = Customer_ID;
            _db.ExecuteSql(cmd);
        }
    }
}