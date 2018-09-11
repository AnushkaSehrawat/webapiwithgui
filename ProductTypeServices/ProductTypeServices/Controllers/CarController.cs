using ProductDataLibrary;
using ProductTypeServices.Models;
using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ProductTypeServices.Controllers
{
    public class CarController : ApiController
    {
        // Display Details 
        [HttpGet]
        public IEnumerable<Car> displayCarProducts()
        {
            using (ProductTypesEntities obj = new ProductTypesEntities())
            {

                return obj.Cars.ToList();
            }
        }

        // Create Car Product
        [HttpPost]

        public void createActivity([FromBody]Car carobj)

        {
            using (ProductTypesEntities obj = new ProductTypesEntities())
            {
                var id = obj.Cars.Max(p => p.ProductId);
                int maxid = Int32.Parse(id.ToString());
                maxid += 1;
                carobj.ProductId = maxid;
                carobj.IsBooked = "false";
                carobj.IsSaved = "false";
                obj.Cars.Add(carobj);
                obj.SaveChanges();
            }

        }
        // Book Car
        [HttpPut]
        public void book([FromBody]ItemSelector item)
        {
            using (ProductTypesEntities obj = new ProductTypesEntities())
            {
                if (item.type == "book")
                {
                    var refobj = obj.Cars.Find(item.id);
                    string IsBooked = obj.Cars.Find(item.id).IsBooked;
                    IsBooked = "true";

                    refobj.IsBooked = IsBooked;

                    obj.SaveChanges();
                }
                else
                {
                    var refobj = obj.Cars.Find(item.id);
                    string IsSaved = obj.Cars.Find(item.id).IsSaved;
                    IsSaved = "true";

                    refobj.IsSaved = IsSaved;

                    obj.SaveChanges();
                }
            }
        }
    }
}
