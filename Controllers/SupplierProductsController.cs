using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Data.Model.ModelDB;
using WebApplication1.Data.Repositories;



using System.Data;
using System.Data.Entity;
using System.Net;
using System.Reflection;
using System.Runtime.Serialization;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using WebGrease.Activities;
using System.IO;


namespace WebApplication1.Controllers
{
    public class SupplierProductsController : Controller
    {
        // GET: SupplierProducts
        private readonly northwinds2Entities db = new northwinds2Entities(); //intellisense wants this to be read only, makes sense until we have to add a delete command?
        private ProductRepository productRepository;
        public SupplierProductsController()
        {
            productRepository = new ProductRepository(db);
        }
        
        public ActionResult Index(int? id) //supplier ID   seems to be lazy loading?
        {
            if(id!=null)
            {
                var products = productRepository.GetProductBySupplier(id);
                return View(products);
            }
            else
            {
              
                var products = productRepository.GetAllProducts();
                System.Diagnostics.Debug.WriteLine(products);
                //return View(products); //must not be attaching correctly
                return View(db.Products);
            }
            
        }

        public ActionResult Details(int? id)
        {
            return null;
            //return ControllerContext.MyDisplayRouteInfo(id); //make an app for routing?
        }
        
    }
    public static class HtmlExtensions
    {
        
        public static MvcHtmlString BufferImage(byte[] image)
        {
            //var img = string.Format("data:image/jpg;base64,{0}", Convert.ToBase64String(image));
            //            var img =$"data:{"jpeg"};base64,{Convert.ToBase64String(image)}";
            
            string alt = "";
            var base64 = Convert.ToBase64String(image);
            var imgSrc = $"data:image/png;base64,{base64}";
            var img = $"<imgSrc src=\"{imgSrc}\" alt=\"{alt}\" />";
            System.Diagnostics.Debug.WriteLine(img);
            return new MvcHtmlString(img);
            //return new MvcHtmlString("<img src='" + img + "'/>");
        }
        public static MvcHtmlString BufferImageOffset(byte[] image)
        {
            if (image == null) //required due to sql returned models being seemingly unattached to an entity
            {
                return MvcHtmlString.Empty;
            }
            
            var strippedImageData = new byte[image.Length - 78]; //May be beneficial to extract the image dimensions from the header 
            byte[] imageHeader =new byte [78];
            Array.Copy(image, 0, imageHeader, 0, 78);
            Array.Copy(image, 78, strippedImageData, 0, strippedImageData.Length);
            
            var headerBase64 = Convert.ToBase64String(imageHeader);
            var base64 = Convert.ToBase64String(strippedImageData);
            //System.Diagnostics.Debug.WriteLine("base64 stripped data is " + strippedImageData);

            //var headerImgSrc = $"data:image/ping;base64,{headerBase64}"
            var imgSrc = $"data:image/png;base64,{base64}"; //the data this makes with a stripped image is valid when decoded base64 -> image

            var img = $"<imgSrc src=\"{imgSrc}\" />";
            //return new MvcHtmlString(img);

            return new MvcHtmlString(imgSrc);
            //return new MvcHtmlString("header "+ headerBase64 + "body "+imgSrc);
            //MyImage = new FileContentResult(image, "image/jpg"); //a few formatting issues with this approach
            //return MyImage;

        }

        /*public Image ByteArrayToImage(byte[] byteArrayIn)
        {
            return byteArrayToImage(byteArrayIn, true);
        }

        public Image ByteArrayToImage(byte[] byteArrayIn, bool stripOleHeader)
        {
            int strippedImageLength = byteArrayIn.Length - (stripOleHeader ? 78 : 0);
            byte[] strippedImageData = new byte[strippedImageLength];
            Array.Copy(byteArrayIn, (stripOleHeader ? 78 : 0), strippedImageData, 0, strippedImageLength);
            MemoryStream ms = new MemoryStream(strippedImageData);
            return Image.FromStream(ms);
        }*/

    }
}
