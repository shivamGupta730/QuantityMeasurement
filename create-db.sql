-- Create Database & Table for QuantityMeasurement
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'QuantityMeasurementDB')
CREATE DATABASE QuantityMeasurementDB;

USE QuantityMeasurementDB;

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Measurements')
CREATE TABLE Measurements (
    Id int IDENTITY(1,1) PRIMARY KEY,
    Value1 float NOT NULL,
    Value2 float NULL,
    UnitType nvarchar(50) NOT NULL,
    OperationType nvarchar(50) NOT NULL,
    Result float NOT NULL,
    CreatedDate datetime2 DEFAULT GETDATE()
);

PRINT ' QuantityMeasurementDB ready!';
