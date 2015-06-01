USE [ProjectH]
GO
/****** Object:  Table [dbo].[Sach]    Script Date: 6/1/2015 9:19:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sach](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[TenSach] [nvarchar](max) NULL,
	[Tacgia] [nvarchar](max) NULL,
	[NXB] [nvarchar](max) NULL,
	[hinhanh] [ntext] NULL,
 CONSTRAINT [PK_Sach] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SachChoMuon]    Script Date: 6/1/2015 9:19:44 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SachChoMuon](
	[id_sach] [int] NOT NULL,
	[id_nguoidung] [nvarchar](128) NOT NULL,
	[SoLuong] [int] NULL,
	[Loai] [bit] NULL,
	[Comment] [ntext] NULL,
 CONSTRAINT [PK_SachChoMuon] PRIMARY KEY CLUSTERED 
(
	[id_sach] ASC,
	[id_nguoidung] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[SachChoMuon]  WITH CHECK ADD  CONSTRAINT [FK_SachChoMuon_AspNetUsers] FOREIGN KEY([id_nguoidung])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[SachChoMuon] CHECK CONSTRAINT [FK_SachChoMuon_AspNetUsers]
GO
ALTER TABLE [dbo].[SachChoMuon]  WITH CHECK ADD  CONSTRAINT [FK_SachChoMuon_Sach] FOREIGN KEY([id_sach])
REFERENCES [dbo].[Sach] ([id])
GO
ALTER TABLE [dbo].[SachChoMuon] CHECK CONSTRAINT [FK_SachChoMuon_Sach]
GO
