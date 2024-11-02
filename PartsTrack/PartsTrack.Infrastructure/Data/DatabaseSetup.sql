  -- Create Parts table
CREATE TABLE Parts (
    PartId INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    Description NVARCHAR(255),
    Price DECIMAL(10, 2) NOT NULL,
    StockQuantity INT NOT NULL
);

-- Create Warehouses table
CREATE TABLE Warehouses (
    WarehouseId INT PRIMARY KEY IDENTITY(1,1),
    Location NVARCHAR(100) NOT NULL,
    Capacity INT NOT NULL
);

-- Create Inventory table
CREATE TABLE Inventory (
    InventoryId INT PRIMARY KEY IDENTITY(1,1),
    PartId INT NOT NULL,
    WarehouseId INT NOT NULL,
    Quantity INT NOT NULL,
    FOREIGN KEY (PartId) REFERENCES Parts(PartId),
    FOREIGN KEY (WarehouseId) REFERENCES Warehouses(WarehouseId)
);

-- Optional: Insert sample data into Parts
INSERT INTO Parts (Name, Description, Price, StockQuantity) VALUES 
('Brake Pad', 'High-quality brake pad', 29.99, 100),
('Oil Filter', 'Standard oil filter', 7.99, 200),
('Spark Plug', 'Premium spark plug', 4.99, 300);

-- Optional: Insert sample data into Warehouses
INSERT INTO Warehouses (Location, Capacity) VALUES 
('New York', 1000),
('Los Angeles', 1500),
('Chicago', 1200);

-- Optional: Insert sample data into Inventory
INSERT INTO Inventory (PartId, WarehouseId, Quantity) VALUES 
(1, 1, 50),
(2, 2, 100),
(3, 3, 150);
