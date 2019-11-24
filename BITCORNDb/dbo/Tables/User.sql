CREATE TABLE [dbo].[User] (
    [UserId]   INT            IDENTITY (420000000, 1) NOT NULL,
    [Level]    VARCHAR (50)   NULL,
    [Username] VARCHAR(50)  NULL,
    [Avatar]   VARCHAR (2048) NULL,
    PRIMARY KEY CLUSTERED ([UserId] ASC)
);

