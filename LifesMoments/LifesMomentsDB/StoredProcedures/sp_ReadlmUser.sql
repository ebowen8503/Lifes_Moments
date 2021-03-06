USE [LifesMomentsDB]
GO
/****** Object:  StoredProcedure [dbo].[sp_ReadlmUser]    Script Date: 1/27/17 3:02:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- =============================================
-- Author:		Erica Bowen
-- Create date: 01/24/2017
-- Description:	 Read User
-- =============================================
CREATE PROCEDURE sp_ReadlmUser 
	-- Add the parameters for the stored procedure here
	@userID INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	BEGIN TRY
    
	SELECT
		us.firstName,
		us.lastName,
		us.emailAddress,
		us.userPassword
		FROM lmUser us
		WHERE us.userID = @userID

	END TRY
	BEGIN CATCH
	      
		  DECLARE @error_message NVARCHAR(MAX),
		  @error_procedure NVARCHAR(MAX)

		  SELECT @error_message = ERROR_MESSAGE(),
			@error_procedure = ERROR_PROCEDURE();

		  
		  EXEC sp_LogErrors
				@userID,
				@error_procedure,
				@error_message

	END CATCH
END
