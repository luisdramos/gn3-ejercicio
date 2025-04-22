using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace api.Models
{
    public class Departamentos
    {
        public int ClaveDepartamento { get; set; } = 0;
        public string Descripcion { get; set; } = string.Empty;
        
    }
}
