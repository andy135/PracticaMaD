/* 
 * SQL Server Script
 * 
 * In a local environment (for example, with the SQLServerExpress instance 
 * included in the VStudio installation) it will be necessary to create the 
 * database and the user required by the connection string. So, the following
 * steps are needed:
 *
 *     Configure the @Default_DB_Path variable with the path where 
 *     database and log files will be created  
 *
 * This script can be executed from MS Sql Server Management Studio Express,
 * but also it is possible to use a command Line syntax:
 *
 *    > sqlcmd.exe -U [user] -P [password] -I -i SqlServerCreateTables.sql
 *
 */


 /******************************************************************************/
 /*** PATH to store the db files. This path must exists in the local system. ***/
 /******************************************************************************/
 DECLARE @Default_DB_Path as VARCHAR(64)  
 SET @Default_DB_Path = N'C:\SourceCode\DataBase\'
 
USE [master]


/***** Drop database if already exists  ******/
IF  EXISTS (SELECT name FROM sys.databases WHERE name = 'mad_test')
DROP DATABASE [mad_test]


USE [master]


/* DataBase Creation */

	                              
DECLARE @sql nvarchar(500)

SET @sql = 
  N'CREATE DATABASE [mad_test] 
    ON PRIMARY ( NAME = mad_test, FILENAME = "' + @Default_DB_Path + N'mad_test.mdf")
    LOG ON ( NAME = mad_test_log, FILENAME = "' + @Default_DB_Path + N'mad_test_log.ldf")'

EXEC(@sql)
PRINT N'Database [MaD_test] created.'
GO


USE [mad_test]

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
	categoryId bigint NOT NULL,

	CONSTRAINT [PK_Event] PRIMARY KEY (eventId),
	CONSTRAINT [FK_Category] FOREIGN KEY(categoryId)
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
	date datetime2 NOT NULL,
	description varchar(255) NOT NULL,
	eventId bigint NOT NULL,

	CONSTRAINT [PK_Recomendation] PRIMARY KEY (recomendationId),
    CONSTRAINT [FK_RecomendationEvent] FOREIGN KEY(eventId)
        REFERENCES Event (eventId) ON DELETE CASCADE
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

GO

