USE [salad]
GO
/****** Object:  StoredProcedure [dbo].[InsertaPlanNutricional]    Script Date: 7/26/2019 2:18:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE OR ALTER PROCEDURE InsertaIngrediente
	@nombre varchar (50)
AS
BEGIN
	INSERT INTO ingrediente
	VALUES (@nombre)
END
