-- Creates the login SoScienceExecuter with password 'k6UwAf4K*puBTEb^'.
CREATE DATABASE SecurePassword;
GO

USE SecurePassword;
GO
CREATE LOGIN SecurePasswordExecuter   
	WITH PASSWORD = 'YvbQ3~XDEE#]8GxA';  
CREATE USER SecurePasswordExecuter FOR LOGIN SecurePasswordExecuter;  
GO
GO
CREATE TABLE UserTable(
	Username NVARCHAR(255) NOT NULL,
	Password NVARCHAR(255) NOT NULL,
	Salt NVARCHAR(255) NOT NULL,
	PRIMARY KEY(Username),
);
GO
CREATE PROCEDURE SPInsertUser @username NVARCHAR(255), @password NVARCHAR(255), @salt NVARCHAR(255)
AS
	IF EXISTS (SELECT * FROM UserTable WHERE Username = @username)
	BEGIN
	   SELECT 0;
	END
	ELSE
	BEGIN
		INSERT INTO UserTable (Username,Password,Salt) VALUES (@username,@password,@salt);
		SELECT COUNT(Salt) FROM UserTable WHERE Username = @username;
	END
GO

CREATE PROCEDURE SPGetUser @username NVARCHAR(255)
AS
	SELECT Username, Password, Salt FROM UserTable WHERE Username = @username;
GO


GRANT EXECUTE ON dbo.SPInsertUser TO SecurePasswordExecuter;
GRANT EXECUTE ON dbo.SPGetUser TO SecurePasswordExecuter;

GO 
