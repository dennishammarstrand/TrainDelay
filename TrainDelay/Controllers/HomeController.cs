using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TrainDelay.Models;

namespace TrainDelay.Controllers
{
    public class HomeController : Controller
    {
        private HttpClient client;

        public HomeController(IHttpClientFactory httpClientFactory)
        {
            client = httpClientFactory.CreateClient("Trafikverket");
        }

        public async Task<IActionResult> Index()
        {
            var httpContent = new StringContent(System.IO.File.ReadAllText("TrainStationRequest.xml"), Encoding.UTF8, "application/xml");
            var response = await client.PostAsync("/v1.3/data.json", httpContent);
            var json = await response.Content.ReadAsStringAsync();
            var desResponse = JsonConvert.DeserializeObject<TrafikverketResponse>(await response.Content.ReadAsStringAsync());
            var geometryList = new GeometryList() { Geometries = new List<Geometry>() };
            desResponse.Response.Result.ForEach(x => x.TrainStation.ForEach(z => geometryList.Geometries.Add(z.Geometry)));
            return View(geometryList);
        }

        public async Task<IActionResult> Privacy()
        {
            var httpContent = new StringContent(System.IO.File.ReadAllText("TrainAnnouncementRequest.xml"), Encoding.UTF8);
            var response = await client.PostAsync("/v1.3/data.json", httpContent);
            var json = await response.Content.ReadAsStringAsync();
            var desResponse = JsonConvert.DeserializeObject<TrafikverketResponse>(await response.Content.ReadAsStringAsync());
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
