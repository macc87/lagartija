/*
Navicat SQL Server Data Transfer

Source Server         : SQLSERVER
Source Server Version : 120000
Source Host           : :1433
Source Database       : Fantasy
Source Schema         : dbo

Target Server Type    : SQL Server
Target Server Version : 120000
File Encoding         : 65001

Date: 2018-01-18 03:36:20
*/


-- ----------------------------
-- Table structure for Accounts
-- ----------------------------
DROP TABLE [dbo].[Accounts]
GO
CREATE TABLE [dbo].[Accounts] (
[Login] nvarchar(50) NOT NULL ,
[Email] nvarchar(250) NOT NULL ,
[Password] nvarchar(50) NOT NULL 
)


GO

-- ----------------------------
-- Records of Accounts
-- ----------------------------
INSERT INTO [dbo].[Accounts] ([Login], [Email], [Password]) VALUES (N'admin', N'admin@admins.com', N'password')
GO
GO

-- ----------------------------
-- Table structure for ClimaConditions
-- ----------------------------
DROP TABLE [dbo].[ClimaConditions]
GO
CREATE TABLE [dbo].[ClimaConditions] (
[ClimaConditionsId] bigint NOT NULL IDENTITY(1,1) ,
[Condition] nvarchar(MAX) NOT NULL 
)


GO
DBCC CHECKIDENT(N'[dbo].[ClimaConditions]', RESEED, 3)
GO

-- ----------------------------
-- Records of ClimaConditions
-- ----------------------------
SET IDENTITY_INSERT [dbo].[ClimaConditions] ON
GO
INSERT INTO [dbo].[ClimaConditions] ([ClimaConditionsId], [Condition]) VALUES (N'1', N'Rainy')
GO
GO
INSERT INTO [dbo].[ClimaConditions] ([ClimaConditionsId], [Condition]) VALUES (N'2', N'Sunny')
GO
GO
INSERT INTO [dbo].[ClimaConditions] ([ClimaConditionsId], [Condition]) VALUES (N'3', N'Cloudy')
GO
GO
SET IDENTITY_INSERT [dbo].[ClimaConditions] OFF
GO

-- ----------------------------
-- Table structure for ContestGames
-- ----------------------------
DROP TABLE [dbo].[ContestGames]
GO
CREATE TABLE [dbo].[ContestGames] (
[ContestId] bigint NOT NULL ,
[GameId] bigint NOT NULL ,
[ContestGameId] bigint NOT NULL IDENTITY(1,1) 
)


GO
DBCC CHECKIDENT(N'[dbo].[ContestGames]', RESEED, 6)
GO

-- ----------------------------
-- Records of ContestGames
-- ----------------------------
SET IDENTITY_INSERT [dbo].[ContestGames] ON
GO
INSERT INTO [dbo].[ContestGames] ([ContestId], [GameId], [ContestGameId]) VALUES (N'1', N'1', N'1')
GO
GO
INSERT INTO [dbo].[ContestGames] ([ContestId], [GameId], [ContestGameId]) VALUES (N'2', N'1', N'4')
GO
GO
INSERT INTO [dbo].[ContestGames] ([ContestId], [GameId], [ContestGameId]) VALUES (N'1', N'2', N'2')
GO
GO
INSERT INTO [dbo].[ContestGames] ([ContestId], [GameId], [ContestGameId]) VALUES (N'2', N'2', N'5')
GO
GO
INSERT INTO [dbo].[ContestGames] ([ContestId], [GameId], [ContestGameId]) VALUES (N'1', N'3', N'3')
GO
GO
INSERT INTO [dbo].[ContestGames] ([ContestId], [GameId], [ContestGameId]) VALUES (N'2', N'3', N'6')
GO
GO
SET IDENTITY_INSERT [dbo].[ContestGames] OFF
GO

-- ----------------------------
-- Table structure for ContestLineups
-- ----------------------------
DROP TABLE [dbo].[ContestLineups]
GO
CREATE TABLE [dbo].[ContestLineups] (
[ContestLineupId] int NOT NULL IDENTITY(1,1) ,
[ContestId] bigint NOT NULL ,
[LineUpId] bigint NOT NULL 
)


GO

-- ----------------------------
-- Records of ContestLineups
-- ----------------------------
SET IDENTITY_INSERT [dbo].[ContestLineups] ON
GO
SET IDENTITY_INSERT [dbo].[ContestLineups] OFF
GO

-- ----------------------------
-- Table structure for Contests
-- ----------------------------
DROP TABLE [dbo].[Contests]
GO
CREATE TABLE [dbo].[Contests] (
[ContestId] bigint NOT NULL IDENTITY(1,1) ,
[ContestTypeId] bigint NOT NULL ,
[Name] nvarchar(MAX) NOT NULL ,
[SignedUp] int NOT NULL ,
[MaxCapacity] int NOT NULL ,
[EntryFee] float(53) NOT NULL ,
[SalaryCap] float(53) NOT NULL 
)


