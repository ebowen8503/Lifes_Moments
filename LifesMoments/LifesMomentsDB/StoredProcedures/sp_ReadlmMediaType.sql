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
-- Description:	 Read Media Type
-- =============================================
CREATE PROCEDURE sp_ReadlmMediaType
	-- Add the parameters for the stored procedure here
	@mediaTypeID INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	BEGIN TRY
    
	SELECT
		me.mediaTypeID,
		me.mediaTypeDescription
		FROM lmMediaType me
		WHERE me.mediaTypeID = @mediaTypeID


	END TRY
	BEGIN CATCH
	      
		  DECLARE @error_message NVARCHAR(MAX),
		  @error_procedure NVARCHAR(MAX)

		  SELECT @error_message = ERROR_MESSAGE(),
			@error_procedure = ERROR_PROCEDURE();

		  
		  EXEC sp_LogErrors
				@mediaTypeID,
				@error_procedure,
				@error_message

	END CATCH
END
