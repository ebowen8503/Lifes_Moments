USE LifesMomentsDB

GO

CREATE TABLE lmAlbumItem
(
	albumItemID INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	albumItemDescription NVARCHAR(200) NOT NULL,
	albumItemlocation NVARCHAR(200) NOT NULL,
	mediaTypeIDFK INT FOREIGN KEY REFERENCES lmMediaType(mediaTypeID) NOT NULL,
	albumIDFK INT FOREIGN KEY REFERENCES lmAlbum(albumID) NOT NULL
)