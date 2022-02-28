
use NOMINA_MASTER
go

exec Login 'gsantos', '102030'
go

select * from tblRol
go

--ADMIN
--update usuarios set rolUsuarioId = 1
--where id = 'E33424C2-A2C6-4B01-A799-FCEF21EC1A3D'
--go

--RRHH Encargado.
--update usuarios set rolUsuarioId = 2
--where id = 'E33424C2-A2C6-4B01-A799-FCEF21EC1A3D'
--go

--RRHH Auxiliar.
--update usuarios set rolUsuarioId = 3
--where id = 'E33424C2-A2C6-4B01-A799-FCEF21EC1A3D'
--go

--RRHH Auxiliar.
update usuarios set rolUsuarioId = 3
where id = 'E33424C2-A2C6-4B01-A799-FCEF21EC1A3D'
go