CREATE TABLE [dbo].[ErrorLogs]
(
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY, 
    [Application] VARCHAR(100) NULL, 
    [Message] VARCHAR(1000) NULL, 
    [StackTrace] VARCHAR(5000) NULL, 
    [Code] VARCHAR(100) NULL, 
    [TImestamp] DATETIME NULL
)
