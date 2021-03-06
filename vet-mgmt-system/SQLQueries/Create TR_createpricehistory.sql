USE [VetMgmtSystemDb]
GO
/****** Object:  Trigger [dbo].[tr_CreatePriceHistory]    Script Date: 07-06-2018 11:03:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TRIGGER [dbo].[tr_CreatePriceHistory] 
	ON [dbo].[MedicalProcedures]
	FOR INSERT
AS
BEGIN
	INSERT INTO
	PriceHistory
	(
		PriceHistoryID,
		PreviousPrice
	)
	SELECT
		MedicalProcedureID,
		Price
	FROM
		INSERTED
END
