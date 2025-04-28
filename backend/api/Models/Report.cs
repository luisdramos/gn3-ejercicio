using Microsoft.AspNetCore.Mvc;

namespace api.Models
{
    public class Report
    {
        public int ClaveEmpleado {  get; set; }
        public string NombreEmpleado { get; set; } = string.Empty;
        public string FechaIngreso { get; set; } = string.Empty;
        public decimal SueldoMensual { get; set; } = 0;
        public string Descripcion { get; set; } = string.Empty;
    }
}
