using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Data;

namespace WebApplication.Controllers
{
    public class ApiController : Controller
    {        
        [HttpGet]
        public async Task<ActionResult> GetDepartamentosAPI()
        {
            HttpClient _httpClient = new HttpClient();
            string _apiUrl = ConfigurationManager.AppSettings["ApiUrl"];
            var departamentos = new List<Departamentos>();

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"{_apiUrl}api/Departamentos");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    departamentos = JsonConvert.DeserializeObject<List<Departamentos>>(content);                    
                }                

            }
            catch (Exception ex)
            {                
                Console.WriteLine($"Error: {ex.Message}");
            }

            return Json(departamentos, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> GetSueldosAPI()
        {
            HttpClient _httpClient = new HttpClient();
            string _apiUrl = ConfigurationManager.AppSettings["ApiUrl"];
            var sueldos = new List<Sueldos>();

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"{_apiUrl}api/Sueldos");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    sueldos = JsonConvert.DeserializeObject<List<Sueldos>>(content);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            return Json(sueldos, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> GetEmpleadosAPI()
        {
            HttpClient _httpClient = new HttpClient();
            string _apiUrl = ConfigurationManager.AppSettings["ApiUrl"];
            var empleados = new List<Empleados>();

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"{_apiUrl}api/Empleados");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    empleados = JsonConvert.DeserializeObject<List<Empleados>>(content);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            return Json(empleados, JsonRequestBehavior.AllowGet);
        }
    }
}