GO
DBCC CHECKIDENT(N'[dbo].[Contests]', RESEED, 2)
GO

-- ----------------------------
-- Records of Contests
-- ----------------------------
SET IDENTITY_INSERT [dbo].[Contests] ON
GO
INSERT INTO [dbo].[Contests] ([ContestId], [ContestTypeId], [Name], [SignedUp], [MaxCapacity], [EntryFee], [SalaryCap]) VALUES (N'1', N'1', N'First Test Contest', N'200', N'500', N'5', N'300000')
GO
GO
INSERT INTO [dbo].[Contests] ([ContestId], [ContestTypeId], [Name], [SignedUp], [MaxCapacity], [EntryFee], [SalaryCap]) VALUES (N'2', N'2', N'Second Test Contest', N'30', N'150', N'35', N'450000')
GO
GO
SET IDENTITY_INSERT [dbo].[Contests] OFF
GO

-- ----------------------------
-- Table structure for ContestTypes
-- ----------------------------
DROP TABLE [dbo].[ContestTypes]
GO
CREATE TABLE [dbo].[ContestTypes] (
[ContestTypeId] bigint NOT NULL IDENTITY(1,1) ,
[Type] nvarchar(MAX) NOT NULL 
)


GO
DBCC CHECKIDENT(N'[dbo].[ContestTypes]', RESEED, 3)
GO

-- ----------------------------
-- Records of ContestTypes
-- ----------------------------
SET IDENTITY_INSERT [dbo].[ContestTypes] ON
GO
INSERT INTO [dbo].[ContestTypes] ([ContestTypeId], [Type]) VALUES (N'1', N'Head-To-Head')
GO
GO
INSERT INTO [dbo].[ContestTypes] ([ContestTypeId], [Type]) VALUES (N'2', N'Tournament')
GO
GO
INSERT INTO [dbo].[ContestTypes] ([ContestTypeId], [Type]) VALUES (N'3', N'First-5')
GO
GO
SET IDENTITY_INSERT [dbo].[ContestTypes] OFF
GO

-- ----------------------------
-- Table structure for Games
-- ----------------------------
DROP TABLE [dbo].[Games]
GO
CREATE TABLE [dbo].[Games] (
[GameId] bigint NOT NULL IDENTITY(1,1) ,
[Scheduled] datetime NOT NULL ,
[Humidity] float(53) NOT NULL ,
[Temperture] float(53) NOT NULL ,
[VenueId] bigint NOT NULL ,
[ClimaConditionsId] bigint NOT NULL ,
[TeamTeamId] bigint NOT NULL ,
[TeamTeamId1] bigint NOT NULL 
)


GO
DBCC CHECKIDENT(N'[dbo].[Games]', RESEED, 3)
GO

-- ----------------------------
-- Records of Games
-- ----------------------------
SET IDENTITY_INSERT [dbo].[Games] ON
GO
INSERT INTO [dbo].[Games] ([GameId], [Scheduled], [Humidity], [Temperture], [VenueId], [ClimaConditionsId], [TeamTeamId], [TeamTeamId1]) VALUES (N'1', N'2018-01-17 23:34:30.000', N'90', N'30', N'1', N'1', N'3', N'2')
GO
GO
INSERT INTO [dbo].[Games] ([GameId], [Scheduled], [Humidity], [Temperture], [VenueId], [ClimaConditionsId], [TeamTeamId], [TeamTeamId1]) VALUES (N'2', N'2018-01-17 23:34:40.000', N'50', N'45', N'3', N'2', N'5', N'4')
GO
GO
INSERT INTO [dbo].[Games] ([GameId], [Scheduled], [Humidity], [Temperture], [VenueId], [ClimaConditionsId], [TeamTeamId], [TeamTeamId1]) VALUES (N'3', N'2018-01-17 23:36:24.000', N'40', N'37', N'5', N'3', N'8', N'6')
GO
GO
SET IDENTITY_INSERT [dbo].[Games] OFF
GO

-- ----------------------------
-- Table structure for Goals
-- ----------------------------
DROP TABLE [dbo].[Goals]
GO
CREATE TABLE [dbo].[Goals] (
[GoalId] bigint NOT NULL IDENTITY(1,1) ,
[Name] nvarchar(MAX) NOT NULL ,
[CompletionCount] int NOT NULL ,
[GoalLogo] nvarchar(MAX) NOT NULL ,
[SportId] bigint NOT NULL 
)


GO
DBCC CHECKIDENT(N'[dbo].[Goals]', RESEED, 2)
GO

