/*
 Navicat Premium Data Transfer

 Source Server         : .
 Source Server Type    : SQL Server
 Source Server Version : 15002000
 Source Host           : .:1433
 Source Catalog        : WebAdmin
 Source Schema         : dbo

 Target Server Type    : SQL Server
 Target Server Version : 15002000
 File Encoding         : 65001

 Date: 10/03/2023 16:44:05
*/


-- ----------------------------
-- Table structure for Base_Menu_Buttons
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[Base_Menu_Buttons]') AND type IN ('U'))
	DROP TABLE [dbo].[Base_Menu_Buttons]
GO

CREATE TABLE [dbo].[Base_Menu_Buttons] (
  [Id] varchar(50) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [ButtonName] varchar(255) COLLATE Chinese_PRC_CI_AS  NULL,
  [CoreUrl] varchar(255) COLLATE Chinese_PRC_CI_AS  NULL,
  [PageId] varchar(50) COLLATE Chinese_PRC_CI_AS  NOT NULL
)
GO

ALTER TABLE [dbo].[Base_Menu_Buttons] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'按钮表主键Id',
'SCHEMA', N'dbo',
'TABLE', N'Base_Menu_Buttons',
'COLUMN', N'Id'
GO

EXEC sp_addextendedproperty
'MS_Description', N'按钮名称',
'SCHEMA', N'dbo',
'TABLE', N'Base_Menu_Buttons',
'COLUMN', N'ButtonName'
GO

EXEC sp_addextendedproperty
'MS_Description', N'路由地址',
'SCHEMA', N'dbo',
'TABLE', N'Base_Menu_Buttons',
'COLUMN', N'CoreUrl'
GO

EXEC sp_addextendedproperty
'MS_Description', N'菜单页面主键Id',
'SCHEMA', N'dbo',
'TABLE', N'Base_Menu_Buttons',
'COLUMN', N'PageId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'按钮表',
'SCHEMA', N'dbo',
'TABLE', N'Base_Menu_Buttons'
GO


-- ----------------------------
-- Records of Base_Menu_Buttons
-- ----------------------------

-- ----------------------------
-- Table structure for Base_Menu_Tree
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[Base_Menu_Tree]') AND type IN ('U'))
	DROP TABLE [dbo].[Base_Menu_Tree]
GO

