
-- Creación de la tabla Users
CREATE TABLE Users (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Role INT NOT NULL
);
GO

-- Creación de la tabla Sales
CREATE TABLE Sales (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Region NVARCHAR(100) NOT NULL,
    Amount DECIMAL(18,2) NOT NULL,
    Date DATETIME NOT NULL
);
GO

-- Inserción de datos en Users
INSERT INTO Users (Name, Role) VALUES ('Ana', 0);     -- 0 para Analyst
INSERT INTO Users (Name, Role) VALUES ('Carlos', 1);  -- 1 para Manager
INSERT INTO Users (Name, Role) VALUES ('María', 2);   -- 2 para Director
GO

-- Inserción de datos en Sales
INSERT INTO Sales (Region, Amount, Date) VALUES ('Norte', 10000.00, '2023-01-01');
INSERT INTO Sales (Region, Amount, Date) VALUES ('Sur', 15000.00, '2023-02-01');
INSERT INTO Sales (Region, Amount, Date) VALUES ('Este', 20000.00, '2023-03-01');
INSERT INTO Sales (Region, Amount, Date) VALUES ('Oeste', 25000.00, '2023-04-01');
GO

