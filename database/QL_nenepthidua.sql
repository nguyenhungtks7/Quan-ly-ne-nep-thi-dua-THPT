USE [QL_nenepthidua]
GO
/****** Object:  Table [dbo].[DanhGia]    Script Date: 4/7/2024 8:09:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DanhGia](
	[IdDanhGia] [int] IDENTITY(1,1) NOT NULL,
	[IdLop] [int] NULL,
	[TongDiem] [decimal](10, 2) NULL,
	[XepLoai] [nvarchar](12) NULL,
	[Tuan] [nvarchar](10) NULL,
	[Thang] [nvarchar](10) NULL,
	[HocKy] [nvarchar](10) NULL,
	[NamHoc] [nvarchar](15) NULL,
PRIMARY KEY CLUSTERED 
(
	[IdDanhGia] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DiemHocTap]    Script Date: 4/7/2024 8:09:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DiemHocTap](
	[IdDiemht] [int] IDENTITY(1,1) NOT NULL,
	[IdLop] [int] NULL,
	[DiemHocTap] [decimal](10, 2) NULL,
	[NgayCapNhat] [date] NULL,
	[Tuan] [nvarchar](10) NULL,
	[Thang] [nvarchar](10) NULL,
	[HocKy] [nvarchar](10) NULL,
	[NamHoc] [nvarchar](15) NULL,
	[Diemgiotot] [decimal](10, 2) NULL,
	[Diemgiotb] [decimal](10, 2) NULL,
	[Diemgioyeu] [decimal](10, 2) NULL,
	[Diemgiokem] [decimal](10, 2) NULL,
	[Diemtotsdb] [decimal](10, 2) NULL,
	[Diemkemsdb] [decimal](10, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[IdDiemht] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DiemNeNep]    Script Date: 4/7/2024 8:09:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DiemNeNep](
	[IdDiemnn] [int] IDENTITY(1,1) NOT NULL,
	[IdLop] [int] NULL,
	[DiemNeNep] [decimal](10, 2) NULL,
	[NgayCapNhat] [date] NULL,
	[Tuan] [nvarchar](10) NULL,
	[Thang] [nvarchar](10) NULL,
	[HocKy] [nvarchar](10) NULL,
	[NamHoc] [nvarchar](15) NULL,
PRIMARY KEY CLUSTERED 
(
	[IdDiemnn] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GVCN]    Script Date: 4/7/2024 8:09:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GVCN](
	[IdGVCN] [int] IDENTITY(1,1) NOT NULL,
	[HoTenGVCN] [nvarchar](50) NULL,
	[NgaySinh] [date] NULL,
	[SoDienThoai] [nvarchar](11) NULL,
	[Email] [nvarchar](50) NULL,
	[GioiTinh] [nvarchar](5) NULL,
PRIMARY KEY CLUSTERED 
(
	[IdGVCN] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HocSinh]    Script Date: 4/7/2024 8:09:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HocSinh](
	[IdHocSinh] [int] IDENTITY(1,1) NOT NULL,
	[HoTenHocSinh] [nvarchar](50) NULL,
	[NgaySinh] [date] NULL,
	[GioiTinh] [nvarchar](5) NULL,
	[DiaChi] [nvarchar](60) NULL,
	[Email] [nvarchar](50) NULL,
	[IdLop] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdHocSinh] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Lop]    Script Date: 4/7/2024 8:09:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Lop](
	[IdLop] [int] IDENTITY(1,1) NOT NULL,
	[TenLop] [nvarchar](6) NULL,
	[NamHoc] [nvarchar](15) NULL,
	[IdGVCN] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdLop] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TaiKhoan]    Script Date: 4/7/2024 8:09:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TaiKhoan](
	[IdTaiKhoan] [int] IDENTITY(1,1) NOT NULL,
	[TenDangNhap] [nvarchar](50) NULL,
	[MatKhau] [nvarchar](max) NULL,
	[QuyenTruyCap] [int] NULL,
	[TrangThai] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdTaiKhoan] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ViPham]    Script Date: 4/7/2024 8:09:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ViPham](
	[IdViPham] [int] IDENTITY(1,1) NOT NULL,
	[IdHocSinh] [int] NULL,
	[LoaiViPham] [nvarchar](50) NULL,
	[MoTaViPham] [nvarchar](max) NULL,
	[NgayViPham] [date] NULL,
	[TenTuan] [nvarchar](10) NULL,
	[DiemTru] [decimal](10, 2) NULL,
	[Thang] [nvarchar](10) NULL,
	[HocKy] [nvarchar](10) NULL,
	[NamHoc] [nvarchar](15) NULL,
PRIMARY KEY CLUSTERED 
(
	[IdViPham] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[DanhGia]  WITH CHECK ADD  CONSTRAINT [FK__DanhGia__IdLop__59063A47] FOREIGN KEY([IdLop])
REFERENCES [dbo].[Lop] ([IdLop])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[DanhGia] CHECK CONSTRAINT [FK__DanhGia__IdLop__59063A47]
GO
ALTER TABLE [dbo].[DiemHocTap]  WITH CHECK ADD  CONSTRAINT [FK__DiemHocTa__IdLop__5BE2A6F2] FOREIGN KEY([IdLop])
REFERENCES [dbo].[Lop] ([IdLop])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[DiemHocTap] CHECK CONSTRAINT [FK__DiemHocTa__IdLop__5BE2A6F2]
GO
ALTER TABLE [dbo].[DiemNeNep]  WITH CHECK ADD  CONSTRAINT [FK__DiemNeNep__IdLop__5EBF139D] FOREIGN KEY([IdLop])
REFERENCES [dbo].[Lop] ([IdLop])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[DiemNeNep] CHECK CONSTRAINT [FK__DiemNeNep__IdLop__5EBF139D]
GO
ALTER TABLE [dbo].[HocSinh]  WITH CHECK ADD  CONSTRAINT [FK__HocSinh__IdLop__5629CD9C] FOREIGN KEY([IdLop])
REFERENCES [dbo].[Lop] ([IdLop])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[HocSinh] CHECK CONSTRAINT [FK__HocSinh__IdLop__5629CD9C]
GO
ALTER TABLE [dbo].[Lop]  WITH CHECK ADD  CONSTRAINT [FK__Lop__IdGVCN__534D60F1] FOREIGN KEY([IdGVCN])
REFERENCES [dbo].[GVCN] ([IdGVCN])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Lop] CHECK CONSTRAINT [FK__Lop__IdGVCN__534D60F1]
GO
ALTER TABLE [dbo].[ViPham]  WITH CHECK ADD  CONSTRAINT [FK__ViPham__IdHocSin__6383C8BA] FOREIGN KEY([IdHocSinh])
REFERENCES [dbo].[HocSinh] ([IdHocSinh])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ViPham] CHECK CONSTRAINT [FK__ViPham__IdHocSin__6383C8BA]
GO
