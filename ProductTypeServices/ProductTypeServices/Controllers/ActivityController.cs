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
    public class ActivityController : ApiController
    {
        // Display Details 
        [HttpGet]
        public IEnumerable<Activity> displayActivities()
        {
            using (ProductTypesEntities obj = new ProductTypesEntities())
            {

                return obj.Activities.ToList();
            }
        }
       
        // Create Activity Product
        [HttpPost]

        public void createActivity([FromBody]Activity activityobj)

        {
            using (ProductTypesEntities obj = new ProductTypesEntities())
            {
                var id = obj.Activities.Max(p => p.ProductId);
                int maxid = Int32.Parse(id.ToString());
                maxid += 1;
                activityobj.ProductId = maxid;
                activityobj.IsBooked = "false";
                activityobj.IsSaved = "false";
                obj.Activities.Add(activityobj);
                obj.SaveChanges();
            }

        }
        // Book Activity
        [HttpPut]
        public void book([FromBody]ItemSelector item)
        {
            using (ProductTypesEntities obj = new ProductTypesEntities())
            {
                if (item.type == "book")
                {
                    var refobj = obj.Activities.Find(item.id);
                    string IsBooked = obj.Activities.Find(item.id).IsBooked;
                    IsBooked = "true";

                    refobj.IsBooked = IsBooked;

                    obj.SaveChanges();
                }
                else
                {
                    var refobj = obj.Activities.Find(item.id);
                    string IsSaved = obj.Activities.Find(item.id).IsSaved;
                    IsSaved = "true";

                    refobj.IsSaved = IsSaved;

                    obj.SaveChanges();
                }
            }
        }
    }
}
