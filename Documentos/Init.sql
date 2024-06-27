create database papeleria;
use Papeleria;

select * from Encargados;
select * from Articulos;
select * from StockArticulos;
select * from TiposMovimiento;

-- PRECARGA DE ENCARGADOS
INSERT INTO Encargados (Nombre, Apellido, Email, Contrase�a) VALUES
('admin', 'admin', 'admin@admin.com', 'Admin1!'),
('Diego', 'G�mez', 'diegogomez@gmail.com', 'Admin1!'),
('Mar�a', 'L�pez', 'marialopez@gmail.com', 'Admin1!'),
('Javier', 'Rodr�guez', 'javierrodriguez@gmail.com', 'Admin1!'),
('Valentina', 'P�rez', 'valentinaperez@gmail.com', 'Admin1!'),
('Sebasti�n', 'Garc�a', 'sebastiangarcia@gmail.com', 'Admin1!'),
('Camila', 'Fern�ndez', 'camilafernandez@gmail.com', 'Admin1!'),
('Mart�n', 'Silva', 'martinsilva@gmail.com', 'Admin1!'),
('Luc�a', 'Gonz�lez', 'luciagonzalez@gmail.com', 'Admin1!'),
('Mateo', 'Mart�nez', 'mateomartinez@gmail.com', 'Admin1!'),
('Ana', 'Rojas', 'anarojas@gmail.com', 'Admin1!');

INSERT INTO TiposMovimiento (Nombre, EsAumento)
    VALUES 
        ('Venta', 0),
        ('Devolucion', 1),
        ('Compra', 1);

-- PRECARGA DE ARTICULOS
INSERT INTO Articulos (Nombre, Descripcion, Codigo, Precio) VALUES
('Block de post it', 'mayor a 5 menor a 300', 'ABC1234567890', 250.00),
('Lapicera BIC', 'Tinta azul', 'DEF1234567890', 30.00),
('Cuaderno A4', '80 hojas cuadriculado', 'GHI1234567890', 120.00),
('Carpeta archivadora', 'Anillada, tama�o A4', 'JKL1234567890', 450.00),
('Corrector l�quido', '15ml', 'MNO1234567890', 60.00),
('Resaltador amarillo', 'Tinta fluorescente', 'PQR1234567890', 40.00),
('Marcador permanente', 'Negro, punta fina', 'STU1234567890', 35.00),
('Regla 30cm', 'Pl�stico transparente', 'VWX1234567890', 25.00),
('Calculadora cient�fica', '120 funciones', 'YZA1234567890', 850.00),
('Goma de borrar', 'Blanca, tama�o grande', 'BCD1234567890', 20.00),
('Sacapuntas', 'Con dep�sito', 'EFG1234567890', 15.00),
('Tijeras escolares', 'Punta redonda', 'HIJ1234567890', 45.00),
('Cartuchera', 'Con varios compartimentos', 'KLM1234567890', 150.00),
('Pegamento en barra', '20g', 'NOP1234567890', 35.00),
('Hojas A4', '500 hojas, 80g/m2', 'QRS1234567890', 300.00),
('Tijeras de oficina', 'Punta afilada', 'TUV1234567890', 60.00),
('Bolsa pl�stica', 'Tama�o carta', 'WXY1234567890', 10.00),
('Perforadora de papel dos agujeros', '2 agujeros', 'ZAB1234567890', 100.00),
('Notas adhesivas', '100 hojas', 'CDE1234567890', 45.00),
('Rotulador', 'Negro, punta gruesa', 'FGH1234567890', 50.00),
('Cinta adhesiva', 'Transparente, 50m', 'IJK1234567890', 70.00),
('Libro contable', '200 p�ginas, rayado', 'LMN1234567890', 400.00),
('Cuaderno espiral', 'A4, 100 hojas', 'OPQ1234567890', 150.00),
('Folder manila', 'Tama�o carta, pack 10', 'RST1234567890', 80.00),
('Bloc de notas', 'A5, 50 hojas', 'UVW1234567890', 90.00),
('Borrador pizarra', 'Para marcadores', 'XYZ1234567890', 55.00),
('Pizarr�n blanco', '40x60cm', 'ABC2345678901', 500.00),
('Marcadores de pizarra', 'Pack de 4 colores', 'DEF2345678901', 100.00),
('Libreta de apuntes', 'Pocket, 50 hojas', 'GHI2345678901', 40.00),
('Cinta correctora', '8m', 'JKL2345678901', 25.00),
('Caja organizadora', 'Pl�stica, tama�o mediano', 'MNO2345678901', 200.00),
('Mochila escolar', 'Varios bolsillos', 'PQR2345678901', 950.00),
('Bol�grafo multicolor', '4 colores en 1', 'STU2345678901', 65.00),
('Cartucho de tinta', 'Para impresora HP', 'VWX2345678901', 600.00),
('Portaminas', '0.7mm', 'YZA2345678901', 35.00),
('Grampa met�lica', 'Para engrapadora', 'BCD2345678901', 20.00),
('Engrapadora', 'Tama�o est�ndar', 'EFG2345678901', 150.00),
('Archivador', 'A-Z, 12 secciones', 'HIJ2345678901', 300.00),
('Libreta de direcciones', 'Tama�o bolsillo', 'KLM2345678901', 50.00),
('Puntero l�ser', 'Con linterna', 'NOP2345678901', 400.00),
('Bol�grafo azul', 'Tinta gel, 0.5mm', 'OPQ2345678901', 25.00),
('Cinta de embalaje', 'Ancho 48mm, 50m', 'RST2345678901', 70.00),
('Lapicera de lujo', 'Cuerpo met�lico', 'UVW2345678901', 300.00),
('Archivador de palanca', 'A4, 5cm espina', 'XYZ2345678901', 120.00),
('Bolsa de polipropileno', 'Transparente, 30x40cm', 'ABC3456789012', 15.00),
('Etiqueta adhesiva pack 100', 'Pack 100, 70x25mm', 'DEF3456789012', 45.00),
('Papel fotogr�fico', 'A4, 100 hojas, 200g', 'GHI3456789012', 500.00),
('Marcador borrable', 'Pack de 6 colores', 'JKL3456789012', 90.00),
('Cinta adhesiva doble faz', '1cm x 10m', 'MNO3456789012', 60.00),
('Papel crep�', 'Varios colores, 50x200cm', 'PQR3456789012', 20.00),
('Sobres manila', 'Tama�o oficio, pack 10', 'STU3456789012', 50.00),
('Bolsa de burbujas', '30x40cm, pack 10', 'VWX3456789012', 75.00),
('Papel manteca', '50 hojas, A4', 'YZA3456789012', 40.00),
('Portadocumentos', 'A4, pl�stico', 'BCD3456789012', 25.00),
('Separadores de carpetas', 'Pack de 5 colores', 'EFG3456789012', 35.00),
('Cinta de embalaje marr�n', 'Marr�n, 48mm x 50m', 'HIJ3456789012', 65.00),
('Etiquetas impresoras', 'A4, pack de 100 hojas', 'KLM3456789012', 250.00),
('Notas autoadhesivas', 'Pack de 6 colores', 'NOP3456789012', 80.00),
('Tablero de corcho', '60x90cm', 'QRS3456789012', 350.00),
('Estuche de l�pices', 'Con cremallera', 'TUV3456789012', 100.00),
('Calendario de escritorio', '2024', 'WXY3456789012', 150.00),
('Bol�grafo de gel', 'Pack de 5 colores', 'ZAB3456789012', 70.00),
('Caja de archivo', 'Cart�n, tama�o carta', 'CDE3456789012', 40.00),
('Planificador semanal', 'A4, 52 hojas', 'FGH3456789012', 180.00),
('Cuaderno universitario', 'A4, 200 hojas, rayado', 'IJK3456789012', 250.00),
('Papel de aluminio', '30cm x 50m', 'LMN3456789012', 80.00),
('Goma EVA', 'Pack de 10 colores', 'OPQ3456789012', 60.00),
('Perforadora de papel tres agujeros', '3 agujeros', 'RST3456789012', 130.00),
('Rotulador permanente', 'Rojo, punta media', 'UVW3456789012', 40.00),
('Portaminas met�lico', '0.5mm', 'XYZ3456789012', 55.00),
('Carpeta de presentaci�n', 'Con ventana, A4', 'ABC4567890123', 35.00),
('Tijeras multiusos', '20cm, acero inoxidable', 'DEF4567890123', 95.00),
('Estuche r�gido', 'Para l�pices, con cremallera', 'GHI4567890123', 85.00),
('Calculadora b�sica', '8 d�gitos, solar', 'JKL4567890123', 120.00),
('Pegamento l�quido', '30ml', 'MNO4567890123', 20.00),
('Bol�grafo retr�ctil', 'Tinta azul, 1.0mm', 'PQR4567890123', 30.00),
('Separadores alfab�ticos', 'A-Z, tama�o carta', 'STU4567890123', 55.00),
('Clip sujetapapeles', 'Pack de 100, 32mm', 'VWX4567890123', 15.00),
('Carpeta colgante', 'Tama�o carta, verde', 'YZA4567890123', 70.00),
('Etiqueta adhesiva pack 50', 'Pack 50, 105x48mm', 'BCD4567890123', 40.00),
('Papel reciclado', 'A4, 500 hojas, 80g', 'EFG4567890123', 350.00);

