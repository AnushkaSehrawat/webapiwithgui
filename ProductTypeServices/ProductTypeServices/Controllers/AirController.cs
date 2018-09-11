using ProductDataLibrary;
using ProductTypeServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.UI;
using ProductDataLibrary;

namespace ProductTypeServices.Controllers
{
    public class AirController : ApiController
    {
        // Display Details 
        [HttpGet]
        public IEnumerable<Air> displayAirProducts()
        {
            using (ProductTypesEntities obj = new ProductTypesEntities())
            {

                return obj.Airs.ToList();
            }
        }
        // Create AirProduct
        [HttpPost]

        public void createAirProduct([FromBody]Air airobjectobj)

        {
            using (ProductTypesEntities obj = new ProductTypesEntities())
            {
                var id = obj.Airs.Max(p => p.ProductId);
                int maxid = Int32.Parse(id.ToString());
                maxid += 1;
                airobjectobj.ProductId = maxid;
                airobjectobj.IsBooked = "false";
                airobjectobj.IsSaved = "false";
                obj.Airs.Add(airobjectobj);
                obj.SaveChanges();
            }

        }
        // Book AirProduct
        [HttpPut]
        public void book([FromBody]ItemSelector item)
        {
            using (ProductTypesEntities obj = new ProductTypesEntities())
            {
                if (item.type == "book")
                {
                    var refobj = obj.Airs.Find(item.id);
                    string IsBooked = obj.Airs.Find(item.id).IsBooked;
                    IsBooked = "true";

                    refobj.IsBooked = IsBooked;

                    obj.SaveChanges();
                }
                else
                {
                    var refobj = obj.Airs.Find(item.id);
                    string IsSaved = obj.Airs.Find(item.id).IsSaved;
                    IsSaved = "true";

                    refobj.IsSaved = IsSaved;

                    obj.SaveChanges();
                }
            }
        }


    }
}
