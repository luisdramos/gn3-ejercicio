using Microsoft.AspNetCore.Mvc;

namespace api.Models
{
    public class Sueldos
    {
        public int ClaveEmpleado { get; set; } = 0;
        public decimal SueldoMensual { get; set; } = 0;
        public string FormaPago { get; set; } = "Efectivo";        

    }
}
