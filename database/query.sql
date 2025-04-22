
ALTER TABLE Empleados DROP COLUMN Departamento;

ALTER TABLE Empleados ADD ClaveDepartamento INT NULL, FOREIGN KEY (ClaveDepartamento) REFERENCES Departamentos(ClaveDepartamento);

INSERT INTO Departamentos (ClaveDepartamento, Descripcion)
VALUES 
    (1, 'Recursos Humanos'),
    (2, 'Finanzas'),
    (3, 'Tecnolog�as de la Informaci�n'),
    (4, 'Ventas'),
	(5, 'Marketing');

INSERT INTO Empleados (ClaveEmpleado, NombreEmpleado, FechaIngreso, FechaNacimiento, ClaveDepartamento)
VALUES
    (101, 'Ana Ram�rez',      '2022-04-01', '1990-06-15', 1),
    (102, 'Carlos Torres',    '2021-07-12', '1988-02-23', 2),
    (103, 'Laura Guti�rrez',  '2023-01-10', '1995-11-05', 3),
    (104, 'Pedro S�nchez',    '2020-09-20', '1991-04-30', 4),
    (105, 'Diana Robles',     '2022-12-05', '1993-10-11', 1),
    (106, 'Miguel R�os',      '2019-06-17', '1987-03-08', 2),
    (107, 'Sof�a N��ez',      '2023-03-25', '1996-12-22', 3),
    (108, 'Jorge Herrera',    '2021-11-09', '1992-08-18', 4),
    (109, 'Elena Morales',    '2020-02-28', '1990-01-04', 1),
    (110, 'Ra�l Jim�nez',     '2022-08-14', '1989-05-27', 2),
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