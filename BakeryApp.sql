create database if not exists BakeryApp;

create user 'Bakery' identified by "BakeryApp.*";

grant all privileges on BakeryApp.* to 'Bakery';

use BakeryApp;


/* Creación de Tablas */
create table Categorias(
	IdCategoria int not null auto_increment,
	NombreCategoria varchar(40) not null,
    DescripcionCategoria varchar(255) not null,
    ImagenCategoria varchar(80) not null,
    constraint pk_id_categoria primary key (IdCategoria),
    constraint uq_nombre_categoria unique (NombreCategoria)
);


/* Selects */
select * from Categorias;

/* Consulta para ver el tamaño de la base de datos en MB */
SELECT ROUND(SUM(data_length + index_length) / 1024 / 1024, 2) AS "Database Size (MB)"
FROM information_schema.tables
WHERE table_schema = 'BakeryApp';



