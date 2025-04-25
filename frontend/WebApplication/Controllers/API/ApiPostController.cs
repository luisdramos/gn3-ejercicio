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
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace WebApplication.Controllers
{
    public class ApiPostController : Controller
    {
        private readonly HttpClient _httpClient;

        public ApiPostController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        [System.Web.Mvc.HttpPost]
        public System.Web.Mvc.ContentResult SetDepartamentosAPI([FromBody] Departamentos departamento)
        {            
            string apiUrl = ConfigurationManager.AppSettings["ApiUrl"];

            var jsonContent = JsonConvert.SerializeObject(departamento);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            

            var response = _httpClient.PostAsync($"{apiUrl}api/Departamentos", content);

            var result = new System.Web.Mvc.ContentResult
            {
                Content = JsonConvert.SerializeObject(response),
                ContentType = "application/json"
            };

            return result;
        }

        /*[HttpPost]
        public async Task<ActionResult> SetSueldosAPI()
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

        [HttpPost]
        public async Task<ActionResult> SetEmpleadosAPI()
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
        }*/
    }   
}