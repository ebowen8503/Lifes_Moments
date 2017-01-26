USE LifesMomentsDB

GO

CREATE TABLE lmMediaType
(
	mediaTypeID INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	mediaTypeDescription VARCHAR(50) NOT NULL,
)