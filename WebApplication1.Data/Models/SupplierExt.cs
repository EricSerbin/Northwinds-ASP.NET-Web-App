using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Data.Model.ModelDB;

namespace WebApplication1.Data.Models
{
    public class SupplierExt: Supplier
    {
            public decimal Latitude { get; set; }
            public decimal Longitude { get; set; }
    }

}

