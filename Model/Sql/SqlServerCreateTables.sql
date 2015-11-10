/* 
 * SQL Server Script
 * 
 * This script can be directly executed to configure the test database from
 * PCs located at CECAFI Lab. The database and the corresponding users are 
 * already created in the sql server, so it will create the tables needed 
 * in the samples. 
 * 
 * In a local environment (for example, with the SQLServerExpress instance 
 * included in the VStudio installation) it will be necessary to create the 
 * database and the user required by the connection string. So, the following
 * steps are needed:
 *
 *      Configure within the CREATE DATABASE sql-sentence the path where 
 *      database and log files will be created  
 *
 * This script can be executed from MS Sql Server Management Studio Express,
 * but also it is possible to use a command Line syntax:
 *
 *    > sqlcmd.exe -U [user] -P [password] -I -i SqlServerCreateTables.sql
 *
 */

 
USE [mad]


/* ********** Drop Table UserProfile if already exists *********** */
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[Comment]') AND type in ('U'))
ALTER TABLE [Comment] DROP CONSTRAINT FK_CommentUser
ALTER TABLE [Comment] DROP CONSTRAINT FK_CommentEvent
DROP TABLE [Comment]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[GroupMember]') AND type in ('U'))
ALTER TABLE [GroupMember] DROP CONSTRAINT FK_Member
ALTER TABLE [GroupMember] DROP CONSTRAINT FK_GroupGuser
DROP TABLE [GroupMember]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[RecomendationToGroup]') AND type in ('U'))
ALTER TABLE [RecomendationToGroup] DROP CONSTRAINT FK_ToGroup
ALTER TABLE [RecomendationToGroup] DROP CONSTRAINT FK_ToRecomendation
DROP TABLE [RecomendationToGroup]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[Recomendation]') AND type in ('U'))
ALTER TABLE [Recomendation] DROP CONSTRAINT FK_RecomendationGroup
DROP TABLE [Recomendation]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[UserProfile]') AND type in ('U'))
DROP TABLE [UserProfile]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[Event]') AND type in ('U'))
ALTER TABLE [Event] DROP CONSTRAINT FK_Category
DROP TABLE [Event]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[Category]') AND type in ('U'))
DROP TABLE [Category]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[UserGroup]') AND type in ('U'))
DROP TABLE [UserGroup]
GO

/*
 * Create tables.
 * UserProfile table is created. Indexes required for the 
 * most common operations are also defined.
 */

/*  UserProfile */

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


CREATE TABLE Category (
	categoryId bigint IDENTITY(1,1) NOT NULL,
	categoryName varchar(30) NOT NULL,

	CONSTRAINT [PK_Category] PRIMARY KEY (categoryId)
)

PRINT N'Table Category created.'
GO

/* ********** Drop Table Event if already exists *********** */




/*
 * Create tables.
 * Event table is created. Indexes required for the 
 * most common operations are also defined.
 */

/*  Event */

CREATE TABLE Event (
	eventId bigint IDENTITY(1,1) NOT NULL,
	eventName varchar(30) NOT NULL,
	review varchar(255) NOT NULL,
	date datetime2 NOT NULL,
	category bigint NOT NULL,

	CONSTRAINT [PK_Event] PRIMARY KEY (eventId),
	CONSTRAINT [FK_Category] FOREIGN KEY(category)
        REFERENCES Category (categoryId) ON DELETE CASCADE
)

PRINT N'Table Event created.'
GO


/* ********** Drop Table Group if already exists *********** */



/*
 * Create tables.
 * UserGroup table is created. Indexes required for the 
 * most common operations are also defined.
 */

/*  UserGroup */

CREATE TABLE UserGroup (
	groupId bigint IDENTITY(1,1) NOT NULL,
	groupName varchar(30) NOT NULL,
	description varchar(255) NOT NULL,

	CONSTRAINT [PK_Group] PRIMARY KEY (groupId)
)

PRINT N'Table UserGroup created.'
GO

/* ********** Drop Table Comment if already exists *********** */



/*
 * Create tables.
 * Comment table is created. Indexes required for the 
 * most common operations are also defined.
 */

/*  Comment */

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

/* ********** Drop Table Recomendation if already exists *********** */



/*
 * Create tables.
 * Recomendation table is created. Indexes required for the 
 * most common operations are also defined.
 */

/*  Recomendation */

CREATE TABLE Recomendation (
	recomendationId bigint IDENTITY(1,1) NOT NULL,
	groupId bigint NOT NULL,
	date datetime2 NOT NULL,
	description varchar(255) NOT NULL,

	CONSTRAINT [PK_Recomendation] PRIMARY KEY (recomendationId),
	CONSTRAINT [FK_RecomendationGroup] FOREIGN KEY(groupId)
        REFERENCES UserGroup (groupId) ON DELETE CASCADE
)

PRINT N'Table Recomendation created.'
GO

/* ********** Drop Table GroupMember if already exists *********** */



/*
 * Create tables.
 * GroupMember table is created. Indexes required for the 
 * most common operations are also defined.
 */

/*  GroupMember */

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

/* ********** Drop Table RecomendationToUser if already exists *********** */



/*
 * Create tables.
 * RecomendationToUser table is created. Indexes required for the 
 * most common operations are also defined.
 */

/*  RecomendationToUser */

CREATE TABLE RecomendationToGroup (
	groupId bigint NOT NULL,
	recomendationId bigint NOT NULL,

	CONSTRAINT [FK_ToGroup] FOREIGN KEY(groupId)
        REFERENCES UserGroup (groupId) ON DELETE CASCADE,
    CONSTRAINT [FK_ToRecomendation] FOREIGN KEY(recomendationId)
        REFERENCES Recomendation (recomendationId) ON DELETE NO ACTION,
	CONSTRAINT [PK_RecomendationTOGroup] PRIMARY KEY (groupId,recomendationId)
)

PRINT N'Table RecomendationToUser created.'
GO

/* Crear tabla que relacion recomendaciones con usuarios */

GO