-- PRECARGA DE MOVIMIENTOS DE STOCK
DECLARE @fechaActual DATETIME = GETDATE();

-- Obtener IDs de Articulos (limitando a 10 para evitar combinaciones excesivas)
DECLARE @ArticulosIds TABLE (
    ArticuloId INT
);

INSERT INTO @ArticulosIds (ArticuloId)
SELECT TOP 10 ArticuloId
FROM Articulos
ORDER BY NEWID();

-- Obtener IDs de TiposMovimiento (limitando a 3 para evitar combinaciones excesivas)
DECLARE @TiposMovimientoIds TABLE (
    TipoMovimientoId INT
);

INSERT INTO @TiposMovimientoIds (TipoMovimientoId)
SELECT TOP 3 TipoMovimientoId
FROM TiposMovimiento
ORDER BY NEWID();

-- Obtener emails
DECLARE @Emails TABLE (
    Email varchar(500)
);

INSERT INTO @Emails (Email)
SELECT TOP 10 Email
FROM Encargados
ORDER BY NEWID();

-- Generar movimientos de stock aleatorios (limitando a 30 registros)
INSERT INTO StockArticulos(ArticuloId, EmailEncargado, Fecha, CantidadMovida, TipoMovimientoId)
SELECT TOP 30
    ai.ArticuloId,
    e.Email,
    @fechaActual AS Fecha,
    ABS(CHECKSUM(NEWID())) % 29 + 1 AS CantidadMovida,
    tmi.TipoMovimientoId
FROM @ArticulosIds ai
CROSS JOIN @TiposMovimientoIds tmi
CROSS JOIN @Emails e
ORDER BY NEWID(); -- Ordenar aleatoriamente para obtener 30 combinaciones diferentes
