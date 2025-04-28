using api.Models;
using Microsoft.AspNetCore.Mvc;
using SqlServerConnector;
using System.Data;


[ApiController]
[Route("api/[controller]")]
public class ReportController(SqlHelper sqlHelper) : ControllerBase
{
    private class Reports
    {
        public int ClaveEmpleado { get; set; }
        public string NombreEmpleado { get; set; } = string.Empty;
        public DateTime FechaIngreso { get; set; }
        public decimal SueldoMensual { get; set; } = 0;
        public string Descripcion { get; set; } = string.Empty;
    }

    [HttpGet]
    public async Task<ActionResult> GetReportRH()
    {
        var tabla = await sqlHelper.ExecuteQueryAsync("exec ObtenerEmpleadosPorDepartamentos");

        var reports = new List<object>();

        if (tabla.Rows.Count == 0)
            return NotFound("No se encontraron empleados.");

        foreach (DataRow row in tabla.Rows)
        {
            var report = new Reports
            {
                ClaveEmpleado = (int)row["ClaveEmpleado"],
                NombreEmpleado = (string)row["NombreEmpleado"],
                FechaIngreso = (DateTime)row["FechaIngreso"],
                SueldoMensual = (decimal)row["SueldoMensual"],
                Descripcion = (string)row["Descripcion"]
            };
           
            reports.Add(report);
        }

        return Ok(reports);
    }

}
