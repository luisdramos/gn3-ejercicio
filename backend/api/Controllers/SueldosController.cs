using api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SqlServerConnector;
using System.Data;


[ApiController]
[Route("api/[controller]")]
public class SueldosController(SqlHelper sqlHelper) : ControllerBase
{
    private class Sueldo
    {
        public int ClaveEmpleado { get; set; }
        public decimal SueldoMensual { get; set; }
        public string FormaPago { get; set; } = string.Empty;        
    }

    private class ResponseSueldo
    {
        public int ClaveEmpleado { get; set; }
        public decimal SueldoMensual { get; set; }
        public string FormaPago { get; set; } = string.Empty;
        public string Mensaje { get; set; } = string.Empty;
    }

    [HttpGet]
    public async Task<ActionResult> GetSueldos()
    {
        var tabla = await sqlHelper.ExecuteQueryAsync("exec usp_get_sueldos @id = null");

        var sueldos = new List<object>();

        if (tabla.Rows.Count == 0)
            return NotFound("No se encontraron sueldos.");

        foreach (DataRow row in tabla.Rows)
        {
            var sueldo = new Sueldo
            {
                ClaveEmpleado = (int)row["ClaveEmpleado"],
                SueldoMensual = (decimal)row["SueldoMensual"],
                FormaPago = (string)row["FormaPago"]
            };

            sueldos.Add(sueldo);
        }

        return Ok(sueldos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetSueldo(int id)
    {
        var tabla = await sqlHelper.ExecuteQueryAsync($"exec usp_get_sueldos @id = {id}");

        var sueldos = new List<object>();

        if (tabla.Rows.Count == 0)
            return NotFound("No se encontraron sueldos.");

        foreach (DataRow row in tabla.Rows)
        {
            var sueldo = new Sueldo
            {
                ClaveEmpleado = (int)row["ClaveEmpleado"],
                SueldoMensual = (decimal)row["SueldoMensual"],
                FormaPago = (string)row["FormaPago"]
            };

            sueldos.Add(sueldo);
        }

        return Ok(sueldos);
    }

    [HttpPost]
    public async Task<ActionResult> SetEmpleado([FromForm] Sueldos model)
    {
        var _ClaveEmpleado = model.ClaveEmpleado;

        if (_ClaveEmpleado == 0)
            return BadRequest("Faltan datos para agregar el sueldo.");

        var _SueldoMensual = model.SueldoMensual;

        if (_SueldoMensual == 0)
            return BadRequest("Faltan datos para agregar el sueldo.");       

        var _FormaPago = model.FormaPago;

        if (string.IsNullOrEmpty(_FormaPago))
            return BadRequest("Faltan datos para agregar el sueldo.");        


        var sueldo = new ResponseSueldo
        {
            ClaveEmpleado = _ClaveEmpleado,
            SueldoMensual = _SueldoMensual,
            FormaPago = _FormaPago,
            Mensaje = "Sueldo agregado/actualizado correctamente."
        };   
        
        var query = $"exec usp_ins_sueldos @ClaveEmpleado = {_ClaveEmpleado}, @SueldoMensual = {_SueldoMensual}, @FormaPago = '{_FormaPago}' ";        

        var tabla = await sqlHelper.ExecuteQueryAsync(query);

        if (tabla.Rows.Count == 0)
            return NotFound("Error al guardar.");

        return Ok(sueldo);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteSueldo(int id)
    {
        var tabla = await sqlHelper.ExecuteQueryAsync($"exec usp_delete_sueldo @id = {id}");

        return Ok("Registro eliminado correctamente");
    }

}
