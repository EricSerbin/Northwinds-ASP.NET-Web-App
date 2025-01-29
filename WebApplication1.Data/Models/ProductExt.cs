using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Data.Model.ModelDB;

namespace WebApplication1.Data.Models
{

    public class ProductExt : Product
    {
        [Required]
        [StringLength(10)]
        public string CompanyName { get; set; }

        [Required]    
        public string CategoryName { get; set; }

        public new string CategoryID { get; set; }

        public string Description { get; set; }

        public byte[] Picture { get; set; }
        //public DbSet<Product> Product { get; set; }

        /*public ICollection*/
        /*public ICollection<Product> Posts { get; }*/

    }
}