-- ----------------------------
-- Records of Goals
-- ----------------------------
SET IDENTITY_INSERT [dbo].[Goals] ON
GO
INSERT INTO [dbo].[Goals] ([GoalId], [Name], [CompletionCount], [GoalLogo], [SportId]) VALUES (N'1', N'Hits', N'6', N'hitlogo.png', N'1')
GO
GO
INSERT INTO [dbo].[Goals] ([GoalId], [Name], [CompletionCount], [GoalLogo], [SportId]) VALUES (N'2', N'Doubles', N'1', N'doublelogo.png', N'1')
GO
GO
SET IDENTITY_INSERT [dbo].[Goals] OFF
GO

-- ----------------------------
-- Table structure for Injuries
-- ----------------------------
DROP TABLE [dbo].[Injuries]
GO
CREATE TABLE [dbo].[Injuries] (
[InjuryId] varchar(36) NOT NULL ,
[Comment] varchar(MAX) NULL ,
[Description] varchar(500) NULL ,
[Status] varchar(50) NOT NULL ,
[StartDate] datetime NOT NULL ,
[UpdateDate] datetime NOT NULL 
)


GO

-- ----------------------------
-- Records of Injuries
-- ----------------------------

-- ----------------------------
-- Table structure for InjuryPlayers
-- ----------------------------
DROP TABLE [dbo].[InjuryPlayers]
GO
CREATE TABLE [dbo].[InjuryPlayers] (
[PlayerId] varchar(36) NOT NULL ,
[Status] varchar(50) NOT NULL ,
[Position] varchar(10) NOT NULL ,
[PrimaryPosition] varchar(10) NOT NULL ,
[FirstName] varchar(100) NOT NULL ,
[LastName] varchar(150) NOT NULL ,
[JerseyNumber] varchar(3) NOT NULL ,
[PreferredName] varchar(50) NOT NULL ,
[InjuryId] varchar(36) NOT NULL ,
[InjuryTeamId] varchar(36) NOT NULL 
)


GO

-- ----------------------------
-- Records of InjuryPlayers
-- ----------------------------

-- ----------------------------
-- Table structure for InjuryTeams
-- ----------------------------
DROP TABLE [dbo].[InjuryTeams]
GO
CREATE TABLE [dbo].[InjuryTeams] (
[TeamId] varchar(36) NOT NULL ,
[Name] varchar(20) NOT NULL ,
[Abbr] varchar(10) NOT NULL ,
[Market] varchar(20) NULL ,
[LeagueId] varchar(36) NOT NULL 
)


GO

-- ----------------------------
-- Records of InjuryTeams
-- ----------------------------

-- ----------------------------
-- Table structure for Leagues
-- ----------------------------
DROP TABLE [dbo].[Leagues]
GO
CREATE TABLE [dbo].[Leagues] (
[LeagueId] varchar(36) NOT NULL ,
[Name] varchar(50) NOT NULL ,
[Alias] varchar(10) NOT NULL 
)


GO

-- ----------------------------
-- Records of Leagues
-- ----------------------------

-- ----------------------------
-- Table structure for LineUps
-- ----------------------------
DROP TABLE [dbo].[LineUps]
GO
CREATE TABLE [dbo].[LineUps] (
[LineUpId] bigint NOT NULL IDENTITY(1,1) ,
[AccountLogin] nvarchar(50) NOT NULL ,
[PlayerLineupId] bigint NOT NULL 
)


GO

-- ----------------------------
-- Records of LineUps
-- ----------------------------
SET IDENTITY_INSERT [dbo].[LineUps] ON
GO
SET IDENTITY_INSERT [dbo].[LineUps] OFF
GO

-- ----------------------------
-- Table structure for Notifications
-- ----------------------------
DROP TABLE [dbo].[Notifications]
GO
CREATE TABLE [dbo].[Notifications] (
[NotificationId] bigint NOT NULL IDENTITY(1,1) ,
[Name] nvarchar(MAX) NOT NULL ,
[Content] nvarchar(MAX) NOT NULL ,
[InitialDate] datetime NOT NULL ,
[FinalDate] datetime NOT NULL 
)


GO
DBCC CHECKIDENT(N'[dbo].[Notifications]', RESEED, 3)
GO

-- ----------------------------
-- Records of Notifications
-- ----------------------------
SET IDENTITY_INSERT [dbo].[Notifications] ON
GO
INSERT INTO [dbo].[Notifications] ([NotificationId], [Name], [Content], [InitialDate], [FinalDate]) VALUES (N'1', N'Notification 1', N'This notification is just a Test', N'2018-01-18 02:02:00.000', N'2018-01-22 02:02:05.000')
GO
GO
INSERT INTO [dbo].[Notifications] ([NotificationId], [Name], [Content], [InitialDate], [FinalDate]) VALUES (N'2', N'Notification 2', N'This notification is just a test 2', N'2018-01-19 02:02:39.000', N'2018-01-31 02:02:46.000')
GO
GO
INSERT INTO [dbo].[Notifications] ([NotificationId], [Name], [Content], [InitialDate], [FinalDate]) VALUES (N'3', N'Notification 3', N'This notification is just a test 3', N'2018-01-16 02:03:11.000', N'2018-02-08 02:03:17.000')
GO
GO
SET IDENTITY_INSERT [dbo].[Notifications] OFF
GO

