using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;

namespace WebApplication2.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Stock { get; set; }
        public string Description { get; set; }

    }

    public class ProductsDBAdapter
    {
        private string connectionString;

        public ProductsDBAdapter()
        {
            this.connectionString = ConfigurationManager.ConnectionStrings["default"].ConnectionString;
        }

        public IDictionary<int, Product> Products
        {
            get
            {
                Dictionary<int, Product> result = new Dictionary<int, Product>();
                Product p = null;

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand comm = conn.CreateCommand())
                    {
                        conn.Open();
                        comm.CommandText = "SELECT * FROM PRODUCTS";
                        using (SqlDataReader reader = comm.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while(reader.Read())
                                {
                                    p = new Product();
                                    p.Id = reader.GetInt32(0);
                                    p.Name = reader.GetString(1);
                                    p.Stock = reader.GetInt32(2);
                                    p.Description = reader.GetString(3);

                                    result.Add(p.Id, p);
                                }
                            }
                            reader.Close();
                        }
                    }
                    conn.Close();

                    return result;
                }

            }
        }

    }
}