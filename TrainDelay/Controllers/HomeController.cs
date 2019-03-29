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
        public async Task<IActionResult> Index()
        {
            HttpClient client = new HttpClient();
            var httpContent = new StringContent(System.IO.File.ReadAllText("TrainStationRequest.xml"), Encoding.UTF8, "application/xml");
            var response = await client.PostAsync("http://api.trafikinfo.trafikverket.se/v1.3/data.json", httpContent);
            var json = await response.Content.ReadAsStringAsync();
            var value = JsonConvert.DeserializeObject<TrafikverketStationsResponse>(await response.Content.ReadAsStringAsync());
            var coord = new GeometryList() { Geometries = new List<Geometry>() };
            value.Response.Result.ForEach(x => x.TrainStation.ForEach(z => coord.Geometries.Add(z.Geometry)));
            return View(coord);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
