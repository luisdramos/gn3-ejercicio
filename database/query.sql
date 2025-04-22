use dev

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

ALTER TABLE Empleados DROP COLUMN Departamento;

ALTER TABLE Empleados ADD ClaveDepartamento INT NULL, FOREIGN KEY (ClaveDepartamento) REFERENCES Departamentos(ClaveDepartamento);

INSERT INTO Departamentos (ClaveDepartamento, Descripcion)
VALUES 
    (1, 'Recursos Humanos'),
    (2, 'Finanzas'),
    (3, 'Tecnologías de la Información'),
    (4, 'Ventas'),
	(5, 'Marketing');

INSERT INTO Empleados (ClaveEmpleado, NombreEmpleado, FechaIngreso, FechaNacimiento, ClaveDepartamento)
VALUES
    (101, 'Ana Ramírez',      '2022-04-01', '1990-06-15', 1),
    (102, 'Carlos Torres',    '2021-07-12', '1988-02-23', 2),
    (103, 'Laura Gutiérrez',  '2023-01-10', '1995-11-05', 3),
    (104, 'Pedro Sánchez',    '2020-09-20', '1991-04-30', 4),
    (105, 'Diana Robles',     '2022-12-05', '1993-10-11', 1),
    (106, 'Miguel Ríos',      '2019-06-17', '1987-03-08', 2),
    (107, 'Sofía Núñez',      '2023-03-25', '1996-12-22', 3),
    (108, 'Jorge Herrera',    '2021-11-09', '1992-08-18', 4),
    (109, 'Elena Morales',    '2020-02-28', '1990-01-04', 1),
    (110, 'Raúl Jiménez',     '2022-08-14', '1989-05-27', 2),
	(111, 'Marisol Ramirez',      '2025-10-01', '1990-06-15', 5);	


INSERT INTO Sueldos (ClaveEmpleado, SueldoMensual, FormaPago)
VALUES
    (101, 15000.00, 'Transferencia'),
    (102, 18000.00, 'Transferencia'),
    (103, 20000.00, 'Efectivo'),
    (104, 17000.00, 'Transferencia'),
    (105, 15500.00, 'Efectivo'),
    (106, 19000.00, 'Transferencia'),
    (107, 21000.00, 'Transferencia'),
    (108, 17500.00, 'Efectivo'),
    (109, 16000.00, 'Transferencia'),
    (110, 18500.00, 'Efectivo'),
	(111, 11500.00, 'Transferencia');


	Print('Ejercicios')

/*
1.- Devuelve un listado con los empleados y los datos de los departamentos donde trabaja cada uno junto con su sueldo de cada empleado y su forma de pago
*/
	SELECT 
    E.ClaveEmpleado,
    E.NombreEmpleado,
    E.FechaIngreso,
    E.FechaNacimiento,
    D.ClaveDepartamento,
    D.Descripcion AS Departamento,
    S.SueldoMensual,
    S.FormaPago
FROM Empleados E
INNER JOIN Departamentos D ON E.ClaveDepartamento = D.ClaveDepartamento
INNER JOIN Sueldos S ON E.ClaveEmpleado = S.ClaveEmpleado;

/*
2.- Devuelve un listado con la clave y el nombre del departamento, solamente de aquellos departamentos que tienen empleados.
*/

SELECT DISTINCT 
    D.ClaveDepartamento,
    D.Descripcion AS NombreDepartamento
FROM Departamentos D
INNER JOIN Empleados E ON D.ClaveDepartamento = E.ClaveDepartamento;

/*
3.- Devuelve un listado con el total de sueldos de cada departamento separado por forma de pago
*/

SELECT 
    D.ClaveDepartamento,
    D.Descripcion AS NombreDepartamento,
    S.FormaPago,
    SUM(S.SueldoMensual) AS TotalSueldos
FROM Empleados E
INNER JOIN Departamentos D ON E.ClaveDepartamento = D.ClaveDepartamento
INNER JOIN Sueldos S ON E.ClaveEmpleado = S.ClaveEmpleado
GROUP BY D.ClaveDepartamento, D.Descripcion, S.FormaPago
ORDER BY D.ClaveDepartamento, S.FormaPago;


/*
4.- Devuelve un listado de los empleados mayores de 30 años que tengan un sueldo mayor a 6 mil ordenado de mayor a menor
*/

SELECT 
    E.ClaveEmpleado,
    E.NombreEmpleado,
    E.FechaNacimiento,
    S.SueldoMensual,
    DATEDIFF(YEAR, E.FechaNacimiento, GETDATE()) AS Edad
FROM Empleados E
INNER JOIN Sueldos S ON E.ClaveEmpleado = S.ClaveEmpleado
WHERE 
    DATEDIFF(YEAR, E.FechaNacimiento, GETDATE()) > 30
    AND S.SueldoMensual > 6000
ORDER BY S.SueldoMensual DESC;

/*
5.- Devuelve un listado con los empleados que ingresaron en año actual
*/

SELECT 
    ClaveEmpleado,
    NombreEmpleado,
    FechaIngreso
FROM Empleados
WHERE YEAR(FechaIngreso) = YEAR(GETDATE());


/*
6- Devuelve un listado con la edad y su fecha de nacimiento de cada empleado ordenado de mayor a menor
*/

SELECT 
    ClaveEmpleado,
    NombreEmpleado,
    FechaNacimiento,
    DATEDIFF(YEAR, FechaNacimiento, GETDATE()) AS Edad
FROM Empleados
ORDER BY Edad DESC;


/*
7.- Elaborar un procedimiento almacenado que devuelva el numero de empleado, nombre, 
fecha de ingreso y sueldo filtrado por departamento, el procedimiento almacenado debe recibir la clave del departamento a consultar
*/


CREATE PROCEDURE ObtenerEmpleadosPorDepartamento
    @ClaveDepartamento INT
AS
BEGIN
    SELECT 
        E.ClaveEmpleado,
        E.NombreEmpleado,
        E.FechaIngreso,
        S.SueldoMensual
    FROM Empleados E
    INNER JOIN Sueldos S ON E.ClaveEmpleado = S.ClaveEmpleado
    WHERE E.ClaveDepartamento = @ClaveDepartamento
    ORDER BY E.ClaveEmpleado;
END;


EXEC ObtenerEmpleadosPorDepartamento @ClaveDepartamento = 2;


