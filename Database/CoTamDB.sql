--CREATE DATABASE CoTam GO
--USE CoTam

-- ****** CREATE TABLE ***** --
CREATE TABLE [Admin_Manager](
	[Id] INT IDENTITY(1,1) NOT NULL,
	[Name] NVARCHAR(50) NOT NULL,
	[Phone] CHAR(10) NULL,
	[DateOfBirth] DATE NULL,
	[Email] NVARCHAR(100) NULL,
	[LinkFacebook] NVARCHAR(100) NULL,
	[Avatar] NTEXT NULL,
	[Active] BIT NOT NULL DEFAULT 1,
	[RoleId] INT NOT NULL
);
GO

CREATE TABLE [Role](
	[Id] INT IDENTITY(1,1) NOT NULL,
	[Name] CHAR(10) NOT NULL
);
GO



CREATE TABLE [Area](
	[Id] INT IDENTITY(1,1) NOT NULL,
	[Name] NVARCHAR(100)  NULL,
	[Active] BIT NOT NULL DEFAULT 1,
	[District] NVARCHAR(100) NULL,
	[City] NVARCHAR(100)
);
GO

CREATE TABLE [Building](
	[Id] INT IDENTITY(1,1) NOT NULL,
	[Name] NVARCHAR(100) NOT NULL,
	[Active] BIT NOT NULL DEFAULT 1,
	[AreaId] INT NOT NULL
);
GO

CREATE TABLE [Customer](
	[Id] INT IDENTITY(1,1) NOT NULL,
	[Name] NVARCHAR(50) NOT NULL,
	[Phone] CHAR(10) NULL,
	[DateOfBirth] DATE NULL,
	[Email] NVARCHAR(100) NULL,
	[LinkFacebook] NVARCHAR(100) NULL,
	[Avatar] NTEXT NULL,
	[eWallet] MONEY NOT NULL DEFAULT 0,
	[Active] BIT NOT NULL DEFAULT 1
);
GO

CREATE TABLE [House](
	[Id] INT IDENTITY(1,1) NOT NULL,
	[Number] CHAR(10) NOT NULL,
	[Active] BIT NOT NULL DEFAULT 1,
	[CustomerId] INT NOT NULL,
	[BuildingId] INT NOT NULL
);
GO

CREATE TABLE [HouseWorker](
	[Id] INT IDENTITY(1,1) NOT NULL,
	[Name] NVARCHAR(50) NOT NULL,
	[Phone] CHAR(10) NULL,
	[DateOfBirth] DATE NULL,
	[Email] NVARCHAR(100) NULL,
	[LinkFacebook] NVARCHAR(100) NULL,
	[Avatar] NTEXT NULL,
	[Active] INT NOT NULL DEFAULT 1,
	[AreaId] INT NOT NULL,
	[ManagerId] INT NOT NULL
);
GO

CREATE TABLE [WorkerTag](
	[Id] INT IDENTITY(1,1) NOT NULL,
	[Name] NVARCHAR(50) NOT NULL,
	[HouseWorkerId] INT NOT NULL
);
GO

CREATE TABLE [Service](
	[Id] INT IDENTITY NOT NULL,
	[Name] NVARCHAR(50) NOT NULL,
	[Description] NTEXT NULL,
	[Price] MONEY NOT NULL DEFAULT 0,
	[Active] INT NOT NULL DEFAULT 1
);
GO

CREATE TABLE [ExtraService](
	[Id] INT IDENTITY(1,1) NOT NULL,
	[Name] NVARCHAR(50) NOT NULL,
	[Description] NTEXT NULL,
	[Price] MONEY NOT NULL DEFAULT 0,
	[Active] INT NOT NULL DEFAULT 1,
	[ServiceId] INT NOT NULL
);
GO

CREATE TABLE [Package](
	[Id] INT IDENTITY(1,1) NOT NULL,
	[NumberOfWorker] INT NULL,
	[Duration] INT NULL,
	[Active] INT NOT NULL DEFAULT 1,
	[ServiceId] INT NOT NULL
);
GO

