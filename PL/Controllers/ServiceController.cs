using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace PL.Controllers
{
    public class ServiceController : ApiController
    {
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/Ticket/Cargar")]

        public async Task<IHttpActionResult> Cargar()
        {
            string ruta = "http://localhost:12631/Ticket/CargaTickets";
            using (HttpClient client = new HttpClient())
            {
                var responseTask = await client.GetAsync(ruta);
                //responseTask.Wait();
                //var result = responseTask.Result;
                //if (result.IsSuccessStatusCode)
                //{
                //    var readTask = result.Content.ReadAsStringAsync();
                //    dynamic resultJSON = JObject.Parse(readTask.Result.ToString());
                    
                //}
            }
            return Ok();
        }
    }
}
