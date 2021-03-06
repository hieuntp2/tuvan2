USE [ProjectH]
GO
/****** Object:  Table [dbo].[Sach]    Script Date: 06/01/2015 19:04:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sach](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Ten] [nvarchar](max) NULL,
	[linkanh] [ntext] NULL,
	[NXB] [nvarchar](max) NULL,
	[tacgia] [nvarchar](max) NULL,
 CONSTRAINT [PK_Sach] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SachChoMuon]    Script Date: 06/01/2015 19:04:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SachChoMuon](
	[id_sach] [int] NOT NULL,
	[id_nguoidung] [nvarchar](128) NOT NULL,
	[loai] [bit] NULL,
	[diachi] [text] NULL,
	[comment] [nchar](10) NULL,
	[soluong] [int] NULL,
 CONSTRAINT [PK_SachChoMuon] PRIMARY KEY CLUSTERED 
(
	[id_sach] ASC,
	[id_nguoidung] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  ForeignKey [FK_SachChoMuon_AspNetUsers]    Script Date: 06/01/2015 19:04:50 ******/
ALTER TABLE [dbo].[SachChoMuon]  WITH CHECK ADD  CONSTRAINT [FK_SachChoMuon_AspNetUsers] FOREIGN KEY([id_nguoidung])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[SachChoMuon] CHECK CONSTRAINT [FK_SachChoMuon_AspNetUsers]
GO
/****** Object:  ForeignKey [FK_SachChoMuon_Sach]    Script Date: 06/01/2015 19:04:50 ******/
ALTER TABLE [dbo].[SachChoMuon]  WITH CHECK ADD  CONSTRAINT [FK_SachChoMuon_Sach] FOREIGN KEY([id_sach])
REFERENCES [dbo].[Sach] ([id])
GO
ALTER TABLE [dbo].[SachChoMuon] CHECK CONSTRAINT [FK_SachChoMuon_Sach]
GO


insert Sach values('Sach 1', NULL,'NXB 1', 'TAC GIA 1')
insert Sach values('Sach 2', NULL,'NXB 1', 'TAC GIA 1')
insert Sach values('Sach 3', NULL,'NXB 1', 'TAC GIA 1')
insert Sach values('Sach 4', NULL,'NXB 1', 'TAC GIA 1')
insert Sach values('Sach 5', NULL,'NXB 1', 'TAC GIA 1')
insert Sach values('Sach 6', NULL,'NXB 1', 'TAC GIA 1')
insert Sach values('Sach 7', NULL,'NXB 1', 'TAC GIA 1')
insert Sach values('Sach 8', NULL,'NXB 1', 'TAC GIA 1')
insert Sach values('Sach 9', NULL,'NXB 1', 'TAC GIA 1')
insert Sach values('Sach 10', NULL,'NXB 1', 'TAC GIA 1')
insert Sach values('Sach 11', NULL,'NXB 1', 'TAC GIA 1')
insert Sach values('Sach 12', NULL,'NXB 1', 'TAC GIA 1')

insert SachChoMuon values(1,'441c8d65-bcf2-40a0-b2a2-6db1bec62d1d', 1, 'dia chi 1', 'comment 1', 2)
insert SachChoMuon values(2,'441c8d65-bcf2-40a0-b2a2-6db1bec62d1d', 1, 'dia chi 1', 'comment 1', 2)
insert SachChoMuon values(3,'441c8d65-bcf2-40a0-b2a2-6db1bec62d1d', 1, 'dia chi 1', 'comment 1', 2)
insert SachChoMuon values(4,'441c8d65-bcf2-40a0-b2a2-6db1bec62d1d', 1, 'dia chi 1', 'comment 1', 2)
insert SachChoMuon values(5,'441c8d65-bcf2-40a0-b2a2-6db1bec62d1d', 1, 'dia chi 1', 'comment 1', 2)
insert SachChoMuon values(6,'441c8d65-bcf2-40a0-b2a2-6db1bec62d1d', 1, 'dia chi 1', 'comment 1', 2)
insert SachChoMuon values(7,'441c8d65-bcf2-40a0-b2a2-6db1bec62d1d', 1, 'dia chi 1', 'comment 1', 2)
insert SachChoMuon values(8,'441c8d65-bcf2-40a0-b2a2-6db1bec62d1d', 1, 'dia chi 1', 'comment 1', 2)

insert SachChoMuon values(9,'441c8d65-bcf2-40a0-b2a2-6db1bec62d1d', 0, 'dia chi 2', 'comment 1', 2)
insert SachChoMuon values(10,'441c8d65-bcf2-40a0-b2a2-6db1bec62d1d', 0, 'dia chi 2', 'comment 1', 2)
insert SachChoMuon values(11,'441c8d65-bcf2-40a0-b2a2-6db1bec62d1d', 0, 'dia chi 3', 'comment 1', 2)
insert SachChoMuon values(12,'441c8d65-bcf2-40a0-b2a2-6db1bec62d1d', 0, 'dia chi 4', 'comment 1', 2)
insert SachChoMuon values(13,'441c8d65-bcf2-40a0-b2a2-6db1bec62d1d', 0, 'dia chi 4', 'comment 1', 2)
insert SachChoMuon values(14,'441c8d65-bcf2-40a0-b2a2-6db1bec62d1d', 0, 'dia chi 5', 'comment 1', 2)
insert SachChoMuon values(15,'441c8d65-bcf2-40a0-b2a2-6db1bec62d1d', 0, 'dia chi 6', 'comment 1', 2)
insert SachChoMuon values(16,'441c8d65-bcf2-40a0-b2a2-6db1bec62d1d', 0, 'dia chi 7', 'comment 1', 2)