using ProductDataLibrary;
using ProductTypeServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ProductTypeServices.Controllers
{
    public class HotelController : ApiController
    {
        // Display Details 
        [HttpGet]
        public IEnumerable<Hotel> displayHotels()
        {
            using (ProductTypesEntities obj = new ProductTypesEntities())
            {
               
                return obj.Hotels.ToList();
            }
        }
        // Create Hotel
        [HttpPost]

        public void createHotel([FromBody]Hotel hobj)

        {
            using (ProductTypesEntities obj = new ProductTypesEntities())
            {
                var id = obj.Hotels.Max(p => p.ProductId);
                int maxid = Int32.Parse(id.ToString());
                maxid += 1;
                hobj.ProductId = maxid;
                hobj.IsBooked = "false";
                hobj.IsSaved = "false";
                obj.Hotels.Add(hobj);
                obj.SaveChanges();
            }

        }
        // Book Hotel
        [HttpPut]
        public void book([FromBody]ItemSelector item)
        {
            using (ProductTypesEntities obj = new ProductTypesEntities())
            {
                if (item.type == "book")
                {
                    var refobj = obj.Hotels.Find(item.id);
                    string IsBooked = obj.Hotels.Find(item.id).IsBooked;
                    IsBooked = "true";

                    refobj.IsBooked = IsBooked;

                    obj.SaveChanges();
                }
                else
                {
                    var refobj = obj.Hotels.Find(item.id);
                    string IsSaved = obj.Hotels.Find(item.id).IsSaved;
                    IsSaved = "true";

                    refobj.IsSaved = IsSaved;

                    obj.SaveChanges();
                }
            }
        }

    }
}