-- ----------------------------
-- Table structure for PlayerLineups
-- ----------------------------
DROP TABLE [dbo].[PlayerLineups]
GO
CREATE TABLE [dbo].[PlayerLineups] (
[PlayerLineupId] bigint NOT NULL IDENTITY(1,1) ,
[LineupId] bigint NOT NULL ,
[PlayerId] bigint NOT NULL 
)


GO

-- ----------------------------
-- Records of PlayerLineups
-- ----------------------------
SET IDENTITY_INSERT [dbo].[PlayerLineups] ON
GO
SET IDENTITY_INSERT [dbo].[PlayerLineups] OFF
GO

-- ----------------------------
-- Table structure for Players
-- ----------------------------
DROP TABLE [dbo].[Players]
GO
CREATE TABLE [dbo].[Players] (
[PlayerId] bigint NOT NULL IDENTITY(1,1) ,
[FirstName] nvarchar(MAX) NOT NULL ,
[LastName] nvarchar(MAX) NOT NULL ,
[PreferredName] nvarchar(MAX) NOT NULL ,
[TeamId] bigint NOT NULL ,
[PositionId] bigint NOT NULL ,
[Salary] float(53) NOT NULL ,
[Photo] nvarchar(MAX) NOT NULL 
)


GO
DBCC CHECKIDENT(N'[dbo].[Players]', RESEED, 8)
GO

-- ----------------------------
-- Records of Players
-- ----------------------------
SET IDENTITY_INSERT [dbo].[Players] ON
GO
INSERT INTO [dbo].[Players] ([PlayerId], [FirstName], [LastName], [PreferredName], [TeamId], [PositionId], [Salary], [Photo]) VALUES (N'1', N'Player1', N'Player', N'Player1', N'2', N'1', N'300', N'player1.png')
GO
GO
INSERT INTO [dbo].[Players] ([PlayerId], [FirstName], [LastName], [PreferredName], [TeamId], [PositionId], [Salary], [Photo]) VALUES (N'2', N'Player2', N'Player', N'Player2', N'2', N'2', N'470', N'player2.png')
GO
GO
INSERT INTO [dbo].[Players] ([PlayerId], [FirstName], [LastName], [PreferredName], [TeamId], [PositionId], [Salary], [Photo]) VALUES (N'3', N'Player3', N'Player', N'Player3', N'2', N'4', N'560', N'player3.png')
GO
GO
INSERT INTO [dbo].[Players] ([PlayerId], [FirstName], [LastName], [PreferredName], [TeamId], [PositionId], [Salary], [Photo]) VALUES (N'4', N'Player4', N'Player', N'Player4', N'2', N'3', N'300', N'player4.png')
GO
GO
INSERT INTO [dbo].[Players] ([PlayerId], [FirstName], [LastName], [PreferredName], [TeamId], [PositionId], [Salary], [Photo]) VALUES (N'5', N'Player5', N'Player', N'Player5', N'3', N'1', N'300', N'player5.png')
GO
GO
INSERT INTO [dbo].[Players] ([PlayerId], [FirstName], [LastName], [PreferredName], [TeamId], [PositionId], [Salary], [Photo]) VALUES (N'6', N'Player6', N'Player', N'Player6', N'3', N'2', N'470', N'player6.png')
GO
GO
INSERT INTO [dbo].[Players] ([PlayerId], [FirstName], [LastName], [PreferredName], [TeamId], [PositionId], [Salary], [Photo]) VALUES (N'7', N'Player7', N'Player', N'Player7', N'3', N'4', N'560', N'player7.png')
GO
GO
INSERT INTO [dbo].[Players] ([PlayerId], [FirstName], [LastName], [PreferredName], [TeamId], [PositionId], [Salary], [Photo]) VALUES (N'8', N'Player8', N'Player', N'Player8', N'3', N'3', N'300', N'player8.png')
GO
GO
SET IDENTITY_INSERT [dbo].[Players] OFF
GO

-- ----------------------------
-- Table structure for Positions
-- ----------------------------
DROP TABLE [dbo].[Positions]
GO
CREATE TABLE [dbo].[Positions] (
[PositionId] bigint NOT NULL IDENTITY(1,1) ,
[PositionName] nvarchar(MAX) NOT NULL ,
[SportId] bigint NOT NULL 
)


GO
DBCC CHECKIDENT(N'[dbo].[Positions]', RESEED, 9)
GO

