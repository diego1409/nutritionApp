USE [salad]
GO
/****** Object:  StoredProcedure [dbo].[InsertaIngrediente]    Script Date: 7/26/2019 3:43:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE OR ALTER PROCEDURE EliminaAlergia
	@idUsuario int,
	@idIngrediente int
AS
BEGIN
	-- Si la alergia se habia insertado antes, se procede a eliminar 
	IF ((SELECT count(*) FROM alergia where idUsuario = @idUsuario AND idIngrediente = @idIngrediente) <> 0)
	BEGIN
		DELETE FROM alergia
		WHERE idUsuario =  @idUsuario
		AND idIngrediente = @idIngrediente
	END
END
