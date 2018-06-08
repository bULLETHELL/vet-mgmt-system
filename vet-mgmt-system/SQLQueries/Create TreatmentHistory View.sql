USE VetMgmtSystemDb;  
GO  
CREATE VIEW TreatmentHistory
AS
    SELECT Patients.PatientID, Patients.Name, Patients.Age, Owners.FirstName, Owners.LastName, MedicalProcedures.Name AS 'Procedure' , Dates.Date FROM Patients
    LEFT OUTER JOIN Animals ON Patients.AnimalID = Animals.AnimalID 
	LEFT JOIN Owners ON Patients.OwnerID = Owners.OwnerID
	LEFT JOIN MedicalProcedures ON Patients.MedicalProcedureID = MedicalProcedures.MedicalProcedureID
	LEFT JOIN PatientsMedicalProcedures ON Patients.MedicalProcedureID = PatientsMedicalProcedures.ProcedureID
	LEFT JOIN Dates ON PatientsMedicalProcedures.Date = Dates.Date AND Patients.PatientID = PatientsMedicalProcedures.PatientID
