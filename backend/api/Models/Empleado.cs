using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace api.Models
{
    public class Empleados
    {
        public int ClaveEmpleado { get; set; } = 0;
        public string NombreEmpleado { get; set; } = string.Empty;
        public string FechaIngreso { get; set; } = string.Empty;
        public string FechaNacimiento { get; set; } = string.Empty;
        public int? ClaveDepartamento { get; set; } = 0;  
    }
}