-- ----------------------------
-- Records of Positions
-- ----------------------------
SET IDENTITY_INSERT [dbo].[Positions] ON
GO
INSERT INTO [dbo].[Positions] ([PositionId], [PositionName], [SportId]) VALUES (N'1', N'First Base', N'1')
GO
GO
INSERT INTO [dbo].[Positions] ([PositionId], [PositionName], [SportId]) VALUES (N'2', N'Second Base', N'1')
GO
GO
INSERT INTO [dbo].[Positions] ([PositionId], [PositionName], [SportId]) VALUES (N'3', N'Third Base', N'1')
GO
GO
INSERT INTO [dbo].[Positions] ([PositionId], [PositionName], [SportId]) VALUES (N'4', N'Left Field', N'1')
GO
GO
INSERT INTO [dbo].[Positions] ([PositionId], [PositionName], [SportId]) VALUES (N'5', N'Right Field', N'1')
GO
GO
INSERT INTO [dbo].[Positions] ([PositionId], [PositionName], [SportId]) VALUES (N'6', N'Center Field', N'1')
GO
GO
INSERT INTO [dbo].[Positions] ([PositionId], [PositionName], [SportId]) VALUES (N'7', N'Short Stop', N'1')
GO
GO
INSERT INTO [dbo].[Positions] ([PositionId], [PositionName], [SportId]) VALUES (N'8', N'Pitcher', N'1')
GO
GO
INSERT INTO [dbo].[Positions] ([PositionId], [PositionName], [SportId]) VALUES (N'9', N'Catcher', N'1')
GO
GO
SET IDENTITY_INSERT [dbo].[Positions] OFF
GO

-- ----------------------------
-- Table structure for Promotions
-- ----------------------------
DROP TABLE [dbo].[Promotions]
GO
CREATE TABLE [dbo].[Promotions] (
[PromotionId] bigint NOT NULL IDENTITY(1,1) ,
[Name] nvarchar(MAX) NOT NULL ,
[Content] nvarchar(MAX) NOT NULL ,
[Code] nvarchar(MAX) NOT NULL 
)


GO

-- ----------------------------
-- Records of Promotions
-- ----------------------------
SET IDENTITY_INSERT [dbo].[Promotions] ON
GO
SET IDENTITY_INSERT [dbo].[Promotions] OFF
GO

-- ----------------------------
-- Table structure for Sports
-- ----------------------------
DROP TABLE [dbo].[Sports]
GO
CREATE TABLE [dbo].[Sports] (
[SportId] bigint NOT NULL IDENTITY(1,1) ,
[Name] nvarchar(100) NOT NULL 
)


GO

-- ----------------------------
-- Records of Sports
-- ----------------------------
SET IDENTITY_INSERT [dbo].[Sports] ON
GO
INSERT INTO [dbo].[Sports] ([SportId], [Name]) VALUES (N'1', N'Baseball')
GO
GO
SET IDENTITY_INSERT [dbo].[Sports] OFF
GO

-- ----------------------------
-- Table structure for Teams
-- ----------------------------
DROP TABLE [dbo].[Teams]
GO
CREATE TABLE [dbo].[Teams] (
[TeamId] bigint NOT NULL IDENTITY(1,1) ,
[TeamName] nvarchar(MAX) NOT NULL ,
[TeamLogo] nvarchar(MAX) NOT NULL ,
[SportId] bigint NOT NULL 
)


GO
DBCC CHECKIDENT(N'[dbo].[Teams]', RESEED, 10)
GO

-- ----------------------------
-- Records of Teams
-- ----------------------------
SET IDENTITY_INSERT [dbo].[Teams] ON
GO
INSERT INTO [dbo].[Teams] ([TeamId], [TeamName], [TeamLogo], [SportId]) VALUES (N'2', N'New York Yankees', N'yankeelogo.png', N'1')
GO
GO
INSERT INTO [dbo].[Teams] ([TeamId], [TeamName], [TeamLogo], [SportId]) VALUES (N'3', N'Saint Louis Cardinals', N'cardinalslogo.png', N'1')
GO
GO
INSERT INTO [dbo].[Teams] ([TeamId], [TeamName], [TeamLogo], [SportId]) VALUES (N'4', N'Oakland Athletics', N'oaklandlogo.png', N'1')
GO
GO
INSERT INTO [dbo].[Teams] ([TeamId], [TeamName], [TeamLogo], [SportId]) VALUES (N'5', N'Boston Red Sox', N'redsoxlogo.png', N'1')
GO
GO
INSERT INTO [dbo].[Teams] ([TeamId], [TeamName], [TeamLogo], [SportId]) VALUES (N'6', N'San Francisco Giants', N'giantslogo.png', N'1')
GO
GO
INSERT INTO [dbo].[Teams] ([TeamId], [TeamName], [TeamLogo], [SportId]) VALUES (N'8', N'Los Angeles Dodgers', N'dodgerslogo.png', N'1')
GO
GO
INSERT INTO [dbo].[Teams] ([TeamId], [TeamName], [TeamLogo], [SportId]) VALUES (N'10', N'Miami Marlins', N'marlinslogo.png', N'1')
GO
GO
SET IDENTITY_INSERT [dbo].[Teams] OFF
GO

