/*
Navicat SQL Server Data Transfer

Source Server         : MSSQLSERVER
Source Server Version : 120000
Source Host           : .:1433
Source Database       : Fantasy
Source Schema         : dbo

Target Server Type    : SQL Server
Target Server Version : 120000
File Encoding         : 65001

Date: 2017-12-23 13:33:40
*/


-- ----------------------------
-- Table structure for Account
-- ----------------------------
DROP TABLE [dbo].[Account]
GO
CREATE TABLE [dbo].[Account] (
[Login] nvarchar(50) NOT NULL ,
[Email] nvarchar(250) NULL ,
[Password] nvarchar(50) NULL 
)


GO

-- ----------------------------
-- Records of Account
-- ----------------------------
INSERT INTO [dbo].[Account] ([Login], [Email], [Password]) VALUES (N'admin', N'admin@admins.com', N'password')
GO
GO

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

-- ----------------------------
-- Table structure for ClimaConditions
-- ----------------------------
DROP TABLE [dbo].[ClimaConditions]
GO
CREATE TABLE [dbo].[ClimaConditions] (
[ClimaId] int NOT NULL IDENTITY(1,1) ,
[Condition] nvarchar(MAX) NOT NULL 
)


GO

-- ----------------------------
-- Records of ClimaConditions
-- ----------------------------
SET IDENTITY_INSERT [dbo].[ClimaConditions] ON
GO
SET IDENTITY_INSERT [dbo].[ClimaConditions] OFF
GO

-- ----------------------------
-- Table structure for ContestGame
-- ----------------------------
DROP TABLE [dbo].[ContestGame]
GO
CREATE TABLE [dbo].[ContestGame] (
[Contests_ContestId] int NOT NULL ,
[Games_GameId] int NOT NULL 
)


GO

-- ----------------------------
-- Records of ContestGame
-- ----------------------------

-- ----------------------------
-- Table structure for Contests
-- ----------------------------
DROP TABLE [dbo].[Contests]
GO
CREATE TABLE [dbo].[Contests] (
[ContestId] int NOT NULL IDENTITY(1,1) ,
[ContestTypeId] int NOT NULL ,
[Name] nvarchar(MAX) NOT NULL ,
[SignedUp] int NOT NULL ,
[MaxCapacity] int NOT NULL ,
[EntryFee] float(53) NOT NULL ,
[SalaryCap] float(53) NOT NULL 
)


GO

-- ----------------------------
-- Records of Contests
-- ----------------------------
SET IDENTITY_INSERT [dbo].[Contests] ON
GO
SET IDENTITY_INSERT [dbo].[Contests] OFF
GO

-- ----------------------------
-- Table structure for ContestTypes
-- ----------------------------
DROP TABLE [dbo].[ContestTypes]
GO
CREATE TABLE [dbo].[ContestTypes] (
[ContestTypeId] int NOT NULL IDENTITY(1,1) ,
[Type] nvarchar(MAX) NOT NULL 
)


GO

-- ----------------------------
-- Records of ContestTypes
-- ----------------------------
SET IDENTITY_INSERT [dbo].[ContestTypes] ON
GO
SET IDENTITY_INSERT [dbo].[ContestTypes] OFF
GO

-- ----------------------------
-- Table structure for Games
-- ----------------------------
DROP TABLE [dbo].[Games]
GO
CREATE TABLE [dbo].[Games] (
[GameId] int NOT NULL IDENTITY(1,1) ,
[Scheduled] datetime NOT NULL ,
[Humidity] float(53) NOT NULL ,
[Temperture] float(53) NOT NULL ,
[VenueId] int NOT NULL ,
[AwayTeam_TeamId] int NOT NULL ,
[HomeTeam_TeamId] int NOT NULL ,
[ClimaCondition_ClimaId] int NOT NULL 
)


GO

-- ----------------------------
-- Records of Games
-- ----------------------------
SET IDENTITY_INSERT [dbo].[Games] ON
GO
SET IDENTITY_INSERT [dbo].[Games] OFF
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
-- Table structure for LineUpContest
-- ----------------------------
DROP TABLE [dbo].[LineUpContest]
GO
CREATE TABLE [dbo].[LineUpContest] (
[LineUps_LineUpId] int NOT NULL ,
[Contests_ContestId] int NOT NULL 
)


GO

-- ----------------------------
-- Records of LineUpContest
-- ----------------------------

