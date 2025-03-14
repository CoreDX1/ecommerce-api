--  SQL PosgtgreSQL

-- Tabla para almacenar información de los clientes
CREATE TABLE customers (
    customer_id SERIAL PRIMARY KEY,
    first_name VARCHAR(50) NOT NULL,
    last_name VARCHAR(50) NOT NULL,
    email VARCHAR(100) UNIQUE NOT NULL,
    phone_number VARCHAR(20),
    address TEXT,
    registration_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Tabla para almacenar información de las categorías de productos
CREATE TABLE categories (
    category_id SERIAL PRIMARY KEY,
    name VARCHAR(100) UNIQUE NOT NULL,
    description TEXT
);

-- Tabla para almacenar información de los productos
CREATE TABLE products (
    product_id SERIAL PRIMARY KEY,
    name VARCHAR(255) NOT NULL,
    description TEXT,
    price DECIMAL(10, 2) NOT NULL,
    stock_quantity INTEGER NOT NULL DEFAULT 0,
    category_id INTEGER REFERENCES categories(category_id),
    creation_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    last_updated TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Tabla para almacenar información de las órdenes
CREATE TABLE orders (
    order_id SERIAL PRIMARY KEY,
    customer_id INTEGER REFERENCES customers(customer_id),
    order_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    shipping_address TEXT,
    order_status VARCHAR(50) DEFAULT 'Pendiente', -- Ejemplo de estados: Pendiente, Procesando, Enviado, Entregado, Cancelado
    total_amount DECIMAL(12, 2)
);

-- Tabla para almacenar los ítems de cada orden
CREATE TABLE order_items (
    order_item_id SERIAL PRIMARY KEY,
    order_id INTEGER REFERENCES orders(order_id),
    product_id INTEGER REFERENCES products(product_id),
    quantity INTEGER NOT NULL DEFAULT 1,
    unit_price DECIMAL(10, 2) NOT NULL,
    subtotal DECIMAL(12, 2)
);

-- Tabla para almacenar información de envío (opcional, pero recomendable)
CREATE TABLE shipping (
    shipping_id SERIAL PRIMARY KEY,
    order_id INTEGER REFERENCES orders(order_id),
    shipping_date TIMESTAMP,
    tracking_number VARCHAR(100),
    shipping_carrier VARCHAR(100),
    delivery_date TIMESTAMP
);

-- Tabla para almacenar información de pagos (opcional, pero recomendable)
CREATE TABLE payments (
    payment_id SERIAL PRIMARY KEY,
    order_id INTEGER REFERENCES orders(order_id),
    payment_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    payment_method VARCHAR(50), -- Ejemplo: Tarjeta de crédito, PayPal, Transferencia bancaria
    transaction_id VARCHAR(100),
    payment_status VARCHAR(50) DEFAULT 'Pendiente' -- Ejemplo: Pendiente, Aprobado, Rechazado
);

-- Índices para mejorar el rendimiento de las consultas más comunes
CREATE INDEX idx_customers_email ON customers (email);
CREATE INDEX idx_products_name ON products (name);
CREATE INDEX idx_products_category_id ON products (category_id);
CREATE INDEX idx_orders_customer_id ON orders (customer_id);
CREATE INDEX idx_order_items_order_id ON order_items (order_id);
CREATE INDEX idx_order_items_product_id ON order_items (product_id);


-- Tabla para almacenar información de usuarios
CREATE TABLE users (
    user_id SERIAL PRIMARY KEY,
    customer_id INTEGER REFERENCES customers(customer_id) UNIQUE, -- Opcional: Si cada usuario está asociado a un cliente
    username VARCHAR(50) UNIQUE NOT NULL,
    password_hash VARCHAR(255) NOT NULL, -- Almacenar el hash de la contraseña, ¡NUNCA la contraseña en texto plano!
    email VARCHAR(100) UNIQUE NOT NULL, -- Podría ser redundante si ya está en customers, pero útil para login
    registration_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    last_login TIMESTAMP,
    is_active BOOLEAN DEFAULT TRUE,
    email_verified BOOLEAN DEFAULT FALSE,
    verification_token VARCHAR(255), -- Token para la verificación por correo electrónico
    password_reset_token VARCHAR(255), -- Token para restablecer la contraseña
    password_reset_expiry TIMESTAMP
);
