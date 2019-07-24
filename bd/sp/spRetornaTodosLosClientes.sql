USE [salad]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create procedure dbo.spRetornaTodosLosClientes
as
begin
select 
	[cedula],
	[nombre],
	[apellido1],
	[apellido2],
	[genero],
	[telefono1],
	[correo],
	[fechaNac],
	[estatura],
	[peso],
	[proposito],
	[usuario]
from [dbo].[usuario] where [tipoUsuario]= 'C'
end