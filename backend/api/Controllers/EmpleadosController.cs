using api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SqlServerConnector;
using System.Data;


[ApiController]
[Route("api/[controller]")]
public class EmpleadosController(SqlHelper sqlHelper) : ControllerBase
{    

    private class Empleado 
    {
        public int ClaveEmpleado { get; set; }
        public string NombreEmpleado { get; set; } = string.Empty;
        public DateTime FechaIngreso { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public int? ClaveDepartamento { get; set; }
    }

    private class ResponseEmpleado
    {
        public int ClaveEmpleado { get; set; }
        public string NombreEmpleado { get; set; } = string.Empty;
        public string FechaIngreso { get; set; } = string.Empty;
        public string FechaNacimiento { get; set; } = string.Empty;
        public int? ClaveDepartamento { get; set; }
        public string Mensaje { get; set; } = string.Empty;
    }

    [HttpGet]    
    public async Task<ActionResult> GetEmpleados()
    {        
        var tabla = await sqlHelper.ExecuteQueryAsync("exec usp_get_empleados @id = null");

        var empleados = new List<object>();

        if (tabla.Rows.Count == 0)
            return NotFound("No se encontraron empleados.");

        foreach (DataRow row in tabla.Rows)
        {
            var empleado = new Empleado
            {
                ClaveEmpleado = (int)row["ClaveEmpleado"],
                NombreEmpleado = (string)row["NombreEmpleado"],
                FechaIngreso = (DateTime)row["FechaIngreso"],
                FechaNacimiento = (DateTime)row["FechaNacimiento"],
                ClaveDepartamento = (int)row["ClaveDepartamento"]
            };
            empleados.Add(empleado);
        }

        return Ok(empleados);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetEmpleado(int id)
    {
        var tabla = await sqlHelper.ExecuteQueryAsync($"exec usp_get_empleados @id = {id}");

        var empleados = new List<object>();

        if (tabla.Rows.Count == 0)
            return NotFound("No se encontraron empleados.");

        foreach (DataRow row in tabla.Rows)
        {
            var empleado = new Empleado
            {
                ClaveEmpleado = (int)row["ClaveEmpleado"],
                NombreEmpleado = (string)row["NombreEmpleado"],
                FechaIngreso = (DateTime)row["FechaIngreso"],
                FechaNacimiento = (DateTime)row["FechaNacimiento"],
                ClaveDepartamento = (int)row["ClaveDepartamento"]
            };
            empleados.Add(empleado);
        }

        return Ok(empleados);
    }

    [HttpPost]
    public async Task<ActionResult> SetEmpleado([FromForm] Empleados model)
    {
        var _ClaveEmpleado = model.ClaveEmpleado;
        
        if (_ClaveEmpleado == 0)
            return BadRequest("Faltan datos para agregar el empleado.");

        var _NombreEmpleado = model.NombreEmpleado;

        if (string.IsNullOrEmpty(_NombreEmpleado))
            return BadRequest("Faltan datos para agregar el empleado.");

        var _FechaIngreso = model.FechaIngreso;

        if (string.IsNullOrEmpty(_FechaIngreso))
            return BadRequest("Faltan datos para agregar el empleado.");

        var _FechaNacimiento = model.FechaNacimiento;

        if (string.IsNullOrEmpty(_FechaNacimiento))
            return BadRequest("Faltan datos para agregar el empleado.");

        var _ClaveDepartamento = model.ClaveDepartamento;

        if (_ClaveDepartamento == 0)
            return BadRequest("Faltan datos para agregar el empleado.");


        var empleado = new ResponseEmpleado
        {
            ClaveEmpleado = _ClaveEmpleado,
            NombreEmpleado = _NombreEmpleado,
            FechaIngreso = _FechaIngreso,
            FechaNacimiento = _FechaNacimiento,
            ClaveDepartamento = _ClaveDepartamento,
            Mensaje = "Empleado agregado/actualizado correctamente."
        };

        var query = $"exec usp_ins_empleados @ClaveEmpleado = { _ClaveEmpleado}, @NombreEmpleado = '{_NombreEmpleado}', @FechaIngreso = '{_FechaIngreso}', @FechaNacimiento = '{_FechaNacimiento}', @ClaveDepartamento = { _ClaveDepartamento}";

        var tabla = await sqlHelper.ExecuteQueryAsync(query);        

        if (tabla.Rows.Count == 0)
            return NotFound("Error al guardar.");       

        return Ok(empleado);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteEmpleado(int id)
    {
        var tabla = await sqlHelper.ExecuteQueryAsync($"exec usp_delete_empleados @id = {id}");        

        return Ok("Registro eliminado correctamente");
    }

}
