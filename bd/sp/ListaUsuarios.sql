USE [salad]
GO
/****** Object:  StoredProcedure [dbo].[ListaUsuarios]    Script Date: 7/26/2019 11:59:44 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[ListaUsuarios]
as
SELECT * from usuario where tipoUsuario!='B'
