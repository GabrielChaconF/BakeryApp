
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

create table RecuperarContra(
	IdRecuperacion int not null auto_increment,
	IdPersona int not null,
    CodigoRecuperacion varchar(80) not null,
    FechaExpiracion datetime not null,
    constraint pk_id_recuperar_contra primary key (IdRecuperacion),
    constraint fk_id_usuario_contra foreign key(IdPersona) references Personas(IdPersona)
);

create table Provincias(
	IdProvincia int not null auto_increment,
    NombreProvincia varchar(20) not null,
    constraint pk_provincias primary key (IdProvincia)
);

create table Cantones(
	IdCanton int not null auto_increment,
    NombreCanton varchar(25) not null,
    IdProvincia int not null,
    constraint pk_canton primary key (IdCanton),
    constraint fk_canton_provincia foreign key(IdProvincia) references Provincias(IdProvincia)
);

create table Distritos(
	IdDistrito int not null auto_increment,
    NombreDistrito varchar(50) not null,
    IdCanton int not null,
    constraint pk_distrito primary key (IdDistrito),
    constraint fk_distrito_canton foreign key(IdCanton) references Cantones(IdCanton)
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
 
/* Creacion Usuario admin */

insert into Personas(Nombre, PrimerApellido, SegundoApellido, Correo, Contra, Telefono, IdRol)
values ('admin', 'admin', 'admin', 'adminbakery@gmail.com', '$2a$11$VN60QaOYMoc1i4/8K3gMMe1TFjAkg/6bNCK0kwr.NudfxKAsda9Iy', '0000-0000', 1);
 
/* Creacion Provincias */

insert into Provincias(NombreProvincia)
values ('San José'),
('Alajuela'),
('Heredia'),
('Cartago');

/* Creacion cantones */

insert into Cantones (NombreCanton, IdProvincia) VALUES
('Barva', 3),
('Flores', 3),
('Heredia', 3),
('San Isidro', 3),
('San Pablo', 3),
('San Rafael', 3),
('Santo Domingo', 3),
('Santa Barbara', 3),
('Belén', 3),
('Alajuela', 2),
('Grecia', 2),
('Atenas', 2),
('Cartago', 4),
('Paraíso', 4),
('San José', 1),
('Escazú', 1),
('Desamparados', 1),
('Goicoechea', 1),
('Santa Ana', 1),
('Alajuelita', 1),
('Moravia', 1),
('Tibás', 1),
('Curridabat', 1),
('Montes de Oca', 1);

/* Creacion distritos*/

insert into Distritos (IdCanton, NombreDistrito) values
-- Barva
(1, 'Barva'),
(1, 'San Pedro'),
(1, 'San Pablo'),
(1, 'San Roque'),
(1, 'Santa Lucía'),
(1, 'San José de la Montaña'),
(1, 'Puente Salas'),
-- Flores
(2, 'San Joaquín'),
(2, 'Barrantes'),
(2, 'Llorente'),
-- Heredia
(3, 'Heredia'),
(3, 'Mercedes'),
(3, 'San Francisco'),
(3, 'Ulloa'),
(3, 'Varablanca'),
-- San Isidro
(4, 'San Isidro'),
(4, 'San José'),
(4, 'Concepción'),
(4, 'San Francisco'),
-- San Pablo
(5, 'San Pablo'),
(5, 'Santa Lucía'),
(5, 'San Roque'),
-- San Rafael
(6, 'San Rafael'),
(6, 'San Josecito'),
(6, 'Santiago'),
(6, 'Angeles'),
(6, 'Concepción'),
-- Santo Domingo
(7, 'Santo Domingo'),
(7, 'San Vicente'),
(7, 'San Miguel'),
(7, 'Paracito'),
(7, 'Santo Tomás'),
(7, 'Santa Rosa'),
(7, 'Tures'),
(7, 'Pará'),
-- Santa Bárbara
(8, 'Santa Bárbara'),
(8, 'San Pedro'),
(8, 'San Juan'),
(8, 'Jesús'),
(8, 'Santo Domingo'),
(8, 'Purabá'),
-- Belén
(9, 'San Antonio'),
(9, 'La Ribera'),
(9, 'La Asunción'),
-- Alajuela
(10, 'Alajuela'),
(10, 'San José'),
(10, 'Carrizal'),
(10, 'San Antonio'),
(10, 'Guácima'),
(10, 'San Isidro'),
(10, 'San Rafael'),
(10, 'Rio Segundo'),
(10, 'Desamparados'),
(10, 'Turrúcares'),
(10, 'Tambor'),
(10, 'La Garita'),
-- Grecia
(11, 'Grecia'),
(11, 'San Isidro'),
(11, 'San José'),
(11, 'San Roque'),
(11, 'Tacares'),
(11, 'Puente de Piedra'),
(11, 'Bolivar'),
-- Atenas
(12, 'Atenas'),
(12, 'Jesús'),
(12, 'Mercedes'),
(12, 'San Isidro'),
(12, 'Concepción'),
(12, 'San José'),
-- Cartago
(13, 'Oriental'),
(13, 'Occidental'),
(13, 'Carmen'),
(13, 'San Nicolás'),
(13, 'Aguascalientes'),
(13, 'Guadalupe'),
(13, 'Corralillo'),
(13, 'Tierra Blanca'),
(13, 'Dulce Nombre'),
(13, 'Llano Grande'),
(13, 'Quebradilla'),
-- Paraíso
(14, 'Paraíso'),
(14, 'Santiago'),
(14, 'Orosi'),
(14, 'Cachí'),
(14, 'Llanos de Santa Lucia'),
(14, 'Birrisito'),
-- San José
(15, 'Carmen'),
(15, 'Merced'),
(15, 'Hospital'),
(15, 'Catedral'),
(15, 'Zapote'),
(15, 'San Francisco de Dos Rios'),
(15, 'La Uruca'),
(15, 'Mata Redonda'),
(15, 'Pavas'),
(15, 'Hatillo'),
(15, 'San Sebastián'),
-- Escazú
(16, 'Escazú'),
(16, 'San Antonio'),
(16, 'San Rafael'),
-- Desamparados
(17, 'Desamparados'),
(17, 'San Miguel'),
(17, 'San Juan de Dios'),
(17, 'San Rafael Arriba'),
(17, 'San Antonio'),
(17, 'Frailes'),
(17, 'Patarrá'),
(17, 'San Cristóbal'),
(17, 'Damas'),
(17, 'San Rafael Abajo'),
(17, 'Gravillas'),
(17, 'Los Guido'),
-- Goicoechea
(18, 'Guadalupe'),
(18, 'San Francisco'),
(18, 'Calle Blancos'),
(18, 'Mata de Plátano'),
(18, 'Ipís'),
(18, 'Rancho Redondo'),
(18, 'Purral'),
-- Santa Ana
(19, 'Santa Ana'),
(19, 'Salitral'),
(19, 'Pozos'),
(19, 'Uruca'),
(19, 'Piedades'),
(19, 'Brasil'),
-- Alajuelita
(20, 'Alajuelita'),
(20, 'San Josecito'),
(20, 'San  Antonio'),
(20, 'Concepción'),
(20, 'San Felipe'),
-- Moravia
(21, 'San Vicente'),
(21, 'San Jerónimo'),
(21, 'La Trinidad'),
-- Tibás
(22, 'San Juan'),
(22, 'Cinco Esquinas'),
(22, 'Anselmo Llorente'),
(22, 'León XIII'),
(22, 'Colima'),
-- Curridabat
(23, 'Curridabat'),
(23, 'Granadilla'),
(23, 'Sánchez'),
(23, 'Tirrases'),
-- Montes de Oca
(24, 'San Pedro'),
(24, 'Sabanilla'),
(24, 'Mercedes'),
(24, 'San Rafael');

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

select * from Provincias;

select * from Cantones;

select * from Distritos;

select * from RecuperarContra;


/* Consulta para ver el tamaño de la base de datos en MB */

SELECT ROUND(SUM(data_length + index_length) / 1024 / 1024, 2) AS "Database Size (MB)"

FROM information_schema.tables

WHERE table_schema = 'BakeryApp';
 