-- ----------------------------
-- Table structure for LineUpPlayer
-- ----------------------------
DROP TABLE [dbo].[LineUpPlayer]
GO
CREATE TABLE [dbo].[LineUpPlayer] (
[LineUps_LineUpId] int NOT NULL ,
[Players_PlayerId] int NOT NULL 
)


GO

-- ----------------------------
-- Records of LineUpPlayer
-- ----------------------------

-- ----------------------------
-- Table structure for LineUps
-- ----------------------------
DROP TABLE [dbo].[LineUps]
GO
CREATE TABLE [dbo].[LineUps] (
[LineUpId] int NOT NULL IDENTITY(1,1) ,
[AccountLogin] nvarchar(50) NOT NULL 
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
[NotificationId] int NOT NULL IDENTITY(1,1) ,
[Name] nvarchar(MAX) NOT NULL ,
[Content] nvarchar(MAX) NOT NULL ,
[InitialDate] datetime NOT NULL ,
[FinalDate] datetime NOT NULL 
)


GO

-- ----------------------------
-- Records of Notifications
-- ----------------------------
SET IDENTITY_INSERT [dbo].[Notifications] ON
GO
SET IDENTITY_INSERT [dbo].[Notifications] OFF
GO

-- ----------------------------
-- Table structure for Players
-- ----------------------------
DROP TABLE [dbo].[Players]
GO
CREATE TABLE [dbo].[Players] (
[PlayerId] int NOT NULL IDENTITY(1,1) ,
[FirstName] nvarchar(MAX) NOT NULL ,
[LastName] nvarchar(MAX) NOT NULL ,
[PreferredName] nvarchar(MAX) NOT NULL ,
[TeamId] int NOT NULL ,
[PositionId] int NOT NULL ,
[Salary] float(53) NOT NULL ,
[Photo] nvarchar(MAX) NOT NULL ,
[Sport_SportId] int NOT NULL 
)


GO

-- ----------------------------
-- Records of Players
-- ----------------------------
SET IDENTITY_INSERT [dbo].[Players] ON
GO
SET IDENTITY_INSERT [dbo].[Players] OFF
GO

-- ----------------------------
-- Table structure for Positions
-- ----------------------------
DROP TABLE [dbo].[Positions]
GO
CREATE TABLE [dbo].[Positions] (
[PositionId] int NOT NULL IDENTITY(1,1) ,
[PositionName] nvarchar(MAX) NOT NULL ,
[SportId] nvarchar(MAX) NOT NULL 
)


GO

-- ----------------------------
-- Records of Positions
-- ----------------------------
SET IDENTITY_INSERT [dbo].[Positions] ON
GO
SET IDENTITY_INSERT [dbo].[Positions] OFF
GO

-- ----------------------------
-- Table structure for Promotions
-- ----------------------------
DROP TABLE [dbo].[Promotions]
GO
CREATE TABLE [dbo].[Promotions] (
[PromoId] int NOT NULL IDENTITY(1,1) ,
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
[SportId] int NOT NULL IDENTITY(1,1) ,
[Name] nvarchar(100) NOT NULL ,
[PositionId] int NOT NULL 
)


GO

-- ----------------------------
-- Records of Sports
-- ----------------------------
SET IDENTITY_INSERT [dbo].[Sports] ON
GO
SET IDENTITY_INSERT [dbo].[Sports] OFF
GO

-- ----------------------------
-- Table structure for Teams
-- ----------------------------
DROP TABLE [dbo].[Teams]
GO
CREATE TABLE [dbo].[Teams] (
[TeamId] int NOT NULL IDENTITY(1,1) ,
[TeamName] nvarchar(MAX) NOT NULL ,
[TeamLogo] nvarchar(MAX) NOT NULL ,
[SportId] int NOT NULL 
)


GO

-- ----------------------------
-- Records of Teams
-- ----------------------------
SET IDENTITY_INSERT [dbo].[Teams] ON
GO
SET IDENTITY_INSERT [dbo].[Teams] OFF
GO

-- ----------------------------
-- Table structure for Venues
-- ----------------------------
DROP TABLE [dbo].[Venues]
GO
CREATE TABLE [dbo].[Venues] (
[VenueId] int NOT NULL IDENTITY(1,1) ,
[Name] nvarchar(MAX) NOT NULL ,
[Surface] nvarchar(MAX) NOT NULL ,
[State] nvarchar(MAX) NOT NULL ,
[Country] nvarchar(MAX) NOT NULL 
)


GO

-- ----------------------------
-- Records of Venues
-- ----------------------------
SET IDENTITY_INSERT [dbo].[Venues] ON
GO
SET IDENTITY_INSERT [dbo].[Venues] OFF
GO

