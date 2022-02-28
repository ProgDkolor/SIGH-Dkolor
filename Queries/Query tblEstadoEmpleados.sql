
/*
	-- GERSON SANTOS MATEO
	Esta tabla fue creada el 2/22/2022
*/

use NOMINA_DK
go

create table tblEstadoEmpleado
(
	id int identity,
	nombre varchar(100),
	estado int
)
go
insert into tblEstadoEmpleado values('Activo', 1), ('Inactivo', 0)