-- ----------------------------
-- Table structure for Venues
-- ----------------------------
DROP TABLE [dbo].[Venues]
GO
CREATE TABLE [dbo].[Venues] (
[VenueId] bigint NOT NULL IDENTITY(1,1) ,
[Name] nvarchar(MAX) NOT NULL ,
[Surface] nvarchar(MAX) NOT NULL ,
[State] nvarchar(MAX) NOT NULL ,
[Country] nvarchar(MAX) NOT NULL 
)


GO
DBCC CHECKIDENT(N'[dbo].[Venues]', RESEED, 7)
GO

-- ----------------------------
-- Records of Venues
-- ----------------------------
SET IDENTITY_INSERT [dbo].[Venues] ON
GO
INSERT INTO [dbo].[Venues] ([VenueId], [Name], [Surface], [State], [Country]) VALUES (N'1', N'Yankee Stadium', N'1000', N'New York', N'US')
GO
GO
INSERT INTO [dbo].[Venues] ([VenueId], [Name], [Surface], [State], [Country]) VALUES (N'2', N'Busch Stadium', N'1000', N'Misuri', N'US')
GO
GO
INSERT INTO [dbo].[Venues] ([VenueId], [Name], [Surface], [State], [Country]) VALUES (N'3', N'Oakland Coliseum', N'1000', N'California', N'US')
GO
GO
INSERT INTO [dbo].[Venues] ([VenueId], [Name], [Surface], [State], [Country]) VALUES (N'4', N'  Fenway Park', N'1000', N'Massachusetts', N'US')
GO
GO
INSERT INTO [dbo].[Venues] ([VenueId], [Name], [Surface], [State], [Country]) VALUES (N'5', N'AT&T Park', N'1000', N'California', N'US')
GO
GO
INSERT INTO [dbo].[Venues] ([VenueId], [Name], [Surface], [State], [Country]) VALUES (N'6', N'Dodger-Stadium', N'1000', N'California', N'US')
GO
GO
INSERT INTO [dbo].[Venues] ([VenueId], [Name], [Surface], [State], [Country]) VALUES (N'7', N'Hard Rock Stadium', N'1000', N'Florida', N'US')
GO
GO
SET IDENTITY_INSERT [dbo].[Venues] OFF
GO

-- ----------------------------
-- Indexes structure for table Accounts
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table Accounts
-- ----------------------------
ALTER TABLE [dbo].[Accounts] ADD PRIMARY KEY ([Login])
GO

-- ----------------------------
-- Indexes structure for table ClimaConditions
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table ClimaConditions
-- ----------------------------
ALTER TABLE [dbo].[ClimaConditions] ADD PRIMARY KEY ([ClimaConditionsId])
GO

-- ----------------------------
-- Indexes structure for table ContestGames
-- ----------------------------
CREATE INDEX [IX_FK_ContestGameGame] ON [dbo].[ContestGames]
([GameId] ASC) 
GO

-- ----------------------------
-- Primary Key structure for table ContestGames
-- ----------------------------
ALTER TABLE [dbo].[ContestGames] ADD PRIMARY KEY ([ContestId], [GameId], [ContestGameId])
GO

-- ----------------------------
-- Indexes structure for table ContestLineups
-- ----------------------------
CREATE INDEX [IX_FK_ContestLineupContest] ON [dbo].[ContestLineups]
([ContestId] ASC) 
GO
CREATE INDEX [IX_FK_LineUpContestLineup] ON [dbo].[ContestLineups]
([LineUpId] ASC) 
GO

-- ----------------------------
-- Primary Key structure for table ContestLineups
-- ----------------------------
ALTER TABLE [dbo].[ContestLineups] ADD PRIMARY KEY ([ContestLineupId], [ContestId], [LineUpId])
GO

-- ----------------------------
-- Indexes structure for table Contests
-- ----------------------------
CREATE INDEX [IX_FK_ContestType_Contest] ON [dbo].[Contests]
([ContestTypeId] ASC) 
GO

-- ----------------------------
-- Primary Key structure for table Contests
-- ----------------------------
ALTER TABLE [dbo].[Contests] ADD PRIMARY KEY ([ContestId])
GO

-- ----------------------------
-- Indexes structure for table ContestTypes
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table ContestTypes
-- ----------------------------
ALTER TABLE [dbo].[ContestTypes] ADD PRIMARY KEY ([ContestTypeId])
GO

