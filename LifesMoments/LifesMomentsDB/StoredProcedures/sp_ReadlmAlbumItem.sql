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
-- Description:	 Read Album Item
-- =============================================
CREATE PROCEDURE sp_ReadlmAlbumItem
	-- Add the parameters for the stored procedure here
	@albumItemID INT
	 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	BEGIN TRY
    
	SELECT
		alb.albumItemDescription,
		me.mediaTypeDescription,
		al.albumDescription,
		al.albumDate
		FROM lmAlbumItem alb
		LEFT JOIN lmAlbum al ON alb.albumIDFK = al.albumID
		LEFT JOIN lmMediaType me ON alb.mediaTypeIDFK = me.mediaTypeID
		WHERE alb.albumItemID = @albumItemID

	END TRY
	BEGIN CATCH
	      
		  DECLARE @error_message NVARCHAR(MAX),
		  @error_procedure NVARCHAR(MAX)

		  SELECT @error_message = ERROR_MESSAGE(),
			@error_procedure = ERROR_PROCEDURE();

		  
		  EXEC sp_LogErrors
				@albumItemID,
				@error_procedure,
				@error_message

	END CATCH
END