CREATE TABLE [Promotion](
	[Id] INT IDENTITY(1,1) NOT NULL,
	[Code] VARCHAR(20) NOT NULL,
	[Description] NTEXT NULL,
	[Value] MONEY NULL,
	[Discount] FLOAT NULL,
	[Amount] INT NULL,
	[StartDate] DATETIME NULL,
	[EndDate] DATETIME NULL,
	[Active] BIT NOT NULL DEFAULT 1
);
GO

CREATE TABLE [CustomerPromotion](
	[Id] INT IDENTITY(1,1) NOT NULL,
	[isUsed] BIT NOT NULL DEFAULT 0,
	[CustomerId] INT NOT NULL,
	[PromotionId] INT NOT NULL
);
GO

CREATE TABLE [PaymentMethod](
	[Id] INT IDENTITY(1,1) NOT NULL,
	[Name] NVARCHAR(50) NOT NULL,
	[Active] BIT NOT NULL DEFAULT 1
);
GO


CREATE TABLE [Order](
	[Id] INT IDENTITY(1,1) NOT NULL,
	[DateTime] DATETIME NOT NULL DEFAULT GETDATE(),
	[SubTotal] MONEY NOT NULL DEFAULT 0,
	[Total] MONEY NOT NULL DEFAULT 0,
	[HouseId] INT NOT NULL,
	[PackageId] INT NOT NULL,
	[PromotionId] INT NULL,
	[PaymentMethodId] INT NOT NULL,
	[OrderState] INT NOT NULL
);	
GO

CREATE TABLE [OrderDetail](
	[Id] INT IDENTITY(1,1) NOT NULL,
	[ExtraServiceId] INT NULL,
	[OrderId] INT NOT NULL
);
GO

CREATE TABLE [WorkerInOrder](
	[Id] INT IDENTITY(1,1) NOT NULL,
	[HouseWorkerId] INT NOT NULL,
	[OrderId] INT NOT NULL,
	[Rating] INT NULL
);	
GO

CREATE TABLE [Information](
	[Id] INT IDENTITY(1,1) NOT NULL,
	[Name] NVARCHAR(100) NOT NULL,
	[Discription] NTEXT NULL,
	[Active] BIT NOT NULL DEFAULT 1
);
GO

CREATE TABLE [RefreshToken](
	[Id] INT IDENTITY(1,1) NOT NULL,
	[UserId] int,
    [Token] ntext,
	[JwtId] ntext,
	[IsUsed] bit,
	[IsRevoked] bit,
	[IssuedAt] DateTime,
	[ExpiredAt] DateTime
);
GO

-- ****** CREATE CONSTRAINT ***** --
-- *** Primary key *** --
ALTER TABLE [Admin_Manager] ADD CONSTRAINT [PK_Admin_Manager] PRIMARY KEY (Id);
GO

ALTER TABLE [Role] ADD CONSTRAINT [PK_Role] PRIMARY KEY (Id);
GO

ALTER TABLE [Area] ADD CONSTRAINT [PK_Area] PRIMARY KEY (Id);
GO

ALTER TABLE [Building] ADD CONSTRAINT [PK_Building] PRIMARY KEY (Id);
GO

ALTER TABLE [Customer] ADD CONSTRAINT [PK_Customer] PRIMARY KEY (Id);
GO

ALTER TABLE [House] ADD CONSTRAINT [PK_House] PRIMARY KEY (Id);
GO

ALTER TABLE [HouseWorker] ADD CONSTRAINT [PK_HouseWorker] PRIMARY KEY (Id);
GO

ALTER TABLE [WorkerTag] ADD CONSTRAINT [PK_WorkerTag] PRIMARY KEY (Id);
GO

ALTER TABLE [Service] ADD CONSTRAINT [PK_Service] PRIMARY KEY (Id);
GO

ALTER TABLE [ExtraService] ADD CONSTRAINT [PK_ExtraService] PRIMARY KEY (Id);
GO

ALTER TABLE [Package] ADD CONSTRAINT [PK_Package] PRIMARY KEY (Id);
GO

