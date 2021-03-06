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
-- Author:		Erica Bowen
-- Create date: 01/24/2017
-- Description:	 Create Album Item
-- =============================================
CREATE PROCEDURE sp_CreatelmAlbum
	-- Add the parameters for the stored procedure here
	@albumDescription VARCHAR(50),
	@albumDate DATETIME,
	@userIDFK INT 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	BEGIN TRY
    
	INSERT lmAlbum
		(
			   albumDescription,
			   albumDate,
			   userIDFK
		)
		VALUES
		(
			@albumDescription,
			@albumDate,
			@userIDFK

		)

	END TRY
	BEGIN CATCH
	      
		  DECLARE @error_message NVARCHAR(MAX),
		  @error_procedure NVARCHAR(MAX)

		  SELECT @error_message = ERROR_MESSAGE(),
			@error_procedure = ERROR_PROCEDURE();

		  
		  EXEC sp_LogErrors
				@userIDFK,
				@error_procedure,
				@error_message

	END CATCH
END
