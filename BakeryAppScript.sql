
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
 
 create table Productos(
	IdProducto int not null auto_increment,
	NombreProducto varchar(40) not null,
    DescripcionProducto varchar(255) not null,
    PrecioProducto varchar(80) not null,
    IdCategoria int not null, 
    RecetaProducto varchar(80) not null,
    ImagenProducto varchar(80) not null,
    constraint pk_id_Producto primary key (IdProducto),
    constraint fk_id_Categoria foreign key (IdCategoria) references Categorias(IdCategoria),
    constraint uq_nombre_Producto unique (NombreProducto)
);

create table Ingredientes(
	IdIngrediente int not null auto_increment,
    NombreIngrediente varchar(50) not null,
    DescripcionIngrediente varchar(100) not null,
    CantidadIngrediente decimal(10,2) not null,
    UnidadMedidaIngrediente varchar(50) not null,
    PrecioUnidadIngrediente decimal(10,2) not null,
    FechaCaducidadIngrediente date,
    constraint pk_id_Ingrediente primary key (IdIngrediente)
);