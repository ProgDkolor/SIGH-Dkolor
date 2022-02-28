
use NOMINA_MASTER
go
exec Login 'gsantos', '102030'
go
/* 
	Este inner join consulta tres tablas para capturar las rutas pero lo que se busca es capturar
	la información de las rutas.
*/
select distinct ruta.id, ruta.nombre, ruta.ruta, ruta.ModuloId, menu.permisosPorRolId from tblRutas ruta
inner join tblMenu menu on ruta.id = menu.rutaId
inner join tblPermisosUsuarios permisosUsuarios on menu.permisosPorRolId = 2
go

--select distinct ruta.ModuloId from tblRutas ruta
--inner join tblMenu menu on ruta.id = menu.rutaId
--inner join tblPermisosUsuarios permisosUsuarios on menu.permisosPorRolId = 2
--go

select * from tblRutas -- Se guardan las rutas
go
select * from tblModulo -- se guardan los modulos
go
select * from tblMenu -- se guardan la rutaId y permisosPorRolId que atan al usuario a las rutas.
go

--insert into tblMenu values(1, 5),(6, 5),(2, 5)

select * from tblPermisosPorRol -- se guardan los permisos por roles asignados. | Se podria dialogar para agregar un campo llamado descripción
go
--alter table tblPermisosPorRol add IdUsuario int, C int, R int, U int, D int, CRUD int
select * from tblRol -- se guardan los roles
go
select * from tblPermisosUsuarios -- se guardan los permisos de usuarios
go
select * from NOMINA_DK..cargoEmpleados
go

select * from tblPermisosPorRol where IdUsuario = 'E33424C2-A2C6-4B01-A799-FCEF21EC1A3D'

--update tblPermisosPorRol set IdUsuario = NULL --'E33424C2-A2C6-4B01-A799-FCEF21EC1A3D'
--where id = 1
--go
--update tblPermisosPorRol set IdUsuario = NULL --'E33424C2-A2C6-4B01-A799-FCEF21EC1A3D'
--where id = 2
--go
--update tblPermisosPorRol set IdUsuario = NULL --'E33424C2-A2C6-4B01-A799-FCEF21EC1A3D'
--where id = 4
--update tblPermisosPorRol set IdUsuario = NULL --'E33424C2-A2C6-4B01-A799-FCEF21EC1A3D'
--where id = 5
