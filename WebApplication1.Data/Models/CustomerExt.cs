using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Data.Model.ModelDB;

namespace WebApplication1.Data.Models
{
    public abstract class CustomerExt : Customer
    {
        [Required]
        [StringLength(10)]
        public new string CompanyName { get; set; }

        [Required]
        [Range(0, 10)]
        public int Number { get; set; }

        [Required]
        public new string ContactName { get; set; }
    }
}
