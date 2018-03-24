
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 03/23/2018 23:26:12
-- Generated from EDMX file: C:\Data\Work\Freelance\FantasyLeague\Project\lagartija\Service.API\DataAccess\DataAccess\Models\MSSQL\Fantasy\Model.edmx
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
IF OBJECT_ID(N'[dbo].[FK_GoalSport]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Goals] DROP CONSTRAINT [FK_GoalSport];
GO
IF OBJECT_ID(N'[dbo].[FK_NotificationAccount]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Notifications] DROP CONSTRAINT [FK_NotificationAccount];
GO
IF OBJECT_ID(N'[dbo].[FK_AccountAccountFriends]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AccountFriends] DROP CONSTRAINT [FK_AccountAccountFriends];
GO
IF OBJECT_ID(N'[dbo].[FK_AccountAccountFriends1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AccountFriends] DROP CONSTRAINT [FK_AccountAccountFriends1];
GO
IF OBJECT_ID(N'[dbo].[FK_PlayerInjury]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Injuries] DROP CONSTRAINT [FK_PlayerInjury];
GO
IF OBJECT_ID(N'[dbo].[FK_PlayerNewsPlayer]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[NewsPlayers] DROP CONSTRAINT [FK_PlayerNewsPlayer];
GO
IF OBJECT_ID(N'[dbo].[FK_NewsNewsPlayer]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[NewsPlayers] DROP CONSTRAINT [FK_NewsNewsPlayer];
GO
IF OBJECT_ID(N'[dbo].[FK_TeamNewsTeam]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[NewsTeams] DROP CONSTRAINT [FK_TeamNewsTeam];
GO
IF OBJECT_ID(N'[dbo].[FK_NewsNewsTeam]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[NewsTeams] DROP CONSTRAINT [FK_NewsNewsTeam];
GO
IF OBJECT_ID(N'[dbo].[FK_TeamTeamLeague]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TeamLeagues] DROP CONSTRAINT [FK_TeamTeamLeague];
GO
IF OBJECT_ID(N'[dbo].[FK_LeagueTeamLeague]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TeamLeagues] DROP CONSTRAINT [FK_LeagueTeamLeague];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Injuries]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Injuries];
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
IF OBJECT_ID(N'[dbo].[Information]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Information];
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
IF OBJECT_ID(N'[dbo].[Goals]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Goals];
GO
IF OBJECT_ID(N'[dbo].[Notifications]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Notifications];
GO
IF OBJECT_ID(N'[dbo].[AccountFriends]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AccountFriends];
GO
IF OBJECT_ID(N'[dbo].[News]', 'U') IS NOT NULL
    DROP TABLE [dbo].[News];
GO
IF OBJECT_ID(N'[dbo].[NewsPlayers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[NewsPlayers];
GO
IF OBJECT_ID(N'[dbo].[NewsTeams]', 'U') IS NOT NULL
    DROP TABLE [dbo].[NewsTeams];
GO
IF OBJECT_ID(N'[dbo].[TeamLeagues]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TeamLeagues];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Injuries'
CREATE TABLE [dbo].[Injuries] (
    [InjuryId] bigint  NOT NULL,
    [Comment] varchar(max)  NULL,
    [Description] varchar(500)  NULL,
    [Status] varchar(50)  NOT NULL,
    [StartDate] datetime  NOT NULL,
    [UpdateDate] datetime  NOT NULL,
    [PlayerPlayerId] bigint  NOT NULL
);
GO

-- Creating table 'Leagues'
CREATE TABLE [dbo].[Leagues] (
    [LeagueId] bigint  NOT NULL,
    [Name] varchar(50)  NOT NULL,
    [Alias] varchar(10)  NOT NULL
);
GO

-- Creating table 'Accounts'
CREATE TABLE [dbo].[Accounts] (
    [Login] nvarchar(50)  NOT NULL,
    [Email] nvarchar(250)  NOT NULL,
    [Password] nvarchar(50)  NOT NULL,
    [Money] bigint  NOT NULL,
    [Point] bigint  NOT NULL
);
GO

-- Creating table 'Sports'
CREATE TABLE [dbo].[Sports] (
    [SportId] bigint IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(100)  NOT NULL
);
GO

-- Creating table 'Information'
CREATE TABLE [dbo].[Information] (
    [InformationId] bigint IDENTITY(1,1) NOT NULL,
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
    [SportId] bigint  NOT NULL,
    [Abbr] nvarchar(max)  NOT NULL,
    [Market] nvarchar(max)  NOT NULL
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

-- Creating table 'Notifications'
CREATE TABLE [dbo].[Notifications] (
    [NotificationId] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Content] nvarchar(max)  NOT NULL,
    [AccountLogin] nvarchar(50)  NOT NULL,
    [Link] nvarchar(max)  NOT NULL,
    [Active] bit  NOT NULL
);
GO

-- Creating table 'AccountFriends'
CREATE TABLE [dbo].[AccountFriends] (
    [AccountFriendsId] bigint IDENTITY(1,1) NOT NULL,
    [AccountLogin] nvarchar(50)  NOT NULL,
    [AccountLogin1] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'News'
CREATE TABLE [dbo].[News] (
    [NewsId] bigint IDENTITY(1,1) NOT NULL,
    [Tittle] nvarchar(max)  NOT NULL,
    [Content] nvarchar(max)  NOT NULL,
    [Date] datetime  NOT NULL
);
GO

-- Creating table 'NewsPlayers'
CREATE TABLE [dbo].[NewsPlayers] (
    [NewsPlayerId] bigint IDENTITY(1,1) NOT NULL,
    [PlayerId] bigint  NOT NULL,
    [NewsId] bigint  NOT NULL
);
GO

-- Creating table 'NewsTeams'
CREATE TABLE [dbo].[NewsTeams] (
    [NewsTeamId] bigint IDENTITY(1,1) NOT NULL,
    [TeamId] bigint  NOT NULL,
    [NewsId] bigint  NOT NULL
);
GO

-- Creating table 'TeamLeagues'
CREATE TABLE [dbo].[TeamLeagues] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [TeamTeamId] bigint  NOT NULL,
    [LeagueLeagueId] bigint  NOT NULL
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

-- Creating primary key on [InformationId] in table 'Information'
ALTER TABLE [dbo].[Information]
ADD CONSTRAINT [PK_Information]
    PRIMARY KEY CLUSTERED ([InformationId] ASC);
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

-- Creating primary key on [NotificationId] in table 'Notifications'
ALTER TABLE [dbo].[Notifications]
ADD CONSTRAINT [PK_Notifications]
    PRIMARY KEY CLUSTERED ([NotificationId] ASC);
GO

-- Creating primary key on [AccountFriendsId], [AccountLogin], [AccountLogin1] in table 'AccountFriends'
ALTER TABLE [dbo].[AccountFriends]
ADD CONSTRAINT [PK_AccountFriends]
    PRIMARY KEY CLUSTERED ([AccountFriendsId], [AccountLogin], [AccountLogin1] ASC);
GO

-- Creating primary key on [NewsId] in table 'News'
ALTER TABLE [dbo].[News]
ADD CONSTRAINT [PK_News]
    PRIMARY KEY CLUSTERED ([NewsId] ASC);
GO

-- Creating primary key on [NewsPlayerId], [PlayerId], [NewsId] in table 'NewsPlayers'
ALTER TABLE [dbo].[NewsPlayers]
ADD CONSTRAINT [PK_NewsPlayers]
    PRIMARY KEY CLUSTERED ([NewsPlayerId], [PlayerId], [NewsId] ASC);
GO

-- Creating primary key on [NewsTeamId], [TeamId], [NewsId] in table 'NewsTeams'
ALTER TABLE [dbo].[NewsTeams]
ADD CONSTRAINT [PK_NewsTeams]
    PRIMARY KEY CLUSTERED ([NewsTeamId], [TeamId], [NewsId] ASC);
GO

-- Creating primary key on [Id] in table 'TeamLeagues'
ALTER TABLE [dbo].[TeamLeagues]
ADD CONSTRAINT [PK_TeamLeagues]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

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

-- Creating foreign key on [AccountLogin] in table 'Notifications'
ALTER TABLE [dbo].[Notifications]
ADD CONSTRAINT [FK_NotificationAccount]
    FOREIGN KEY ([AccountLogin])
    REFERENCES [dbo].[Accounts]
        ([Login])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_NotificationAccount'
CREATE INDEX [IX_FK_NotificationAccount]
ON [dbo].[Notifications]
    ([AccountLogin]);
GO

-- Creating foreign key on [AccountLogin] in table 'AccountFriends'
ALTER TABLE [dbo].[AccountFriends]
ADD CONSTRAINT [FK_AccountAccountFriends]
    FOREIGN KEY ([AccountLogin])
    REFERENCES [dbo].[Accounts]
        ([Login])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AccountAccountFriends'
CREATE INDEX [IX_FK_AccountAccountFriends]
ON [dbo].[AccountFriends]
    ([AccountLogin]);
GO

-- Creating foreign key on [AccountLogin1] in table 'AccountFriends'
ALTER TABLE [dbo].[AccountFriends]
ADD CONSTRAINT [FK_AccountAccountFriends1]
    FOREIGN KEY ([AccountLogin1])
    REFERENCES [dbo].[Accounts]
        ([Login])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AccountAccountFriends1'
CREATE INDEX [IX_FK_AccountAccountFriends1]
ON [dbo].[AccountFriends]
    ([AccountLogin1]);
GO

-- Creating foreign key on [PlayerPlayerId] in table 'Injuries'
ALTER TABLE [dbo].[Injuries]
ADD CONSTRAINT [FK_PlayerInjury]
    FOREIGN KEY ([PlayerPlayerId])
    REFERENCES [dbo].[Players]
        ([PlayerId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PlayerInjury'
CREATE INDEX [IX_FK_PlayerInjury]
ON [dbo].[Injuries]
    ([PlayerPlayerId]);
GO

-- Creating foreign key on [PlayerId] in table 'NewsPlayers'
ALTER TABLE [dbo].[NewsPlayers]
ADD CONSTRAINT [FK_PlayerNewsPlayer]
    FOREIGN KEY ([PlayerId])
    REFERENCES [dbo].[Players]
        ([PlayerId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PlayerNewsPlayer'
CREATE INDEX [IX_FK_PlayerNewsPlayer]
ON [dbo].[NewsPlayers]
    ([PlayerId]);
GO

-- Creating foreign key on [NewsId] in table 'NewsPlayers'
ALTER TABLE [dbo].[NewsPlayers]
ADD CONSTRAINT [FK_NewsNewsPlayer]
    FOREIGN KEY ([NewsId])
    REFERENCES [dbo].[News]
        ([NewsId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_NewsNewsPlayer'
CREATE INDEX [IX_FK_NewsNewsPlayer]
ON [dbo].[NewsPlayers]
    ([NewsId]);
GO

-- Creating foreign key on [TeamId] in table 'NewsTeams'
ALTER TABLE [dbo].[NewsTeams]
ADD CONSTRAINT [FK_TeamNewsTeam]
    FOREIGN KEY ([TeamId])
    REFERENCES [dbo].[Teams]
        ([TeamId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TeamNewsTeam'
CREATE INDEX [IX_FK_TeamNewsTeam]
ON [dbo].[NewsTeams]
    ([TeamId]);
GO

-- Creating foreign key on [NewsId] in table 'NewsTeams'
ALTER TABLE [dbo].[NewsTeams]
ADD CONSTRAINT [FK_NewsNewsTeam]
    FOREIGN KEY ([NewsId])
    REFERENCES [dbo].[News]
        ([NewsId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_NewsNewsTeam'
CREATE INDEX [IX_FK_NewsNewsTeam]
ON [dbo].[NewsTeams]
    ([NewsId]);
GO

-- Creating foreign key on [TeamTeamId] in table 'TeamLeagues'
ALTER TABLE [dbo].[TeamLeagues]
ADD CONSTRAINT [FK_TeamTeamLeague]
    FOREIGN KEY ([TeamTeamId])
    REFERENCES [dbo].[Teams]
        ([TeamId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TeamTeamLeague'
CREATE INDEX [IX_FK_TeamTeamLeague]
ON [dbo].[TeamLeagues]
    ([TeamTeamId]);
GO

-- Creating foreign key on [LeagueLeagueId] in table 'TeamLeagues'
ALTER TABLE [dbo].[TeamLeagues]
ADD CONSTRAINT [FK_LeagueTeamLeague]
    FOREIGN KEY ([LeagueLeagueId])
    REFERENCES [dbo].[Leagues]
        ([LeagueId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_LeagueTeamLeague'
CREATE INDEX [IX_FK_LeagueTeamLeague]
ON [dbo].[TeamLeagues]
    ([LeagueLeagueId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------