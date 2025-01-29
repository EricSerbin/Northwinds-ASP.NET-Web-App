using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Data.Model.ModelDB;
using Dapper;
using WebApplication1.Data.Models;

namespace WebApplication1.Data.Repositories
{
    public class CustomerRepository
    {
        
        private readonly string _connectionString;
        public CustomerRepository(northwinds2Entities context)
        {
            _connectionString=context.Database.Connection.ConnectionString;
        }

        public bool Delete(int id)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                //var sql = "DELETE FROM Customers WHERE ID = @id";
                var sql = @"IF Exists (SELECT * FROM Customers WHERE ID = @id)
                                BEGIN 
                                    DELETE FROM Customers WHERE ID = id
                                END
                                ELSE
                                BEGIN
                                    SELECT 0
                                END";
                conn.Execute(sql, new { id });
            }
            return false;
        }

        public Customer Find(int? id) //CustomerExt is more apt when DbContext is updated to distance from entity framework
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var sql = "SELECT * FROM Customers WHERE ID = @id";
                return conn.Query<Customer>(sql, new { id }).FirstOrDefault();
            }
        }

        

        public IEnumerable<Customer> GetCustomers()
        {
            //    <add name="northwinds2Entities" connectionString="metadata=res://*/Model.ModelDB.Northwinds2.csdl|res://*/Model.ModelDB.Northwinds2.ssdl|res://*/Model.ModelDB.Northwinds2.msl;provider=System.Data.SqlClient;provider connection string=&quot;data source=DESKTOP-2IIBDVA\SQLEXPRESS01;initial catalog=northwind;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework&quot;" providerName="System.Data.EntityClient" />

            //            var _connectionString = "Data Source=.;Initial Catalog=Northwind;user id=admin"; //DESKTOP\\SQLEXPRESS

            //var _connectionString = "Data Source=.;Initial Catalog=northwinds2Entities;user id=admin"; //DESKTOP\\SQLEXPRESS
            //var _connectionString = "Server=.;Database=Northwind;Trusted_Connection=True;";
            using ( var conn = new SqlConnection(_connectionString))
            {
                var sql= "SELECT * FROM Customers";
                return conn.Query<Customer>(sql);
                //return SqlConnection.Query
                //return db.Customers.ToList();
            }
        }
    }
   
}
