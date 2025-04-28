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
    public class ApiController : Controller
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private readonly string _apiUrl = ConfigurationManager.AppSettings["ApiUrl"];

        [System.Web.Mvc.HttpGet]
        public async Task<System.Web.Mvc.ActionResult> GetDepartamentosAPI()
        {
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

        [System.Web.Mvc.HttpPost]
        public async Task<System.Web.Mvc.ContentResult> SetDepartamentoAPI([FromBody] Departamentos departamento)
        {
            using (var formContent = new MultipartFormDataContent())
            {
                formContent.Add(new StringContent(departamento.ClaveDepartamento.ToString()), "ClaveDepartamento");
                formContent.Add(new StringContent(departamento.Descripcion), "Descripcion");

                var response = await _httpClient.PostAsync($"{_apiUrl}api/Departamentos", formContent);
                var responseContent = await response.Content.ReadAsStringAsync();

                return new System.Web.Mvc.ContentResult
                {
                    Content = responseContent,
                    ContentType = "application/json"
                };
            }
        }

        [System.Web.Mvc.HttpPost]
        public async Task<System.Web.Mvc.ContentResult> DeleteDepartamentoAPI([FromBody] Departamentos departamento)
        {
            var id = departamento.ClaveDepartamento.ToString();
            var response = await _httpClient.DeleteAsync($"{_apiUrl}api/Departamentos/{id}");
            var responseContent = await response.Content.ReadAsStringAsync();
            
            return new System.Web.Mvc.ContentResult
            {
                Content = responseContent,
                ContentType = "application/json"
            };
        }

        [System.Web.Mvc.HttpGet]
        public async Task<System.Web.Mvc.ActionResult> GetSueldosAPI()
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

        [System.Web.Mvc.HttpPost]
        public async Task<System.Web.Mvc.ContentResult> SetSueldoAPI([FromBody] Sueldos sueldo)
        {
            using (var formContent = new MultipartFormDataContent())
            {
                formContent.Add(new StringContent(sueldo.ClaveEmpleado.ToString()), "ClaveEmpleado");
                formContent.Add(new StringContent(sueldo.SueldoMensual.ToString()), "SueldoMensual");
                formContent.Add(new StringContent(sueldo.FormaPago), "FormaPago");

                var response = await _httpClient.PostAsync($"{_apiUrl}api/Sueldos", formContent);
                var responseContent = await response.Content.ReadAsStringAsync();

                return new System.Web.Mvc.ContentResult
                {
                    Content = responseContent,
                    ContentType = "application/json"
                };
            }
        }

        [System.Web.Mvc.HttpPost]
        public async Task<System.Web.Mvc.ContentResult> DeleteSueldoAPI([FromBody] Sueldos sueldo)
        {
            var id = sueldo.ClaveEmpleado.ToString();
            var response = await _httpClient.DeleteAsync($"{_apiUrl}api/Sueldos/{id}");
            var responseContent = await response.Content.ReadAsStringAsync();
            return new System.Web.Mvc.ContentResult
            {
                Content = responseContent,
                ContentType = "application/json"
            };
        }

        [System.Web.Mvc.HttpGet]
        public async Task<System.Web.Mvc.ActionResult> GetEmpleadosAPI()
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