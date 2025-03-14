INSERT INTO customers (first_name, last_name, email, phone_number, address) VALUES
('Ricardo', 'Sánchez', 'ricardo.sanchez@mail.com', '555-987-6543', 'Avenida del Libertador 1000, Ciudad D'),
('Isabel', 'Romero', 'isabel.romero@net.org', '111-555-7777', 'Calle Corrientes 200, Ciudad E'),
('Gabriel', 'Díaz', 'gabriel.diaz@example.co', '333-111-9999', 'Pasaje Lavalle 300, Ciudad F'),
('Valentina', 'Suárez', 'valentina.suarez@domain.net', '777-333-5555', 'Bulevar San Martín 400, Ciudad D'),
('Martín', 'Gómez', 'martin.gomez@web.io', '999-777-3333', 'Travesía Florida 500, Ciudad E'),
('Lucía', 'Jiménez', 'lucia.jimenez@mail.org', '222-888-4444', 'Calle Reconquista 600, Ciudad F'),
('Andrés', 'Castro', 'andres.castro@net.com', '888-222-6666', 'Avenida Santa Fe 700, Ciudad D'),
('Camila', 'Vargas', 'camila.vargas@example.net', '444-999-1111', 'Pasaje Tucumán 800, Ciudad E'),
('Javier', 'Ruiz', 'javier.ruiz@domain.co', '666-444-8888', 'Bulevar Callao 900, Ciudad F'),
('Florencia', 'Alonso', 'florencia.alonso@web.org', '000-111-2222', 'Travesía Cerrito 100, Ciudad D'),
('Sebastián', 'Herrera', 'sebastian.herrera@mail.io', '123-000-4567', 'Calle Maipú 201, Ciudad E'),
('Natalia', 'Paz', 'natalia.paz@net.co', '987-321-6540', 'Avenida 9 de Julio 302, Ciudad F'),
('Tomás', 'Mendoza', 'tomas.mendoza@example.com', '555-678-9012', 'Pasaje Suipacha 403, Ciudad D'),
('Agustina', 'Flores', 'agustina.flores@domain.org', '111-999-3333', 'Bulevar Rivadavia 504, Ciudad E'),
('Manuel', 'Silva', 'manuel.silva@web.net', '333-444-7777', 'Travesía Paraguay 605, Ciudad F'),
('Josefina', 'Núñez', 'josefina.nunez@mail.co', '777-888-1111', 'Calle Florida 706, Ciudad D'),
('Lucas', 'Duran', 'lucas.duran@net.org', '999-555-2222', 'Avenida Córdoba 807, Ciudad E'),
('Emilia', 'Rojas', 'emilia.rojas@example.io', '222-111-8888', 'Pasaje Esmeralda 908, Ciudad F'),
('Santiago', 'Benítez', 'santiago.benitez@domain.com', '888-666-4444', 'Bulevar Pueyrredón 109, Ciudad D'),
('Valeria', 'Ortiz', 'valeria.ortiz@web.co', '444-000-9999', 'Travesía Juncal 200, Ciudad E');




INSERT INTO categories (name, description) VALUES
('Electrónica', 'Dispositivos electrónicos y accesorios'),
('Ropa y Accesorios', 'Prendas de vestir, calzado y complementos'),
('Libros', 'Libros de diversos géneros'),
('Hogar y Jardín', 'Artículos para el hogar, muebles y productos de jardinería'),
('Deportes y Aire Libre', 'Equipamiento deportivo y artículos para actividades al aire libre');


-- Insertar productos para la categoría 'Electrónica' (category_id = 1)
INSERT INTO products (name, description, price, stock_quantity, category_id) VALUES
('Smartphone de alta gama', 'Último modelo con cámara avanzada y gran pantalla', 999.99, 50, 1),
('Auriculares inalámbricos', 'Auriculares con cancelación de ruido y Bluetooth', 149.50, 100, 1),
('Smartwatch deportivo', 'Reloj inteligente con GPS y monitor de frecuencia cardíaca', 249.00, 75, 1),
('Tablet de 10 pulgadas', 'Tablet con buena resolución para entretenimiento y trabajo', 329.75, 60, 1);

-- Insertar productos para la categoría 'Ropa y Accesorios' (category_id = 2)
INSERT INTO products (name, description, price, stock_quantity, category_id) VALUES
('Camiseta de algodón para hombre', 'Camiseta básica de manga corta en varios colores', 19.99, 200, 2),
('Vestido de verano para mujer', 'Vestido ligero y fresco ideal para el verano', 39.95, 150, 2),
('Zapatillas deportivas unisex', 'Zapatillas cómodas para correr o entrenar', 79.00, 120, 2),
('Bolso de cuero', 'Bolso elegante y práctico para uso diario', 59.50, 90, 2);

-- Insertar productos para la categoría 'Libros' (category_id = 3)
INSERT INTO products (name, description, price, stock_quantity, category_id) VALUES
('Cien años de soledad', 'Novela clásica de Gabriel García Márquez', 12.50, 300, 3),
('El Señor de los Anillos', 'Trilogía de fantasía épica de J.R.R. Tolkien', 25.00, 250, 3),
('Sapiens: De animales a dioses', 'Ensayo sobre la historia de la humanidad de Yuval Noah Harari', 18.75, 180, 3),
('Orgullo y prejuicio', 'Novela romántica de Jane Austen', 9.99, 220, 3);

-- Insertar productos para la categoría 'Hogar y Jardín' (category_id = 4)
INSERT INTO products (name, description, price, stock_quantity, category_id) VALUES
('Juego de sábanas de algodón', 'Juego de sábanas suaves y confortables para cama doble', 45.00, 100, 4),
('Mesa de centro para salón', 'Mesa de madera moderna para el centro de la sala de estar', 89.99, 50, 4),
('Set de herramientas de jardinería', 'Set básico de herramientas para el cuidado del jardín', 32.50, 80, 4),
('Maceta de cerámica grande', 'Maceta decorativa para plantas de interior o exterior', 22.00, 120, 4);

-- Insertar productos para la categoría 'Deportes y Aire Libre' (category_id = 5)
INSERT INTO products (name, description, price, stock_quantity, category_id) VALUES
('Bicicleta de montaña', 'Bicicleta con suspensión delantera ideal para terrenos irregulares', 399.00, 40, 5),
('Pelota de fútbol profesional', 'Pelota de reglamento para partidos y entrenamientos', 29.99, 150, 5),
('Tienda de campaña para 4 personas', 'Tienda resistente al agua ideal para camping', 129.75, 60, 5),
('Mochila de senderismo de 50 litros', 'Mochila cómoda y espaciosa para excursiones de un día', 65.00, 90, 5);