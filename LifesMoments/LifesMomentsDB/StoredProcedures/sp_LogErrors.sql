-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:	 Erica Bowen
-- Create date: 01/23/2017
-- Description:	Read Log Errors
-- =============================================
CREATE PROCEDURE sp_LogErrors
	-- Add the parameters for the stored procedure here
	@userIDFK INT = 0,
	@sp_Name VARCHAR(100)= '',
	@errorDescription NVARCHAR(MAX)=''
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	BEGIN TRY
    -- Insert statements for procedure here
		INSERT lmErrorLog
		(
			userIDFK,
			sp_Name,
			errorDescription
		)
		SELECT @userIDFK, 
			   @sp_Name, 
			   @errorDescription
	END TRY

	BEGIN CATCH
	      
		  RETURN Error_Message();

	END CATCH


END
