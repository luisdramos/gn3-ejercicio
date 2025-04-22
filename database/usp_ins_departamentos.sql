SET NOCOUNT ON
USE dev
GO

IF EXISTS (SELECT * FROM SYSOBJECTS WHERE ID = OBJECT_ID('dbo.usp_ins_departamentos') AND SYSSTAT & 0xf = 4)
	DROP PROCEDURE dbo.usp_ins_empleados
GO

CREATE PROCEDURE usp_ins_departamentos
	@ClaveDepartamento INT = NULL,
	@Descripcion VARCHAR(MAX) = NULL
AS 
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

DECLARE @val INT = NULL
DECLARE @NEWEMP INT = 0

IF @ClaveDepartamento IS NULL BEGIN SET @NEWEMP = 0 END 
IF @Descripcion IS NULL BEGIN SET @NEWEMP = 0 END 

-- Se valida si existe la claveEmpleado
IF EXISTS (SELECT TOP 1 * FROM Departamentos WHERE ClaveDepartamento= @ClaveDepartamento)
BEGIN 
	print ('existe')
	SET @val = 1
END 

IF @val IS NULL 
BEGIN 
	INSERT INTO Departamentos (ClaveDepartamento, Descripcion)
	VALUES (@ClaveDepartamento, @Descripcion)
	
	SET @NEWEMP = @ClaveDepartamento
END 

IF @val IS NOT NULL 
BEGIN 
	UPDATE Departamentos 
		SET 
			Descripcion = @Descripcion
		WHERE ClaveDepartamento = @ClaveDepartamento

		SET @NEWEMP = @ClaveDepartamento
END 

SELECT @NEWEMP AS RESPONSE