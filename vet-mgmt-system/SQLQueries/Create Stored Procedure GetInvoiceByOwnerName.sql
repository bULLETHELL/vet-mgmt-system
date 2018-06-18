USE VetMgmtSystemDb;  
GO  
CREATE PROCEDURE proc_GetInvoiceByOwnerName
	@OwnerName nvarchar(max)
AS
BEGIN
	SET NOCOUNT ON;

SELECT Owners.FirstName, Owners.LastName, Addresses.StreetName, Addresses.StreetNo, ZipCity.ZipCode, ZipCity.City, Patients.Name AS PatientName, MedicalProcedures.Name AS MedicalProcedureName, MedicalProcedures.Price, Dates.Date FROM Owners
LEFT OUTER JOIN Addresses ON Owners.AddressID = Addresses.AddressID
LEFT JOIN ZipCity ON Owners.ZipCode = ZipCity.ZipCode
LEFT JOIN Patients ON Owners.OwnerID = Patients.OwnerID
LEFT JOIN MedicalProcedures ON Patients.MedicalProcedureID = MedicalProcedures.MedicalProcedureID
LEFT JOIN PatientsMedicalProcedures ON Patients.MedicalProcedureID = PatientsMedicalProcedures.ProcedureID
LEFT JOIN Dates ON PatientsMedicalProcedures.Date = Dates.Date AND Patients.PatientID = PatientsMedicalProcedures.PatientID
WHERE Owners.FirstName + Owners.LastName = @OwnerName OR Owners.FirstName = @OwnerName OR Owners.LastName = @OwnerName
END