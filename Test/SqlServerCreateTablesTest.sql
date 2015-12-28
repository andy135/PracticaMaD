DECLARE @Default_DB_Path as VARCHAR(64)
SET @Default_DB_Path = N'C:\Workspace\C#\MaD\DB'


USE [master]

/****** Drop database if already exists  ******/
IF  EXISTS (SELECT name FROM sys.databases WHERE name = 'mad_test')
DROP DATABASE [mad_test]

DECLARE @sql nvarchar(500)

SET @sql =
  N'CREATE DATABASE [mad_test]
    ON PRIMARY ( NAME = mad_test, FILENAME = "' + @Default_DB_Path + N'mad_test.mdf")
    LOG ON ( NAME = mad_test_log, FILENAME = "' + @Default_DB_Path + N'mad_test_log.ldf")'

EXEC(@sql)
PRINT N'Test Database created.'
PRINT N'Done'
GO

/** ************************************************************************ **/
USE [mad_test]

CREATE TABLE UserProfile (
	usrId bigint IDENTITY(1,1) NOT NULL,
	loginName varchar(30) NOT NULL,
	enPassword varchar(50) NOT NULL,
	firstName varchar(30) NOT NULL,
	lastName varchar(40) NOT NULL,
	email varchar(60) NOT NULL,
	language varchar(2) NOT NULL,
	country varchar(2) NOT NULL,

	CONSTRAINT [PK_UserProfile] PRIMARY KEY (usrId),
	CONSTRAINT [UniqueKey_Login] UNIQUE (loginName)
)

CREATE NONCLUSTERED INDEX [IX_UserProfileIndexByLoginName]
ON [UserProfile] ([loginName] ASC)

PRINT N'Table UserProfile created.'
GO

/** ************************************************************************ **/

CREATE TABLE Tag (
	tagId bigint IDENTITY(1,1) NOT NULL,
	tagName varchar(30) NOT NULL,
	usedNum bigint NOT NULL,


	CONSTRAINT [PK_Tag] PRIMARY KEY (tagId),
	CONSTRAINT [UniqueTag_Name] UNIQUE (tagName)
)

CREATE NONCLUSTERED INDEX [IX_TagIndexByTagName]
ON [Tag] ([tagName] ASC)

PRINT N'Table Tag created.'
GO

/** ************************************************************************ **/

CREATE TABLE Category (
	categoryId bigint IDENTITY(1,1) NOT NULL,
	categoryName varchar(30) NOT NULL,

	CONSTRAINT [PK_Category] PRIMARY KEY (categoryId)
)

PRINT N'Table Category created.'
GO

/** ************************************************************************ **/

CREATE TABLE Event (
	eventId bigint IDENTITY(1,1) NOT NULL,
	eventName varchar(30) NOT NULL,
	review varchar(255) NOT NULL,
	date datetime2 NOT NULL,
	categoryId bigint NOT NULL,

	CONSTRAINT [PK_Event] PRIMARY KEY (eventId),
	CONSTRAINT [FK_Category] FOREIGN KEY(categoryId)
        REFERENCES Category (categoryId) ON DELETE CASCADE
)

PRINT N'Table Event created.'
GO

/** ************************************************************************ **/

CREATE TABLE UserGroup (
	groupId bigint IDENTITY(1,1) NOT NULL,
	groupName varchar(30) NOT NULL,
	description varchar(255) NOT NULL,

	CONSTRAINT [PK_Group] PRIMARY KEY (groupId)
)

PRINT N'Table UserGroup created.'
GO

/** ************************************************************************ **/

CREATE TABLE Comment (
	commentId bigint IDENTITY(1,1) NOT NULL,
	usrId bigint NOT NULL,
	eventId bigint NOT NULL,
	date datetime2 NOT NULL,
	texto varchar(255) NOT NULL,

	CONSTRAINT [PK_Comment] PRIMARY KEY (commentId),
	CONSTRAINT [FK_CommentUser] FOREIGN KEY(usrId)
        REFERENCES UserProfile (usrId) ON DELETE CASCADE,
    CONSTRAINT [FK_CommentEvent] FOREIGN KEY(eventId)
        REFERENCES Event (eventId) ON DELETE CASCADE
)

PRINT N'Table Comment created.'
GO

/** ************************************************************************ **/

CREATE TABLE Recomendation (
	recomendationId bigint IDENTITY(1,1) NOT NULL,
	date datetime2 NOT NULL,
	description varchar(255) NOT NULL,
	eventId bigint NOT NULL,

	CONSTRAINT [PK_Recomendation] PRIMARY KEY (recomendationId),
    CONSTRAINT [FK_RecomendationEvent] FOREIGN KEY(eventId)
        REFERENCES Event (eventId) ON DELETE CASCADE
)

PRINT N'Table Recomendation created.'
GO

/** ************************************************************************ **/

CREATE TABLE GroupMember (
	usrId bigint NOT NULL,
	groupId bigint NOT NULL,

	CONSTRAINT [PK_GroupMember] PRIMARY KEY (usrId,groupId),
	CONSTRAINT [FK_Member] FOREIGN KEY(usrId)
        REFERENCES UserProfile (usrId) ON DELETE CASCADE,
    CONSTRAINT [FK_GroupGuser] FOREIGN KEY(groupId)
        REFERENCES UserGroup (groupId) ON DELETE CASCADE
)

PRINT N'Table GroupMember created.'
GO

/** ************************************************************************ **/

CREATE TABLE RecomendationToGroup (
	groupId bigint NOT NULL,
	recomendationId bigint NOT NULL,

    CONSTRAINT [PK_RecomendationTOGroup] PRIMARY KEY (groupId,recomendationId),
	  CONSTRAINT [FK_ToGroup] FOREIGN KEY(groupId)
        REFERENCES UserGroup (groupId) ON DELETE CASCADE,
    CONSTRAINT [FK_ToRecomendation] FOREIGN KEY(recomendationId)
        REFERENCES Recomendation (recomendationId) ON DELETE CASCADE
)

PRINT N'Table RecomendationToUser created.'
GO

/** ************************************************************************ **/

CREATE TABLE TagToComment (
	tagId bigint NOT NULL,
	commentId bigint NOT NULL,

  CONSTRAINT [PK_TagToComment] PRIMARY KEY (tagId,commentId),
	CONSTRAINT [FK_ToTag] FOREIGN KEY(tagId)
        REFERENCES Tag (tagId) ON DELETE CASCADE,
    CONSTRAINT [FK_ToComment] FOREIGN KEY(commentId)
        REFERENCES Comment (commentId) ON DELETE CASCADE
)

PRINT N'Table TagToComment created.'
GO

GO
