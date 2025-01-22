using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Data.Model.ModelDB;

namespace WebApplication1.Data.Models
{
    public class ProductExt : Product 
    {
        public string CompanyName { get; set; }
        public string CategoryName { get; set; }

        public new string CategoryID { get; set; }

        public string Description { get; set; }

        public byte[] Picture { get; set; }

    }
}
