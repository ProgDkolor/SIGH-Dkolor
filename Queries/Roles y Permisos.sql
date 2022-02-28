

select * from usuarios
go

--create table tblEstadoUsuario
--(
--	id int unique identity,
--	nombre varchar(100),
--	estado int
--)
--go

--create table tblRol
--(
--	id int unique identity,
--	nombre varchar(max) not null
--)
--go

--create table tblPermisosUsuarios
--(
--	id int unique identity,
--	nombre varchar(max),
--	descripcion varchar(max)
--)
--go

--create table tblPermisosPorRol
--(
--	id int unique identity,
--	rolUsuarioid int,
--	permisoUsuarioId int,
--	cargoEmpleadoId int
--)
--go

--create table tblRutas
--(
--	id int unique identity,
--	nombre varchar(max),
--	ruta varchar(max)
--)

--alter table tblRutas add ModuloId int
--go

--create table tblMenu
--(
--	id int unique identity,
--	rutaId int,
--	permisosPorRolId int
--)

create table tblModulo
(
	id int identity,
	nombre varchar(100)
)

select * from tblEstadoUsuario
select * from tblRol
select * from tblPermisosUsuarios
select * from tblPermisosPorRol
select * from tblRutas
select * from tblMenu

--update usuarios set rolUsuarioId = 1
--where id = 'E33424C2-A2C6-4B01-A799-FCEF21EC1A3D'