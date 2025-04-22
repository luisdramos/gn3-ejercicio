SET NOCOUNT ON
USE dev
GO

IF EXISTS (SELECT * FROM SYSOBJECTS WHERE ID = OBJECT_ID('dbo.usp_get_departamentos') AND SYSSTAT & 0xf = 4)
	DROP PROCEDURE dbo.usp_get_departamentos
GO

CREATE PROCEDURE usp_get_departamentos
	@ID INT
AS 
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

SELECT ClaveDepartamento,Descripcion
FROM Departamentos
WHERE (@ID IS NOT NULL AND ClaveDepartamento= @ID)
OR @ID IS NULL 
