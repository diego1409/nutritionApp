USE [salad]
GO
/****** Object:  StoredProcedure [dbo].[InsertaMedicion]    Script Date: 7/24/2019 11:33:53 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE OR ALTER PROCEDURE [dbo].[InsertaMedicion]
	@idUsuario int,
	@peso decimal(5,2),
	@grasa decimal(5,2),
	@musculo decimal(5,2),
	@agua decimal(5,2),
	@hueso decimal(5,2),
	@observaciones varchar(100)
	
AS
BEGIN
	DECLARE @imc decimal(5,2);
	DECLARE @estatura tinyint;
	
	SET @estatura= (select estatura from usuario where idUsuario=@idUsuario);
	--Dividido por 10000 porque se divide entre 100 para pasar a metros y luego otros 100 para definirlo como porcentaje
	SET @imc = (select @peso / (select SQUARE(@estatura)/10000));

	Insert into medicion (idUsuario,peso,grasa,musculo,agua,hueso,observaciones,imc,fecha)
	Values (@idUsuario,@peso,@grasa,@musculo,@agua,@hueso,@observaciones,@imc,GETDATE());

	Update usuario set peso = @peso where idUsuario=@idUsuario

	return @imc
END
