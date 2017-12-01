
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 12/01/2017 02:56:17
-- Generated from EDMX file: D:\Work\Freelance\FantasyLeague\Project\lagartija\Service.API\DataAccess\DataAccess\Models\MSSQL\Fantasy\Model.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Fantasy];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_InjuryPlayer_Injury]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[InjuryPlayers] DROP CONSTRAINT [FK_InjuryPlayer_Injury];
GO
IF OBJECT_ID(N'[dbo].[FK_InjuryPlayer_InjuryTeam]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[InjuryPlayers] DROP CONSTRAINT [FK_InjuryPlayer_InjuryTeam];
GO
IF OBJECT_ID(N'[dbo].[FK_InjuryTeam_League]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[InjuryTeams] DROP CONSTRAINT [FK_InjuryTeam_League];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Injuries]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Injuries];
GO
IF OBJECT_ID(N'[dbo].[InjuryPlayers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[InjuryPlayers];
GO
IF OBJECT_ID(N'[dbo].[InjuryTeams]', 'U') IS NOT NULL
    DROP TABLE [dbo].[InjuryTeams];
GO
IF OBJECT_ID(N'[dbo].[Leagues]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Leagues];
GO
IF OBJECT_ID(N'[dbo].[Accounts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Accounts];
GO
IF OBJECT_ID(N'[dbo].[Sports]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Sports];
GO
IF OBJECT_ID(N'[dbo].[Notifications]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Notifications];
GO
IF OBJECT_ID(N'[dbo].[ClimaConditions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ClimaConditions];
GO
IF OBJECT_ID(N'[dbo].[Venues]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Venues];
GO
IF OBJECT_ID(N'[dbo].[Players]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Players];
GO
IF OBJECT_ID(N'[dbo].[Positions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Positions];
GO
IF OBJECT_ID(N'[dbo].[Teams]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Teams];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Injuries'
CREATE TABLE [dbo].[Injuries] (
    [InjuryId] varchar(36)  NOT NULL,
    [Comment] varchar(max)  NULL,
    [Description] varchar(500)  NULL,
    [Status] varchar(50)  NOT NULL,
    [StartDate] datetime  NOT NULL,
    [UpdateDate] datetime  NOT NULL
);
GO

-- Creating table 'InjuryPlayers'
CREATE TABLE [dbo].[InjuryPlayers] (
    [PlayerId] varchar(36)  NOT NULL,
    [Status] varchar(50)  NOT NULL,
    [Position] varchar(10)  NOT NULL,
    [PrimaryPosition] varchar(10)  NOT NULL,
    [FirstName] varchar(100)  NOT NULL,
    [LastName] varchar(150)  NOT NULL,
    [JerseyNumber] varchar(3)  NOT NULL,
    [PreferredName] varchar(50)  NOT NULL,
    [InjuryId] varchar(36)  NOT NULL,
    [InjuryTeamId] varchar(36)  NOT NULL
);
GO

-- Creating table 'InjuryTeams'
CREATE TABLE [dbo].[InjuryTeams] (
    [TeamId] varchar(36)  NOT NULL,
    [Name] varchar(20)  NOT NULL,
    [Abbr] varchar(10)  NOT NULL,
    [Market] varchar(20)  NULL,
    [LeagueId] varchar(36)  NOT NULL
);
GO

-- Creating table 'Leagues'
CREATE TABLE [dbo].[Leagues] (
    [LeagueId] varchar(36)  NOT NULL,
    [Name] varchar(50)  NOT NULL,
    [Alias] varchar(10)  NOT NULL
);
GO

-- Creating table 'Accounts'
CREATE TABLE [dbo].[Accounts] (
    [Login] nvarchar(50)  NOT NULL,
    [Email] nvarchar(250)  NOT NULL,
    [Password] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'Sports'
CREATE TABLE [dbo].[Sports] (
    [SportId] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(100)  NOT NULL,
    [PositionId] int  NOT NULL
);
GO

-- Creating table 'Notifications'
CREATE TABLE [dbo].[Notifications] (
    [NotificationId] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Content] nvarchar(max)  NOT NULL,
    [InitialDate] datetime  NOT NULL,
    [FinalDate] datetime  NOT NULL
);
GO

-- Creating table 'ClimaConditions'
CREATE TABLE [dbo].[ClimaConditions] (
    [ClimaId] int IDENTITY(1,1) NOT NULL,
    [Condition] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Venues'
CREATE TABLE [dbo].[Venues] (
    [VenueId] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Surface] nvarchar(max)  NOT NULL,
    [State] nvarchar(max)  NOT NULL,
    [Country] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Players'
CREATE TABLE [dbo].[Players] (
    [PlayerId] int IDENTITY(1,1) NOT NULL,
    [FirstName] nvarchar(max)  NOT NULL,
    [LastName] nvarchar(max)  NOT NULL,
    [PreferredName] nvarchar(max)  NOT NULL,
    [TeamId] int  NOT NULL,
    [PositionId] int  NOT NULL,
    [Salary] float  NOT NULL,
    [Photo] nvarchar(max)  NOT NULL,
    [Sport_SportId] int  NOT NULL
);
GO

-- Creating table 'Positions'
CREATE TABLE [dbo].[Positions] (
    [PositionId] int IDENTITY(1,1) NOT NULL,
    [PositionName] nvarchar(max)  NOT NULL,
    [SportId] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Teams'
CREATE TABLE [dbo].[Teams] (
    [TeamId] int IDENTITY(1,1) NOT NULL,
    [TeamName] nvarchar(max)  NOT NULL,
    [TeamLogo] nvarchar(max)  NOT NULL,
    [SportId] int  NOT NULL
);
GO

-- Creating table 'ContestTypes'
CREATE TABLE [dbo].[ContestTypes] (
    [ContestTypeId] int IDENTITY(1,1) NOT NULL,
    [Type] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Contests'
CREATE TABLE [dbo].[Contests] (
    [ContestId] int IDENTITY(1,1) NOT NULL,
    [ContestTypeId] int  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [SignedUp] int  NOT NULL,
    [MaxCapacity] int  NOT NULL,
    [EntryFee] float  NOT NULL,
    [SalaryCap] float  NOT NULL
);
GO

-- Creating table 'Games'
CREATE TABLE [dbo].[Games] (
    [GameId] int IDENTITY(1,1) NOT NULL,
    [Scheduled] datetime  NOT NULL,
    [Humidity] float  NOT NULL,
    [Temperture] float  NOT NULL,
    [VenueId] int  NOT NULL,
    [AwayTeam_TeamId] int  NOT NULL,
    [HomeTeam_TeamId] int  NOT NULL,
    [ClimaCondition_ClimaId] int  NOT NULL,
    [GameWinner_WinnerId] int  NOT NULL
);
GO

-- Creating table 'GameWinners'
CREATE TABLE [dbo].[GameWinners] (
    [WinnerId] int IDENTITY(1,1) NOT NULL
);
GO

-- Creating table 'Promotions'
CREATE TABLE [dbo].[Promotions] (
    [PromoId] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Content] nvarchar(max)  NOT NULL,
    [Code] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'LineUps'
CREATE TABLE [dbo].[LineUps] (
    [LineUpId] int IDENTITY(1,1) NOT NULL,
    [AccountLogin] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'ContestGame'
CREATE TABLE [dbo].[ContestGame] (
    [Contests_ContestId] int  NOT NULL,
    [Games_GameId] int  NOT NULL
);
GO

-- Creating table 'LineUpPlayer'
CREATE TABLE [dbo].[LineUpPlayer] (
    [LineUps_LineUpId] int  NOT NULL,
    [Players_PlayerId] int  NOT NULL
);
GO

-- Creating table 'LineUpContest'
CREATE TABLE [dbo].[LineUpContest] (
    [LineUps_LineUpId] int  NOT NULL,
    [Contests_ContestId] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [InjuryId] in table 'Injuries'
ALTER TABLE [dbo].[Injuries]
ADD CONSTRAINT [PK_Injuries]
    PRIMARY KEY CLUSTERED ([InjuryId] ASC);
GO

-- Creating primary key on [PlayerId] in table 'InjuryPlayers'
ALTER TABLE [dbo].[InjuryPlayers]
ADD CONSTRAINT [PK_InjuryPlayers]
    PRIMARY KEY CLUSTERED ([PlayerId] ASC);
GO

-- Creating primary key on [TeamId] in table 'InjuryTeams'
ALTER TABLE [dbo].[InjuryTeams]
ADD CONSTRAINT [PK_InjuryTeams]
    PRIMARY KEY CLUSTERED ([TeamId] ASC);
GO

-- Creating primary key on [LeagueId] in table 'Leagues'
ALTER TABLE [dbo].[Leagues]
ADD CONSTRAINT [PK_Leagues]
    PRIMARY KEY CLUSTERED ([LeagueId] ASC);
GO

-- Creating primary key on [Login] in table 'Accounts'
ALTER TABLE [dbo].[Accounts]
ADD CONSTRAINT [PK_Accounts]
    PRIMARY KEY CLUSTERED ([Login] ASC);
GO

-- Creating primary key on [SportId] in table 'Sports'
ALTER TABLE [dbo].[Sports]
ADD CONSTRAINT [PK_Sports]
    PRIMARY KEY CLUSTERED ([SportId] ASC);
GO

-- Creating primary key on [NotificationId] in table 'Notifications'
ALTER TABLE [dbo].[Notifications]
ADD CONSTRAINT [PK_Notifications]
    PRIMARY KEY CLUSTERED ([NotificationId] ASC);
GO

-- Creating primary key on [ClimaId] in table 'ClimaConditions'
ALTER TABLE [dbo].[ClimaConditions]
ADD CONSTRAINT [PK_ClimaConditions]
    PRIMARY KEY CLUSTERED ([ClimaId] ASC);
GO

-- Creating primary key on [VenueId] in table 'Venues'
ALTER TABLE [dbo].[Venues]
ADD CONSTRAINT [PK_Venues]
    PRIMARY KEY CLUSTERED ([VenueId] ASC);
GO

-- Creating primary key on [PlayerId] in table 'Players'
ALTER TABLE [dbo].[Players]
ADD CONSTRAINT [PK_Players]
    PRIMARY KEY CLUSTERED ([PlayerId] ASC);
GO

-- Creating primary key on [PositionId] in table 'Positions'
ALTER TABLE [dbo].[Positions]
ADD CONSTRAINT [PK_Positions]
    PRIMARY KEY CLUSTERED ([PositionId] ASC);
GO

-- Creating primary key on [TeamId] in table 'Teams'
ALTER TABLE [dbo].[Teams]
ADD CONSTRAINT [PK_Teams]
    PRIMARY KEY CLUSTERED ([TeamId] ASC);
GO

-- Creating primary key on [ContestTypeId] in table 'ContestTypes'
ALTER TABLE [dbo].[ContestTypes]
ADD CONSTRAINT [PK_ContestTypes]
    PRIMARY KEY CLUSTERED ([ContestTypeId] ASC);
GO

-- Creating primary key on [ContestId] in table 'Contests'
ALTER TABLE [dbo].[Contests]
ADD CONSTRAINT [PK_Contests]
    PRIMARY KEY CLUSTERED ([ContestId] ASC);
GO

-- Creating primary key on [GameId] in table 'Games'
ALTER TABLE [dbo].[Games]
ADD CONSTRAINT [PK_Games]
    PRIMARY KEY CLUSTERED ([GameId] ASC);
GO

-- Creating primary key on [WinnerId] in table 'GameWinners'
ALTER TABLE [dbo].[GameWinners]
ADD CONSTRAINT [PK_GameWinners]
    PRIMARY KEY CLUSTERED ([WinnerId] ASC);
GO

-- Creating primary key on [PromoId] in table 'Promotions'
ALTER TABLE [dbo].[Promotions]
ADD CONSTRAINT [PK_Promotions]
    PRIMARY KEY CLUSTERED ([PromoId] ASC);
GO

-- Creating primary key on [LineUpId] in table 'LineUps'
ALTER TABLE [dbo].[LineUps]
ADD CONSTRAINT [PK_LineUps]
    PRIMARY KEY CLUSTERED ([LineUpId] ASC);
GO

-- Creating primary key on [Contests_ContestId], [Games_GameId] in table 'ContestGame'
ALTER TABLE [dbo].[ContestGame]
ADD CONSTRAINT [PK_ContestGame]
    PRIMARY KEY CLUSTERED ([Contests_ContestId], [Games_GameId] ASC);
GO

-- Creating primary key on [LineUps_LineUpId], [Players_PlayerId] in table 'LineUpPlayer'
ALTER TABLE [dbo].[LineUpPlayer]
ADD CONSTRAINT [PK_LineUpPlayer]
    PRIMARY KEY CLUSTERED ([LineUps_LineUpId], [Players_PlayerId] ASC);
GO

-- Creating primary key on [LineUps_LineUpId], [Contests_ContestId] in table 'LineUpContest'
ALTER TABLE [dbo].[LineUpContest]
ADD CONSTRAINT [PK_LineUpContest]
    PRIMARY KEY CLUSTERED ([LineUps_LineUpId], [Contests_ContestId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [InjuryId] in table 'InjuryPlayers'
ALTER TABLE [dbo].[InjuryPlayers]
ADD CONSTRAINT [FK_InjuryPlayer_Injury]
    FOREIGN KEY ([InjuryId])
    REFERENCES [dbo].[Injuries]
        ([InjuryId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_InjuryPlayer_Injury'
CREATE INDEX [IX_FK_InjuryPlayer_Injury]
ON [dbo].[InjuryPlayers]
    ([InjuryId]);
GO

-- Creating foreign key on [InjuryTeamId] in table 'InjuryPlayers'
ALTER TABLE [dbo].[InjuryPlayers]
ADD CONSTRAINT [FK_InjuryPlayer_InjuryTeam]
    FOREIGN KEY ([InjuryTeamId])
    REFERENCES [dbo].[InjuryTeams]
        ([TeamId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_InjuryPlayer_InjuryTeam'
CREATE INDEX [IX_FK_InjuryPlayer_InjuryTeam]
ON [dbo].[InjuryPlayers]
    ([InjuryTeamId]);
GO

-- Creating foreign key on [LeagueId] in table 'InjuryTeams'
ALTER TABLE [dbo].[InjuryTeams]
ADD CONSTRAINT [FK_InjuryTeam_League]
    FOREIGN KEY ([LeagueId])
    REFERENCES [dbo].[Leagues]
        ([LeagueId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_InjuryTeam_League'
CREATE INDEX [IX_FK_InjuryTeam_League]
ON [dbo].[InjuryTeams]
    ([LeagueId]);
GO

-- Creating foreign key on [PositionId] in table 'Sports'
ALTER TABLE [dbo].[Sports]
ADD CONSTRAINT [FK_Position_Sport]
    FOREIGN KEY ([PositionId])
    REFERENCES [dbo].[Positions]
        ([PositionId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Position_Sport'
CREATE INDEX [IX_FK_Position_Sport]
ON [dbo].[Sports]
    ([PositionId]);
GO

-- Creating foreign key on [SportId] in table 'Teams'
ALTER TABLE [dbo].[Teams]
ADD CONSTRAINT [FK_Sport_Team]
    FOREIGN KEY ([SportId])
    REFERENCES [dbo].[Sports]
        ([SportId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Sport_Team'
CREATE INDEX [IX_FK_Sport_Team]
ON [dbo].[Teams]
    ([SportId]);
GO

-- Creating foreign key on [TeamId] in table 'Players'
ALTER TABLE [dbo].[Players]
ADD CONSTRAINT [FK_TeamPlayer]
    FOREIGN KEY ([TeamId])
    REFERENCES [dbo].[Teams]
        ([TeamId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TeamPlayer'
CREATE INDEX [IX_FK_TeamPlayer]
ON [dbo].[Players]
    ([TeamId]);
GO

-- Creating foreign key on [Sport_SportId] in table 'Players'
ALTER TABLE [dbo].[Players]
ADD CONSTRAINT [FK_Player_Sport]
    FOREIGN KEY ([Sport_SportId])
    REFERENCES [dbo].[Sports]
        ([SportId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Player_Sport'
CREATE INDEX [IX_FK_Player_Sport]
ON [dbo].[Players]
    ([Sport_SportId]);
GO

-- Creating foreign key on [PositionId] in table 'Players'
ALTER TABLE [dbo].[Players]
ADD CONSTRAINT [FK_Position_Player]
    FOREIGN KEY ([PositionId])
    REFERENCES [dbo].[Positions]
        ([PositionId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Position_Player'
CREATE INDEX [IX_FK_Position_Player]
ON [dbo].[Players]
    ([PositionId]);
GO

-- Creating foreign key on [ContestTypeId] in table 'Contests'
ALTER TABLE [dbo].[Contests]
ADD CONSTRAINT [FK_ContestType_Contest]
    FOREIGN KEY ([ContestTypeId])
    REFERENCES [dbo].[ContestTypes]
        ([ContestTypeId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ContestType_Contest'
CREATE INDEX [IX_FK_ContestType_Contest]
ON [dbo].[Contests]
    ([ContestTypeId]);
GO

-- Creating foreign key on [AwayTeam_TeamId] in table 'Games'
ALTER TABLE [dbo].[Games]
ADD CONSTRAINT [FK_Game_AwayTeam]
    FOREIGN KEY ([AwayTeam_TeamId])
    REFERENCES [dbo].[Teams]
        ([TeamId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Game_AwayTeam'
CREATE INDEX [IX_FK_Game_AwayTeam]
ON [dbo].[Games]
    ([AwayTeam_TeamId]);
GO

-- Creating foreign key on [HomeTeam_TeamId] in table 'Games'
ALTER TABLE [dbo].[Games]
ADD CONSTRAINT [FK_GameTeam]
    FOREIGN KEY ([HomeTeam_TeamId])
    REFERENCES [dbo].[Teams]
        ([TeamId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_GameTeam'
CREATE INDEX [IX_FK_GameTeam]
ON [dbo].[Games]
    ([HomeTeam_TeamId]);
GO

-- Creating foreign key on [ClimaCondition_ClimaId] in table 'Games'
ALTER TABLE [dbo].[Games]
ADD CONSTRAINT [FK_ClimaConditionsGame]
    FOREIGN KEY ([ClimaCondition_ClimaId])
    REFERENCES [dbo].[ClimaConditions]
        ([ClimaId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ClimaConditionsGame'
CREATE INDEX [IX_FK_ClimaConditionsGame]
ON [dbo].[Games]
    ([ClimaCondition_ClimaId]);
GO

-- Creating foreign key on [VenueId] in table 'Games'
ALTER TABLE [dbo].[Games]
ADD CONSTRAINT [FK_VenueGame]
    FOREIGN KEY ([VenueId])
    REFERENCES [dbo].[Venues]
        ([VenueId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_VenueGame'
CREATE INDEX [IX_FK_VenueGame]
ON [dbo].[Games]
    ([VenueId]);
GO

-- Creating foreign key on [Contests_ContestId] in table 'ContestGame'
ALTER TABLE [dbo].[ContestGame]
ADD CONSTRAINT [FK_ContestGame_Contest]
    FOREIGN KEY ([Contests_ContestId])
    REFERENCES [dbo].[Contests]
        ([ContestId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Games_GameId] in table 'ContestGame'
ALTER TABLE [dbo].[ContestGame]
ADD CONSTRAINT [FK_ContestGame_Game]
    FOREIGN KEY ([Games_GameId])
    REFERENCES [dbo].[Games]
        ([GameId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ContestGame_Game'
CREATE INDEX [IX_FK_ContestGame_Game]
ON [dbo].[ContestGame]
    ([Games_GameId]);
GO

-- Creating foreign key on [GameWinner_WinnerId] in table 'Games'
ALTER TABLE [dbo].[Games]
ADD CONSTRAINT [FK_GameWinnerGame]
    FOREIGN KEY ([GameWinner_WinnerId])
    REFERENCES [dbo].[GameWinners]
        ([WinnerId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_GameWinnerGame'
CREATE INDEX [IX_FK_GameWinnerGame]
ON [dbo].[Games]
    ([GameWinner_WinnerId]);
GO

-- Creating foreign key on [AccountLogin] in table 'LineUps'
ALTER TABLE [dbo].[LineUps]
ADD CONSTRAINT [FK_AccountLineUp]
    FOREIGN KEY ([AccountLogin])
    REFERENCES [dbo].[Accounts]
        ([Login])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AccountLineUp'
CREATE INDEX [IX_FK_AccountLineUp]
ON [dbo].[LineUps]
    ([AccountLogin]);
GO

-- Creating foreign key on [LineUps_LineUpId] in table 'LineUpPlayer'
ALTER TABLE [dbo].[LineUpPlayer]
ADD CONSTRAINT [FK_LineUpPlayer_LineUp]
    FOREIGN KEY ([LineUps_LineUpId])
    REFERENCES [dbo].[LineUps]
        ([LineUpId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Players_PlayerId] in table 'LineUpPlayer'
ALTER TABLE [dbo].[LineUpPlayer]
ADD CONSTRAINT [FK_LineUpPlayer_Player]
    FOREIGN KEY ([Players_PlayerId])
    REFERENCES [dbo].[Players]
        ([PlayerId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_LineUpPlayer_Player'
CREATE INDEX [IX_FK_LineUpPlayer_Player]
ON [dbo].[LineUpPlayer]
    ([Players_PlayerId]);
GO

-- Creating foreign key on [LineUps_LineUpId] in table 'LineUpContest'
ALTER TABLE [dbo].[LineUpContest]
ADD CONSTRAINT [FK_LineUpContest_LineUp]
    FOREIGN KEY ([LineUps_LineUpId])
    REFERENCES [dbo].[LineUps]
        ([LineUpId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Contests_ContestId] in table 'LineUpContest'
ALTER TABLE [dbo].[LineUpContest]
ADD CONSTRAINT [FK_LineUpContest_Contest]
    FOREIGN KEY ([Contests_ContestId])
    REFERENCES [dbo].[Contests]
        ([ContestId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_LineUpContest_Contest'
CREATE INDEX [IX_FK_LineUpContest_Contest]
ON [dbo].[LineUpContest]
    ([Contests_ContestId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------