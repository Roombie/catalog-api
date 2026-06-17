-- ============================================================
--  Catálogo / Inventario - Esquema SQL Server
--  Ejecuta este script una vez en tu instancia de SQL Server
--  (SSMS, Azure Data Studio, o sqlcmd).
-- ============================================================

-- 1) Crear la base de datos (opcional, si no la creas con EF migrations)
IF DB_ID('CatalogDb') IS NULL
    CREATE DATABASE CatalogDb;
GO

USE CatalogDb;
GO

-- ============================================================
--  TABLAS
--  NOTA: si usas EF Core Migrations, EF puede crear estas tablas
--  por ti. Mantén este script como referencia y para el SP.
-- ============================================================

IF OBJECT_ID('dbo.Products', 'U') IS NOT NULL DROP TABLE dbo.Products;
IF OBJECT_ID('dbo.Categories', 'U') IS NOT NULL DROP TABLE dbo.Categories;
IF OBJECT_ID('dbo.Users', 'U') IS NOT NULL DROP TABLE dbo.Users;
GO

CREATE TABLE dbo.Categories (
    Id        INT IDENTITY(1,1) PRIMARY KEY,
    Name      NVARCHAR(100) NOT NULL UNIQUE,
    CreatedAt DATETIME2     NOT NULL DEFAULT SYSUTCDATETIME()
);
GO

CREATE TABLE dbo.Products (
    Id         INT IDENTITY(1,1) PRIMARY KEY,
    Name       NVARCHAR(150) NOT NULL,
    Sku        NVARCHAR(50)  NOT NULL UNIQUE,
    CategoryId INT           NOT NULL,
    Price      DECIMAL(10,2) NOT NULL,
    Stock      INT           NOT NULL DEFAULT 0,
    IsActive   BIT           NOT NULL DEFAULT 1,
    CreatedAt  DATETIME2     NOT NULL DEFAULT SYSUTCDATETIME(),
    CONSTRAINT FK_Products_Categories
        FOREIGN KEY (CategoryId) REFERENCES dbo.Categories(Id)
);
GO

CREATE TABLE dbo.Users (
    Id           INT IDENTITY(1,1) PRIMARY KEY,
    Username     NVARCHAR(50)  NOT NULL UNIQUE,
    PasswordHash NVARCHAR(MAX) NOT NULL,   -- lo genera la API (BCrypt), nunca texto plano
    Role         NVARCHAR(20)  NOT NULL DEFAULT 'Admin',
    CreatedAt    DATETIME2     NOT NULL DEFAULT SYSUTCDATETIME()
);
GO

-- ============================================================
--  DATOS DE PRUEBA (seed)
--  IMPORTANTE: el usuario admin NO se siembra aquí, porque el
--  PasswordHash lo debe generar tu API con BCrypt para que el
--  login funcione. Crea el admin con tu endpoint /api/auth/register
--  la primera vez (o con un seeder en Program.cs). Ver BUILD_GUIDE.md.
-- ============================================================

INSERT INTO dbo.Categories (Name) VALUES
    (N'Accesorios'),
    (N'Bisutería'),
    (N'Bolsos');
GO

INSERT INTO dbo.Products (Name, Sku, CategoryId, Price, Stock) VALUES
    (N'Aretes de perla',        N'ACC-001', 1, 350.00, 12),
    (N'Collar minimalista',     N'BIS-001', 2, 480.00,  3),
    (N'Pulsera trenzada',       N'BIS-002', 2, 220.00, 25),
    (N'Bolso de mano cuero',    N'BOL-001', 3, 1850.00, 2),
    (N'Diadema floral',         N'ACC-002', 1, 180.00,  0);
GO

-- ============================================================
--  STORED PROCEDURE: reporte de bajo stock
--  Hace JOIN entre Products y Categories y devuelve los
--  productos cuyo stock está por debajo del umbral.
--  Esto es lo que respalda "SQL Server + SP" en tu CV.
-- ============================================================

IF OBJECT_ID('dbo.sp_LowStockReport', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_LowStockReport;
GO

CREATE PROCEDURE dbo.sp_LowStockReport
    @Threshold INT = 5
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        p.Id           AS ProductId,
        p.Name         AS ProductName,
        p.Sku          AS Sku,
        c.Name         AS CategoryName,
        p.Stock        AS Stock,
        p.Price        AS Price
    FROM dbo.Products  AS p
    INNER JOIN dbo.Categories AS c ON c.Id = p.CategoryId
    WHERE p.IsActive = 1
      AND p.Stock <= @Threshold
    ORDER BY p.Stock ASC, p.Name ASC;
END
GO

-- Prueba rápida:
-- EXEC dbo.sp_LowStockReport @Threshold = 5;
