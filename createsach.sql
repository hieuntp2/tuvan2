USE [ProjectH]
GO
/****** Object:  Table [dbo].[Sach]    Script Date: 05/29/2015 20:09:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sach](
	[id] [int] NOT NULL,
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
/****** Object:  Table [dbo].[SachChoMuon]    Script Date: 05/29/2015 20:09:02 ******/
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
/****** Object:  ForeignKey [FK_SachChoMuon_AspNetUsers]    Script Date: 05/29/2015 20:09:02 ******/
ALTER TABLE [dbo].[SachChoMuon]  WITH CHECK ADD  CONSTRAINT [FK_SachChoMuon_AspNetUsers] FOREIGN KEY([id_nguoidung])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[SachChoMuon] CHECK CONSTRAINT [FK_SachChoMuon_AspNetUsers]
GO
/****** Object:  ForeignKey [FK_SachChoMuon_Sach]    Script Date: 05/29/2015 20:09:02 ******/
ALTER TABLE [dbo].[SachChoMuon]  WITH CHECK ADD  CONSTRAINT [FK_SachChoMuon_Sach] FOREIGN KEY([id_sach])
REFERENCES [dbo].[Sach] ([id])
GO
ALTER TABLE [dbo].[SachChoMuon] CHECK CONSTRAINT [FK_SachChoMuon_Sach]
GO
