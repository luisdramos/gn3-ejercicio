using api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SqlServerConnector;
using System.Data;


[ApiController]
[Route("api/[controller]")]
public class EmpleadosController(SqlHelper sqlHelper) : ControllerBase
{
    private readonly DevDbContext _context;

    [HttpGet]    
    public async Task<ActionResult> GetEmpleados()
    {        
        var tabla = await sqlHelper.ExecuteQueryAsync("SELECT * FROM Empleados");

        var empleados = new List<object>();

        if (tabla.Rows.Count == 0)
            return NotFound("No se encontraron empleados.");

        foreach (DataRow row in tabla.Rows)
        {
            var empleado = new
            {
                ClaveEmpleado = row["ClaveEmpleado"],
                NombreEmpleado = row["NombreEmpleado"],
                FechaIngreso = row["FechaIngreso"],
                FechaNacimiento = row["FechaNacimiento"],
                Departamento = row["ClaveDepartamento"]
            };
            empleados.Add(empleado);
        }

        return Ok(empleados);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Empleado>> GetEmpleado(int id)
    {
        var empleado = await _context.Empleados
            .Include(e => e.Departamento)
            .Include(e => e.Sueldo)
            .FirstOrDefaultAsync(e => e.ClaveEmpleado == id);

        if (empleado == null)
            return NotFound();

        return empleado;
    }
}
