using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

public class Departamentos
{
    public int? ClaveDepartamento { get; set; }
    public string Descripcion { get; set; } = string.Empty;
}

public class Sueldos
{
    public int ClaveEmpleado { get; set; }
    public decimal SueldoMensual { get; set; }
    public string FormaPago { get; set; } = string.Empty;
}

public class Empleados
{
    public int ClaveEmpleado { get; set; } = 0;
    public string NombreEmpleado { get; set; } = string.Empty;
    public string FechaIngreso { get; set; } = string.Empty;
    public string FechaNacimiento { get; set; } = string.Empty;
    public int? ClaveDepartamento { get; set; } = 0;
}