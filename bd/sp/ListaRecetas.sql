USE [salad]
GO
/****** Object:  StoredProcedure [dbo].[ListaUsuarios]    Script Date: 7/26/2019 11:59:44 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE OR ALTER PROCEDURE [dbo].[ListaRecetas]
AS
BEGIN
    SELECT
        idReceta,
        nombre,
        tiempo,
        (CASE tiempoComida   
            WHEN 'D' THEN 'Desayuno'
            WHEN 'MM' THEN 'Merienda Mañana'
            WHEN 'A' THEN 'Almuerzo'
            WHEN 'MT' THEN 'Merienda Tarde'
            WHEN 'C' THEN 'Cena'
            WHEN 'MN' THEN 'Merienda Noche'
            ELSE 'Comida'  
        END) AS tiempoComida
    FROM receta
END