-- ----------------------------
-- Indexes structure for table Games
-- ----------------------------
CREATE INDEX [IX_FK_ClimaConditionsGame] ON [dbo].[Games]
([ClimaConditionsId] ASC) 
GO
CREATE INDEX [IX_FK_VenueGame] ON [dbo].[Games]
([VenueId] ASC) 
GO
CREATE INDEX [IX_FK_GameTeam] ON [dbo].[Games]
([TeamTeamId] ASC) 
GO
CREATE INDEX [IX_FK_GameTeam1] ON [dbo].[Games]
([TeamTeamId1] ASC) 
GO

-- ----------------------------
-- Primary Key structure for table Games
-- ----------------------------
ALTER TABLE [dbo].[Games] ADD PRIMARY KEY ([GameId])
GO

-- ----------------------------
-- Indexes structure for table Goals
-- ----------------------------
CREATE INDEX [IX_FK_GoalSport] ON [dbo].[Goals]
([SportId] ASC) 
GO

-- ----------------------------
-- Primary Key structure for table Goals
-- ----------------------------
ALTER TABLE [dbo].[Goals] ADD PRIMARY KEY ([GoalId])
GO

-- ----------------------------
-- Indexes structure for table Injuries
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table Injuries
-- ----------------------------
ALTER TABLE [dbo].[Injuries] ADD PRIMARY KEY ([InjuryId])
GO

-- ----------------------------
-- Indexes structure for table InjuryPlayers
-- ----------------------------
CREATE INDEX [IX_FK_InjuryPlayer_Injury] ON [dbo].[InjuryPlayers]
([InjuryId] ASC) 
GO
CREATE INDEX [IX_FK_InjuryPlayer_InjuryTeam] ON [dbo].[InjuryPlayers]
([InjuryTeamId] ASC) 
GO

-- ----------------------------
-- Primary Key structure for table InjuryPlayers
-- ----------------------------
ALTER TABLE [dbo].[InjuryPlayers] ADD PRIMARY KEY ([PlayerId])
GO

-- ----------------------------
-- Indexes structure for table InjuryTeams
-- ----------------------------
CREATE INDEX [IX_FK_InjuryTeam_League] ON [dbo].[InjuryTeams]
([LeagueId] ASC) 
GO

-- ----------------------------
-- Primary Key structure for table InjuryTeams
-- ----------------------------
ALTER TABLE [dbo].[InjuryTeams] ADD PRIMARY KEY ([TeamId])
GO

-- ----------------------------
-- Indexes structure for table Leagues
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table Leagues
-- ----------------------------
ALTER TABLE [dbo].[Leagues] ADD PRIMARY KEY ([LeagueId])
GO

-- ----------------------------
-- Indexes structure for table LineUps
-- ----------------------------
CREATE INDEX [IX_FK_AccountLineUp] ON [dbo].[LineUps]
([AccountLogin] ASC) 
GO

-- ----------------------------
-- Primary Key structure for table LineUps
-- ----------------------------
ALTER TABLE [dbo].[LineUps] ADD PRIMARY KEY ([LineUpId])
GO

-- ----------------------------
-- Indexes structure for table Notifications
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table Notifications
-- ----------------------------
ALTER TABLE [dbo].[Notifications] ADD PRIMARY KEY ([NotificationId])
GO

-- ----------------------------
-- Indexes structure for table PlayerLineups
-- ----------------------------
CREATE INDEX [IX_FK_PlayerLineupLineUp] ON [dbo].[PlayerLineups]
([LineupId] ASC) 
GO
CREATE INDEX [IX_FK_PlayerLineupPlayer] ON [dbo].[PlayerLineups]
([PlayerId] ASC) 
GO

-- ----------------------------
-- Primary Key structure for table PlayerLineups
-- ----------------------------
ALTER TABLE [dbo].[PlayerLineups] ADD PRIMARY KEY ([PlayerLineupId], [LineupId], [PlayerId])
GO

-- ----------------------------
-- Indexes structure for table Players
-- ----------------------------
CREATE INDEX [IX_FK_TeamPlayer] ON [dbo].[Players]
([TeamId] ASC) 
GO
CREATE INDEX [IX_FK_Position_Player] ON [dbo].[Players]
([PositionId] ASC) 
GO

-- ----------------------------
-- Primary Key structure for table Players
-- ----------------------------
ALTER TABLE [dbo].[Players] ADD PRIMARY KEY ([PlayerId])
GO

-- ----------------------------
-- Indexes structure for table Positions
-- ----------------------------
CREATE INDEX [IX_FK_SportPosition] ON [dbo].[Positions]
([SportId] ASC) 
GO

-- ----------------------------
-- Primary Key structure for table Positions
-- ----------------------------
ALTER TABLE [dbo].[Positions] ADD PRIMARY KEY ([PositionId])
GO

