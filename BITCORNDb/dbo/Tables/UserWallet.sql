CREATE TABLE [dbo].[UserWallet] (
    [UserId]       INT             NOT NULL,
    [WalletServer] INT             NULL,
    [Balance]      NUMERIC (19, 8) NULL,
    [CornAddy]     VARCHAR (50)    NULL,
    PRIMARY KEY CLUSTERED ([UserId] ASC),
    CONSTRAINT [FK_UserWallet_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([UserId])
);

