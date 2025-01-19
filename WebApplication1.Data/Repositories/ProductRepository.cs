using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Data.Model.ModelDB;
using Dapper;
using System.Data.SqlClient;

namespace WebApplication1.Data.Repositories
{
    
    public class ProductRepository
    {
        private readonly string _connectionString;
        public ProductRepository(northwinds2Entities context)
        {
            _connectionString = context.Database.Connection.ConnectionString;
        }

        public IEnumerable<Product> GetProductBySupplier(int? id)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var sql = "SELECT * FROM Products WHERE SupplierID = @id";
                return conn.Query<Product>(sql, new { id }).ToList();
            }
        }

        public IEnumerable<Product> GetAllProducts()
        {
            using (var conn = new SqlConnection(_connectionString))
            {

                var sql = "SELECT * FROM Products";
                return conn.Query<Product>(sql, new { }).ToList();  

                //return conn.Query<Product>(sql, new { Ids = ids }).ToList();


                /*var sql = "SELECT * FROM Products";
                //var productIndicesSpeculative = "SELECT * FROM ProductID";
                var productIndices = "SELECT P.ProductID FROM Products P";  //way of extracting all IDs 

                System.Diagnostics.Debug.WriteLine(productIndices);
                //var connList = conn.Query<Product>(productIndices);

                for (int i = 0; i < productIndices.Count(); i++) //the count uses the select phrase, may require AS
                {
                    System.Diagnostics.Debug.WriteLine(i);
                    //var sql = "SELECT * FROM Products WHERE ProductID = @productIndices[i]";
                    z = i;
                }
                var lengthList = productIndices.Count();
                //return conn.Query<Product>(sql, new { lengthList }).ToList();
                return conn.Query<Product>(sql, new {productIndices}).ToList();*/

            }
        }
        
    }
}