-- ----------------------------
-- Indexes structure for table Promotions
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table Promotions
-- ----------------------------
ALTER TABLE [dbo].[Promotions] ADD PRIMARY KEY ([PromotionId])
GO

-- ----------------------------
-- Indexes structure for table Sports
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table Sports
-- ----------------------------
ALTER TABLE [dbo].[Sports] ADD PRIMARY KEY ([SportId])
GO

-- ----------------------------
-- Indexes structure for table Teams
-- ----------------------------
CREATE INDEX [IX_FK_Sport_Team] ON [dbo].[Teams]
([SportId] ASC) 
GO

-- ----------------------------
-- Primary Key structure for table Teams
-- ----------------------------
ALTER TABLE [dbo].[Teams] ADD PRIMARY KEY ([TeamId])
GO

-- ----------------------------
-- Indexes structure for table Venues
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table Venues
-- ----------------------------
ALTER TABLE [dbo].[Venues] ADD PRIMARY KEY ([VenueId])
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[ContestGames]
-- ----------------------------
ALTER TABLE [dbo].[ContestGames] ADD FOREIGN KEY ([ContestId]) REFERENCES [dbo].[Contests] ([ContestId]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO
ALTER TABLE [dbo].[ContestGames] ADD FOREIGN KEY ([GameId]) REFERENCES [dbo].[Games] ([GameId]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[ContestLineups]
-- ----------------------------
ALTER TABLE [dbo].[ContestLineups] ADD FOREIGN KEY ([ContestId]) REFERENCES [dbo].[Contests] ([ContestId]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO
ALTER TABLE [dbo].[ContestLineups] ADD FOREIGN KEY ([LineUpId]) REFERENCES [dbo].[LineUps] ([LineUpId]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[Contests]
-- ----------------------------
ALTER TABLE [dbo].[Contests] ADD FOREIGN KEY ([ContestTypeId]) REFERENCES [dbo].[ContestTypes] ([ContestTypeId]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[Games]
-- ----------------------------
ALTER TABLE [dbo].[Games] ADD FOREIGN KEY ([ClimaConditionsId]) REFERENCES [dbo].[ClimaConditions] ([ClimaConditionsId]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO
ALTER TABLE [dbo].[Games] ADD FOREIGN KEY ([TeamTeamId]) REFERENCES [dbo].[Teams] ([TeamId]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO
ALTER TABLE [dbo].[Games] ADD FOREIGN KEY ([TeamTeamId1]) REFERENCES [dbo].[Teams] ([TeamId]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO
ALTER TABLE [dbo].[Games] ADD FOREIGN KEY ([VenueId]) REFERENCES [dbo].[Venues] ([VenueId]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[Goals]
-- ----------------------------
ALTER TABLE [dbo].[Goals] ADD FOREIGN KEY ([SportId]) REFERENCES [dbo].[Sports] ([SportId]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[InjuryPlayers]
-- ----------------------------
ALTER TABLE [dbo].[InjuryPlayers] ADD FOREIGN KEY ([InjuryId]) REFERENCES [dbo].[Injuries] ([InjuryId]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO
ALTER TABLE [dbo].[InjuryPlayers] ADD FOREIGN KEY ([InjuryTeamId]) REFERENCES [dbo].[InjuryTeams] ([TeamId]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[InjuryTeams]
-- ----------------------------
ALTER TABLE [dbo].[InjuryTeams] ADD FOREIGN KEY ([LeagueId]) REFERENCES [dbo].[Leagues] ([LeagueId]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[LineUps]
-- ----------------------------
ALTER TABLE [dbo].[LineUps] ADD FOREIGN KEY ([AccountLogin]) REFERENCES [dbo].[Accounts] ([Login]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[PlayerLineups]
-- ----------------------------
ALTER TABLE [dbo].[PlayerLineups] ADD FOREIGN KEY ([LineupId]) REFERENCES [dbo].[LineUps] ([LineUpId]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO
ALTER TABLE [dbo].[PlayerLineups] ADD FOREIGN KEY ([PlayerId]) REFERENCES [dbo].[Players] ([PlayerId]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[Players]
-- ----------------------------
ALTER TABLE [dbo].[Players] ADD FOREIGN KEY ([PositionId]) REFERENCES [dbo].[Positions] ([PositionId]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO
ALTER TABLE [dbo].[Players] ADD FOREIGN KEY ([TeamId]) REFERENCES [dbo].[Teams] ([TeamId]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[Positions]
-- ----------------------------
ALTER TABLE [dbo].[Positions] ADD FOREIGN KEY ([SportId]) REFERENCES [dbo].[Sports] ([SportId]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[Teams]
-- ----------------------------
ALTER TABLE [dbo].[Teams] ADD FOREIGN KEY ([SportId]) REFERENCES [dbo].[Sports] ([SportId]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO
