
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

create table UnidadesMedida(
	IdUnidad int not null auto_increment,
	NombreUnidad varchar(50) not null,
    constraint pk_id_unidad_medida primary key (IdUnidad)
);

create table Ingredientes(
	IdIngrediente int not null auto_increment,
    NombreIngrediente varchar(50) not null,
    DescripcionIngrediente varchar(100) not null,
    CantidadIngrediente int not null,
    UnidadMedidaIngrediente int not null,
    PrecioUnidadIngrediente decimal(10,2) not null,
    FechaCaducidadIngrediente date not null,
    constraint pk_id_Ingrediente primary key (IdIngrediente),
    constraint uk_nombre_ingrediente unique (NombreIngrediente),
    constraint fk_unidad_ingrediente foreign key (UnidadMedidaIngrediente) references UnidadesMedida(IdUnidad)
);

create table Recetas(
	IdReceta int not null auto_increment,
    NombreReceta varchar(50) not null,
    Instrucciones longtext not null,
    constraint pk_id_receta primary key (IdReceta),
    constraint uk_nombre_receta unique (NombreReceta)
);

create table IngredientesRecetas(
	IdIngrediente int not null,
    IdReceta int not null,
    constraint pk_id_ingredientes_recetas primary key (IdIngrediente, IdReceta),
    constraint fk_id_ingrediente_rec foreign key(IdIngrediente) references Ingredientes(IdIngrediente) on delete cascade,
    constraint fk_id_receta_rec foreign key(IdReceta) references Recetas(IdReceta) on delete cascade
);

create table Productos(
	IdProducto int not null auto_increment,
	NombreProducto varchar(40) not null,
    DescripcionProducto varchar(255) not null,
    PrecioProducto decimal(10,2) not null,
    IdCategoria int not null, 
    IdReceta int not null,
    ImagenProducto varchar(80) not null,
    constraint pk_id_Producto primary key (IdProducto),
    constraint fk_id_Categoria foreign key (IdCategoria) references Categorias(IdCategoria) on delete cascade,
    constraint fk_id_receta foreign key (IdReceta) references Recetas(IdReceta) on delete cascade,
    constraint uq_nombre_Producto unique (NombreProducto)
);

create table Roles(	
	IdRol int not null,
    NombreRol varchar(20) not null,
    constraint pk_id_rol primary key (IdRol)
);

create table Personas(
	IdPersona int not null auto_increment,
    Nombre varchar(25) not null,
    PrimerApellido varchar(25) not null,
    SegundoApellido varchar(25) not null,
    Correo varchar(80) not null,
    Contra varchar(100) not null,
    Telefono varchar(13) not null,
    CodigoRecuperacion varchar(20),
    IdRol int not null,
    constraint pk_id_persona primary key(IdPersona),
    constraint per_fk_id_rol foreign key(IdRol) references Roles(IdRol),
    constraint uk_correo_per unique (Correo),
    constraint uk_telefono_per unique(telefono)
);

create table Marketing(
	IdMarketing int not null auto_increment,
	Nombre varchar(25) not null,
    Correo varchar(80) not null,
    constraint pk_id_marketing primary key(IdMarketing)
);

/* Creacion de Roles */
insert into Roles(IdRol, NombreRol)
values (1, 'ADMINISTRADOR'),
('2', 'EMPLEADO'),
('3', 'CLIENTE');
 
/* Creacion unidades de medida */

INSERT INTO UnidadesMedida (NombreUnidad) VALUES
('No tiene'),
('Gramos'),
('Mililitros'),
('Litros'),
('Tazas'),
('Cucharadas'),
('Cucharaditas'),
('Onzas'),
('Libras'),
('Kilogramos'),
('Litros'),
('Pulgadas'),
('Centímetros'),
('Metros'),
('Pies'),
('Pulgadas cuadradas'),
('Pies cuadrados'),
('Pulgadas cúbicas'),
('Pies cúbicos'),
('Miligramos'),
('Microgramos'),
('Galones'),
('Cuartos'),
('Pintas'),
('Cucharas'),
('Cucharaditas'),
('Tazas'),
('Cucharadas de sopa'),
('Libras de peso'),
('Onzas líquidas'),
('Unidades'),
('Paquetes'),
('Bolsas'),
('Tiras'),
('Granos'),
('Barras'),
('Rollos'),
('Envases'),
('Tabletas'),
('Palets'),
('Botellas'),
('Ampollas'),
('Sacos'),
('Latas'),
('Sobres'),
('Tubos'),
('Placas'),
('Pliegos'),
('Cartuchos');
 
/* Selects */

select * from UnidadesMedida;


select * from Categorias;

select * from Roles;

select * from Personas;
 
 select * from Ingredientes;
 
 select * from Recetas;

select * from ingredientesRecetas;

select * from Productos;

select * from Marketing;


/* Consulta para ver el tamaño de la base de datos en MB */

SELECT ROUND(SUM(data_length + index_length) / 1024 / 1024, 2) AS "Database Size (MB)"

FROM information_schema.tables

WHERE table_schema = 'BakeryApp';
 




