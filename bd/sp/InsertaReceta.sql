USE [salad]
GO
/****** Object:  StoredProcedure [dbo].[InsertaIngrediente]    Script Date: 7/26/2019 3:43:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE OR ALTER PROCEDURE InsertaReceta
	@foto image NULL,
	@nombre varchar(100),
	@dificultad char(1),
	@tiempo int,
	@tiempoComida char(2),
	@carbos decimal (5,2),
	@proteinas decimal (5,2),
	@grasas decimal (5,2),
	@azucares decimal (5,2),
	@calorias int,
	@pasos varchar(8000)

AS
BEGIN
	INSERT INTO receta
	VALUES (@foto,@nombre,@dificultad,@tiempo,@tiempoComida,@carbos,@proteinas,@grasas,@azucares,@calorias,@pasos)
END
