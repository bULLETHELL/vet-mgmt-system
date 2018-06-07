CREATE DATABASE VetMgmtSystemDb
GO
USE VetMgmtSystemDb;
GO

CREATE TABLE Animals (
	AnimalID int IDENTITY(1,1) NOT NULL PRIMARY KEY,
	AnimalName nvarchar(max) NOT NULL
);

CREATE TABLE Patients (
	PatientID int IDENTITY(1,1) NOT NULL PRIMARY KEY,
	Name nvarchar(max) NOT NULL,
	Age int NOT NULL,
	OwnerID int NOT NULL,
	AnimalID int NOT NULL,
	MedicalProcedureID int NOT NULL
);

CREATE TABLE Owners (
	OwnerID int IDENTITY(1,1) NOT NULL PRIMARY KEY,
	FirstName nvarchar(max) NOT NULL,
	LastName nvarchar(max) NOT NULL,
	ZipCode int NOT NULL,
	AddressID int NOT NULL
);

CREATE TABLE ZipCity (
	ZipCode int NOT NULL PRIMARY KEY,
	City nvarchar(max) NOT NULL
);

CREATE TABLE Addresses (
	AddressID int IDENTITY(1,1) NOT NULL PRIMARY KEY,
	StreetName nvarchar(max) NOT NULL,
	StreetNo nvarchar(max) NOT NULL
);

CREATE TABLE MedicalProcedures (
	MedicalProcedureID int IDENTITY(1,1) NOT NULL PRIMARY KEY,
	Name nvarchar(max) NOT NULL,
	Price float NOT NULL
);

CREATE TABLE Dates (
	Date date NOT NULL PRIMARY KEY
);

CREATE TABLE PatientsMedicalProcedures (
	PatientID int NOT NULL,
	ProcedureID int NOT NULL,
	Date date NOT NULL,
	CONSTRAINT PK_Patients_MedicalProcedures PRIMARY KEY (PatientID, ProcedureID, Date)
);

CREATE TABLE PriceHistory (
	PriceHistoryID int NOT NULL PRIMARY KEY,
	PreviousPrice float NOT NULL
);

ALTER TABLE Patients
ADD FOREIGN KEY (AnimalID) REFERENCES Animals(AnimalID);

ALTER TABLE Patients
ADD FOREIGN KEY (OwnerID) REFERENCES Owners(OwnerID);

ALTER TABLE Owners
ADD FOREIGN KEY (ZipCode) REFERENCES ZipCity(ZipCode);

ALTER TABLE Owners
ADD FOREIGN KEY (AddressID) REFERENCES Addresses(AddressID);

ALTER TABLE PatientsMedicalProcedures
ADD FOREIGN KEY (Date) REFERENCES Dates(Date);

ALTER TABLE PatientsMedicalProcedures
ADD FOREIGN KEY (PatientID) REFERENCES Patients(PatientID);

ALTER TABLE PatientsMedicalProcedures
ADD FOREIGN KEY (ProcedureID) REFERENCES MedicalProcedures(MedicalProcedureID);

ALTER TABLE PriceHistory
ADD FOREIGN KEY (PriceHistoryID) REFERENCES MedicalProcedures(MedicalProcedureID);
