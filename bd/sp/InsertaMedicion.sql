USE [salad]
GO
/****** Object:  StoredProcedure [dbo].[InsertaMedicion]    Script Date: 7/22/2019 3:10:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[InsertaMedicion]
	@idUsuario int,
	@peso real,
	@grasa real,
	@musculo real,
	@agua real,
	@hueso real,
	@observaciones varchar(100),
	@imc decimal(5,2)
AS
BEGIN
	Insert into medicion (idUsuario,peso,grasa,musculo,agua,hueso,observaciones,imc,fecha)
	Values (@idUsuario,@peso,@grasa,@musculo,@agua,@hueso,@observaciones,@imc,GETDATE())
END
