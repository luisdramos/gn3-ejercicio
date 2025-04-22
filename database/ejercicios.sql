use dev
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


