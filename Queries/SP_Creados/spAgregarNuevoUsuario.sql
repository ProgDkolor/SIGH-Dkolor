
/*
	Gerson Santos Mateo 21-2-2022

	Este SP fue creado para almacenar los nuevos usuarios.
*/

use NOMINA_MASTER
go

create procedure spAgregarNuevoUsuario
(
	@usuario varchar(50),
	@nombre varchar(100),
	@apellido varchar(100),
	@clave varchar(max),
	@perfil int,
	@estado int,
	@cedula varchar(11),
	@modulo int,
	@cargoEmpleadoId int,
	@rolUsuarioId int
)
as
begin
	insert into usuarios(id, usuario, nombre, apellido, clave, perfil, fecha, estado, cedula, modulo, cargoEmpleadosId, rolUsuarioId)
	values (NEWID(), @usuario, @nombre, @apellido, HASHBYTES('SHA2_256',@clave), @perfil, GETDATE(), @estado, @cedula, @modulo, @cargoEmpleadoId, @rolUsuarioId)

	select top 1 * from usuarios order by fecha desc
end
go

select * from usuarios
go

--------------------------  usuario,		nombre,		   apellido,	  clave,  perfil,     estado,	cedula,		    modulo,  cargo,  rolUsuarioId
--exec spAgregarNuevoUsuario 'GSandler', 'Grazy Sarer', 'Sandler Castillo', '1234', 1,          1,		'22300585993',  1,       7,      2
--delete from usuarios where cedula like '%22300585993%'
go