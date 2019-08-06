USE [salad]
GO
/****** Object:  StoredProcedure [dbo].[ModificaUsuario]    Script Date: 8/5/2019 10:57:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  PROCEDURE [dbo].[AgregaPasosReceta]
	@pasos varchar(20)
AS
BEGIN
	
	Update receta
		set pasos=@pasos
		WHERE idReceta=(Select max(idReceta) from receta)
END