-- ----------------------------
-- Indexes structure for table Account
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table Account
-- ----------------------------
ALTER TABLE [dbo].[Account] ADD PRIMARY KEY ([Login])
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
ALTER TABLE [dbo].[ClimaConditions] ADD PRIMARY KEY ([ClimaId])
GO

-- ----------------------------
-- Indexes structure for table ContestGame
-- ----------------------------
CREATE INDEX [IX_FK_ContestGame_Game] ON [dbo].[ContestGame]
([Games_GameId] ASC) 
GO

-- ----------------------------
-- Primary Key structure for table ContestGame
-- ----------------------------
ALTER TABLE [dbo].[ContestGame] ADD PRIMARY KEY ([Contests_ContestId], [Games_GameId])
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
CREATE INDEX [IX_FK_Game_AwayTeam] ON [dbo].[Games]
([AwayTeam_TeamId] ASC) 
GO
CREATE INDEX [IX_FK_GameTeam] ON [dbo].[Games]
([HomeTeam_TeamId] ASC) 
GO
CREATE INDEX [IX_FK_ClimaConditionsGame] ON [dbo].[Games]
([ClimaCondition_ClimaId] ASC) 
GO
CREATE INDEX [IX_FK_VenueGame] ON [dbo].[Games]
([VenueId] ASC) 
GO

-- ----------------------------
-- Primary Key structure for table Games
-- ----------------------------
ALTER TABLE [dbo].[Games] ADD PRIMARY KEY ([GameId])
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
-- Indexes structure for table LineUpContest
-- ----------------------------
CREATE INDEX [IX_FK_LineUpContest_Contest] ON [dbo].[LineUpContest]
([Contests_ContestId] ASC) 
GO

-- ----------------------------
-- Primary Key structure for table LineUpContest
-- ----------------------------
ALTER TABLE [dbo].[LineUpContest] ADD PRIMARY KEY ([LineUps_LineUpId], [Contests_ContestId])
GO

-- ----------------------------
-- Indexes structure for table LineUpPlayer
-- ----------------------------
CREATE INDEX [IX_FK_LineUpPlayer_Player] ON [dbo].[LineUpPlayer]
([Players_PlayerId] ASC) 
GO

-- ----------------------------
-- Primary Key structure for table LineUpPlayer
-- ----------------------------
ALTER TABLE [dbo].[LineUpPlayer] ADD PRIMARY KEY ([LineUps_LineUpId], [Players_PlayerId])
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
-- Indexes structure for table Players
-- ----------------------------
CREATE INDEX [IX_FK_TeamPlayer] ON [dbo].[Players]
([TeamId] ASC) 
GO
CREATE INDEX [IX_FK_Player_Sport] ON [dbo].[Players]
([Sport_SportId] ASC) 
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
ALTER TABLE [dbo].[Promotions] ADD PRIMARY KEY ([PromoId])
GO

-- ----------------------------
-- Indexes structure for table Sports
-- ----------------------------
CREATE INDEX [IX_FK_Position_Sport] ON [dbo].[Sports]
([PositionId] ASC) 
GO

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
-- Foreign Key structure for table [dbo].[ContestGame]
-- ----------------------------
ALTER TABLE [dbo].[ContestGame] ADD FOREIGN KEY ([Contests_ContestId]) REFERENCES [dbo].[Contests] ([ContestId]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO
ALTER TABLE [dbo].[ContestGame] ADD FOREIGN KEY ([Games_GameId]) REFERENCES [dbo].[Games] ([GameId]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[LineUpContest]
-- ----------------------------
ALTER TABLE [dbo].[LineUpContest] ADD FOREIGN KEY ([Contests_ContestId]) REFERENCES [dbo].[Contests] ([ContestId]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO
ALTER TABLE [dbo].[LineUpContest] ADD FOREIGN KEY ([LineUps_LineUpId]) REFERENCES [dbo].[LineUps] ([LineUpId]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[LineUpPlayer]
-- ----------------------------
ALTER TABLE [dbo].[LineUpPlayer] ADD FOREIGN KEY ([LineUps_LineUpId]) REFERENCES [dbo].[LineUps] ([LineUpId]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO
ALTER TABLE [dbo].[LineUpPlayer] ADD FOREIGN KEY ([Players_PlayerId]) REFERENCES [dbo].[Players] ([PlayerId]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[LineUps]
-- ----------------------------
ALTER TABLE [dbo].[LineUps] ADD FOREIGN KEY ([AccountLogin]) REFERENCES [dbo].[Accounts] ([Login]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO
