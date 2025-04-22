using api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SqlServerConnector;
using System.Data;


[ApiController]
[Route("api/[controller]")]
public class DepartamentosController(SqlHelper sqlHelper) : ControllerBase
{

    private class Departamento
    {
        public int? ClaveDepartamento { get; set; }
        public string Descripcion { get; set; } = string.Empty;        
    }

    private class ResponseDepartamento
    {
        public int? ClaveDepartamento { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public string Mensaje { get; set; } = string.Empty;
    }

    [HttpGet]
    public async Task<ActionResult> GetDepartamentos()
    {
        var tabla = await sqlHelper.ExecuteQueryAsync("exec usp_get_departamentos @id = null");

        var departamentos = new List<object>();

        if (tabla.Rows.Count == 0)
            return NotFound("No se encontraron empleados.");

        foreach (DataRow row in tabla.Rows)
        {
            var departamento = new Departamento
            {
                ClaveDepartamento = (int)row["ClaveDepartamento"],
                Descripcion = (string)row["Descripcion"]
            };

            departamentos.Add(departamento);
        }

        return Ok(departamentos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetDepartamento(int id)
    {
        var tabla = await sqlHelper.ExecuteQueryAsync($"exec usp_get_departamentos @id = {id}");

       var departamentos = new List<object>();

        if (tabla.Rows.Count == 0)
            return NotFound("No se encontraron empleados.");

        foreach (DataRow row in tabla.Rows)
        {
            var departamento = new Departamento
            {
                ClaveDepartamento = (int)row["ClaveDepartamento"],
                Descripcion = (string)row["Descripcion"]
            };

            departamentos.Add(departamento);
        }

        return Ok(departamentos);
    }

    [HttpPost]
    public async Task<ActionResult> SetDepartamento([FromForm] Departamentos model)
    {
        var _ClaveDepartamento = model.ClaveDepartamento;

        if (_ClaveDepartamento == 0)
            return BadRequest("Faltan datos para agregar el Departamento.");

        var _Descripcion = model.Descripcion;

        if (string.IsNullOrEmpty(_Descripcion))
            return BadRequest("Faltan datos para agregar el Departamento.");        

        var departamento = new ResponseDepartamento
        {
            ClaveDepartamento = _ClaveDepartamento,
            Descripcion = _Descripcion,
            Mensaje = "Departamento agregado/actualizado correctamente."
        };        

        var query = $"exec usp_ins_departamentos @ClaveDepartamento = {_ClaveDepartamento}, @Descripcion = '{_Descripcion}' ";

        var tabla = await sqlHelper.ExecuteQueryAsync(query);

        if (tabla.Rows.Count == 0)
            return NotFound("Error al guardar.");

        return Ok(departamento);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteDepartamento(int id)
    {
        var tabla = await sqlHelper.ExecuteQueryAsync($"exec usp_delete_departamento @id = {id}");

        return Ok("Registro eliminado correctamente");
    }

}
