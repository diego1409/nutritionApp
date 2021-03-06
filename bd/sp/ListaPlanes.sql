USE [salad]
GO
/****** Object:  StoredProcedure [dbo].[ListaUsuarios]    Script Date: 7/26/2019 11:59:44 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE OR ALTER PROCEDURE [dbo].[ListaPlanes]
	@idUsuario int
AS
BEGIN
	
	SELECT
		idPlan,
		fecha,
		(CASE carbos  
			--A= Alta, M= Media, B= Baja, N= Nula
			WHEN 'A' THEN 'Alta'
			WHEN 'M' THEN 'Media'
			WHEN 'B' THEN 'Baja'
			WHEN 'N' THEN 'Nula'
			ELSE 'Nula'  
		END) AS carbos,
		(CASE proteinas  
			--A= Alta, M= Media, B= Baja, N= Nula
			WHEN 'A' THEN 'Alta'
			WHEN 'M' THEN 'Media'
			WHEN 'B' THEN 'Baja'
			WHEN 'N' THEN 'Nula'
			ELSE 'Nula'  
		END) AS proteinas,
		(CASE grasas  
			--A= Alta, M= Media, B= Baja, N= Nula
			WHEN 'A' THEN 'Alta'
			WHEN 'M' THEN 'Media'
			WHEN 'B' THEN 'Baja'
			WHEN 'N' THEN 'Nula'
			ELSE 'Nula'  
		END) AS grasas,
		(CASE azucares  
			--A= Alta, M= Media, B= Baja, N= Nula
			WHEN 'A' THEN 'Alta'
			WHEN 'M' THEN 'Media'
			WHEN 'B' THEN 'Baja'
			WHEN 'N' THEN 'Nula'
			ELSE 'Nula'  
		END) AS azucares,
		calorias
	FROM planNutricional
	WHERE idUsuario = @idUsuario

END