ALTER TABLE [Promotion] ADD CONSTRAINT [PK_Promotion] PRIMARY KEY (Id);
GO

ALTER TABLE [CustomerPromotion] ADD CONSTRAINT [PK_CustomerPromotion] PRIMARY KEY (Id);
GO

ALTER TABLE [PaymentMethod] ADD CONSTRAINT [PK_PaymentMethod] PRIMARY KEY (Id);
GO

ALTER TABLE [Order] ADD CONSTRAINT [PK_Order] PRIMARY KEY (Id);
GO

ALTER TABLE [OrderDetail] ADD CONSTRAINT [PK_OrderDetail] PRIMARY KEY (Id);
GO

ALTER TABLE [WorkerInOrder] ADD CONSTRAINT [PK_WorkerInOrder] PRIMARY KEY (Id);
GO

ALTER TABLE [Information] ADD CONSTRAINT [PK_Information] PRIMARY KEY (Id);
GO

ALTER TABLE [RefreshToken] ADD CONSTRAINT [PK_RefreshToken] PRIMARY KEY (Id);
GO

-- *** Foreign key *** --
ALTER TABLE [Admin_Manager]
ADD CONSTRAINT FK_AdminManager_Role
    FOREIGN KEY ([RoleId])
    REFERENCES [Role]([Id]);
GO

ALTER TABLE [Building]
ADD CONSTRAINT FK_Building_Area
    FOREIGN KEY ([AreaId])
    REFERENCES [Area]([Id]);
GO

ALTER TABLE [House]
ADD CONSTRAINT FK_House_Customer
    FOREIGN KEY ([CustomerId])
    REFERENCES [Customer]([Id]);
GO

ALTER TABLE [House]
ADD CONSTRAINT FK_House_Building
    FOREIGN KEY ([BuildingId])
    REFERENCES [Building]([Id]);
GO

ALTER TABLE [HouseWorker]
ADD CONSTRAINT FK_HouseWorker_Manager
    FOREIGN KEY ([ManagerId])
    REFERENCES [Admin_Manager]([Id]);
GO

ALTER TABLE [WorkerTag]
ADD CONSTRAINT FK_WorkerTag_HouseWorker
    FOREIGN KEY ([HouseWorkerId])
    REFERENCES [HouseWorker]([Id]);
GO

ALTER TABLE [ExtraService]
ADD CONSTRAINT FK_ExtraService_Service
    FOREIGN KEY ([ServiceId])
    REFERENCES [Service]([Id]);
GO

ALTER TABLE [Package]
ADD CONSTRAINT FK_Package_Service
    FOREIGN KEY ([ServiceId])
    REFERENCES [Service]([Id]);
GO

ALTER TABLE [CustomerPromotion]
ADD CONSTRAINT FK_CustomerPromotion_Customer
    FOREIGN KEY ([CustomerId])
    REFERENCES [Customer]([Id]);
GO

ALTER TABLE [CustomerPromotion]
ADD CONSTRAINT FK_CustomerPromotion_Promotion
    FOREIGN KEY ([PromotionId])
    REFERENCES [Promotion]([Id]);
GO

ALTER TABLE [Order]
ADD CONSTRAINT FK_Order_House
    FOREIGN KEY ([HouseId])
    REFERENCES [House]([Id]);
GO

ALTER TABLE [Order]
ADD CONSTRAINT FK_Order_Package
    FOREIGN KEY ([PackageId])
    REFERENCES [Package]([Id]);
GO

ALTER TABLE [Order]
ADD CONSTRAINT FK_Order_Promotion
    FOREIGN KEY ([PromotionId])
    REFERENCES [Promotion]([Id]);
GO

ALTER TABLE [Order]
ADD CONSTRAINT FK_Order_PaymentMethod
    FOREIGN KEY ([PaymentMethodId])
    REFERENCES [PaymentMethod]([Id]);
GO

ALTER TABLE [OrderDetail]
ADD CONSTRAINT FK_OrderDetail_Order
    FOREIGN KEY ([OrderId])
    REFERENCES [Order]([Id]);
