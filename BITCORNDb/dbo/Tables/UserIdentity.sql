CREATE TABLE [dbo].[UserIdentity] (
    [UserId]         INT           NOT NULL,
    [TwitchUsername] VARCHAR (100) NULL,
	[Auth0Nickname]  VARCHAR (100) NULL,
    [Auth0Id]        VARCHAR (100) NULL,
    [Twitchid]       VARCHAR (100) NULL,
    [Discordid]      VARCHAR (100) NULL,
    [Twitterid]      VARCHAR (100) NULL,
    [Redditid]       VARCHAR (100) NULL,

    PRIMARY KEY CLUSTERED ([UserId] ASC),
    CONSTRAINT [FK_UserIdentity_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([UserId])
);

