using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FinalTest.Models
{
    public class OrderService
    {
        /// <summary>
        /// 連結資料庫
        /// </summary>
        /// <returns></returns>
        private string GetDBConnectionString()
        {
            return
                System.Configuration.ConfigurationManager.ConnectionStrings["DBConn"].ConnectionString;
        }


        /// <summary>
        /// 取得資料
        /// </summary>
        /// <returns></returns>
        public List<Models.OrderDetails> GetCodeValName()
        {
            DataTable dataTable = new DataTable();
            string sql = @"Select [CodeId] + ' ' + [CodeVal] AS CodeValName ,[CodeId] From [dbo].[CodeTable] where [dbo].[CodeTable].CodeType = 'TITLE';";
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dataTable);
                conn.Close();
            }
            return this.MapProductDataToList(dataTable);
        }
        /// <summary>
        /// 取得產品資料變成List
        /// </summary>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        private List<OrderDetails> MapProductDataToList(DataTable dataTable)
        {
            List<Models.OrderDetails> result = new List<OrderDetails>();
            foreach (DataRow row in dataTable.Rows)
            {
                result.Add(new OrderDetails
                {
                    CodeId = row["CodeId"].ToString(),
                    CodeValName = row["CodeValName"].ToString(),
                });
            }
            return result;
        }


        /// <summary>
        /// 取得搜尋條件資料
        /// </summary>
        /// <returns></returns>
        public List<Models.Order> SearchOrder(Models.Order order)
        {
            DataTable dataTable = new DataTable();
            string sql = @"Select [CustomerID], CompanyName, [CodeId] + ' ' + [CodeVal] AS CodeValName , ContactName
                            From [Sales].[Customers] join [dbo].[CodeTable] on [Sales].[Customers].ContactTitle = [dbo].[CodeTable].CodeId
                                  Where (CustomerID = @CustomerID OR @CustomerID = 0)
                                   AND (ContactName = @ContactName OR @ContactName is null)
                                    AND (CompanyName = @CompanyName OR @CompanyName is null)
                                    AND (CodeId = @CodeId OR @CodeId is null)

                     ";

            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@CustomerID", order.CustomerID));
                cmd.Parameters.Add(new SqlParameter("@CompanyName", order.CompanyName == null ? (object)DBNull.Value : order.CompanyName));
                cmd.Parameters.Add(new SqlParameter("@ContactName", order.ContactName == null ? (object)DBNull.Value : order.ContactName));
                cmd.Parameters.Add(new SqlParameter("@CodeId", order.CodeId == null ? (object)DBNull.Value : order.CodeId));
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dataTable);
                conn.Close();
            }
            return this.MapOrderDataToList(dataTable);
        }


        /// <summary>
        /// 取得訂單資料變成List
        /// </summary>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        private List<Order> MapOrderDataToList(DataTable dataTable)
        {
            List<Models.Order> result = new List<Order>();
            foreach (DataRow row in dataTable.Rows)
            {
                result.Add(new Order
                {

                    CompanyName = row["CompanyName"].ToString(),
                    CustomerID = (int)row["CustomerID"],
                    CodeValName = row["CodeValName"].ToString(),
                    ContactName = row["ContactName"].ToString()
                });
            }
            return result;
        }



        public void DeleteOrderById(string CustomerID)
        {
            try
            {
                string sql = @"DELETE
                FROM [Sales].[Customers]
                Where [CustomerID] = @CustomerID";

                using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.Add(new SqlParameter("CustomerID", CustomerID));
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }






        /// <summary>
        /// 新增訂單
        /// </summary>
        /// <param name="order"></param>
        public string InsertOrder(Models.Order order)
        {
            string sql = @"Insert INTO [Sales].[Customers]
                         (
                            CustomerID,CompanyName,ContactName,ContactTitle,CreationDate,Address,City,
                            Region,Country,Phone,Fax,CodeValName,PostalCode
                         )
                         VALUES
                         (
                            @CustomerID,@CompanyName,@ContactName,@ContactTitle,@CreationDate,@Address,@City,
                            @Region,@Country,@Phone,@Fax,@CodeValName,@PostalCode
                         )
                         Select SCOPE_IDENTITY()
                         ";

            string OrderID;
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@CustomerID", order.CustomerID));
                cmd.Parameters.Add(new SqlParameter("@CompanyName", order.CompanyName));
                cmd.Parameters.Add(new SqlParameter("@ContactName", order.ContactName == null ? (object)DBNull.Value : order.ContactName));
                cmd.Parameters.Add(new SqlParameter("@ContactTitle", order.ContactTitle));
                cmd.Parameters.Add(new SqlParameter("@CreationDate", order.CreationDate == null ? (object)DBNull.Value : order.CreationDate));
                cmd.Parameters.Add(new SqlParameter("@Address", order.Address));
                cmd.Parameters.Add(new SqlParameter("@City", order.City ?? string.Empty));
                cmd.Parameters.Add(new SqlParameter("@Region", order.Region ?? string.Empty));
                cmd.Parameters.Add(new SqlParameter("@Country", order.Country ?? string.Empty));
                cmd.Parameters.Add(new SqlParameter("@Phone", order.Phone) );
                cmd.Parameters.Add(new SqlParameter("@Fax", order.Fax) );
                cmd.Parameters.Add(new SqlParameter("@CodeValName", order.CodeValName ?? string.Empty));
                cmd.Parameters.Add(new SqlParameter("@PostalCode", order.PostalCode ?? string.Empty));
                OrderID = cmd.ExecuteScalar().ToString();

                conn.Close();
            }
            return OrderID;
        }


    }
}