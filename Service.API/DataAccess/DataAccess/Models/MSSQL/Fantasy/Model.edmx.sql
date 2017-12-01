
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 11/30/2017 23:47:34
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
    ALTER TABLE [dbo].[InjuryPlayer] DROP CONSTRAINT [FK_InjuryPlayer_Injury];
GO
IF OBJECT_ID(N'[dbo].[FK_InjuryPlayer_InjuryTeam]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[InjuryPlayer] DROP CONSTRAINT [FK_InjuryPlayer_InjuryTeam];
GO
IF OBJECT_ID(N'[dbo].[FK_InjuryTeam_League]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[InjuryTeam] DROP CONSTRAINT [FK_InjuryTeam_League];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Account]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Account];
GO
IF OBJECT_ID(N'[dbo].[Injury]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Injury];
GO
IF OBJECT_ID(N'[dbo].[InjuryPlayer]', 'U') IS NOT NULL
    DROP TABLE [dbo].[InjuryPlayer];
GO
IF OBJECT_ID(N'[dbo].[InjuryTeam]', 'U') IS NOT NULL
    DROP TABLE [dbo].[InjuryTeam];
GO
IF OBJECT_ID(N'[dbo].[League]', 'U') IS NOT NULL
    DROP TABLE [dbo].[League];
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
    [Name] nvarchar(100)  NOT NULL
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
    [Number] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Players_MLBPlayer'
CREATE TABLE [dbo].[Players_MLBPlayer] (
    [Property1] nvarchar(max)  NOT NULL,
    [PlayerId] int  NOT NULL
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

-- Creating primary key on [PlayerId] in table 'Players_MLBPlayer'
ALTER TABLE [dbo].[Players_MLBPlayer]
ADD CONSTRAINT [PK_Players_MLBPlayer]
    PRIMARY KEY CLUSTERED ([PlayerId] ASC);
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

-- Creating foreign key on [PlayerId] in table 'Players_MLBPlayer'
ALTER TABLE [dbo].[Players_MLBPlayer]
ADD CONSTRAINT [FK_MLBPlayer_inherits_Player]
    FOREIGN KEY ([PlayerId])
    REFERENCES [dbo].[Players]
        ([PlayerId])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------