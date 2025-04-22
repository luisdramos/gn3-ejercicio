using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace api.Models
{
    public class Empleado
    {
        [Key]
        public int ClaveEmpleado { get; set; }
        public string NombreEmpleado { get; set; } = string.Empty;
        public DateTime FechaIngreso { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public int? ClaveDepartamento { get; set; }

        public Departamento? Departamento { get; set; }
        public Sueldo? Sueldo { get; set; }
    }
}
