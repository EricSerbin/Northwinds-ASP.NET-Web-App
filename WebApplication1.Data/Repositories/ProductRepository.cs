using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Data.Model.ModelDB;
using Dapper;
using System.Data.SqlClient;
using WebApplication1.Data.Models;

namespace WebApplication1.Data.Repositories
{
    
    public class ProductRepository
    {
        private readonly string _connectionString;
        public ProductRepository(northwinds2Entities context)
        {
            _connectionString = context.Database.Connection.ConnectionString;
        }

        public IEnumerable<Product> GetProductBySupplier(int? id) //Entity Framework Method
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var sql = "SELECT * FROM Products WHERE SupplierID = @id";
                return conn.Query<Product>(sql, new { id }).ToList();
            }
        }

        public IEnumerable<Product> GetAllProducts() //Entity Framework method
        {
            using (var conn = new SqlConnection(_connectionString))
            {

                var sql = "SELECT * FROM Products";
                return conn.Query<Product>(sql, new { }).ToList();  

            }
        }
        public IEnumerable<ProductExt> GetProductBySupplierExt(int? id)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var sql = @"SELECT 
                            ProductID
	                            ,p.ProductName
	                            ,p.SupplierID
	                            ,P.CategoryID
	                            ,p.QuantityPerUnit
	                            ,p.UnitPrice
	                            ,p.UnitsInStock
	                            ,p.UnitsOnOrder
	                            ,p.ReorderLevel
	                            ,p.Discontinued
	                            ,s.CompanyName
	                            ,c.CategoryName
                                ,c.Description
                                ,c.Picture
                            FROM Products p
                            left join Suppliers s on p.SupplierID = s.SupplierID
                            left join Categories c on c.CategoryID = p.CategoryID

                            WHERE p.SupplierID = @id";
                return conn.Query<ProductExt>(sql, new { id }).ToList();
            }
        }
        public IEnumerable<ProductExt> GetAllProductsExt()
        {
            using (var conn = new SqlConnection(_connectionString))
            {

                var sql = @"SELECT 
                            ProductID
	                            ,p.ProductName
	                            ,p.SupplierID
	                            ,P.CategoryID
	                            ,p.QuantityPerUnit
	                            ,p.UnitPrice
	                            ,p.UnitsInStock
	                            ,p.UnitsOnOrder
	                            ,p.ReorderLevel
	                            ,p.Discontinued
	                            ,s.CompanyName
	                            ,c.CategoryName
                                ,c.Description
                                ,c.Picture
                            FROM Products p
                            left join Suppliers s on p.SupplierID = s.SupplierID
                            left join Categories c on c.CategoryID = p.CategoryID";
                return conn.Query<ProductExt>(sql, new { }).ToList();

            }
        }

    }
}

