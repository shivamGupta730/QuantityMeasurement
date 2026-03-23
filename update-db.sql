-- Update DB Schema for UC16
USE QuantityMeasurementDB;

IF EXISTS (SELECT * FROM sys.tables WHERE name = 'Measurements')
DROP TABLE Measurements;

CREATE TABLE Measurements (
    Id int IDENTITY(1,1) PRIMARY KEY,
    InputValue float NOT NULL,
    FromUnit nvarchar(50) NOT NULL,
    ToUnit nvarchar(50) NOT NULL,
    ResultValue float NOT NULL,
    ConversionDateTime datetime2 DEFAULT GETDATE()
);

PRINT ' Measurements table updated for UC16!';
