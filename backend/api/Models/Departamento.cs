using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace api.Models
{
    public class Departamento
    {
        [Key]
        public int ClaveDepartamento { get; set; }
        public string Descripcion { get; set; } = string.Empty;

        public ICollection<Empleado>? Empleados { get; set; }
    }
}
