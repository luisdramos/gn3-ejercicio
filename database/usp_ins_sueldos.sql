SET NOCOUNT ON
USE dev
GO

IF EXISTS (SELECT * FROM SYSOBJECTS WHERE ID = OBJECT_ID('dbo.usp_ins_sueldos') AND SYSSTAT & 0xf = 4)
	DROP PROCEDURE dbo.usp_ins_sueldos
GO

CREATE PROCEDURE usp_ins_sueldos
	@ClaveEmpleado INT = NULL,
	@SueldoMensual DECIMAL = NULL,
	@FormaPago VARCHAR(MAX) = NULL
AS 
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

DECLARE @val INT = NULL
DECLARE @NEWEMP INT = 0

IF @ClaveEmpleado IS NULL BEGIN SET @NEWEMP = 0 END 
IF @SueldoMensual IS NULL BEGIN SET @NEWEMP = 0 END 
IF @FormaPago IS NULL BEGIN SET @NEWEMP = 0 END 

-- Se valida si existe la claveEmpleado
IF EXISTS (SELECT TOP 1 * FROM Sueldos WHERE ClaveEmpleado= @ClaveEmpleado)
BEGIN 
	print ('existe')
	SET @val = 1
END 

IF @val IS NULL 
BEGIN 
	INSERT INTO Sueldos (ClaveEmpleado, SueldoMensual, FormaPago)
	VALUES (@ClaveEmpleado, @SueldoMensual, @FormaPago)
	
	SET @NEWEMP = @ClaveEmpleado
END 

IF @val IS NOT NULL 
BEGIN 
	UPDATE Sueldos 
		SET 
			SueldoMensual = @SueldoMensual,
			FormaPago = @FormaPago
		WHERE ClaveEmpleado = @ClaveEmpleado

		SET @NEWEMP = @ClaveEmpleado
END 

SELECT @NEWEMP AS RESPONSE