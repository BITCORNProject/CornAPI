CREATE TABLE [dbo].[CornTransaction]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Platform] VARCHAR(50) NULL, 
    [TxType] NVARCHAR(50) NULL, 
    [Amount] NUMERIC(19, 8) NULL, 
    [SenderId] VARCHAR(100) NULL, 
    [ReceiverId] VARCHAR(100) NULL, 
    [Timestamp] DATETIME NULL
)
