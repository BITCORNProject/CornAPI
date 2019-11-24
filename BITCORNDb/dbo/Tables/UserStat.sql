CREATE TABLE [dbo].[UserStat] (
    [UserId] INT NOT NULL,
    [Tipped] INT NULL, 
    [TIppedTotal] NUMERIC(19, 8) NULL, 
    [TopTiped] NUMERIC(19, 8) NULL, 
    [Tip] INT NULL, 
    [TipTotal] NUMERIC(19, 8) NULL, 
    [TopTip] NUMERIC(19, 8) NULL, 
    [Rained] INT NULL, 
    [RainTotal] NUMERIC(19, 8) NULL, 
    [TopRain] NUMERIC(19, 8) NULL, 
    [RainedOn] INT NULL, 
    [RainedOnTotal] NUMERIC(19, 8) NULL, 
    [TopRainedOn] NUMERIC(19, 8) NULL, 
    PRIMARY KEY CLUSTERED ([UserId] ASC), 
    CONSTRAINT [FK_UserStat_Users] FOREIGN KEY ([UserId]) REFERENCES [User]([UserId])
);