GO

ALTER TABLE [WorkerInOrder]
ADD CONSTRAINT FK_WorkerInOrder_HouseWorker
    FOREIGN KEY ([HouseWorkerId])
    REFERENCES [HouseWorker]([Id]);
GO

ALTER TABLE [WorkerInOrder]
ADD CONSTRAINT FK_WorkerInOrder_Order
    FOREIGN KEY ([OrderId])
    REFERENCES [Order]([Id]);
GO

-- ****** INSERT DATA ***** --
INSERT INTO [Role]
([Name])
VALUES
('Admin');
GO

INSERT INTO [Admin_Manager]
([Name], [Phone], [DateOfBirth], [Email], [LinkFacebook], [Avatar], [RoleId])
VALUES
(N'Trần Thành Đạt', '0793808821', '2001-06-25', 'datttse151444', 'https://www.facebook.com/thanhdat25062001', NULL, 1);
GO


INSERT INTO [Area]
([Name], [City], [District])
VALUES
(N'Vinhomes Grand Park', N'Hồ Chí Minh', N'Quận 9');
GO

INSERT INTO [Building]
([Name], [AreaId])
VALUES
('S1.06', 1);
GO

INSERT INTO [Customer]
([Name], [Phone], [DateOfBirth], [Email], [LinkFacebook], [Avatar])
VALUES
(N'Huỳnh Lê Thủy Tiên', '0849666957', '2001-10-29', 'tien.huynhlt.tn@gmail.com', 'https://www.facebook.com/tienhuynh.tn/', NULL);
GO

INSERT INTO [House]
([Number], [CustomerId], [BuildingId])
VALUES
('001', 1, 1);
GO

INSERT INTO [HouseWorker]
([Name], [Phone], [DateOfBirth], [Email], [LinkFacebook], [Avatar], [AreaId], [ManagerId])
VALUES
(N'Nguyễn Thị A', NULL, NULL, NULL, NULL, NULL, 1, 1);
GO

INSERT INTO [WorkerTag]
([Name], [HouseWorkerId])
VALUES
(N'Dọn dẹp nhà', 1);
GO

INSERT INTO [Service]
([Name], [Description], [Price])
VALUES
(N'Dọn dẹp nhà cơ bản', N'Dịch vụ giúp bạn dọn dẹp từng ngóc ngách trong căn nhà nhỏ của bạn', 100000);
GO

INSERT INTO [ExtraService]
([Name], [Description], [Price], [ServiceId])
VALUES
(N'Chăm sóc thú cưng', N'Dọn dẹp nhà cơ bản và chăm sóc thú cưng. Tại sao không?', 70000, 1);
GO

INSERT INTO [Package]
([NumberOfWorker], [Duration], [ServiceId])
VALUES
(1, 1, 1);
GO

INSERT INTO [Promotion]
([Code], [Description], [Value], [Discount], [Amount], [StartDate], [EndDate])
VALUES
('BIRTHDAY01', N'Giảm giá cực sốc nhân ngày sinh nhật của Cô Tấm', NULL, 0.5, 1000, GETDATE(), NULL);
GO

INSERT INTO [CustomerPromotion]
([CustomerId], [PromotionId])
VALUES
(1, 1);
GO

INSERT INTO [PaymentMethod]
([Name])
VALUES
(N'Tiền mặt');
GO

INSERT INTO [Order]
([SubTotal], [Total], [HouseId], [PackageId], [PromotionId], [PaymentMethodId], [OrderState])
VALUES
(100000, 100000, 1, 1, NULL, 1, 1);
GO

INSERT INTO [OrderDetail]
([ExtraServiceId], [OrderId])
VALUES
(NULL, 1);
GO

INSERT INTO [WorkerInOrder]
([HouseWorkerId], [OrderId], [Rating])
VALUES
(1, 1, 5);
GO

INSERT INTO [Information]
([Name], [Discription])
VALUES
(N'Giới thiệu ứng dụng', N'Đây là ứng dụng cho thuê người giúp việc');
GO