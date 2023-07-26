using ASPNET_MVC_ChartsDemo.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ASPNET_MVC_ChartsDemo.Controllers
{
    public class ChartController : Controller
    {
        // GET: Home    
        public ActionResult Chart()
        {
            List<Chart> dataPoints = new List<Chart>();

            dataPoints.Add(new Chart("Tỷ lệ OPC", 10));
            dataPoints.Add(new Chart("Tỷ lệ Cồn", 30));
            dataPoints.Add(new Chart("Tỷ lệ Sủi", 80));
            dataPoints.Add(new Chart("Tỷ lệ Phiến", 39));
            dataPoints.Add(new Chart("Tỷ lệ 25", 30));
          

            ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);

            return View();
        }
    }
}