
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 01/05/2018 10:46:43
-- Generated from EDMX file: C:\Data\Develop\Projects\fantasyleague\src\lagartija\Service.API\DataAccess\DataAccess\Models\MSSQL\Fantasy\Model.edmx
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
IF OBJECT_ID(N'[dbo].[FK_Sport_Team]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Teams] DROP CONSTRAINT [FK_Sport_Team];
GO
IF OBJECT_ID(N'[dbo].[FK_TeamPlayer]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Players] DROP CONSTRAINT [FK_TeamPlayer];
GO
IF OBJECT_ID(N'[dbo].[FK_Position_Player]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Players] DROP CONSTRAINT [FK_Position_Player];
GO
IF OBJECT_ID(N'[dbo].[FK_ContestType_Contest]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Contests] DROP CONSTRAINT [FK_ContestType_Contest];
GO
IF OBJECT_ID(N'[dbo].[FK_ClimaConditionsGame]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Games] DROP CONSTRAINT [FK_ClimaConditionsGame];
GO
IF OBJECT_ID(N'[dbo].[FK_AccountLineUp]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[LineUps] DROP CONSTRAINT [FK_AccountLineUp];
GO
IF OBJECT_ID(N'[dbo].[FK_SportPosition]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Positions] DROP CONSTRAINT [FK_SportPosition];
GO
IF OBJECT_ID(N'[dbo].[FK_ContestGameContest]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ContestGames] DROP CONSTRAINT [FK_ContestGameContest];
GO
IF OBJECT_ID(N'[dbo].[FK_ContestGameGame]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ContestGames] DROP CONSTRAINT [FK_ContestGameGame];
GO
IF OBJECT_ID(N'[dbo].[FK_VenueGame]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Games] DROP CONSTRAINT [FK_VenueGame];
GO
IF OBJECT_ID(N'[dbo].[FK_GameTeam]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Games] DROP CONSTRAINT [FK_GameTeam];
GO
IF OBJECT_ID(N'[dbo].[FK_GameTeam1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Games] DROP CONSTRAINT [FK_GameTeam1];
GO
IF OBJECT_ID(N'[dbo].[FK_PlayerLineupLineUp]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PlayerLineups] DROP CONSTRAINT [FK_PlayerLineupLineUp];
GO
IF OBJECT_ID(N'[dbo].[FK_PlayerLineupPlayer]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PlayerLineups] DROP CONSTRAINT [FK_PlayerLineupPlayer];
GO
IF OBJECT_ID(N'[dbo].[FK_ContestLineupContest]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ContestLineups] DROP CONSTRAINT [FK_ContestLineupContest];
GO
IF OBJECT_ID(N'[dbo].[FK_LineUpContestLineup]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ContestLineups] DROP CONSTRAINT [FK_LineUpContestLineup];
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
IF OBJECT_ID(N'[dbo].[ContestTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ContestTypes];
GO
IF OBJECT_ID(N'[dbo].[Contests]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Contests];
GO
IF OBJECT_ID(N'[dbo].[Games]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Games];
GO
IF OBJECT_ID(N'[dbo].[Promotions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Promotions];
GO
IF OBJECT_ID(N'[dbo].[LineUps]', 'U') IS NOT NULL
    DROP TABLE [dbo].[LineUps];
GO
IF OBJECT_ID(N'[dbo].[ContestGames]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ContestGames];
GO
IF OBJECT_ID(N'[dbo].[PlayerLineups]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PlayerLineups];
GO
IF OBJECT_ID(N'[dbo].[ContestLineups]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ContestLineups];
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
    [SportId] bigint IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(100)  NOT NULL
);
GO

-- Creating table 'Notifications'
CREATE TABLE [dbo].[Notifications] (
    [NotificationId] bigint IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Content] nvarchar(max)  NOT NULL,
    [InitialDate] datetime  NOT NULL,
    [FinalDate] datetime  NOT NULL
);
GO

-- Creating table 'ClimaConditions'
CREATE TABLE [dbo].[ClimaConditions] (
    [ClimaConditionsId] bigint IDENTITY(1,1) NOT NULL,
    [Condition] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Venues'
CREATE TABLE [dbo].[Venues] (
    [VenueId] bigint IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Surface] nvarchar(max)  NOT NULL,
    [State] nvarchar(max)  NOT NULL,
    [Country] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Players'
CREATE TABLE [dbo].[Players] (
    [PlayerId] bigint IDENTITY(1,1) NOT NULL,
    [FirstName] nvarchar(max)  NOT NULL,
    [LastName] nvarchar(max)  NOT NULL,
    [PreferredName] nvarchar(max)  NOT NULL,
    [TeamId] bigint  NOT NULL,
    [PositionId] bigint  NOT NULL,
    [Salary] float  NOT NULL,
    [Photo] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Positions'
CREATE TABLE [dbo].[Positions] (
    [PositionId] bigint IDENTITY(1,1) NOT NULL,
    [PositionName] nvarchar(max)  NOT NULL,
    [SportId] bigint  NOT NULL
);
GO

-- Creating table 'Teams'
CREATE TABLE [dbo].[Teams] (
    [TeamId] bigint IDENTITY(1,1) NOT NULL,
    [TeamName] nvarchar(max)  NOT NULL,
    [TeamLogo] nvarchar(max)  NOT NULL,
    [SportId] bigint  NOT NULL
);
GO

-- Creating table 'ContestTypes'
CREATE TABLE [dbo].[ContestTypes] (
    [ContestTypeId] bigint IDENTITY(1,1) NOT NULL,
    [Type] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Contests'
CREATE TABLE [dbo].[Contests] (
    [ContestId] bigint IDENTITY(1,1) NOT NULL,
    [ContestTypeId] bigint  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [SignedUp] int  NOT NULL,
    [MaxCapacity] int  NOT NULL,
    [EntryFee] float  NOT NULL,
    [SalaryCap] float  NOT NULL
);
GO

-- Creating table 'Games'
CREATE TABLE [dbo].[Games] (
    [GameId] bigint IDENTITY(1,1) NOT NULL,
    [Scheduled] datetime  NOT NULL,
    [Humidity] float  NOT NULL,
    [Temperture] float  NOT NULL,
    [VenueId] bigint  NOT NULL,
    [ClimaConditionsId] bigint  NOT NULL,
    [TeamTeamId] bigint  NOT NULL,
    [TeamTeamId1] bigint  NOT NULL
);
GO

-- Creating table 'Promotions'
CREATE TABLE [dbo].[Promotions] (
    [PromotionId] bigint IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Content] nvarchar(max)  NOT NULL,
    [Code] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'LineUps'
CREATE TABLE [dbo].[LineUps] (
    [LineUpId] bigint IDENTITY(1,1) NOT NULL,
    [AccountLogin] nvarchar(50)  NOT NULL,
    [PlayerLineupId] bigint  NOT NULL
);
GO

-- Creating table 'ContestGames'
CREATE TABLE [dbo].[ContestGames] (
    [ContestId] bigint  NOT NULL,
    [GameId] bigint  NOT NULL,
    [ContestGameId] bigint IDENTITY(1,1) NOT NULL
);
GO

-- Creating table 'PlayerLineups'
CREATE TABLE [dbo].[PlayerLineups] (
    [PlayerLineupId] bigint IDENTITY(1,1) NOT NULL,
    [LineupId] bigint  NOT NULL,
    [PlayerId] bigint  NOT NULL
);
GO

-- Creating table 'ContestLineups'
CREATE TABLE [dbo].[ContestLineups] (
    [ContestLineupId] int IDENTITY(1,1) NOT NULL,
    [ContestId] bigint  NOT NULL,
    [LineUpId] bigint  NOT NULL
);
GO

-- Creating table 'Goals'
CREATE TABLE [dbo].[Goals] (
    [GoalId] bigint IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [CompletionCount] int  NOT NULL,
    [GoalLogo] nvarchar(max)  NOT NULL,
    [SportId] bigint  NOT NULL
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

-- Creating primary key on [ClimaConditionsId] in table 'ClimaConditions'
ALTER TABLE [dbo].[ClimaConditions]
ADD CONSTRAINT [PK_ClimaConditions]
    PRIMARY KEY CLUSTERED ([ClimaConditionsId] ASC);
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

-- Creating primary key on [PromotionId] in table 'Promotions'
ALTER TABLE [dbo].[Promotions]
ADD CONSTRAINT [PK_Promotions]
    PRIMARY KEY CLUSTERED ([PromotionId] ASC);
GO

-- Creating primary key on [LineUpId] in table 'LineUps'
ALTER TABLE [dbo].[LineUps]
ADD CONSTRAINT [PK_LineUps]
    PRIMARY KEY CLUSTERED ([LineUpId] ASC);
GO

-- Creating primary key on [ContestId], [GameId], [ContestGameId] in table 'ContestGames'
ALTER TABLE [dbo].[ContestGames]
ADD CONSTRAINT [PK_ContestGames]
    PRIMARY KEY CLUSTERED ([ContestId], [GameId], [ContestGameId] ASC);
GO

-- Creating primary key on [PlayerLineupId], [LineupId], [PlayerId] in table 'PlayerLineups'
ALTER TABLE [dbo].[PlayerLineups]
ADD CONSTRAINT [PK_PlayerLineups]
    PRIMARY KEY CLUSTERED ([PlayerLineupId], [LineupId], [PlayerId] ASC);
GO

-- Creating primary key on [ContestLineupId], [ContestId], [LineUpId] in table 'ContestLineups'
ALTER TABLE [dbo].[ContestLineups]
ADD CONSTRAINT [PK_ContestLineups]
    PRIMARY KEY CLUSTERED ([ContestLineupId], [ContestId], [LineUpId] ASC);
GO

-- Creating primary key on [GoalId] in table 'Goals'
ALTER TABLE [dbo].[Goals]
ADD CONSTRAINT [PK_Goals]
    PRIMARY KEY CLUSTERED ([GoalId] ASC);
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

-- Creating foreign key on [ClimaConditionsId] in table 'Games'
ALTER TABLE [dbo].[Games]
ADD CONSTRAINT [FK_ClimaConditionsGame]
    FOREIGN KEY ([ClimaConditionsId])
    REFERENCES [dbo].[ClimaConditions]
        ([ClimaConditionsId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ClimaConditionsGame'
CREATE INDEX [IX_FK_ClimaConditionsGame]
ON [dbo].[Games]
    ([ClimaConditionsId]);
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

-- Creating foreign key on [SportId] in table 'Positions'
ALTER TABLE [dbo].[Positions]
ADD CONSTRAINT [FK_SportPosition]
    FOREIGN KEY ([SportId])
    REFERENCES [dbo].[Sports]
        ([SportId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SportPosition'
CREATE INDEX [IX_FK_SportPosition]
ON [dbo].[Positions]
    ([SportId]);
GO

-- Creating foreign key on [ContestId] in table 'ContestGames'
ALTER TABLE [dbo].[ContestGames]
ADD CONSTRAINT [FK_ContestGameContest]
    FOREIGN KEY ([ContestId])
    REFERENCES [dbo].[Contests]
        ([ContestId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [GameId] in table 'ContestGames'
ALTER TABLE [dbo].[ContestGames]
ADD CONSTRAINT [FK_ContestGameGame]
    FOREIGN KEY ([GameId])
    REFERENCES [dbo].[Games]
        ([GameId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ContestGameGame'
CREATE INDEX [IX_FK_ContestGameGame]
ON [dbo].[ContestGames]
    ([GameId]);
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

-- Creating foreign key on [TeamTeamId] in table 'Games'
ALTER TABLE [dbo].[Games]
ADD CONSTRAINT [FK_GameTeam]
    FOREIGN KEY ([TeamTeamId])
    REFERENCES [dbo].[Teams]
        ([TeamId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_GameTeam'
CREATE INDEX [IX_FK_GameTeam]
ON [dbo].[Games]
    ([TeamTeamId]);
GO

-- Creating foreign key on [TeamTeamId1] in table 'Games'
ALTER TABLE [dbo].[Games]
ADD CONSTRAINT [FK_GameTeam1]
    FOREIGN KEY ([TeamTeamId1])
    REFERENCES [dbo].[Teams]
        ([TeamId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_GameTeam1'
CREATE INDEX [IX_FK_GameTeam1]
ON [dbo].[Games]
    ([TeamTeamId1]);
GO

-- Creating foreign key on [LineupId] in table 'PlayerLineups'
ALTER TABLE [dbo].[PlayerLineups]
ADD CONSTRAINT [FK_PlayerLineupLineUp]
    FOREIGN KEY ([LineupId])
    REFERENCES [dbo].[LineUps]
        ([LineUpId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PlayerLineupLineUp'
CREATE INDEX [IX_FK_PlayerLineupLineUp]
ON [dbo].[PlayerLineups]
    ([LineupId]);
GO

-- Creating foreign key on [PlayerId] in table 'PlayerLineups'
ALTER TABLE [dbo].[PlayerLineups]
ADD CONSTRAINT [FK_PlayerLineupPlayer]
    FOREIGN KEY ([PlayerId])
    REFERENCES [dbo].[Players]
        ([PlayerId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PlayerLineupPlayer'
CREATE INDEX [IX_FK_PlayerLineupPlayer]
ON [dbo].[PlayerLineups]
    ([PlayerId]);
GO

-- Creating foreign key on [ContestId] in table 'ContestLineups'
ALTER TABLE [dbo].[ContestLineups]
ADD CONSTRAINT [FK_ContestLineupContest]
    FOREIGN KEY ([ContestId])
    REFERENCES [dbo].[Contests]
        ([ContestId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ContestLineupContest'
CREATE INDEX [IX_FK_ContestLineupContest]
ON [dbo].[ContestLineups]
    ([ContestId]);
GO

-- Creating foreign key on [LineUpId] in table 'ContestLineups'
ALTER TABLE [dbo].[ContestLineups]
ADD CONSTRAINT [FK_LineUpContestLineup]
    FOREIGN KEY ([LineUpId])
    REFERENCES [dbo].[LineUps]
        ([LineUpId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_LineUpContestLineup'
CREATE INDEX [IX_FK_LineUpContestLineup]
ON [dbo].[ContestLineups]
    ([LineUpId]);
GO

-- Creating foreign key on [SportId] in table 'Goals'
ALTER TABLE [dbo].[Goals]
ADD CONSTRAINT [FK_GoalSport]
    FOREIGN KEY ([SportId])
    REFERENCES [dbo].[Sports]
        ([SportId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_GoalSport'
CREATE INDEX [IX_FK_GoalSport]
ON [dbo].[Goals]
    ([SportId]);
GO

-- Creating foreign key on [SportId] in table 'Positions'
ALTER TABLE [dbo].[Positions]
ADD CONSTRAINT [FK_SportPosition]
    FOREIGN KEY ([SportId])
    REFERENCES [dbo].[Sports]
        ([SportId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SportPosition'
CREATE INDEX [IX_FK_SportPosition]
ON [dbo].[Positions]
    ([SportId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------