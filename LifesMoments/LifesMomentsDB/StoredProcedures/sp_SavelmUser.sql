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
-- =============================================
-- Author:		Erica Bowen
-- Create date: 01/24/2017
-- Description:	 Save User
-- =============================================
CREATE PROCEDURE sp_SavelmUser
	-- Add the parameters for the stored procedure here
	@userID INT= 0,
	@firstName VARCHAR(50),
	@lastName VARCHAR(50),
	@emailAddress VARCHAR(100),
	@userPassword VARCHAR(100)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
		BEGIN TRY
	
			IF NOT EXISTS ( SELECT null FROM lmUser where userID = @userID)
        BEGIN
			INSERT lmUser
			(
				firstName, 
				lastName, 
				emailAddress, 
				userPassword  
			)
    
			VALUES 
			(
				@firstName, 
				@lastName, 
				@emailAddress, 
				@userPassword
			)

			SET @userID = scope_identity()
END
ELSE
BEGIN
			UPDATE lmUser
				SET 
				firstName = @firstName,
				lastName = @lastName,
				emailAddress = @emailAddress,
				userPassword =@userPassword
		
				WHERE userID = @userID
END
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
