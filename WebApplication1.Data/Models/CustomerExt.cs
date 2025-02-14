using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Data.Model.ModelDB;


namespace WebApplication1.Data.Models
{
    
    //[BindProperties]
    public class CustomerExt
    {


        public int ID { get; set; }

        /*[Required(AllowEmptyStrings =false, ErrorMessage = "Company Name is required = test")]
        [StringLength(10, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength =3)]
        [DisplayName("Company Name")]
        public new string CompanyName { get; set; }*/

        [Required(AllowEmptyStrings = false, ErrorMessage = "Company Name is required = test")]
        [StringLength(10, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        [DisplayName("Company Name")]
        public string CompanyName { get; set; }

        [Required]
        [Range(0, 10)]
        public int Number { get; set; }

        [Required]
        public string ContactName { get; set; }
        public string ContactTitle { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
    }
}