CREATE TABLE [dbo].[Base_Menu_Tree] (
  [Id] varchar(50) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [MenuName] varchar(255) COLLATE Chinese_PRC_CI_AS  NULL,
  [ParentId] varchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [CoreUrl] varchar(255) COLLATE Chinese_PRC_CI_AS  NULL,
  [Icon] varchar(255) COLLATE Chinese_PRC_CI_AS  NULL,
  [SortCode] int  NULL,
  [TargetClass] varchar(255) COLLATE Chinese_PRC_CI_AS  NULL,
  [EnabledMark] int  NULL,
  [Description] varchar(2000) COLLATE Chinese_PRC_CI_AS  NULL,
  [CreateDate] datetime  NULL,
  [CreateUserId] varchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [CreateUserName] varchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [ModifyDate] datetime  NULL,
  [ModifyUserId] varchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [ModifyUserName] varchar(50) COLLATE Chinese_PRC_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[Base_Menu_Tree] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'页面菜单表主键Id',
'SCHEMA', N'dbo',
'TABLE', N'Base_Menu_Tree',
'COLUMN', N'Id'
GO

EXEC sp_addextendedproperty
'MS_Description', N'菜单名称',
'SCHEMA', N'dbo',
'TABLE', N'Base_Menu_Tree',
'COLUMN', N'MenuName'
GO

EXEC sp_addextendedproperty
'MS_Description', N'父级主键Id',
'SCHEMA', N'dbo',
'TABLE', N'Base_Menu_Tree',
'COLUMN', N'ParentId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'路由地址',
'SCHEMA', N'dbo',
'TABLE', N'Base_Menu_Tree',
'COLUMN', N'CoreUrl'
GO

EXEC sp_addextendedproperty
'MS_Description', N'图标',
'SCHEMA', N'dbo',
'TABLE', N'Base_Menu_Tree',
'COLUMN', N'Icon'
GO

EXEC sp_addextendedproperty
'MS_Description', N'排序',
'SCHEMA', N'dbo',
'TABLE', N'Base_Menu_Tree',
'COLUMN', N'SortCode'
GO

EXEC sp_addextendedproperty
'MS_Description', N'菜单Class',
'SCHEMA', N'dbo',
'TABLE', N'Base_Menu_Tree',
'COLUMN', N'TargetClass'
GO

EXEC sp_addextendedproperty
'MS_Description', N'有效标志(0无效，1有效)',
'SCHEMA', N'dbo',
'TABLE', N'Base_Menu_Tree',
'COLUMN', N'EnabledMark'
GO

EXEC sp_addextendedproperty
'MS_Description', N'备注',
'SCHEMA', N'dbo',
'TABLE', N'Base_Menu_Tree',
'COLUMN', N'Description'
GO

EXEC sp_addextendedproperty
'MS_Description', N'创建日期',
'SCHEMA', N'dbo',
'TABLE', N'Base_Menu_Tree',
'COLUMN', N'CreateDate'
GO

EXEC sp_addextendedproperty
'MS_Description', N'创建用户主键',
'SCHEMA', N'dbo',
'TABLE', N'Base_Menu_Tree',
'COLUMN', N'CreateUserId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'创建用户',
'SCHEMA', N'dbo',
'TABLE', N'Base_Menu_Tree',
'COLUMN', N'CreateUserName'
GO

EXEC sp_addextendedproperty
'MS_Description', N'修改日期',
'SCHEMA', N'dbo',
'TABLE', N'Base_Menu_Tree',
'COLUMN', N'ModifyDate'
GO

EXEC sp_addextendedproperty
'MS_Description', N'修改用户主键',
'SCHEMA', N'dbo',
'TABLE', N'Base_Menu_Tree',
'COLUMN', N'ModifyUserId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'修改用户',
'SCHEMA', N'dbo',
'TABLE', N'Base_Menu_Tree',
'COLUMN', N'ModifyUserName'
GO

EXEC sp_addextendedproperty
'MS_Description', N'页面菜单表',
'SCHEMA', N'dbo',
'TABLE', N'Base_Menu_Tree'
GO


-- ----------------------------
-- Records of Base_Menu_Tree
-- ----------------------------
INSERT INTO [dbo].[Base_Menu_Tree] ([Id], [MenuName], [ParentId], [CoreUrl], [Icon], [SortCode], [TargetClass], [EnabledMark], [Description], [CreateDate], [CreateUserId], [CreateUserName], [ModifyDate], [ModifyUserId], [ModifyUserName]) VALUES (N'0df42863-23d3-45d5-917d-5cb8f5058259', N'基本信息', N'7249ce6e-076a-4ad6-9e2b-051ed87e318f', N'/SolveBasicInformation/BasicInformation', N'fas ion-android-document', N'1', N'nav-link', N'1', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[Base_Menu_Tree] ([Id], [MenuName], [ParentId], [CoreUrl], [Icon], [SortCode], [TargetClass], [EnabledMark], [Description], [CreateDate], [CreateUserId], [CreateUserName], [ModifyDate], [ModifyUserId], [ModifyUserName]) VALUES (N'25b19366-0a17-44a6-a30c-4b18755544e1', N'机构管理', N'6a7753dd-de3b-471d-a2a9-f062c10505ed', N'/UserManage/OrganizeManage', N'fas ion-network', N'89', N'nav-link', N'1', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[Base_Menu_Tree] ([Id], [MenuName], [ParentId], [CoreUrl], [Icon], [SortCode], [TargetClass], [EnabledMark], [Description], [CreateDate], [CreateUserId], [CreateUserName], [ModifyDate], [ModifyUserId], [ModifyUserName]) VALUES (N'339d018a-c106-43d4-b9d9-4322a02191d8', N'角色管理', N'6a7753dd-de3b-471d-a2a9-f062c10505ed', N'/UserManage/RoleManage', N'fas ion-ios-toggle', N'90', N'nav-link', N'1', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[Base_Menu_Tree] ([Id], [MenuName], [ParentId], [CoreUrl], [Icon], [SortCode], [TargetClass], [EnabledMark], [Description], [CreateDate], [CreateUserId], [CreateUserName], [ModifyDate], [ModifyUserId], [ModifyUserName]) VALUES (N'49d320bf-2ccb-4a70-8df0-1515d7e901e8', N'后台功能', N'0', NULL, NULL, N'98', N'menu-header', N'1', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[Base_Menu_Tree] ([Id], [MenuName], [ParentId], [CoreUrl], [Icon], [SortCode], [TargetClass], [EnabledMark], [Description], [CreateDate], [CreateUserId], [CreateUserName], [ModifyDate], [ModifyUserId], [ModifyUserName]) VALUES (N'67B0E4A4-9CAE-480D-8E2A-BF215B75AE60', N'首页', N'0', N'/Home/Page', N'fas ion-ios-home', N'0', N'nav-link', N'1', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[Base_Menu_Tree] ([Id], [MenuName], [ParentId], [CoreUrl], [Icon], [SortCode], [TargetClass], [EnabledMark], [Description], [CreateDate], [CreateUserId], [CreateUserName], [ModifyDate], [ModifyUserId], [ModifyUserName]) VALUES (N'6a7753dd-de3b-471d-a2a9-f062c10505ed', N'单位组织', N'49d320bf-2ccb-4a70-8df0-1515d7e901e8', N'', N'fas ion-ios-albums', N'99', N'nav-link has-dropdown', N'1', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[Base_Menu_Tree] ([Id], [MenuName], [ParentId], [CoreUrl], [Icon], [SortCode], [TargetClass], [EnabledMark], [Description], [CreateDate], [CreateUserId], [CreateUserName], [ModifyDate], [ModifyUserId], [ModifyUserName]) VALUES (N'797B5450-B7B9-4DED-94BF-99C728CA49E7', N'图标库', N'AD29857B-E8FB-4796-8F0A-9675949191C0', N'/ExpandSystem/ModulesIcons', N'fas ion-cube', N'3', N'nav-link', N'1', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[Base_Menu_Tree] ([Id], [MenuName], [ParentId], [CoreUrl], [Icon], [SortCode], [TargetClass], [EnabledMark], [Description], [CreateDate], [CreateUserId], [CreateUserName], [ModifyDate], [ModifyUserId], [ModifyUserName]) VALUES (N'9BDCA42F-8C58-4E3B-8557-3A85CF6093FD', N'用户管理', N'AD29857B-E8FB-4796-8F0A-9675949191C0', N'/User/Index', N'fas ion-ios-people', N'1', N'nav-link', N'1', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[Base_Menu_Tree] ([Id], [MenuName], [ParentId], [CoreUrl], [Icon], [SortCode], [TargetClass], [EnabledMark], [Description], [CreateDate], [CreateUserId], [CreateUserName], [ModifyDate], [ModifyUserId], [ModifyUserName]) VALUES (N'AD29857B-E8FB-4796-8F0A-9675949191C0', N'系统设置', N'AE56DF22-C039-4AED-B97E-A27163776550', NULL, N'fas ion-gear-b', N'1', N'nav-link has-dropdown', N'1', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[Base_Menu_Tree] ([Id], [MenuName], [ParentId], [CoreUrl], [Icon], [SortCode], [TargetClass], [EnabledMark], [Description], [CreateDate], [CreateUserId], [CreateUserName], [ModifyDate], [ModifyUserId], [ModifyUserName]) VALUES (N'AE56DF22-C039-4AED-B97E-A27163776550', N'系统功能', N'0', NULL, N'', N'99', N'menu-header', N'1', N'', NULL, NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[Base_Menu_Tree] ([Id], [MenuName], [ParentId], [CoreUrl], [Icon], [SortCode], [TargetClass], [EnabledMark], [Description], [CreateDate], [CreateUserId], [CreateUserName], [ModifyDate], [ModifyUserId], [ModifyUserName]) VALUES (N'aed801a8-25a9-4792-83a6-e8af7e7371b4', N'权限管理', N'AD29857B-E8FB-4796-8F0A-9675949191C0', N'/ExpandSystem/AccountRolesView', N'fas ion-ios-person', N'99', N'nav-link', N'1', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[Base_Menu_Tree] ([Id], [MenuName], [ParentId], [CoreUrl], [Icon], [SortCode], [TargetClass], [EnabledMark], [Description], [CreateDate], [CreateUserId], [CreateUserName], [ModifyDate], [ModifyUserId], [ModifyUserName]) VALUES (N'B2757C23-7303-425C-BC32-F8025A314CB8', N'菜单管理', N'AD29857B-E8FB-4796-8F0A-9675949191C0', N'/ExpandSystem/MenuTreeManage', N'fas ion-android-share-alt', N'2', N'nav-link', N'1', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[Base_Menu_Tree] ([Id], [MenuName], [ParentId], [CoreUrl], [Icon], [SortCode], [TargetClass], [EnabledMark], [Description], [CreateDate], [CreateUserId], [CreateUserName], [ModifyDate], [ModifyUserId], [ModifyUserName]) VALUES (N'c7274965-1d4b-4e2a-b8c9-83db1d64e52c', N'部门管理', N'6a7753dd-de3b-471d-a2a9-f062c10505ed', N'/UserManage/DepartmentManage', N'fas ion-android-contacts', N'88', N'nav-link', N'1', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO


-- ----------------------------
-- Table structure for Base_Role
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[Base_Role]') AND type IN ('U'))
	DROP TABLE [dbo].[Base_Role]
GO

CREATE TABLE [dbo].[Base_Role] (
  [Id] varchar(50) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [RoleName] varchar(255) COLLATE Chinese_PRC_CI_AS  NULL,
  [SortCode] int  NULL,
  [EnabledMark] int  NULL,
  [Description] varchar(2000) COLLATE Chinese_PRC_CI_AS  NULL,
  [CreateDate] datetime  NULL,
  [CreateUserId] varchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [CreateUserName] varchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [ModifyDate] datetime  NULL,
  [ModifyUserId] varchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [ModifyUserName] varchar(50) COLLATE Chinese_PRC_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[Base_Role] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'角色表主键Id',
'SCHEMA', N'dbo',
'TABLE', N'Base_Role',
'COLUMN', N'Id'
GO

EXEC sp_addextendedproperty
'MS_Description', N'角色名称',
'SCHEMA', N'dbo',
'TABLE', N'Base_Role',
'COLUMN', N'RoleName'
GO

EXEC sp_addextendedproperty
'MS_Description', N'排序码',
'SCHEMA', N'dbo',
'TABLE', N'Base_Role',
'COLUMN', N'SortCode'
GO

EXEC sp_addextendedproperty
'MS_Description', N'有效标志（0无效，1有效）',
'SCHEMA', N'dbo',
'TABLE', N'Base_Role',
'COLUMN', N'EnabledMark'
GO

EXEC sp_addextendedproperty
'MS_Description', N'备注',
'SCHEMA', N'dbo',
'TABLE', N'Base_Role',
'COLUMN', N'Description'
GO

EXEC sp_addextendedproperty
'MS_Description', N'创建日期',
'SCHEMA', N'dbo',
'TABLE', N'Base_Role',
'COLUMN', N'CreateDate'
GO

EXEC sp_addextendedproperty
'MS_Description', N'创建用户主键',
'SCHEMA', N'dbo',
'TABLE', N'Base_Role',
'COLUMN', N'CreateUserId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'创建用户',
'SCHEMA', N'dbo',
'TABLE', N'Base_Role',
'COLUMN', N'CreateUserName'
GO

EXEC sp_addextendedproperty
'MS_Description', N'修改日期',
'SCHEMA', N'dbo',
'TABLE', N'Base_Role',
'COLUMN', N'ModifyDate'
GO

EXEC sp_addextendedproperty
'MS_Description', N'修改用户主键',
'SCHEMA', N'dbo',
'TABLE', N'Base_Role',
'COLUMN', N'ModifyUserId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'修改用户',
'SCHEMA', N'dbo',
'TABLE', N'Base_Role',
'COLUMN', N'ModifyUserName'
GO

EXEC sp_addextendedproperty
'MS_Description', N'角色表',
'SCHEMA', N'dbo',
'TABLE', N'Base_Role'
GO


-- ----------------------------
-- Records of Base_Role
-- ----------------------------
INSERT INTO [dbo].[Base_Role] ([Id], [RoleName], [SortCode], [EnabledMark], [Description], [CreateDate], [CreateUserId], [CreateUserName], [ModifyDate], [ModifyUserId], [ModifyUserName]) VALUES (N'6D0CBBA5-A843-4D60-821A-345816662A4A', N'超级管理员', N'1', N'1', N'系统账号，不可删除', N'2022-08-09 16:50:55.000', N'System', N'System', N'2022-09-20 10:58:57.390', NULL, NULL)
GO


-- ----------------------------
-- Table structure for Base_Roles_User
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[Base_Roles_User]') AND type IN ('U'))
	DROP TABLE [dbo].[Base_Roles_User]
GO

CREATE TABLE [dbo].[Base_Roles_User] (
  [Id] varchar(50) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [RoleId] varchar(50) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [UserId] varchar(50) COLLATE Chinese_PRC_CI_AS  NOT NULL
)
GO

ALTER TABLE [dbo].[Base_Roles_User] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'用户角色对应关系表主键Id',
'SCHEMA', N'dbo',
'TABLE', N'Base_Roles_User',
'COLUMN', N'Id'
GO

EXEC sp_addextendedproperty
'MS_Description', N'角色表主键Id',
'SCHEMA', N'dbo',
'TABLE', N'Base_Roles_User',
'COLUMN', N'RoleId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'后台账户表主键Id',
'SCHEMA', N'dbo',
'TABLE', N'Base_Roles_User',
'COLUMN', N'UserId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'用户角色对应关系表',
'SCHEMA', N'dbo',
'TABLE', N'Base_Roles_User'
GO


-- ----------------------------
-- Records of Base_Roles_User
-- ----------------------------
INSERT INTO [dbo].[Base_Roles_User] ([Id], [RoleId], [UserId]) VALUES (N'C2138B8E-7168-42F0-A8AC-FF2A70A81596', N'6D0CBBA5-A843-4D60-821A-345816662A4A', N'System')
GO


-- ----------------------------
-- Table structure for Base_Roles_User_Permission
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[Base_Roles_User_Permission]') AND type IN ('U'))
	DROP TABLE [dbo].[Base_Roles_User_Permission]
GO

CREATE TABLE [dbo].[Base_Roles_User_Permission] (
  [Id] varchar(50) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [MenuId] varchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [RoleId] varchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [CreateDate] datetime  NULL,
  [CreateUserId] varchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [CreateUserName] varchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [ModifyDate] datetime  NULL,
  [ModifyUserId] varchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [ModifyUserName] varchar(50) COLLATE Chinese_PRC_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[Base_Roles_User_Permission] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'页面权限表Id',
'SCHEMA', N'dbo',
'TABLE', N'Base_Roles_User_Permission',
'COLUMN', N'Id'
GO

EXEC sp_addextendedproperty
'MS_Description', N'页面菜单Id',
'SCHEMA', N'dbo',
'TABLE', N'Base_Roles_User_Permission',
'COLUMN', N'MenuId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'角色Id',
'SCHEMA', N'dbo',
'TABLE', N'Base_Roles_User_Permission',
'COLUMN', N'RoleId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'创建日期',
'SCHEMA', N'dbo',
'TABLE', N'Base_Roles_User_Permission',
'COLUMN', N'CreateDate'
GO

EXEC sp_addextendedproperty
'MS_Description', N'创建用户主键',
'SCHEMA', N'dbo',
'TABLE', N'Base_Roles_User_Permission',
'COLUMN', N'CreateUserId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'创建用户',
'SCHEMA', N'dbo',
'TABLE', N'Base_Roles_User_Permission',
'COLUMN', N'CreateUserName'
GO

EXEC sp_addextendedproperty
'MS_Description', N'修改日期',
'SCHEMA', N'dbo',
'TABLE', N'Base_Roles_User_Permission',
'COLUMN', N'ModifyDate'
GO

EXEC sp_addextendedproperty
'MS_Description', N'修改用户主键',
'SCHEMA', N'dbo',
'TABLE', N'Base_Roles_User_Permission',
'COLUMN', N'ModifyUserId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'修改用户',
'SCHEMA', N'dbo',
'TABLE', N'Base_Roles_User_Permission',
'COLUMN', N'ModifyUserName'
GO

EXEC sp_addextendedproperty
'MS_Description', N'页面权限表',
'SCHEMA', N'dbo',
'TABLE', N'Base_Roles_User_Permission'
GO


-- ----------------------------
-- Records of Base_Roles_User_Permission
-- ----------------------------
INSERT INTO [dbo].[Base_Roles_User_Permission] ([Id], [MenuId], [RoleId], [CreateDate], [CreateUserId], [CreateUserName], [ModifyDate], [ModifyUserId], [ModifyUserName]) VALUES (N'0029ad55-ee2e-440d-8796-1646e9cfc7e8', N'339d018a-c106-43d4-b9d9-4322a02191d8', N'6D0CBBA5-A843-4D60-821A-345816662A4A', NULL, NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[Base_Roles_User_Permission] ([Id], [MenuId], [RoleId], [CreateDate], [CreateUserId], [CreateUserName], [ModifyDate], [ModifyUserId], [ModifyUserName]) VALUES (N'03e65da3-2b32-415f-bf5e-c94e751a1bc0', N'd32605a3-b392-46f2-af86-060a8adf7ad3', N'6D0CBBA5-A843-4D60-821A-345816662A4A', NULL, NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[Base_Roles_User_Permission] ([Id], [MenuId], [RoleId], [CreateDate], [CreateUserId], [CreateUserName], [ModifyDate], [ModifyUserId], [ModifyUserName]) VALUES (N'061c389c-461a-4b0c-a4cf-41dbf38a24cf', N'aed801a8-25a9-4792-83a6-e8af7e7371b4', N'6D0CBBA5-A843-4D60-821A-345816662A4A', NULL, NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[Base_Roles_User_Permission] ([Id], [MenuId], [RoleId], [CreateDate], [CreateUserId], [CreateUserName], [ModifyDate], [ModifyUserId], [ModifyUserName]) VALUES (N'068bd27a-1eaa-4653-b6b7-30e67a26659f', N'cbb28968-1ddb-4019-b948-ec10e71b10f9', N'6D0CBBA5-A843-4D60-821A-345816662A4A', NULL, NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[Base_Roles_User_Permission] ([Id], [MenuId], [RoleId], [CreateDate], [CreateUserId], [CreateUserName], [ModifyDate], [ModifyUserId], [ModifyUserName]) VALUES (N'13d701fb-e859-4a97-994b-26e8d4cb20fe', N'2ba9e7a5-7750-413b-9d89-35d2429e2fd1', N'6D0CBBA5-A843-4D60-821A-345816662A4A', NULL, NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[Base_Roles_User_Permission] ([Id], [MenuId], [RoleId], [CreateDate], [CreateUserId], [CreateUserName], [ModifyDate], [ModifyUserId], [ModifyUserName]) VALUES (N'1a9a9240-be0b-4d37-aced-9fa7e264297e', N'25b19366-0a17-44a6-a30c-4b18755544e1', N'6D0CBBA5-A843-4D60-821A-345816662A4A', NULL, NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[Base_Roles_User_Permission] ([Id], [MenuId], [RoleId], [CreateDate], [CreateUserId], [CreateUserName], [ModifyDate], [ModifyUserId], [ModifyUserName]) VALUES (N'1bc51b56-5275-4cae-a66c-6bb02bc7f2ae', N'aed801a8-25a9-4792-83a6-e8af7e7371b4', N'6D0CBBA5-A843-4D60-821A-345816662A4A', NULL, NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[Base_Roles_User_Permission] ([Id], [MenuId], [RoleId], [CreateDate], [CreateUserId], [CreateUserName], [ModifyDate], [ModifyUserId], [ModifyUserName]) VALUES (N'26247489-46e3-4ca8-ad2e-d7a50d9ce1ae', N'797B5450-B7B9-4DED-94BF-99C728CA49E7', N'6D0CBBA5-A843-4D60-821A-345816662A4A', NULL, NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[Base_Roles_User_Permission] ([Id], [MenuId], [RoleId], [CreateDate], [CreateUserId], [CreateUserName], [ModifyDate], [ModifyUserId], [ModifyUserName]) VALUES (N'312bc558-f6be-4853-adf4-45058c7b1dd0', N'339d018a-c106-43d4-b9d9-4322a02191d8', N'6D0CBBA5-A843-4D60-821A-345816662A4A', NULL, NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[Base_Roles_User_Permission] ([Id], [MenuId], [RoleId], [CreateDate], [CreateUserId], [CreateUserName], [ModifyDate], [ModifyUserId], [ModifyUserName]) VALUES (N'3306d32f-062f-4b6a-bd5b-d5dc9d3808ec', N'25b19366-0a17-44a6-a30c-4b18755544e1', N'6D0CBBA5-A843-4D60-821A-345816662A4A', NULL, NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[Base_Roles_User_Permission] ([Id], [MenuId], [RoleId], [CreateDate], [CreateUserId], [CreateUserName], [ModifyDate], [ModifyUserId], [ModifyUserName]) VALUES (N'33ba8da2-dcee-4138-9b73-e4721ca13195', N'9BDCA42F-8C58-4E3B-8557-3A85CF6093FD', N'6D0CBBA5-A843-4D60-821A-345816662A4A', NULL, NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[Base_Roles_User_Permission] ([Id], [MenuId], [RoleId], [CreateDate], [CreateUserId], [CreateUserName], [ModifyDate], [ModifyUserId], [ModifyUserName]) VALUES (N'3666ad5a-2200-4517-8ba9-811fe4e2ab13', N'B2757C23-7303-425C-BC32-F8025A314CB8', N'6D0CBBA5-A843-4D60-821A-345816662A4A', NULL, NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[Base_Roles_User_Permission] ([Id], [MenuId], [RoleId], [CreateDate], [CreateUserId], [CreateUserName], [ModifyDate], [ModifyUserId], [ModifyUserName]) VALUES (N'3bd613cf-e883-4576-ba67-ba152f1213c2', N'c7274965-1d4b-4e2a-b8c9-83db1d64e52c', N'6D0CBBA5-A843-4D60-821A-345816662A4A', NULL, NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[Base_Roles_User_Permission] ([Id], [MenuId], [RoleId], [CreateDate], [CreateUserId], [CreateUserName], [ModifyDate], [ModifyUserId], [ModifyUserName]) VALUES (N'3d8b7f11-cff4-4aaf-bf20-4c30fffa46d5', N'9BDCA42F-8C58-4E3B-8557-3A85CF6093FD', N'6D0CBBA5-A843-4D60-821A-345816662A4A', NULL, NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[Base_Roles_User_Permission] ([Id], [MenuId], [RoleId], [CreateDate], [CreateUserId], [CreateUserName], [ModifyDate], [ModifyUserId], [ModifyUserName]) VALUES (N'3fc5bd97-9627-40d8-8c10-238d4fa70362', N'339d018a-c106-43d4-b9d9-4322a02191d8', N'6D0CBBA5-A843-4D60-821A-345816662A4A', NULL, NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[Base_Roles_User_Permission] ([Id], [MenuId], [RoleId], [CreateDate], [CreateUserId], [CreateUserName], [ModifyDate], [ModifyUserId], [ModifyUserName]) VALUES (N'404eb4f3-62bd-49fa-965f-aebddc8fa0fc', N'9BDCA42F-8C58-4E3B-8557-3A85CF6093FD', N'6D0CBBA5-A843-4D60-821A-345816662A4A', NULL, NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[Base_Roles_User_Permission] ([Id], [MenuId], [RoleId], [CreateDate], [CreateUserId], [CreateUserName], [ModifyDate], [ModifyUserId], [ModifyUserName]) VALUES (N'429b2f6f-efaa-44b0-b883-bbb7684c3f35', N'797B5450-B7B9-4DED-94BF-99C728CA49E7', N'6D0CBBA5-A843-4D60-821A-345816662A4A', NULL, NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[Base_Roles_User_Permission] ([Id], [MenuId], [RoleId], [CreateDate], [CreateUserId], [CreateUserName], [ModifyDate], [ModifyUserId], [ModifyUserName]) VALUES (N'42c74562-926e-4493-9d26-ef607d4eeb82', N'339d018a-c106-43d4-b9d9-4322a02191d8', N'6D0CBBA5-A843-4D60-821A-345816662A4A', NULL, NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[Base_Roles_User_Permission] ([Id], [MenuId], [RoleId], [CreateDate], [CreateUserId], [CreateUserName], [ModifyDate], [ModifyUserId], [ModifyUserName]) VALUES (N'4738cbbb-0ef5-408e-94ac-a418ea426b30', N'c7274965-1d4b-4e2a-b8c9-83db1d64e52c', N'6D0CBBA5-A843-4D60-821A-345816662A4A', NULL, NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[Base_Roles_User_Permission] ([Id], [MenuId], [RoleId], [CreateDate], [CreateUserId], [CreateUserName], [ModifyDate], [ModifyUserId], [ModifyUserName]) VALUES (N'4841b516-f118-4e2d-9b08-95f43e63509a', N'25b19366-0a17-44a6-a30c-4b18755544e1', N'6D0CBBA5-A843-4D60-821A-345816662A4A', NULL, NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[Base_Roles_User_Permission] ([Id], [MenuId], [RoleId], [CreateDate], [CreateUserId], [CreateUserName], [ModifyDate], [ModifyUserId], [ModifyUserName]) VALUES (N'48ed2bbe-9a07-45fd-8b18-b6b416ba087f', N'0df42863-23d3-45d5-917d-5cb8f5058259', N'6D0CBBA5-A843-4D60-821A-345816662A4A', NULL, NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[Base_Roles_User_Permission] ([Id], [MenuId], [RoleId], [CreateDate], [CreateUserId], [CreateUserName], [ModifyDate], [ModifyUserId], [ModifyUserName]) VALUES (N'4a8d5218-71ac-4973-926a-8c965f18e0d4', N'B2757C23-7303-425C-BC32-F8025A314CB8', N'6D0CBBA5-A843-4D60-821A-345816662A4A', NULL, NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[Base_Roles_User_Permission] ([Id], [MenuId], [RoleId], [CreateDate], [CreateUserId], [CreateUserName], [ModifyDate], [ModifyUserId], [ModifyUserName]) VALUES (N'4eec1a10-6df7-45f2-a7d7-b94fd37d931a', N'25b19366-0a17-44a6-a30c-4b18755544e1', N'6D0CBBA5-A843-4D60-821A-345816662A4A', NULL, NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[Base_Roles_User_Permission] ([Id], [MenuId], [RoleId], [CreateDate], [CreateUserId], [CreateUserName], [ModifyDate], [ModifyUserId], [ModifyUserName]) VALUES (N'53b5e954-aa1e-4063-9535-769105ed0313', N'B2757C23-7303-425C-BC32-F8025A314CB8', N'6D0CBBA5-A843-4D60-821A-345816662A4A', NULL, NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[Base_Roles_User_Permission] ([Id], [MenuId], [RoleId], [CreateDate], [CreateUserId], [CreateUserName], [ModifyDate], [ModifyUserId], [ModifyUserName]) VALUES (N'552001b8-2ecb-4c4f-a7d7-6b3cbc125ff7', N'2ba9e7a5-7750-413b-9d89-35d2429e2fd1', N'6D0CBBA5-A843-4D60-821A-345816662A4A', NULL, NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[Base_Roles_User_Permission] ([Id], [MenuId], [RoleId], [CreateDate], [CreateUserId], [CreateUserName], [ModifyDate], [ModifyUserId], [ModifyUserName]) VALUES (N'56a76585-6310-48c4-a53f-c372836b6a3e', N'6a7753dd-de3b-471d-a2a9-f062c10505ed', N'6D0CBBA5-A843-4D60-821A-345816662A4A', NULL, NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[Base_Roles_User_Permission] ([Id], [MenuId], [RoleId], [CreateDate], [CreateUserId], [CreateUserName], [ModifyDate], [ModifyUserId], [ModifyUserName]) VALUES (N'5d0e084b-079c-4a72-88df-5648ee8c1114', N'd32605a3-b392-46f2-af86-060a8adf7ad3', N'6D0CBBA5-A843-4D60-821A-345816662A4A', NULL, NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[Base_Roles_User_Permission] ([Id], [MenuId], [RoleId], [CreateDate], [CreateUserId], [CreateUserName], [ModifyDate], [ModifyUserId], [ModifyUserName]) VALUES (N'74dae333-58e6-4efe-902a-fc92f96f02d5', N'd32605a3-b392-46f2-af86-060a8adf7ad3', N'6D0CBBA5-A843-4D60-821A-345816662A4A', NULL, NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[Base_Roles_User_Permission] ([Id], [MenuId], [RoleId], [CreateDate], [CreateUserId], [CreateUserName], [ModifyDate], [ModifyUserId], [ModifyUserName]) VALUES (N'8025e588-3baf-4d37-958d-2d875eac6593', N'797B5450-B7B9-4DED-94BF-99C728CA49E7', N'6D0CBBA5-A843-4D60-821A-345816662A4A', NULL, NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[Base_Roles_User_Permission] ([Id], [MenuId], [RoleId], [CreateDate], [CreateUserId], [CreateUserName], [ModifyDate], [ModifyUserId], [ModifyUserName]) VALUES (N'844ded39-d714-458c-9a0a-7342227ff1fd', N'AE56DF22-C039-4AED-B97E-A27163776550', N'6D0CBBA5-A843-4D60-821A-345816662A4A', NULL, NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[Base_Roles_User_Permission] ([Id], [MenuId], [RoleId], [CreateDate], [CreateUserId], [CreateUserName], [ModifyDate], [ModifyUserId], [ModifyUserName]) VALUES (N'85124611-3bc9-4328-9b33-556bb0e2e9ec', N'9BDCA42F-8C58-4E3B-8557-3A85CF6093FD', N'6D0CBBA5-A843-4D60-821A-345816662A4A', NULL, NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[Base_Roles_User_Permission] ([Id], [MenuId], [RoleId], [CreateDate], [CreateUserId], [CreateUserName], [ModifyDate], [ModifyUserId], [ModifyUserName]) VALUES (N'86c29de4-47ed-40f7-88a1-97f38f9d7ab4', N'797B5450-B7B9-4DED-94BF-99C728CA49E7', N'6D0CBBA5-A843-4D60-821A-345816662A4A', NULL, NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[Base_Roles_User_Permission] ([Id], [MenuId], [RoleId], [CreateDate], [CreateUserId], [CreateUserName], [ModifyDate], [ModifyUserId], [ModifyUserName]) VALUES (N'8a4025b3-7b1f-4035-9e1f-d9b01366e3b6', N'c7274965-1d4b-4e2a-b8c9-83db1d64e52c', N'6D0CBBA5-A843-4D60-821A-345816662A4A', NULL, NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[Base_Roles_User_Permission] ([Id], [MenuId], [RoleId], [CreateDate], [CreateUserId], [CreateUserName], [ModifyDate], [ModifyUserId], [ModifyUserName]) VALUES (N'8e76a886-cc3a-4fdb-a11b-35b4cb0cd8d8', N'd32605a3-b392-46f2-af86-060a8adf7ad3', N'6D0CBBA5-A843-4D60-821A-345816662A4A', NULL, NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[Base_Roles_User_Permission] ([Id], [MenuId], [RoleId], [CreateDate], [CreateUserId], [CreateUserName], [ModifyDate], [ModifyUserId], [ModifyUserName]) VALUES (N'8ed3d9c5-30c6-4fd8-aa11-ce5e9ef14ccb', N'0df42863-23d3-45d5-917d-5cb8f5058259', N'6D0CBBA5-A843-4D60-821A-345816662A4A', NULL, NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[Base_Roles_User_Permission] ([Id], [MenuId], [RoleId], [CreateDate], [CreateUserId], [CreateUserName], [ModifyDate], [ModifyUserId], [ModifyUserName]) VALUES (N'925a1df4-eb45-4a43-a503-6210b55aa047', N'0df42863-23d3-45d5-917d-5cb8f5058259', N'6D0CBBA5-A843-4D60-821A-345816662A4A', NULL, NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[Base_Roles_User_Permission] ([Id], [MenuId], [RoleId], [CreateDate], [CreateUserId], [CreateUserName], [ModifyDate], [ModifyUserId], [ModifyUserName]) VALUES (N'9c430b6b-954e-41e5-b1be-dea3c93997ec', N'B2757C23-7303-425C-BC32-F8025A314CB8', N'6D0CBBA5-A843-4D60-821A-345816662A4A', NULL, NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[Base_Roles_User_Permission] ([Id], [MenuId], [RoleId], [CreateDate], [CreateUserId], [CreateUserName], [ModifyDate], [ModifyUserId], [ModifyUserName]) VALUES (N'9e8bab23-ea53-4349-969c-c1e98b7504e5', N'aed801a8-25a9-4792-83a6-e8af7e7371b4', N'6D0CBBA5-A843-4D60-821A-345816662A4A', NULL, NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[Base_Roles_User_Permission] ([Id], [MenuId], [RoleId], [CreateDate], [CreateUserId], [CreateUserName], [ModifyDate], [ModifyUserId], [ModifyUserName]) VALUES (N'a1544d85-a735-4059-b0a3-d9bae84b11e5', N'c7274965-1d4b-4e2a-b8c9-83db1d64e52c', N'6D0CBBA5-A843-4D60-821A-345816662A4A', NULL, NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[Base_Roles_User_Permission] ([Id], [MenuId], [RoleId], [CreateDate], [CreateUserId], [CreateUserName], [ModifyDate], [ModifyUserId], [ModifyUserName]) VALUES (N'ab83e886-cf88-43fa-83bc-f19e5c4386f2', N'339d018a-c106-43d4-b9d9-4322a02191d8', N'6D0CBBA5-A843-4D60-821A-345816662A4A', NULL, NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[Base_Roles_User_Permission] ([Id], [MenuId], [RoleId], [CreateDate], [CreateUserId], [CreateUserName], [ModifyDate], [ModifyUserId], [ModifyUserName]) VALUES (N'b6c8bfd2-adc4-453d-b751-46eff24b45a7', N'49d320bf-2ccb-4a70-8df0-1515d7e901e8', N'6D0CBBA5-A843-4D60-821A-345816662A4A', NULL, NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[Base_Roles_User_Permission] ([Id], [MenuId], [RoleId], [CreateDate], [CreateUserId], [CreateUserName], [ModifyDate], [ModifyUserId], [ModifyUserName]) VALUES (N'bb445059-4bc6-4658-a31b-5fdcaf2df8fb', N'67B0E4A4-9CAE-480D-8E2A-BF215B75AE60', N'6D0CBBA5-A843-4D60-821A-345816662A4A', NULL, NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[Base_Roles_User_Permission] ([Id], [MenuId], [RoleId], [CreateDate], [CreateUserId], [CreateUserName], [ModifyDate], [ModifyUserId], [ModifyUserName]) VALUES (N'c08e6dbe-923f-46e8-951d-804cd84bab1c', N'aed801a8-25a9-4792-83a6-e8af7e7371b4', N'6D0CBBA5-A843-4D60-821A-345816662A4A', NULL, NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[Base_Roles_User_Permission] ([Id], [MenuId], [RoleId], [CreateDate], [CreateUserId], [CreateUserName], [ModifyDate], [ModifyUserId], [ModifyUserName]) VALUES (N'c295c17a-ee70-433d-87c9-2187b683aae6', N'0df42863-23d3-45d5-917d-5cb8f5058259', N'6D0CBBA5-A843-4D60-821A-345816662A4A', NULL, NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[Base_Roles_User_Permission] ([Id], [MenuId], [RoleId], [CreateDate], [CreateUserId], [CreateUserName], [ModifyDate], [ModifyUserId], [ModifyUserName]) VALUES (N'c8d2f5bd-18f2-4abb-a84b-5d2c751d2756', N'9BDCA42F-8C58-4E3B-8557-3A85CF6093FD', N'6D0CBBA5-A843-4D60-821A-345816662A4A', NULL, NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[Base_Roles_User_Permission] ([Id], [MenuId], [RoleId], [CreateDate], [CreateUserId], [CreateUserName], [ModifyDate], [ModifyUserId], [ModifyUserName]) VALUES (N'd1ca995c-7c13-441f-b3eb-ce7a10371d41', N'7249ce6e-076a-4ad6-9e2b-051ed87e318f', N'6D0CBBA5-A843-4D60-821A-345816662A4A', NULL, NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[Base_Roles_User_Permission] ([Id], [MenuId], [RoleId], [CreateDate], [CreateUserId], [CreateUserName], [ModifyDate], [ModifyUserId], [ModifyUserName]) VALUES (N'd2400378-a8c7-427a-a1bb-df32a73b4ce4', N'2ba9e7a5-7750-413b-9d89-35d2429e2fd1', N'6D0CBBA5-A843-4D60-821A-345816662A4A', NULL, NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[Base_Roles_User_Permission] ([Id], [MenuId], [RoleId], [CreateDate], [CreateUserId], [CreateUserName], [ModifyDate], [ModifyUserId], [ModifyUserName]) VALUES (N'd75cc78b-c65e-412c-adce-a4c846c8a745', N'2ba9e7a5-7750-413b-9d89-35d2429e2fd1', N'6D0CBBA5-A843-4D60-821A-345816662A4A', NULL, NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[Base_Roles_User_Permission] ([Id], [MenuId], [RoleId], [CreateDate], [CreateUserId], [CreateUserName], [ModifyDate], [ModifyUserId], [ModifyUserName]) VALUES (N'd7642059-20f8-48e7-93ed-e3c3cbc535dd', N'd32605a3-b392-46f2-af86-060a8adf7ad3', N'6D0CBBA5-A843-4D60-821A-345816662A4A', NULL, NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[Base_Roles_User_Permission] ([Id], [MenuId], [RoleId], [CreateDate], [CreateUserId], [CreateUserName], [ModifyDate], [ModifyUserId], [ModifyUserName]) VALUES (N'df7bdd97-3c93-4f7b-a546-75d46ecd4e34', N'c7274965-1d4b-4e2a-b8c9-83db1d64e52c', N'6D0CBBA5-A843-4D60-821A-345816662A4A', NULL, NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[Base_Roles_User_Permission] ([Id], [MenuId], [RoleId], [CreateDate], [CreateUserId], [CreateUserName], [ModifyDate], [ModifyUserId], [ModifyUserName]) VALUES (N'e0076db8-831a-4033-991f-d48ec9870b57', N'2ba9e7a5-7750-413b-9d89-35d2429e2fd1', N'6D0CBBA5-A843-4D60-821A-345816662A4A', NULL, NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[Base_Roles_User_Permission] ([Id], [MenuId], [RoleId], [CreateDate], [CreateUserId], [CreateUserName], [ModifyDate], [ModifyUserId], [ModifyUserName]) VALUES (N'e161d64f-b687-4678-a84a-aa14053543fa', N'B2757C23-7303-425C-BC32-F8025A314CB8', N'6D0CBBA5-A843-4D60-821A-345816662A4A', NULL, NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[Base_Roles_User_Permission] ([Id], [MenuId], [RoleId], [CreateDate], [CreateUserId], [CreateUserName], [ModifyDate], [ModifyUserId], [ModifyUserName]) VALUES (N'f0786e14-0ffd-4803-912c-07e46d52743a', N'0df42863-23d3-45d5-917d-5cb8f5058259', N'6D0CBBA5-A843-4D60-821A-345816662A4A', NULL, NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[Base_Roles_User_Permission] ([Id], [MenuId], [RoleId], [CreateDate], [CreateUserId], [CreateUserName], [ModifyDate], [ModifyUserId], [ModifyUserName]) VALUES (N'f20681a7-57a7-4b6b-9d3c-222c064ee559', N'aed801a8-25a9-4792-83a6-e8af7e7371b4', N'6D0CBBA5-A843-4D60-821A-345816662A4A', NULL, NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[Base_Roles_User_Permission] ([Id], [MenuId], [RoleId], [CreateDate], [CreateUserId], [CreateUserName], [ModifyDate], [ModifyUserId], [ModifyUserName]) VALUES (N'f42fd6a3-5b20-42db-a2b7-9abfcd0880da', N'797B5450-B7B9-4DED-94BF-99C728CA49E7', N'6D0CBBA5-A843-4D60-821A-345816662A4A', NULL, NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[Base_Roles_User_Permission] ([Id], [MenuId], [RoleId], [CreateDate], [CreateUserId], [CreateUserName], [ModifyDate], [ModifyUserId], [ModifyUserName]) VALUES (N'f7bd3e37-a0fe-4df7-a27a-674c1bf2c6af', N'25b19366-0a17-44a6-a30c-4b18755544e1', N'6D0CBBA5-A843-4D60-821A-345816662A4A', NULL, NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[Base_Roles_User_Permission] ([Id], [MenuId], [RoleId], [CreateDate], [CreateUserId], [CreateUserName], [ModifyDate], [ModifyUserId], [ModifyUserName]) VALUES (N'fe632363-85fd-4357-9f2f-9a8897841c16', N'AD29857B-E8FB-4796-8F0A-9675949191C0', N'6D0CBBA5-A843-4D60-821A-345816662A4A', NULL, NULL, NULL, NULL, NULL, NULL)
GO


-- ----------------------------
-- Table structure for Base_User
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[Base_User]') AND type IN ('U'))
	DROP TABLE [dbo].[Base_User]
GO

CREATE TABLE [dbo].[Base_User] (
  [Id] varchar(50) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [Account] varchar(100) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [Password] varchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [Secretkey] varchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [RealName] varchar(255) COLLATE Chinese_PRC_CI_AS  NULL,
  [NickName] varchar(255) COLLATE Chinese_PRC_CI_AS  NULL,
  [HeadIcon] varchar(200) COLLATE Chinese_PRC_CI_AS  NULL,
  [Gender] int  NOT NULL,
  [Mobile] varchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [OrganizeId] varchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [DepartmentId] varchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [LockEndDate] datetime  NULL,
  [PreviousVisit] datetime  NULL,
  [LastVisit] datetime  NULL,
  [LogOnCount] int  NOT NULL,
  [JwtToKen] varchar(1500) COLLATE Chinese_PRC_CI_AS  NULL,
  [LoginsNum] int  NOT NULL,
  [SortCode] int  NOT NULL,
  [EnabledMark] int  NOT NULL,
  [Description] varchar(2000) COLLATE Chinese_PRC_CI_AS  NULL,
  [CreateDate] datetime  NULL,
  [CreateUserId] varchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [CreateUserName] varchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [ModifyDate] datetime  NULL,
  [ModifyUserId] varchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [ModifyUserName] varchar(50) COLLATE Chinese_PRC_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[Base_User] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'后台账户表主键Id',
'SCHEMA', N'dbo',
'TABLE', N'Base_User',
'COLUMN', N'Id'
GO

EXEC sp_addextendedproperty
'MS_Description', N'登录账户',
'SCHEMA', N'dbo',
'TABLE', N'Base_User',
'COLUMN', N'Account'
GO

EXEC sp_addextendedproperty
'MS_Description', N'登录密码',
'SCHEMA', N'dbo',
'TABLE', N'Base_User',
'COLUMN', N'Password'
GO

EXEC sp_addextendedproperty
'MS_Description', N'密码加盐值',
'SCHEMA', N'dbo',
'TABLE', N'Base_User',
'COLUMN', N'Secretkey'
GO

EXEC sp_addextendedproperty
'MS_Description', N'真实姓名',
'SCHEMA', N'dbo',
'TABLE', N'Base_User',
'COLUMN', N'RealName'
GO

EXEC sp_addextendedproperty
'MS_Description', N'昵称',
'SCHEMA', N'dbo',
'TABLE', N'Base_User',
'COLUMN', N'NickName'
GO

EXEC sp_addextendedproperty
'MS_Description', N'头像',
'SCHEMA', N'dbo',
'TABLE', N'Base_User',
'COLUMN', N'HeadIcon'
GO

EXEC sp_addextendedproperty
'MS_Description', N'性别(0女1男)',
'SCHEMA', N'dbo',
'TABLE', N'Base_User',
'COLUMN', N'Gender'
GO

EXEC sp_addextendedproperty
'MS_Description', N'联系电话',
'SCHEMA', N'dbo',
'TABLE', N'Base_User',
'COLUMN', N'Mobile'
GO

EXEC sp_addextendedproperty
'MS_Description', N'所属组织（组织表主键Id）',
'SCHEMA', N'dbo',
'TABLE', N'Base_User',
'COLUMN', N'OrganizeId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'所属部门（部门表主键Id）',
'SCHEMA', N'dbo',
'TABLE', N'Base_User',
'COLUMN', N'DepartmentId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'暂停用户结束日期',
'SCHEMA', N'dbo',
'TABLE', N'Base_User',
'COLUMN', N'LockEndDate'
GO

EXEC sp_addextendedproperty
'MS_Description', N'上一次访问时间',
'SCHEMA', N'dbo',
'TABLE', N'Base_User',
'COLUMN', N'PreviousVisit'
GO

EXEC sp_addextendedproperty
'MS_Description', N'最后访问时间',
'SCHEMA', N'dbo',
'TABLE', N'Base_User',
'COLUMN', N'LastVisit'
GO

EXEC sp_addextendedproperty
'MS_Description', N'登录总次数',
'SCHEMA', N'dbo',
'TABLE', N'Base_User',
'COLUMN', N'LogOnCount'
GO

EXEC sp_addextendedproperty
'MS_Description', N'JwtToKen值',
'SCHEMA', N'dbo',
'TABLE', N'Base_User',
'COLUMN', N'JwtToKen'
GO

EXEC sp_addextendedproperty
'MS_Description', N'登录次数',
'SCHEMA', N'dbo',
'TABLE', N'Base_User',
'COLUMN', N'LoginsNum'
GO

EXEC sp_addextendedproperty
'MS_Description', N'排序码',
'SCHEMA', N'dbo',
'TABLE', N'Base_User',
'COLUMN', N'SortCode'
GO

EXEC sp_addextendedproperty
'MS_Description', N'有效标志',
'SCHEMA', N'dbo',
'TABLE', N'Base_User',
'COLUMN', N'EnabledMark'
GO

EXEC sp_addextendedproperty
'MS_Description', N'备注',
'SCHEMA', N'dbo',
'TABLE', N'Base_User',
'COLUMN', N'Description'
GO

EXEC sp_addextendedproperty
'MS_Description', N'创建日期',
'SCHEMA', N'dbo',
'TABLE', N'Base_User',
'COLUMN', N'CreateDate'
GO

EXEC sp_addextendedproperty
'MS_Description', N'创建用户主键',
'SCHEMA', N'dbo',
'TABLE', N'Base_User',
'COLUMN', N'CreateUserId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'创建用户',
'SCHEMA', N'dbo',
'TABLE', N'Base_User',
'COLUMN', N'CreateUserName'
GO

EXEC sp_addextendedproperty
'MS_Description', N'修改日期',
'SCHEMA', N'dbo',
'TABLE', N'Base_User',
'COLUMN', N'ModifyDate'
GO

EXEC sp_addextendedproperty
'MS_Description', N'修改用户主键',
'SCHEMA', N'dbo',
'TABLE', N'Base_User',
'COLUMN', N'ModifyUserId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'修改用户',
'SCHEMA', N'dbo',
'TABLE', N'Base_User',
'COLUMN', N'ModifyUserName'
GO

EXEC sp_addextendedproperty
'MS_Description', N'后台账户表',
'SCHEMA', N'dbo',
'TABLE', N'Base_User'
GO


-- ----------------------------
-- Records of Base_User
-- ----------------------------
INSERT INTO [dbo].[Base_User] ([Id], [Account], [Password], [Secretkey], [RealName], [NickName], [HeadIcon], [Gender], [Mobile], [OrganizeId], [DepartmentId], [LockEndDate], [PreviousVisit], [LastVisit], [LogOnCount], [JwtToKen], [LoginsNum], [SortCode], [EnabledMark], [Description], [CreateDate], [CreateUserId], [CreateUserName], [ModifyDate], [ModifyUserId], [ModifyUserName]) VALUES (N'System', N'System', N'35d34b4bd86bd0946cb0d6badf57f005', N'55f53b7647eccf70', N'henry', N'Henry', NULL, N'1', N'15271964110', N'', NULL, N'1900-01-01 00:00:00.000', N'2023-03-09 10:05:33.000', N'2023-03-09 10:09:11.300', N'99', N'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiaGVucnkiLCJpbmZvIjoie1wiVXNlcklkXCI6XCJTeXN0ZW1cIixcIlVzZXJOYW1lXCI6XCJoZW5yeVwiLFwiQWNjb3VudFwiOlwiU3lzdGVtXCIsXCJQaG9uZVwiOm51bGwsXCJJZE51bWJlclwiOm51bGwsXCJDb21wYW55SWRcIjpudWxsLFwiT3JnYW5pemVJZFwiOlwiXCIsXCJJc1N5c3RlbVwiOnRydWUsXCJTaWduSW5UaW1lXCI6XCIyMDIzLTAzLTA5IDEwOjA5OjExLjg1MVwiLFwiSnd0VG9rZW5cIjpudWxsLFwiTG9naW5Nb2RlXCI6MX0iLCJqdGkiOiJTeXN0ZW0iLCJpYXQiOiIxNjc4MzI3NzUxIiwibmJmIjoiMTY3ODMyNzc1MSIsImV4cCI6IjE2NzgzNDkzNTEiLCJpc3MiOiJXZWJBcHBASXNzdWVyQEhlbnJ5IiwiYXVkIjoiV2ViQXBwQXVkaWVuY2UifQ.nOIKmM4sVUyfhoiv-Q4TqEwXEDIhVHayAceHVOMMWis', N'0', N'1', N'1', NULL, N'2023-02-08 16:27:33.000', N'System', N'System', N'2023-03-09 10:09:11.887', N'System', N'henry')
GO

INSERT INTO [dbo].[Base_User] ([Id], [Account], [Password], [Secretkey], [RealName], [NickName], [HeadIcon], [Gender], [Mobile], [OrganizeId], [DepartmentId], [LockEndDate], [PreviousVisit], [LastVisit], [LogOnCount], [JwtToKen], [LoginsNum], [SortCode], [EnabledMark], [Description], [CreateDate], [CreateUserId], [CreateUserName], [ModifyDate], [ModifyUserId], [ModifyUserName]) VALUES (N'System1', N'System1', N'35d34b4bd86bd0946cb0d6badf57f005', N'55f53b7647eccf70', N'henry', N'Henry', NULL, N'1', N'15271964110', N'', NULL, N'1900-01-01 00:00:00.000', N'2023-02-27 09:08:05.000', N'2023-02-27 09:08:33.943', N'10', N'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiaGVucnkiLCJpbmZvIjoie1wiVXNlcklkXCI6XCJTeXN0ZW0xXCIsXCJVc2VyTmFtZVwiOlwiaGVucnlcIixcIkFjY291bnRcIjpudWxsLFwiUGhvbmVcIjpudWxsLFwiSWROdW1iZXJcIjpudWxsLFwiQ29tcGFueUlkXCI6bnVsbCxcIk9yZ2FuaXplSWRcIjpcIlwiLFwiSXNTeXN0ZW1cIjpmYWxzZSxcIlNpZ25JblRpbWVcIjpcIjIwMjMtMDItMjcgMDk6MDg6MzMuOTU4XCIsXCJKd3RUb2tlblwiOm51bGwsXCJMb2dpbk1vZGVcIjoxfSIsImp0aSI6IlN5c3RlbTEiLCJpYXQiOiIxNjc3NDYwMTEzIiwibmJmIjoiMTY3NzQ2MDExMyIsImV4cCI6IjE2Nzc0ODE3MTMiLCJpc3MiOiJXZWJBcHBASXNzdWVyQEhlbnJ5IiwiYXVkIjoiV2ViQXBwQXVkaWVuY2UifQ.rejwcECS1et5OstYaDbqeXPWg44mOIk2A-9dMSAyj_c', N'0', N'1', N'1', NULL, N'2023-02-08 16:27:33.000', N'System', N'System', N'2023-02-27 09:08:33.957', N'System1', N'henry')
GO

INSERT INTO [dbo].[Base_User] ([Id], [Account], [Password], [Secretkey], [RealName], [NickName], [HeadIcon], [Gender], [Mobile], [OrganizeId], [DepartmentId], [LockEndDate], [PreviousVisit], [LastVisit], [LogOnCount], [JwtToKen], [LoginsNum], [SortCode], [EnabledMark], [Description], [CreateDate], [CreateUserId], [CreateUserName], [ModifyDate], [ModifyUserId], [ModifyUserName]) VALUES (N'System10', N'System1', N'35d34b4bd86bd0946cb0d6badf57f005', N'55f53b7647eccf70', N'henry', N'Henry', NULL, N'1', N'15271964110', N'', NULL, N'1900-01-01 00:00:00.000', N'2023-02-27 09:08:05.000', N'2023-02-27 09:08:33.943', N'10', N'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiaGVucnkiLCJpbmZvIjoie1wiVXNlcklkXCI6XCJTeXN0ZW0xXCIsXCJVc2VyTmFtZVwiOlwiaGVucnlcIixcIkFjY291bnRcIjpudWxsLFwiUGhvbmVcIjpudWxsLFwiSWROdW1iZXJcIjpudWxsLFwiQ29tcGFueUlkXCI6bnVsbCxcIk9yZ2FuaXplSWRcIjpcIlwiLFwiSXNTeXN0ZW1cIjpmYWxzZSxcIlNpZ25JblRpbWVcIjpcIjIwMjMtMDItMjcgMDk6MDg6MzMuOTU4XCIsXCJKd3RUb2tlblwiOm51bGwsXCJMb2dpbk1vZGVcIjoxfSIsImp0aSI6IlN5c3RlbTEiLCJpYXQiOiIxNjc3NDYwMTEzIiwibmJmIjoiMTY3NzQ2MDExMyIsImV4cCI6IjE2Nzc0ODE3MTMiLCJpc3MiOiJXZWJBcHBASXNzdWVyQEhlbnJ5IiwiYXVkIjoiV2ViQXBwQXVkaWVuY2UifQ.rejwcECS1et5OstYaDbqeXPWg44mOIk2A-9dMSAyj_c', N'0', N'1', N'1', NULL, N'2023-02-08 16:27:33.000', N'System', N'System', N'2023-02-27 09:08:33.957', N'System1', N'henry')
GO

INSERT INTO [dbo].[Base_User] ([Id], [Account], [Password], [Secretkey], [RealName], [NickName], [HeadIcon], [Gender], [Mobile], [OrganizeId], [DepartmentId], [LockEndDate], [PreviousVisit], [LastVisit], [LogOnCount], [JwtToKen], [LoginsNum], [SortCode], [EnabledMark], [Description], [CreateDate], [CreateUserId], [CreateUserName], [ModifyDate], [ModifyUserId], [ModifyUserName]) VALUES (N'System11', N'System2', N'35d34b4bd86bd0946cb0d6badf57f005', N'55f53b7647eccf70', N'henry', N'Henry', NULL, N'1', N'15271964110', N'', NULL, N'1900-01-01 00:00:00.000', N'2023-02-10 09:19:44.000', N'2023-02-10 09:27:15.563', N'4', N'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiaGVucnkiLCJpbmZvIjoie1wiVXNlcklkXCI6XCJTeXN0ZW1cIixcIlVzZXJOYW1lXCI6XCJoZW5yeVwiLFwiUGhvbmVcIjpudWxsLFwiSWROdW1iZXJcIjpudWxsLFwiQ29tcGFueUlkXCI6bnVsbCxcIk9yZ2FuaXplSWRcIjpudWxsLFwiSXNTeXN0ZW1cIjp0cnVlLFwiU2lnbkluVGltZVwiOlwiMjAyMy0wMi0xMCAwOToyNzoxNi43MzNcIixcIkp3dFRva2VuXCI6bnVsbCxcIkxvZ2luTW9kZVwiOjF9IiwianRpIjoiU3lzdGVtIiwiaWF0IjoiMTY3NTk5MjQzOCIsIm5iZiI6IjE2NzU5OTI0MzgiLCJleHAiOiIxNjc2MDE0MDM4IiwiaXNzIjoiV2ViQXBwQElzc3VlckBIZW5yeSIsImF1ZCI6IldlYkFwcEF1ZGllbmNlIn0.zv7EBl-qc7_-Xr-RZfmtTuRDSCpX2AQaM1Z4dGMHhxc', N'0', N'1', N'1', NULL, N'2023-02-08 16:27:33.000', N'System', N'System', N'2023-02-10 09:27:18.123', NULL, NULL)
GO

INSERT INTO [dbo].[Base_User] ([Id], [Account], [Password], [Secretkey], [RealName], [NickName], [HeadIcon], [Gender], [Mobile], [OrganizeId], [DepartmentId], [LockEndDate], [PreviousVisit], [LastVisit], [LogOnCount], [JwtToKen], [LoginsNum], [SortCode], [EnabledMark], [Description], [CreateDate], [CreateUserId], [CreateUserName], [ModifyDate], [ModifyUserId], [ModifyUserName]) VALUES (N'System2', N'System2', N'35d34b4bd86bd0946cb0d6badf57f005', N'55f53b7647eccf70', N'henry', N'Henry', NULL, N'1', N'15271964110', N'', NULL, N'1900-01-01 00:00:00.000', N'2023-02-10 09:19:44.000', N'2023-02-10 09:27:15.563', N'4', N'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiaGVucnkiLCJpbmZvIjoie1wiVXNlcklkXCI6XCJTeXN0ZW1cIixcIlVzZXJOYW1lXCI6XCJoZW5yeVwiLFwiUGhvbmVcIjpudWxsLFwiSWROdW1iZXJcIjpudWxsLFwiQ29tcGFueUlkXCI6bnVsbCxcIk9yZ2FuaXplSWRcIjpudWxsLFwiSXNTeXN0ZW1cIjp0cnVlLFwiU2lnbkluVGltZVwiOlwiMjAyMy0wMi0xMCAwOToyNzoxNi43MzNcIixcIkp3dFRva2VuXCI6bnVsbCxcIkxvZ2luTW9kZVwiOjF9IiwianRpIjoiU3lzdGVtIiwiaWF0IjoiMTY3NTk5MjQzOCIsIm5iZiI6IjE2NzU5OTI0MzgiLCJleHAiOiIxNjc2MDE0MDM4IiwiaXNzIjoiV2ViQXBwQElzc3VlckBIZW5yeSIsImF1ZCI6IldlYkFwcEF1ZGllbmNlIn0.zv7EBl-qc7_-Xr-RZfmtTuRDSCpX2AQaM1Z4dGMHhxc', N'0', N'1', N'1', NULL, N'2023-02-08 16:27:33.000', N'System', N'System', N'2023-02-10 09:27:18.123', NULL, NULL)
GO

INSERT INTO [dbo].[Base_User] ([Id], [Account], [Password], [Secretkey], [RealName], [NickName], [HeadIcon], [Gender], [Mobile], [OrganizeId], [DepartmentId], [LockEndDate], [PreviousVisit], [LastVisit], [LogOnCount], [JwtToKen], [LoginsNum], [SortCode], [EnabledMark], [Description], [CreateDate], [CreateUserId], [CreateUserName], [ModifyDate], [ModifyUserId], [ModifyUserName]) VALUES (N'System21', N'System2', N'35d34b4bd86bd0946cb0d6badf57f005', N'55f53b7647eccf70', N'henry', N'Henry', NULL, N'1', N'15271964110', N'', NULL, N'1900-01-01 00:00:00.000', N'2023-02-10 09:19:44.000', N'2023-02-10 09:27:15.563', N'4', N'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiaGVucnkiLCJpbmZvIjoie1wiVXNlcklkXCI6XCJTeXN0ZW1cIixcIlVzZXJOYW1lXCI6XCJoZW5yeVwiLFwiUGhvbmVcIjpudWxsLFwiSWROdW1iZXJcIjpudWxsLFwiQ29tcGFueUlkXCI6bnVsbCxcIk9yZ2FuaXplSWRcIjpudWxsLFwiSXNTeXN0ZW1cIjp0cnVlLFwiU2lnbkluVGltZVwiOlwiMjAyMy0wMi0xMCAwOToyNzoxNi43MzNcIixcIkp3dFRva2VuXCI6bnVsbCxcIkxvZ2luTW9kZVwiOjF9IiwianRpIjoiU3lzdGVtIiwiaWF0IjoiMTY3NTk5MjQzOCIsIm5iZiI6IjE2NzU5OTI0MzgiLCJleHAiOiIxNjc2MDE0MDM4IiwiaXNzIjoiV2ViQXBwQElzc3VlckBIZW5yeSIsImF1ZCI6IldlYkFwcEF1ZGllbmNlIn0.zv7EBl-qc7_-Xr-RZfmtTuRDSCpX2AQaM1Z4dGMHhxc', N'0', N'1', N'1', NULL, N'2023-02-08 16:27:33.000', N'System', N'System', N'2023-02-10 09:27:18.123', NULL, NULL)
GO

INSERT INTO [dbo].[Base_User] ([Id], [Account], [Password], [Secretkey], [RealName], [NickName], [HeadIcon], [Gender], [Mobile], [OrganizeId], [DepartmentId], [LockEndDate], [PreviousVisit], [LastVisit], [LogOnCount], [JwtToKen], [LoginsNum], [SortCode], [EnabledMark], [Description], [CreateDate], [CreateUserId], [CreateUserName], [ModifyDate], [ModifyUserId], [ModifyUserName]) VALUES (N'System3', N'System3', N'35d34b4bd86bd0946cb0d6badf57f005', N'55f53b7647eccf70', N'henry', N'Henry', NULL, N'1', N'15271964110', N'', NULL, N'1900-01-01 00:00:00.000', N'2023-02-10 09:19:44.000', N'2023-02-10 09:27:15.563', N'4', N'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiaGVucnkiLCJpbmZvIjoie1wiVXNlcklkXCI6XCJTeXN0ZW1cIixcIlVzZXJOYW1lXCI6XCJoZW5yeVwiLFwiUGhvbmVcIjpudWxsLFwiSWROdW1iZXJcIjpudWxsLFwiQ29tcGFueUlkXCI6bnVsbCxcIk9yZ2FuaXplSWRcIjpudWxsLFwiSXNTeXN0ZW1cIjp0cnVlLFwiU2lnbkluVGltZVwiOlwiMjAyMy0wMi0xMCAwOToyNzoxNi43MzNcIixcIkp3dFRva2VuXCI6bnVsbCxcIkxvZ2luTW9kZVwiOjF9IiwianRpIjoiU3lzdGVtIiwiaWF0IjoiMTY3NTk5MjQzOCIsIm5iZiI6IjE2NzU5OTI0MzgiLCJleHAiOiIxNjc2MDE0MDM4IiwiaXNzIjoiV2ViQXBwQElzc3VlckBIZW5yeSIsImF1ZCI6IldlYkFwcEF1ZGllbmNlIn0.zv7EBl-qc7_-Xr-RZfmtTuRDSCpX2AQaM1Z4dGMHhxc', N'0', N'1', N'1', NULL, N'2023-02-08 16:27:33.000', N'System', N'System', N'2023-02-10 09:27:18.123', NULL, NULL)
GO

INSERT INTO [dbo].[Base_User] ([Id], [Account], [Password], [Secretkey], [RealName], [NickName], [HeadIcon], [Gender], [Mobile], [OrganizeId], [DepartmentId], [LockEndDate], [PreviousVisit], [LastVisit], [LogOnCount], [JwtToKen], [LoginsNum], [SortCode], [EnabledMark], [Description], [CreateDate], [CreateUserId], [CreateUserName], [ModifyDate], [ModifyUserId], [ModifyUserName]) VALUES (N'System31', N'System3', N'35d34b4bd86bd0946cb0d6badf57f005', N'55f53b7647eccf70', N'henry', N'Henry', NULL, N'1', N'15271964110', N'', NULL, N'1900-01-01 00:00:00.000', N'2023-02-10 09:19:44.000', N'2023-02-10 09:27:15.563', N'4', N'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiaGVucnkiLCJpbmZvIjoie1wiVXNlcklkXCI6XCJTeXN0ZW1cIixcIlVzZXJOYW1lXCI6XCJoZW5yeVwiLFwiUGhvbmVcIjpudWxsLFwiSWROdW1iZXJcIjpudWxsLFwiQ29tcGFueUlkXCI6bnVsbCxcIk9yZ2FuaXplSWRcIjpudWxsLFwiSXNTeXN0ZW1cIjp0cnVlLFwiU2lnbkluVGltZVwiOlwiMjAyMy0wMi0xMCAwOToyNzoxNi43MzNcIixcIkp3dFRva2VuXCI6bnVsbCxcIkxvZ2luTW9kZVwiOjF9IiwianRpIjoiU3lzdGVtIiwiaWF0IjoiMTY3NTk5MjQzOCIsIm5iZiI6IjE2NzU5OTI0MzgiLCJleHAiOiIxNjc2MDE0MDM4IiwiaXNzIjoiV2ViQXBwQElzc3VlckBIZW5yeSIsImF1ZCI6IldlYkFwcEF1ZGllbmNlIn0.zv7EBl-qc7_-Xr-RZfmtTuRDSCpX2AQaM1Z4dGMHhxc', N'0', N'1', N'1', NULL, N'2023-02-08 16:27:33.000', N'System', N'System', N'2023-02-10 09:27:18.123', NULL, NULL)
GO

INSERT INTO [dbo].[Base_User] ([Id], [Account], [Password], [Secretkey], [RealName], [NickName], [HeadIcon], [Gender], [Mobile], [OrganizeId], [DepartmentId], [LockEndDate], [PreviousVisit], [LastVisit], [LogOnCount], [JwtToKen], [LoginsNum], [SortCode], [EnabledMark], [Description], [CreateDate], [CreateUserId], [CreateUserName], [ModifyDate], [ModifyUserId], [ModifyUserName]) VALUES (N'System311', N'System3', N'35d34b4bd86bd0946cb0d6badf57f005', N'55f53b7647eccf70', N'henry', N'Henry', NULL, N'1', N'15271964110', N'', NULL, N'1900-01-01 00:00:00.000', N'2023-02-10 09:19:44.000', N'2023-02-10 09:27:15.563', N'4', N'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiaGVucnkiLCJpbmZvIjoie1wiVXNlcklkXCI6XCJTeXN0ZW1cIixcIlVzZXJOYW1lXCI6XCJoZW5yeVwiLFwiUGhvbmVcIjpudWxsLFwiSWROdW1iZXJcIjpudWxsLFwiQ29tcGFueUlkXCI6bnVsbCxcIk9yZ2FuaXplSWRcIjpudWxsLFwiSXNTeXN0ZW1cIjp0cnVlLFwiU2lnbkluVGltZVwiOlwiMjAyMy0wMi0xMCAwOToyNzoxNi43MzNcIixcIkp3dFRva2VuXCI6bnVsbCxcIkxvZ2luTW9kZVwiOjF9IiwianRpIjoiU3lzdGVtIiwiaWF0IjoiMTY3NTk5MjQzOCIsIm5iZiI6IjE2NzU5OTI0MzgiLCJleHAiOiIxNjc2MDE0MDM4IiwiaXNzIjoiV2ViQXBwQElzc3VlckBIZW5yeSIsImF1ZCI6IldlYkFwcEF1ZGllbmNlIn0.zv7EBl-qc7_-Xr-RZfmtTuRDSCpX2AQaM1Z4dGMHhxc', N'0', N'1', N'1', NULL, N'2023-02-08 16:27:33.000', N'System', N'System', N'2023-02-10 09:27:18.123', NULL, NULL)
GO

INSERT INTO [dbo].[Base_User] ([Id], [Account], [Password], [Secretkey], [RealName], [NickName], [HeadIcon], [Gender], [Mobile], [OrganizeId], [DepartmentId], [LockEndDate], [PreviousVisit], [LastVisit], [LogOnCount], [JwtToKen], [LoginsNum], [SortCode], [EnabledMark], [Description], [CreateDate], [CreateUserId], [CreateUserName], [ModifyDate], [ModifyUserId], [ModifyUserName]) VALUES (N'System3111', N'System3', N'35d34b4bd86bd0946cb0d6badf57f005', N'55f53b7647eccf70', N'henry', N'Henry', NULL, N'1', N'15271964110', N'', NULL, N'1900-01-01 00:00:00.000', N'2023-02-10 09:19:44.000', N'2023-02-10 09:27:15.563', N'4', N'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiaGVucnkiLCJpbmZvIjoie1wiVXNlcklkXCI6XCJTeXN0ZW1cIixcIlVzZXJOYW1lXCI6XCJoZW5yeVwiLFwiUGhvbmVcIjpudWxsLFwiSWROdW1iZXJcIjpudWxsLFwiQ29tcGFueUlkXCI6bnVsbCxcIk9yZ2FuaXplSWRcIjpudWxsLFwiSXNTeXN0ZW1cIjp0cnVlLFwiU2lnbkluVGltZVwiOlwiMjAyMy0wMi0xMCAwOToyNzoxNi43MzNcIixcIkp3dFRva2VuXCI6bnVsbCxcIkxvZ2luTW9kZVwiOjF9IiwianRpIjoiU3lzdGVtIiwiaWF0IjoiMTY3NTk5MjQzOCIsIm5iZiI6IjE2NzU5OTI0MzgiLCJleHAiOiIxNjc2MDE0MDM4IiwiaXNzIjoiV2ViQXBwQElzc3VlckBIZW5yeSIsImF1ZCI6IldlYkFwcEF1ZGllbmNlIn0.zv7EBl-qc7_-Xr-RZfmtTuRDSCpX2AQaM1Z4dGMHhxc', N'0', N'1', N'1', NULL, N'2023-02-08 16:27:33.000', N'System', N'System', N'2023-02-10 09:27:18.123', NULL, NULL)
GO

INSERT INTO [dbo].[Base_User] ([Id], [Account], [Password], [Secretkey], [RealName], [NickName], [HeadIcon], [Gender], [Mobile], [OrganizeId], [DepartmentId], [LockEndDate], [PreviousVisit], [LastVisit], [LogOnCount], [JwtToKen], [LoginsNum], [SortCode], [EnabledMark], [Description], [CreateDate], [CreateUserId], [CreateUserName], [ModifyDate], [ModifyUserId], [ModifyUserName]) VALUES (N'System4', N'System4', N'35d34b4bd86bd0946cb0d6badf57f005', N'55f53b7647eccf70', N'henry', N'Henry', NULL, N'1', N'15271964110', N'', NULL, N'1900-01-01 00:00:00.000', N'2023-02-10 09:19:44.000', N'2023-02-10 09:27:15.563', N'4', N'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiaGVucnkiLCJpbmZvIjoie1wiVXNlcklkXCI6XCJTeXN0ZW1cIixcIlVzZXJOYW1lXCI6XCJoZW5yeVwiLFwiUGhvbmVcIjpudWxsLFwiSWROdW1iZXJcIjpudWxsLFwiQ29tcGFueUlkXCI6bnVsbCxcIk9yZ2FuaXplSWRcIjpudWxsLFwiSXNTeXN0ZW1cIjp0cnVlLFwiU2lnbkluVGltZVwiOlwiMjAyMy0wMi0xMCAwOToyNzoxNi43MzNcIixcIkp3dFRva2VuXCI6bnVsbCxcIkxvZ2luTW9kZVwiOjF9IiwianRpIjoiU3lzdGVtIiwiaWF0IjoiMTY3NTk5MjQzOCIsIm5iZiI6IjE2NzU5OTI0MzgiLCJleHAiOiIxNjc2MDE0MDM4IiwiaXNzIjoiV2ViQXBwQElzc3VlckBIZW5yeSIsImF1ZCI6IldlYkFwcEF1ZGllbmNlIn0.zv7EBl-qc7_-Xr-RZfmtTuRDSCpX2AQaM1Z4dGMHhxc', N'0', N'1', N'1', NULL, N'2023-02-08 16:27:33.000', N'System', N'System', N'2023-02-10 09:27:18.123', NULL, NULL)
GO

INSERT INTO [dbo].[Base_User] ([Id], [Account], [Password], [Secretkey], [RealName], [NickName], [HeadIcon], [Gender], [Mobile], [OrganizeId], [DepartmentId], [LockEndDate], [PreviousVisit], [LastVisit], [LogOnCount], [JwtToKen], [LoginsNum], [SortCode], [EnabledMark], [Description], [CreateDate], [CreateUserId], [CreateUserName], [ModifyDate], [ModifyUserId], [ModifyUserName]) VALUES (N'System41', N'System4', N'35d34b4bd86bd0946cb0d6badf57f005', N'55f53b7647eccf70', N'henry', N'Henry', NULL, N'1', N'15271964110', N'', NULL, N'1900-01-01 00:00:00.000', N'2023-02-10 09:19:44.000', N'2023-02-10 09:27:15.563', N'4', N'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiaGVucnkiLCJpbmZvIjoie1wiVXNlcklkXCI6XCJTeXN0ZW1cIixcIlVzZXJOYW1lXCI6XCJoZW5yeVwiLFwiUGhvbmVcIjpudWxsLFwiSWROdW1iZXJcIjpudWxsLFwiQ29tcGFueUlkXCI6bnVsbCxcIk9yZ2FuaXplSWRcIjpudWxsLFwiSXNTeXN0ZW1cIjp0cnVlLFwiU2lnbkluVGltZVwiOlwiMjAyMy0wMi0xMCAwOToyNzoxNi43MzNcIixcIkp3dFRva2VuXCI6bnVsbCxcIkxvZ2luTW9kZVwiOjF9IiwianRpIjoiU3lzdGVtIiwiaWF0IjoiMTY3NTk5MjQzOCIsIm5iZiI6IjE2NzU5OTI0MzgiLCJleHAiOiIxNjc2MDE0MDM4IiwiaXNzIjoiV2ViQXBwQElzc3VlckBIZW5yeSIsImF1ZCI6IldlYkFwcEF1ZGllbmNlIn0.zv7EBl-qc7_-Xr-RZfmtTuRDSCpX2AQaM1Z4dGMHhxc', N'0', N'1', N'1', NULL, N'2023-02-08 16:27:33.000', N'System', N'System', N'2023-02-10 09:27:18.123', NULL, NULL)
GO

INSERT INTO [dbo].[Base_User] ([Id], [Account], [Password], [Secretkey], [RealName], [NickName], [HeadIcon], [Gender], [Mobile], [OrganizeId], [DepartmentId], [LockEndDate], [PreviousVisit], [LastVisit], [LogOnCount], [JwtToKen], [LoginsNum], [SortCode], [EnabledMark], [Description], [CreateDate], [CreateUserId], [CreateUserName], [ModifyDate], [ModifyUserId], [ModifyUserName]) VALUES (N'System411', N'System4', N'35d34b4bd86bd0946cb0d6badf57f005', N'55f53b7647eccf70', N'henry', N'Henry', NULL, N'1', N'15271964110', N'', NULL, N'1900-01-01 00:00:00.000', N'2023-02-10 09:19:44.000', N'2023-02-10 09:27:15.563', N'4', N'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiaGVucnkiLCJpbmZvIjoie1wiVXNlcklkXCI6XCJTeXN0ZW1cIixcIlVzZXJOYW1lXCI6XCJoZW5yeVwiLFwiUGhvbmVcIjpudWxsLFwiSWROdW1iZXJcIjpudWxsLFwiQ29tcGFueUlkXCI6bnVsbCxcIk9yZ2FuaXplSWRcIjpudWxsLFwiSXNTeXN0ZW1cIjp0cnVlLFwiU2lnbkluVGltZVwiOlwiMjAyMy0wMi0xMCAwOToyNzoxNi43MzNcIixcIkp3dFRva2VuXCI6bnVsbCxcIkxvZ2luTW9kZVwiOjF9IiwianRpIjoiU3lzdGVtIiwiaWF0IjoiMTY3NTk5MjQzOCIsIm5iZiI6IjE2NzU5OTI0MzgiLCJleHAiOiIxNjc2MDE0MDM4IiwiaXNzIjoiV2ViQXBwQElzc3VlckBIZW5yeSIsImF1ZCI6IldlYkFwcEF1ZGllbmNlIn0.zv7EBl-qc7_-Xr-RZfmtTuRDSCpX2AQaM1Z4dGMHhxc', N'0', N'1', N'1', NULL, N'2023-02-08 16:27:33.000', N'System', N'System', N'2023-02-10 09:27:18.123', NULL, NULL)
GO

INSERT INTO [dbo].[Base_User] ([Id], [Account], [Password], [Secretkey], [RealName], [NickName], [HeadIcon], [Gender], [Mobile], [OrganizeId], [DepartmentId], [LockEndDate], [PreviousVisit], [LastVisit], [LogOnCount], [JwtToKen], [LoginsNum], [SortCode], [EnabledMark], [Description], [CreateDate], [CreateUserId], [CreateUserName], [ModifyDate], [ModifyUserId], [ModifyUserName]) VALUES (N'System4111', N'System4', N'35d34b4bd86bd0946cb0d6badf57f005', N'55f53b7647eccf70', N'henry', N'Henry', NULL, N'1', N'15271964110', N'', NULL, N'1900-01-01 00:00:00.000', N'2023-02-10 09:19:44.000', N'2023-02-10 09:27:15.563', N'4', N'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiaGVucnkiLCJpbmZvIjoie1wiVXNlcklkXCI6XCJTeXN0ZW1cIixcIlVzZXJOYW1lXCI6XCJoZW5yeVwiLFwiUGhvbmVcIjpudWxsLFwiSWROdW1iZXJcIjpudWxsLFwiQ29tcGFueUlkXCI6bnVsbCxcIk9yZ2FuaXplSWRcIjpudWxsLFwiSXNTeXN0ZW1cIjp0cnVlLFwiU2lnbkluVGltZVwiOlwiMjAyMy0wMi0xMCAwOToyNzoxNi43MzNcIixcIkp3dFRva2VuXCI6bnVsbCxcIkxvZ2luTW9kZVwiOjF9IiwianRpIjoiU3lzdGVtIiwiaWF0IjoiMTY3NTk5MjQzOCIsIm5iZiI6IjE2NzU5OTI0MzgiLCJleHAiOiIxNjc2MDE0MDM4IiwiaXNzIjoiV2ViQXBwQElzc3VlckBIZW5yeSIsImF1ZCI6IldlYkFwcEF1ZGllbmNlIn0.zv7EBl-qc7_-Xr-RZfmtTuRDSCpX2AQaM1Z4dGMHhxc', N'0', N'1', N'1', NULL, N'2023-02-08 16:27:33.000', N'System', N'System', N'2023-02-10 09:27:18.123', NULL, NULL)
GO

INSERT INTO [dbo].[Base_User] ([Id], [Account], [Password], [Secretkey], [RealName], [NickName], [HeadIcon], [Gender], [Mobile], [OrganizeId], [DepartmentId], [LockEndDate], [PreviousVisit], [LastVisit], [LogOnCount], [JwtToKen], [LoginsNum], [SortCode], [EnabledMark], [Description], [CreateDate], [CreateUserId], [CreateUserName], [ModifyDate], [ModifyUserId], [ModifyUserName]) VALUES (N'System5', N'System5', N'35d34b4bd86bd0946cb0d6badf57f005', N'55f53b7647eccf70', N'henry', N'Henry', NULL, N'1', N'15271964110', N'', NULL, N'1900-01-01 00:00:00.000', N'2023-02-10 09:19:44.000', N'2023-02-10 09:27:15.563', N'4', N'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiaGVucnkiLCJpbmZvIjoie1wiVXNlcklkXCI6XCJTeXN0ZW1cIixcIlVzZXJOYW1lXCI6XCJoZW5yeVwiLFwiUGhvbmVcIjpudWxsLFwiSWROdW1iZXJcIjpudWxsLFwiQ29tcGFueUlkXCI6bnVsbCxcIk9yZ2FuaXplSWRcIjpudWxsLFwiSXNTeXN0ZW1cIjp0cnVlLFwiU2lnbkluVGltZVwiOlwiMjAyMy0wMi0xMCAwOToyNzoxNi43MzNcIixcIkp3dFRva2VuXCI6bnVsbCxcIkxvZ2luTW9kZVwiOjF9IiwianRpIjoiU3lzdGVtIiwiaWF0IjoiMTY3NTk5MjQzOCIsIm5iZiI6IjE2NzU5OTI0MzgiLCJleHAiOiIxNjc2MDE0MDM4IiwiaXNzIjoiV2ViQXBwQElzc3VlckBIZW5yeSIsImF1ZCI6IldlYkFwcEF1ZGllbmNlIn0.zv7EBl-qc7_-Xr-RZfmtTuRDSCpX2AQaM1Z4dGMHhxc', N'0', N'1', N'1', NULL, N'2023-02-08 16:27:33.000', N'System', N'System', N'2023-02-10 09:27:18.123', NULL, NULL)
GO

INSERT INTO [dbo].[Base_User] ([Id], [Account], [Password], [Secretkey], [RealName], [NickName], [HeadIcon], [Gender], [Mobile], [OrganizeId], [DepartmentId], [LockEndDate], [PreviousVisit], [LastVisit], [LogOnCount], [JwtToKen], [LoginsNum], [SortCode], [EnabledMark], [Description], [CreateDate], [CreateUserId], [CreateUserName], [ModifyDate], [ModifyUserId], [ModifyUserName]) VALUES (N'System51', N'System5', N'35d34b4bd86bd0946cb0d6badf57f005', N'55f53b7647eccf70', N'henry', N'Henry', NULL, N'1', N'15271964110', N'', NULL, N'1900-01-01 00:00:00.000', N'2023-02-10 09:19:44.000', N'2023-02-10 09:27:15.563', N'4', N'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiaGVucnkiLCJpbmZvIjoie1wiVXNlcklkXCI6XCJTeXN0ZW1cIixcIlVzZXJOYW1lXCI6XCJoZW5yeVwiLFwiUGhvbmVcIjpudWxsLFwiSWROdW1iZXJcIjpudWxsLFwiQ29tcGFueUlkXCI6bnVsbCxcIk9yZ2FuaXplSWRcIjpudWxsLFwiSXNTeXN0ZW1cIjp0cnVlLFwiU2lnbkluVGltZVwiOlwiMjAyMy0wMi0xMCAwOToyNzoxNi43MzNcIixcIkp3dFRva2VuXCI6bnVsbCxcIkxvZ2luTW9kZVwiOjF9IiwianRpIjoiU3lzdGVtIiwiaWF0IjoiMTY3NTk5MjQzOCIsIm5iZiI6IjE2NzU5OTI0MzgiLCJleHAiOiIxNjc2MDE0MDM4IiwiaXNzIjoiV2ViQXBwQElzc3VlckBIZW5yeSIsImF1ZCI6IldlYkFwcEF1ZGllbmNlIn0.zv7EBl-qc7_-Xr-RZfmtTuRDSCpX2AQaM1Z4dGMHhxc', N'0', N'1', N'1', NULL, N'2023-02-08 16:27:33.000', N'System', N'System', N'2023-02-10 09:27:18.123', NULL, NULL)
GO

INSERT INTO [dbo].[Base_User] ([Id], [Account], [Password], [Secretkey], [RealName], [NickName], [HeadIcon], [Gender], [Mobile], [OrganizeId], [DepartmentId], [LockEndDate], [PreviousVisit], [LastVisit], [LogOnCount], [JwtToKen], [LoginsNum], [SortCode], [EnabledMark], [Description], [CreateDate], [CreateUserId], [CreateUserName], [ModifyDate], [ModifyUserId], [ModifyUserName]) VALUES (N'System511', N'System5', N'35d34b4bd86bd0946cb0d6badf57f005', N'55f53b7647eccf70', N'henry', N'Henry', NULL, N'1', N'15271964110', N'', NULL, N'1900-01-01 00:00:00.000', N'2023-02-10 09:19:44.000', N'2023-02-10 09:27:15.563', N'4', N'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiaGVucnkiLCJpbmZvIjoie1wiVXNlcklkXCI6XCJTeXN0ZW1cIixcIlVzZXJOYW1lXCI6XCJoZW5yeVwiLFwiUGhvbmVcIjpudWxsLFwiSWROdW1iZXJcIjpudWxsLFwiQ29tcGFueUlkXCI6bnVsbCxcIk9yZ2FuaXplSWRcIjpudWxsLFwiSXNTeXN0ZW1cIjp0cnVlLFwiU2lnbkluVGltZVwiOlwiMjAyMy0wMi0xMCAwOToyNzoxNi43MzNcIixcIkp3dFRva2VuXCI6bnVsbCxcIkxvZ2luTW9kZVwiOjF9IiwianRpIjoiU3lzdGVtIiwiaWF0IjoiMTY3NTk5MjQzOCIsIm5iZiI6IjE2NzU5OTI0MzgiLCJleHAiOiIxNjc2MDE0MDM4IiwiaXNzIjoiV2ViQXBwQElzc3VlckBIZW5yeSIsImF1ZCI6IldlYkFwcEF1ZGllbmNlIn0.zv7EBl-qc7_-Xr-RZfmtTuRDSCpX2AQaM1Z4dGMHhxc', N'0', N'1', N'1', NULL, N'2023-02-08 16:27:33.000', N'System', N'System', N'2023-02-10 09:27:18.123', NULL, NULL)
GO

INSERT INTO [dbo].[Base_User] ([Id], [Account], [Password], [Secretkey], [RealName], [NickName], [HeadIcon], [Gender], [Mobile], [OrganizeId], [DepartmentId], [LockEndDate], [PreviousVisit], [LastVisit], [LogOnCount], [JwtToKen], [LoginsNum], [SortCode], [EnabledMark], [Description], [CreateDate], [CreateUserId], [CreateUserName], [ModifyDate], [ModifyUserId], [ModifyUserName]) VALUES (N'System5111', N'System5', N'35d34b4bd86bd0946cb0d6badf57f005', N'55f53b7647eccf70', N'henry', N'Henry', NULL, N'1', N'15271964110', N'', NULL, N'1900-01-01 00:00:00.000', N'2023-02-10 09:19:44.000', N'2023-02-10 09:27:15.563', N'4', N'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiaGVucnkiLCJpbmZvIjoie1wiVXNlcklkXCI6XCJTeXN0ZW1cIixcIlVzZXJOYW1lXCI6XCJoZW5yeVwiLFwiUGhvbmVcIjpudWxsLFwiSWROdW1iZXJcIjpudWxsLFwiQ29tcGFueUlkXCI6bnVsbCxcIk9yZ2FuaXplSWRcIjpudWxsLFwiSXNTeXN0ZW1cIjp0cnVlLFwiU2lnbkluVGltZVwiOlwiMjAyMy0wMi0xMCAwOToyNzoxNi43MzNcIixcIkp3dFRva2VuXCI6bnVsbCxcIkxvZ2luTW9kZVwiOjF9IiwianRpIjoiU3lzdGVtIiwiaWF0IjoiMTY3NTk5MjQzOCIsIm5iZiI6IjE2NzU5OTI0MzgiLCJleHAiOiIxNjc2MDE0MDM4IiwiaXNzIjoiV2ViQXBwQElzc3VlckBIZW5yeSIsImF1ZCI6IldlYkFwcEF1ZGllbmNlIn0.zv7EBl-qc7_-Xr-RZfmtTuRDSCpX2AQaM1Z4dGMHhxc', N'0', N'1', N'1', NULL, N'2023-02-08 16:27:33.000', N'System', N'System', N'2023-02-10 09:27:18.123', NULL, NULL)
GO

INSERT INTO [dbo].[Base_User] ([Id], [Account], [Password], [Secretkey], [RealName], [NickName], [HeadIcon], [Gender], [Mobile], [OrganizeId], [DepartmentId], [LockEndDate], [PreviousVisit], [LastVisit], [LogOnCount], [JwtToKen], [LoginsNum], [SortCode], [EnabledMark], [Description], [CreateDate], [CreateUserId], [CreateUserName], [ModifyDate], [ModifyUserId], [ModifyUserName]) VALUES (N'System6', N'System6', N'35d34b4bd86bd0946cb0d6badf57f005', N'55f53b7647eccf70', N'henry', N'Henry', NULL, N'1', N'15271964110', N'', NULL, N'1900-01-01 00:00:00.000', N'2023-02-10 09:19:44.000', N'2023-02-10 09:27:15.563', N'4', N'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiaGVucnkiLCJpbmZvIjoie1wiVXNlcklkXCI6XCJTeXN0ZW1cIixcIlVzZXJOYW1lXCI6XCJoZW5yeVwiLFwiUGhvbmVcIjpudWxsLFwiSWROdW1iZXJcIjpudWxsLFwiQ29tcGFueUlkXCI6bnVsbCxcIk9yZ2FuaXplSWRcIjpudWxsLFwiSXNTeXN0ZW1cIjp0cnVlLFwiU2lnbkluVGltZVwiOlwiMjAyMy0wMi0xMCAwOToyNzoxNi43MzNcIixcIkp3dFRva2VuXCI6bnVsbCxcIkxvZ2luTW9kZVwiOjF9IiwianRpIjoiU3lzdGVtIiwiaWF0IjoiMTY3NTk5MjQzOCIsIm5iZiI6IjE2NzU5OTI0MzgiLCJleHAiOiIxNjc2MDE0MDM4IiwiaXNzIjoiV2ViQXBwQElzc3VlckBIZW5yeSIsImF1ZCI6IldlYkFwcEF1ZGllbmNlIn0.zv7EBl-qc7_-Xr-RZfmtTuRDSCpX2AQaM1Z4dGMHhxc', N'0', N'1', N'1', NULL, N'2023-02-08 16:27:33.000', N'System', N'System', N'2023-02-10 09:27:18.123', NULL, NULL)
GO

INSERT INTO [dbo].[Base_User] ([Id], [Account], [Password], [Secretkey], [RealName], [NickName], [HeadIcon], [Gender], [Mobile], [OrganizeId], [DepartmentId], [LockEndDate], [PreviousVisit], [LastVisit], [LogOnCount], [JwtToKen], [LoginsNum], [SortCode], [EnabledMark], [Description], [CreateDate], [CreateUserId], [CreateUserName], [ModifyDate], [ModifyUserId], [ModifyUserName]) VALUES (N'System61', N'System6', N'35d34b4bd86bd0946cb0d6badf57f005', N'55f53b7647eccf70', N'henry', N'Henry', NULL, N'1', N'15271964110', N'', NULL, N'1900-01-01 00:00:00.000', N'2023-02-10 09:19:44.000', N'2023-02-10 09:27:15.563', N'4', N'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiaGVucnkiLCJpbmZvIjoie1wiVXNlcklkXCI6XCJTeXN0ZW1cIixcIlVzZXJOYW1lXCI6XCJoZW5yeVwiLFwiUGhvbmVcIjpudWxsLFwiSWROdW1iZXJcIjpudWxsLFwiQ29tcGFueUlkXCI6bnVsbCxcIk9yZ2FuaXplSWRcIjpudWxsLFwiSXNTeXN0ZW1cIjp0cnVlLFwiU2lnbkluVGltZVwiOlwiMjAyMy0wMi0xMCAwOToyNzoxNi43MzNcIixcIkp3dFRva2VuXCI6bnVsbCxcIkxvZ2luTW9kZVwiOjF9IiwianRpIjoiU3lzdGVtIiwiaWF0IjoiMTY3NTk5MjQzOCIsIm5iZiI6IjE2NzU5OTI0MzgiLCJleHAiOiIxNjc2MDE0MDM4IiwiaXNzIjoiV2ViQXBwQElzc3VlckBIZW5yeSIsImF1ZCI6IldlYkFwcEF1ZGllbmNlIn0.zv7EBl-qc7_-Xr-RZfmtTuRDSCpX2AQaM1Z4dGMHhxc', N'0', N'1', N'1', NULL, N'2023-02-08 16:27:33.000', N'System', N'System', N'2023-02-10 09:27:18.123', NULL, NULL)
GO

INSERT INTO [dbo].[Base_User] ([Id], [Account], [Password], [Secretkey], [RealName], [NickName], [HeadIcon], [Gender], [Mobile], [OrganizeId], [DepartmentId], [LockEndDate], [PreviousVisit], [LastVisit], [LogOnCount], [JwtToKen], [LoginsNum], [SortCode], [EnabledMark], [Description], [CreateDate], [CreateUserId], [CreateUserName], [ModifyDate], [ModifyUserId], [ModifyUserName]) VALUES (N'System611', N'System6', N'35d34b4bd86bd0946cb0d6badf57f005', N'55f53b7647eccf70', N'henry', N'Henry', NULL, N'1', N'15271964110', N'', NULL, N'1900-01-01 00:00:00.000', N'2023-02-10 09:19:44.000', N'2023-02-10 09:27:15.563', N'4', N'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiaGVucnkiLCJpbmZvIjoie1wiVXNlcklkXCI6XCJTeXN0ZW1cIixcIlVzZXJOYW1lXCI6XCJoZW5yeVwiLFwiUGhvbmVcIjpudWxsLFwiSWROdW1iZXJcIjpudWxsLFwiQ29tcGFueUlkXCI6bnVsbCxcIk9yZ2FuaXplSWRcIjpudWxsLFwiSXNTeXN0ZW1cIjp0cnVlLFwiU2lnbkluVGltZVwiOlwiMjAyMy0wMi0xMCAwOToyNzoxNi43MzNcIixcIkp3dFRva2VuXCI6bnVsbCxcIkxvZ2luTW9kZVwiOjF9IiwianRpIjoiU3lzdGVtIiwiaWF0IjoiMTY3NTk5MjQzOCIsIm5iZiI6IjE2NzU5OTI0MzgiLCJleHAiOiIxNjc2MDE0MDM4IiwiaXNzIjoiV2ViQXBwQElzc3VlckBIZW5yeSIsImF1ZCI6IldlYkFwcEF1ZGllbmNlIn0.zv7EBl-qc7_-Xr-RZfmtTuRDSCpX2AQaM1Z4dGMHhxc', N'0', N'1', N'1', NULL, N'2023-02-08 16:27:33.000', N'System', N'System', N'2023-02-10 09:27:18.123', NULL, NULL)
GO

INSERT INTO [dbo].[Base_User] ([Id], [Account], [Password], [Secretkey], [RealName], [NickName], [HeadIcon], [Gender], [Mobile], [OrganizeId], [DepartmentId], [LockEndDate], [PreviousVisit], [LastVisit], [LogOnCount], [JwtToKen], [LoginsNum], [SortCode], [EnabledMark], [Description], [CreateDate], [CreateUserId], [CreateUserName], [ModifyDate], [ModifyUserId], [ModifyUserName]) VALUES (N'System6111', N'System6', N'35d34b4bd86bd0946cb0d6badf57f005', N'55f53b7647eccf70', N'henry', N'Henry', NULL, N'1', N'15271964110', N'', NULL, N'1900-01-01 00:00:00.000', N'2023-02-10 09:19:44.000', N'2023-02-10 09:27:15.563', N'4', N'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiaGVucnkiLCJpbmZvIjoie1wiVXNlcklkXCI6XCJTeXN0ZW1cIixcIlVzZXJOYW1lXCI6XCJoZW5yeVwiLFwiUGhvbmVcIjpudWxsLFwiSWROdW1iZXJcIjpudWxsLFwiQ29tcGFueUlkXCI6bnVsbCxcIk9yZ2FuaXplSWRcIjpudWxsLFwiSXNTeXN0ZW1cIjp0cnVlLFwiU2lnbkluVGltZVwiOlwiMjAyMy0wMi0xMCAwOToyNzoxNi43MzNcIixcIkp3dFRva2VuXCI6bnVsbCxcIkxvZ2luTW9kZVwiOjF9IiwianRpIjoiU3lzdGVtIiwiaWF0IjoiMTY3NTk5MjQzOCIsIm5iZiI6IjE2NzU5OTI0MzgiLCJleHAiOiIxNjc2MDE0MDM4IiwiaXNzIjoiV2ViQXBwQElzc3VlckBIZW5yeSIsImF1ZCI6IldlYkFwcEF1ZGllbmNlIn0.zv7EBl-qc7_-Xr-RZfmtTuRDSCpX2AQaM1Z4dGMHhxc', N'0', N'1', N'1', NULL, N'2023-02-08 16:27:33.000', N'System', N'System', N'2023-02-10 09:27:18.123', NULL, NULL)
GO

INSERT INTO [dbo].[Base_User] ([Id], [Account], [Password], [Secretkey], [RealName], [NickName], [HeadIcon], [Gender], [Mobile], [OrganizeId], [DepartmentId], [LockEndDate], [PreviousVisit], [LastVisit], [LogOnCount], [JwtToKen], [LoginsNum], [SortCode], [EnabledMark], [Description], [CreateDate], [CreateUserId], [CreateUserName], [ModifyDate], [ModifyUserId], [ModifyUserName]) VALUES (N'System7', N'System7', N'35d34b4bd86bd0946cb0d6badf57f005', N'55f53b7647eccf70', N'henry', N'Henry', NULL, N'1', N'15271964110', N'', NULL, N'1900-01-01 00:00:00.000', N'2023-02-10 09:19:44.000', N'2023-02-10 09:27:15.563', N'4', N'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiaGVucnkiLCJpbmZvIjoie1wiVXNlcklkXCI6XCJTeXN0ZW1cIixcIlVzZXJOYW1lXCI6XCJoZW5yeVwiLFwiUGhvbmVcIjpudWxsLFwiSWROdW1iZXJcIjpudWxsLFwiQ29tcGFueUlkXCI6bnVsbCxcIk9yZ2FuaXplSWRcIjpudWxsLFwiSXNTeXN0ZW1cIjp0cnVlLFwiU2lnbkluVGltZVwiOlwiMjAyMy0wMi0xMCAwOToyNzoxNi43MzNcIixcIkp3dFRva2VuXCI6bnVsbCxcIkxvZ2luTW9kZVwiOjF9IiwianRpIjoiU3lzdGVtIiwiaWF0IjoiMTY3NTk5MjQzOCIsIm5iZiI6IjE2NzU5OTI0MzgiLCJleHAiOiIxNjc2MDE0MDM4IiwiaXNzIjoiV2ViQXBwQElzc3VlckBIZW5yeSIsImF1ZCI6IldlYkFwcEF1ZGllbmNlIn0.zv7EBl-qc7_-Xr-RZfmtTuRDSCpX2AQaM1Z4dGMHhxc', N'0', N'1', N'1', NULL, N'2023-02-08 16:27:33.000', N'System', N'System', N'2023-02-10 09:27:18.123', NULL, NULL)
GO

INSERT INTO [dbo].[Base_User] ([Id], [Account], [Password], [Secretkey], [RealName], [NickName], [HeadIcon], [Gender], [Mobile], [OrganizeId], [DepartmentId], [LockEndDate], [PreviousVisit], [LastVisit], [LogOnCount], [JwtToKen], [LoginsNum], [SortCode], [EnabledMark], [Description], [CreateDate], [CreateUserId], [CreateUserName], [ModifyDate], [ModifyUserId], [ModifyUserName]) VALUES (N'System71', N'System7', N'35d34b4bd86bd0946cb0d6badf57f005', N'55f53b7647eccf70', N'henry', N'Henry', NULL, N'1', N'15271964110', N'', NULL, N'1900-01-01 00:00:00.000', N'2023-02-10 09:19:44.000', N'2023-02-10 09:27:15.563', N'4', N'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiaGVucnkiLCJpbmZvIjoie1wiVXNlcklkXCI6XCJTeXN0ZW1cIixcIlVzZXJOYW1lXCI6XCJoZW5yeVwiLFwiUGhvbmVcIjpudWxsLFwiSWROdW1iZXJcIjpudWxsLFwiQ29tcGFueUlkXCI6bnVsbCxcIk9yZ2FuaXplSWRcIjpudWxsLFwiSXNTeXN0ZW1cIjp0cnVlLFwiU2lnbkluVGltZVwiOlwiMjAyMy0wMi0xMCAwOToyNzoxNi43MzNcIixcIkp3dFRva2VuXCI6bnVsbCxcIkxvZ2luTW9kZVwiOjF9IiwianRpIjoiU3lzdGVtIiwiaWF0IjoiMTY3NTk5MjQzOCIsIm5iZiI6IjE2NzU5OTI0MzgiLCJleHAiOiIxNjc2MDE0MDM4IiwiaXNzIjoiV2ViQXBwQElzc3VlckBIZW5yeSIsImF1ZCI6IldlYkFwcEF1ZGllbmNlIn0.zv7EBl-qc7_-Xr-RZfmtTuRDSCpX2AQaM1Z4dGMHhxc', N'0', N'1', N'1', NULL, N'2023-02-08 16:27:33.000', N'System', N'System', N'2023-02-10 09:27:18.123', NULL, NULL)
GO

INSERT INTO [dbo].[Base_User] ([Id], [Account], [Password], [Secretkey], [RealName], [NickName], [HeadIcon], [Gender], [Mobile], [OrganizeId], [DepartmentId], [LockEndDate], [PreviousVisit], [LastVisit], [LogOnCount], [JwtToKen], [LoginsNum], [SortCode], [EnabledMark], [Description], [CreateDate], [CreateUserId], [CreateUserName], [ModifyDate], [ModifyUserId], [ModifyUserName]) VALUES (N'System711', N'System7', N'35d34b4bd86bd0946cb0d6badf57f005', N'55f53b7647eccf70', N'henry', N'Henry', NULL, N'1', N'15271964110', N'', NULL, N'1900-01-01 00:00:00.000', N'2023-02-10 09:19:44.000', N'2023-02-10 09:27:15.563', N'4', N'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiaGVucnkiLCJpbmZvIjoie1wiVXNlcklkXCI6XCJTeXN0ZW1cIixcIlVzZXJOYW1lXCI6XCJoZW5yeVwiLFwiUGhvbmVcIjpudWxsLFwiSWROdW1iZXJcIjpudWxsLFwiQ29tcGFueUlkXCI6bnVsbCxcIk9yZ2FuaXplSWRcIjpudWxsLFwiSXNTeXN0ZW1cIjp0cnVlLFwiU2lnbkluVGltZVwiOlwiMjAyMy0wMi0xMCAwOToyNzoxNi43MzNcIixcIkp3dFRva2VuXCI6bnVsbCxcIkxvZ2luTW9kZVwiOjF9IiwianRpIjoiU3lzdGVtIiwiaWF0IjoiMTY3NTk5MjQzOCIsIm5iZiI6IjE2NzU5OTI0MzgiLCJleHAiOiIxNjc2MDE0MDM4IiwiaXNzIjoiV2ViQXBwQElzc3VlckBIZW5yeSIsImF1ZCI6IldlYkFwcEF1ZGllbmNlIn0.zv7EBl-qc7_-Xr-RZfmtTuRDSCpX2AQaM1Z4dGMHhxc', N'0', N'1', N'1', NULL, N'2023-02-08 16:27:33.000', N'System', N'System', N'2023-02-10 09:27:18.123', NULL, NULL)
GO

INSERT INTO [dbo].[Base_User] ([Id], [Account], [Password], [Secretkey], [RealName], [NickName], [HeadIcon], [Gender], [Mobile], [OrganizeId], [DepartmentId], [LockEndDate], [PreviousVisit], [LastVisit], [LogOnCount], [JwtToKen], [LoginsNum], [SortCode], [EnabledMark], [Description], [CreateDate], [CreateUserId], [CreateUserName], [ModifyDate], [ModifyUserId], [ModifyUserName]) VALUES (N'System7111', N'System7', N'35d34b4bd86bd0946cb0d6badf57f005', N'55f53b7647eccf70', N'henry', N'Henry', NULL, N'1', N'15271964110', N'', NULL, N'1900-01-01 00:00:00.000', N'2023-02-10 09:19:44.000', N'2023-02-10 09:27:15.563', N'4', N'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiaGVucnkiLCJpbmZvIjoie1wiVXNlcklkXCI6XCJTeXN0ZW1cIixcIlVzZXJOYW1lXCI6XCJoZW5yeVwiLFwiUGhvbmVcIjpudWxsLFwiSWROdW1iZXJcIjpudWxsLFwiQ29tcGFueUlkXCI6bnVsbCxcIk9yZ2FuaXplSWRcIjpudWxsLFwiSXNTeXN0ZW1cIjp0cnVlLFwiU2lnbkluVGltZVwiOlwiMjAyMy0wMi0xMCAwOToyNzoxNi43MzNcIixcIkp3dFRva2VuXCI6bnVsbCxcIkxvZ2luTW9kZVwiOjF9IiwianRpIjoiU3lzdGVtIiwiaWF0IjoiMTY3NTk5MjQzOCIsIm5iZiI6IjE2NzU5OTI0MzgiLCJleHAiOiIxNjc2MDE0MDM4IiwiaXNzIjoiV2ViQXBwQElzc3VlckBIZW5yeSIsImF1ZCI6IldlYkFwcEF1ZGllbmNlIn0.zv7EBl-qc7_-Xr-RZfmtTuRDSCpX2AQaM1Z4dGMHhxc', N'0', N'1', N'1', NULL, N'2023-02-08 16:27:33.000', N'System', N'System', N'2023-02-10 09:27:18.123', NULL, NULL)
GO

INSERT INTO [dbo].[Base_User] ([Id], [Account], [Password], [Secretkey], [RealName], [NickName], [HeadIcon], [Gender], [Mobile], [OrganizeId], [DepartmentId], [LockEndDate], [PreviousVisit], [LastVisit], [LogOnCount], [JwtToKen], [LoginsNum], [SortCode], [EnabledMark], [Description], [CreateDate], [CreateUserId], [CreateUserName], [ModifyDate], [ModifyUserId], [ModifyUserName]) VALUES (N'System8', N'System8', N'35d34b4bd86bd0946cb0d6badf57f005', N'55f53b7647eccf70', N'henry', N'Henry', NULL, N'1', N'15271964110', N'', NULL, N'1900-01-01 00:00:00.000', N'2023-02-10 09:19:44.000', N'2023-02-10 09:27:15.563', N'4', N'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiaGVucnkiLCJpbmZvIjoie1wiVXNlcklkXCI6XCJTeXN0ZW1cIixcIlVzZXJOYW1lXCI6XCJoZW5yeVwiLFwiUGhvbmVcIjpudWxsLFwiSWROdW1iZXJcIjpudWxsLFwiQ29tcGFueUlkXCI6bnVsbCxcIk9yZ2FuaXplSWRcIjpudWxsLFwiSXNTeXN0ZW1cIjp0cnVlLFwiU2lnbkluVGltZVwiOlwiMjAyMy0wMi0xMCAwOToyNzoxNi43MzNcIixcIkp3dFRva2VuXCI6bnVsbCxcIkxvZ2luTW9kZVwiOjF9IiwianRpIjoiU3lzdGVtIiwiaWF0IjoiMTY3NTk5MjQzOCIsIm5iZiI6IjE2NzU5OTI0MzgiLCJleHAiOiIxNjc2MDE0MDM4IiwiaXNzIjoiV2ViQXBwQElzc3VlckBIZW5yeSIsImF1ZCI6IldlYkFwcEF1ZGllbmNlIn0.zv7EBl-qc7_-Xr-RZfmtTuRDSCpX2AQaM1Z4dGMHhxc', N'0', N'1', N'1', NULL, N'2023-02-08 16:27:33.000', N'System', N'System', N'2023-02-10 09:27:18.123', NULL, NULL)
GO

INSERT INTO [dbo].[Base_User] ([Id], [Account], [Password], [Secretkey], [RealName], [NickName], [HeadIcon], [Gender], [Mobile], [OrganizeId], [DepartmentId], [LockEndDate], [PreviousVisit], [LastVisit], [LogOnCount], [JwtToKen], [LoginsNum], [SortCode], [EnabledMark], [Description], [CreateDate], [CreateUserId], [CreateUserName], [ModifyDate], [ModifyUserId], [ModifyUserName]) VALUES (N'System81', N'System8', N'35d34b4bd86bd0946cb0d6badf57f005', N'55f53b7647eccf70', N'henry', N'Henry', NULL, N'1', N'15271964110', N'', NULL, N'1900-01-01 00:00:00.000', N'2023-02-10 09:19:44.000', N'2023-02-10 09:27:15.563', N'4', N'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiaGVucnkiLCJpbmZvIjoie1wiVXNlcklkXCI6XCJTeXN0ZW1cIixcIlVzZXJOYW1lXCI6XCJoZW5yeVwiLFwiUGhvbmVcIjpudWxsLFwiSWROdW1iZXJcIjpudWxsLFwiQ29tcGFueUlkXCI6bnVsbCxcIk9yZ2FuaXplSWRcIjpudWxsLFwiSXNTeXN0ZW1cIjp0cnVlLFwiU2lnbkluVGltZVwiOlwiMjAyMy0wMi0xMCAwOToyNzoxNi43MzNcIixcIkp3dFRva2VuXCI6bnVsbCxcIkxvZ2luTW9kZVwiOjF9IiwianRpIjoiU3lzdGVtIiwiaWF0IjoiMTY3NTk5MjQzOCIsIm5iZiI6IjE2NzU5OTI0MzgiLCJleHAiOiIxNjc2MDE0MDM4IiwiaXNzIjoiV2ViQXBwQElzc3VlckBIZW5yeSIsImF1ZCI6IldlYkFwcEF1ZGllbmNlIn0.zv7EBl-qc7_-Xr-RZfmtTuRDSCpX2AQaM1Z4dGMHhxc', N'0', N'1', N'1', NULL, N'2023-02-08 16:27:33.000', N'System', N'System', N'2023-02-10 09:27:18.123', NULL, NULL)
GO

INSERT INTO [dbo].[Base_User] ([Id], [Account], [Password], [Secretkey], [RealName], [NickName], [HeadIcon], [Gender], [Mobile], [OrganizeId], [DepartmentId], [LockEndDate], [PreviousVisit], [LastVisit], [LogOnCount], [JwtToKen], [LoginsNum], [SortCode], [EnabledMark], [Description], [CreateDate], [CreateUserId], [CreateUserName], [ModifyDate], [ModifyUserId], [ModifyUserName]) VALUES (N'System811', N'System8', N'35d34b4bd86bd0946cb0d6badf57f005', N'55f53b7647eccf70', N'henry', N'Henry', NULL, N'1', N'15271964110', N'', NULL, N'1900-01-01 00:00:00.000', N'2023-02-10 09:19:44.000', N'2023-02-10 09:27:15.563', N'4', N'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiaGVucnkiLCJpbmZvIjoie1wiVXNlcklkXCI6XCJTeXN0ZW1cIixcIlVzZXJOYW1lXCI6XCJoZW5yeVwiLFwiUGhvbmVcIjpudWxsLFwiSWROdW1iZXJcIjpudWxsLFwiQ29tcGFueUlkXCI6bnVsbCxcIk9yZ2FuaXplSWRcIjpudWxsLFwiSXNTeXN0ZW1cIjp0cnVlLFwiU2lnbkluVGltZVwiOlwiMjAyMy0wMi0xMCAwOToyNzoxNi43MzNcIixcIkp3dFRva2VuXCI6bnVsbCxcIkxvZ2luTW9kZVwiOjF9IiwianRpIjoiU3lzdGVtIiwiaWF0IjoiMTY3NTk5MjQzOCIsIm5iZiI6IjE2NzU5OTI0MzgiLCJleHAiOiIxNjc2MDE0MDM4IiwiaXNzIjoiV2ViQXBwQElzc3VlckBIZW5yeSIsImF1ZCI6IldlYkFwcEF1ZGllbmNlIn0.zv7EBl-qc7_-Xr-RZfmtTuRDSCpX2AQaM1Z4dGMHhxc', N'0', N'1', N'1', NULL, N'2023-02-08 16:27:33.000', N'System', N'System', N'2023-02-10 09:27:18.123', NULL, NULL)
GO

INSERT INTO [dbo].[Base_User] ([Id], [Account], [Password], [Secretkey], [RealName], [NickName], [HeadIcon], [Gender], [Mobile], [OrganizeId], [DepartmentId], [LockEndDate], [PreviousVisit], [LastVisit], [LogOnCount], [JwtToKen], [LoginsNum], [SortCode], [EnabledMark], [Description], [CreateDate], [CreateUserId], [CreateUserName], [ModifyDate], [ModifyUserId], [ModifyUserName]) VALUES (N'System8111', N'System8', N'35d34b4bd86bd0946cb0d6badf57f005', N'55f53b7647eccf70', N'henry', N'Henry', NULL, N'1', N'15271964110', N'', NULL, N'1900-01-01 00:00:00.000', N'2023-02-10 09:19:44.000', N'2023-02-10 09:27:15.563', N'4', N'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiaGVucnkiLCJpbmZvIjoie1wiVXNlcklkXCI6XCJTeXN0ZW1cIixcIlVzZXJOYW1lXCI6XCJoZW5yeVwiLFwiUGhvbmVcIjpudWxsLFwiSWROdW1iZXJcIjpudWxsLFwiQ29tcGFueUlkXCI6bnVsbCxcIk9yZ2FuaXplSWRcIjpudWxsLFwiSXNTeXN0ZW1cIjp0cnVlLFwiU2lnbkluVGltZVwiOlwiMjAyMy0wMi0xMCAwOToyNzoxNi43MzNcIixcIkp3dFRva2VuXCI6bnVsbCxcIkxvZ2luTW9kZVwiOjF9IiwianRpIjoiU3lzdGVtIiwiaWF0IjoiMTY3NTk5MjQzOCIsIm5iZiI6IjE2NzU5OTI0MzgiLCJleHAiOiIxNjc2MDE0MDM4IiwiaXNzIjoiV2ViQXBwQElzc3VlckBIZW5yeSIsImF1ZCI6IldlYkFwcEF1ZGllbmNlIn0.zv7EBl-qc7_-Xr-RZfmtTuRDSCpX2AQaM1Z4dGMHhxc', N'0', N'1', N'1', NULL, N'2023-02-08 16:27:33.000', N'System', N'System', N'2023-02-10 09:27:18.123', NULL, NULL)
GO

INSERT INTO [dbo].[Base_User] ([Id], [Account], [Password], [Secretkey], [RealName], [NickName], [HeadIcon], [Gender], [Mobile], [OrganizeId], [DepartmentId], [LockEndDate], [PreviousVisit], [LastVisit], [LogOnCount], [JwtToKen], [LoginsNum], [SortCode], [EnabledMark], [Description], [CreateDate], [CreateUserId], [CreateUserName], [ModifyDate], [ModifyUserId], [ModifyUserName]) VALUES (N'System9', N'System', N'35d34b4bd86bd0946cb0d6badf57f005', N'55f53b7647eccf70', N'henry', N'Henry', NULL, N'1', N'15271964110', N'', NULL, N'1900-01-01 00:00:00.000', N'2023-03-09 10:05:33.000', N'2023-03-09 10:09:11.300', N'99', N'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiaGVucnkiLCJpbmZvIjoie1wiVXNlcklkXCI6XCJTeXN0ZW1cIixcIlVzZXJOYW1lXCI6XCJoZW5yeVwiLFwiQWNjb3VudFwiOlwiU3lzdGVtXCIsXCJQaG9uZVwiOm51bGwsXCJJZE51bWJlclwiOm51bGwsXCJDb21wYW55SWRcIjpudWxsLFwiT3JnYW5pemVJZFwiOlwiXCIsXCJJc1N5c3RlbVwiOnRydWUsXCJTaWduSW5UaW1lXCI6XCIyMDIzLTAzLTA5IDEwOjA5OjExLjg1MVwiLFwiSnd0VG9rZW5cIjpudWxsLFwiTG9naW5Nb2RlXCI6MX0iLCJqdGkiOiJTeXN0ZW0iLCJpYXQiOiIxNjc4MzI3NzUxIiwibmJmIjoiMTY3ODMyNzc1MSIsImV4cCI6IjE2NzgzNDkzNTEiLCJpc3MiOiJXZWJBcHBASXNzdWVyQEhlbnJ5IiwiYXVkIjoiV2ViQXBwQXVkaWVuY2UifQ.nOIKmM4sVUyfhoiv-Q4TqEwXEDIhVHayAceHVOMMWis', N'0', N'1', N'1', NULL, N'2023-02-08 16:27:33.000', N'System', N'System', N'2023-03-09 10:09:11.887', N'System', N'henry')
GO


-- ----------------------------
-- Table structure for Log_DBlogs
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[Log_DBlogs]') AND type IN ('U'))
	DROP TABLE [dbo].[Log_DBlogs]
GO

CREATE TABLE [dbo].[Log_DBlogs] (
  [Id] varchar(50) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [OperateUserId] varchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [OperateAccount] varchar(255) COLLATE Chinese_PRC_CI_AS  NULL,
  [SqlExecute] varchar(max) COLLATE Chinese_PRC_CI_AS  NULL,
  [LogTime] datetime  NULL
)
GO

ALTER TABLE [dbo].[Log_DBlogs] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'DB执行语句表主键Id',
'SCHEMA', N'dbo',
'TABLE', N'Log_DBlogs',
'COLUMN', N'Id'
GO

EXEC sp_addextendedproperty
'MS_Description', N'操作用户Id',
'SCHEMA', N'dbo',
'TABLE', N'Log_DBlogs',
'COLUMN', N'OperateUserId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'操作用户',
'SCHEMA', N'dbo',
'TABLE', N'Log_DBlogs',
'COLUMN', N'OperateAccount'
GO

EXEC sp_addextendedproperty
'MS_Description', N'执行的sql语句',
'SCHEMA', N'dbo',
'TABLE', N'Log_DBlogs',
'COLUMN', N'SqlExecute'
GO

EXEC sp_addextendedproperty
'MS_Description', N'记录时间',
'SCHEMA', N'dbo',
'TABLE', N'Log_DBlogs',
'COLUMN', N'LogTime'
GO

EXEC sp_addextendedproperty
'MS_Description', N'DB执行语句表',
'SCHEMA', N'dbo',
'TABLE', N'Log_DBlogs'
GO


-- ----------------------------
-- Records of Log_DBlogs
-- ----------------------------

-- ----------------------------
-- Table structure for Log_Loginlogs
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[Log_Loginlogs]') AND type IN ('U'))
	DROP TABLE [dbo].[Log_Loginlogs]
GO

CREATE TABLE [dbo].[Log_Loginlogs] (
  [Id] varchar(50) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [OperateUserId] varchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [OperateAccount] varchar(255) COLLATE Chinese_PRC_CI_AS  NULL,
  [OperateUserName] varchar(255) COLLATE Chinese_PRC_CI_AS  NULL,
  [IPAddress] varchar(255) COLLATE Chinese_PRC_CI_AS  NULL,
  [City] varchar(255) COLLATE Chinese_PRC_CI_AS  NULL,
  [Host] varchar(255) COLLATE Chinese_PRC_CI_AS  NULL,
  [OS] varchar(255) COLLATE Chinese_PRC_CI_AS  NULL,
  [Browser] varchar(500) COLLATE Chinese_PRC_CI_AS  NULL,
  [UserAgent] varchar(max) COLLATE Chinese_PRC_CI_AS  NULL,
  [Result] varchar(max) COLLATE Chinese_PRC_CI_AS  NULL,
  [LoginTime] datetime  NULL
)
GO

ALTER TABLE [dbo].[Log_Loginlogs] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'登录日志表主键Id',
'SCHEMA', N'dbo',
'TABLE', N'Log_Loginlogs',
'COLUMN', N'Id'
GO

EXEC sp_addextendedproperty
'MS_Description', N'操作用户Id',
'SCHEMA', N'dbo',
'TABLE', N'Log_Loginlogs',
'COLUMN', N'OperateUserId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'操作账号',
'SCHEMA', N'dbo',
'TABLE', N'Log_Loginlogs',
'COLUMN', N'OperateAccount'
GO

EXEC sp_addextendedproperty
'MS_Description', N'IP地址',
'SCHEMA', N'dbo',
'TABLE', N'Log_Loginlogs',
'COLUMN', N'IPAddress'
GO

EXEC sp_addextendedproperty
'MS_Description', N'IP所在城市',
'SCHEMA', N'dbo',
'TABLE', N'Log_Loginlogs',
'COLUMN', N'City'
GO

EXEC sp_addextendedproperty
'MS_Description', N'主机',
'SCHEMA', N'dbo',
'TABLE', N'Log_Loginlogs',
'COLUMN', N'Host'
GO

EXEC sp_addextendedproperty
'MS_Description', N'系统版本',
'SCHEMA', N'dbo',
'TABLE', N'Log_Loginlogs',
'COLUMN', N'OS'
GO

EXEC sp_addextendedproperty
'MS_Description', N'浏览器',
'SCHEMA', N'dbo',
'TABLE', N'Log_Loginlogs',
'COLUMN', N'Browser'
GO

EXEC sp_addextendedproperty
'MS_Description', N'浏览器UserAgent',
'SCHEMA', N'dbo',
'TABLE', N'Log_Loginlogs',
'COLUMN', N'UserAgent'
GO

EXEC sp_addextendedproperty
'MS_Description', N'登录结果',
'SCHEMA', N'dbo',
'TABLE', N'Log_Loginlogs',
'COLUMN', N'Result'
GO

EXEC sp_addextendedproperty
'MS_Description', N'登录时间',
'SCHEMA', N'dbo',
'TABLE', N'Log_Loginlogs',
'COLUMN', N'LoginTime'
GO

EXEC sp_addextendedproperty
'MS_Description', N'登录日志表',
'SCHEMA', N'dbo',
'TABLE', N'Log_Loginlogs'
GO


-- ----------------------------
-- Records of Log_Loginlogs
-- ----------------------------

-- ----------------------------
-- Table structure for Log_Serilogs
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[Log_Serilogs]') AND type IN ('U'))
	DROP TABLE [dbo].[Log_Serilogs]
GO

CREATE TABLE [dbo].[Log_Serilogs] (
  [Id] int  IDENTITY(1,1) NOT NULL,
  [Message] nvarchar(max) COLLATE Chinese_PRC_CI_AS  NULL,
  [MessageTemplate] nvarchar(max) COLLATE Chinese_PRC_CI_AS  NULL,
  [Level] nvarchar(max) COLLATE Chinese_PRC_CI_AS  NULL,
  [TimeStamp] datetime  NULL,
  [Exception] nvarchar(max) COLLATE Chinese_PRC_CI_AS  NULL,
  [Properties] nvarchar(max) COLLATE Chinese_PRC_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[Log_Serilogs] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Records of Log_Serilogs
-- ----------------------------
SET IDENTITY_INSERT [dbo].[Log_Serilogs] ON
GO

SET IDENTITY_INSERT [dbo].[Log_Serilogs] OFF
GO


-- ----------------------------
-- Primary Key structure for table Base_Menu_Buttons
-- ----------------------------
ALTER TABLE [dbo].[Base_Menu_Buttons] ADD CONSTRAINT [PK__Base_Men__3214EC07BEB0210B] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table Base_Menu_Tree
-- ----------------------------
ALTER TABLE [dbo].[Base_Menu_Tree] ADD CONSTRAINT [PK__Base_Men__C99ED230BC478F74] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table Base_Role
-- ----------------------------
ALTER TABLE [dbo].[Base_Role] ADD CONSTRAINT [PK__Base_Rol__8AFACE1AF14F8C27] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table Base_Roles_User
-- ----------------------------
ALTER TABLE [dbo].[Base_Roles_User] ADD CONSTRAINT [PK__Base_Rol__3214EC07E50203EB] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table Base_Roles_User_Permission
-- ----------------------------
ALTER TABLE [dbo].[Base_Roles_User_Permission] ADD CONSTRAINT [PK__Base_Rol__3214EC0760DEEDD1] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table Base_User
-- ----------------------------
ALTER TABLE [dbo].[Base_User] ADD CONSTRAINT [PK__Manage_A__349DA5A6DE15FCF8] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table Log_DBlogs
-- ----------------------------
ALTER TABLE [dbo].[Log_DBlogs] ADD CONSTRAINT [PK__Log_DBlo__3214EC077C4C065B] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table Log_Loginlogs
-- ----------------------------
ALTER TABLE [dbo].[Log_Loginlogs] ADD CONSTRAINT [PK__Log_Logi__3214EC0776065A1B] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for Log_Serilogs
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[Log_Serilogs]', RESEED, 382719)
GO


-- ----------------------------
-- Primary Key structure for table Log_Serilogs
-- ----------------------------
ALTER TABLE [dbo].[Log_Serilogs] ADD CONSTRAINT [PK_Log_Serilogs] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO

