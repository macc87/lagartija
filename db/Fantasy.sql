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

Date: 2017-12-03 20:10:53
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
-- Indexes structure for table Account
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table Account
-- ----------------------------
ALTER TABLE [dbo].[Account] ADD PRIMARY KEY ([Login])
GO
