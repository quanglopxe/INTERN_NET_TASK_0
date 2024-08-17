-- T?o c? s? d? li?u
CREATE DATABASE QL_XUONGMAY;
USE QL_XUONGMAY;

-- B?ng Customers (Khách hàng)
CREATE TABLE Customer (
    CustomerID INT PRIMARY KEY,
    CustomerName VARCHAR(100) NOT NULL,
    Phone VARCHAR(255),
    Address VARCHAR(255)
);

-- B?ng Categories (Danh m?c s?n ph?m)
CREATE TABLE Category (
    CategoryID INT PRIMARY KEY,
    CategoryName VARCHAR(100) NOT NULL    
);

-- B?ng Products (S?n ph?m)
CREATE TABLE Product (
    ProductID INT PRIMARY KEY,
    ProductName VARCHAR(100) NOT NULL,
    CategoryID INT,
    Price DECIMAL(10, 2) NOT NULL,
    Description TEXT,
    FOREIGN KEY (CategoryID) REFERENCES Category(CategoryID)
);

-- B?ng Orders (??n hàng)
CREATE TABLE Orders (
    OrderID INT PRIMARY KEY,
    OrderDate DATE NOT NULL,
    CustomerID INT,
    TotalQuantity INT NOT NULL,
    Status VARCHAR(50),
    DeliveryDate DATE,
    FOREIGN KEY (CustomerID) REFERENCES Customer(CustomerID)
);



-- B?ng Stages (Công ?o?n)
CREATE TABLE Stage (
    StageID INT PRIMARY KEY ,
    StageName VARCHAR(100) NOT NULL,
    Description TEXT,
    Sequence INT NOT NULL
);

-- B?ng Supervisors (Tr??ng chuy?n)
CREATE TABLE Supervisor (
    SupervisorID INT PRIMARY KEY ,
    SupervisorName VARCHAR(100) NOT NULL,
    LineID INT
);

-- B?ng OrderDetail (Chi ti?t ??n hàng)
CREATE TABLE OrderDetail (    
    OrderID INT,
    ProductID INT,
	SupervisorID INT,
    Quantity INT NOT NULL,
    Price DECIMAL(10, 2) NOT NULL,
    TotalPrice DECIMAL(10, 2),
    FOREIGN KEY (OrderID) REFERENCES Orders(OrderID),
    FOREIGN KEY (ProductID) REFERENCES Product(ProductID),
	FOREIGN KEY (SupervisorID) REFERENCES Supervisor(SupervisorID)
);

-- B?ng ProductionLines (Chuy?n s?n xu?t)
CREATE TABLE ProductionLines (
    LineID INT PRIMARY KEY ,
    LineName VARCHAR(100) NOT NULL,
    SupervisorID INT,    
    FOREIGN KEY (SupervisorID) REFERENCES Supervisor(SupervisorID)
);

CREATE TABLE Employee (
    EmpID INT PRIMARY KEY ,
    EmpName VARCHAR(100) NOT NULL,
    Username VARCHAR(100) NOT NULL,
	Password VARCHAR(100) NOT NULL,
    LineID INT NOT NULL,
	FOREIGN KEY (LineID) REFERENCES ProductionLines(LineID)
);

-- B?ng Tasks (Nhi?m v?)
CREATE TABLE Tasks (
    TaskID INT PRIMARY KEY ,
    OrderID INT,
    StageID INT,
    AssignedTo INT,
    AssignedBy INT,
    Status VARCHAR(50) NOT NULL,
    StartTime DATETIME,
    EndTime DATETIME,
    Remarks TEXT,
    FOREIGN KEY (OrderID) REFERENCES Orders(OrderID),
    FOREIGN KEY (StageID) REFERENCES Stage(StageID),
    FOREIGN KEY (AssignedTo) REFERENCES Employee(EmpID),
    FOREIGN KEY (AssignedBy) REFERENCES Supervisor(SupervisorID)
);
