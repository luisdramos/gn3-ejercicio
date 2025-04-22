using Microsoft.AspNetCore.Mvc;

namespace api.Models
{
    public class Sueldo
    {
        public int ClaveEmpleado { get; set; }
        public decimal SueldoMensual { get; set; }
        public string FormaPago { get; set; } = string.Empty;        

    }
}
