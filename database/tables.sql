use dev;

CREATE TABLE Empleados (
    ClaveEmpleado INT PRIMARY KEY,
    NombreEmpleado NVARCHAR(100) NOT NULL,
    FechaIngreso DATE NOT NULL,
    FechaNacimiento DATE NOT NULL,
    Departamento NVARCHAR(100) NULL
);

CREATE TABLE Sueldos (
    ClaveEmpleado INT PRIMARY KEY,  -- También puede ser PK y FK al mismo tiempo si es relación 1 a 1
    SueldoMensual DECIMAL(10, 2) NOT NULL,
    FormaPago VARCHAR(20) CHECK (FormaPago IN ('Efectivo', 'Transferencia')),
    FOREIGN KEY (ClaveEmpleado) REFERENCES Empleados(ClaveEmpleado)
);

CREATE TABLE Departamentos (
    ClaveDepartamento INT PRIMARY KEY,
    Descripcion NVARCHAR(100) NOT NULL
);


