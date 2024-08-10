USE [Hungdongho]
GO
/****** Object:  Table [dbo].[ChiTietDonHang]    Script Date: 5/9/2024 3:31:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChiTietDonHang](
	[MaDonHang] [int] NOT NULL,
	[MaSP] [int] NOT NULL,
	[SoLuong] [int] NULL,
	[GiaSP] [decimal](10, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[MaDonHang] ASC,
	[MaSP] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DanhMuc]    Script Date: 5/9/2024 3:31:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DanhMuc](
	[MaDanhMuc] [int] IDENTITY(1,1) NOT NULL,
	[TenDanhMuc] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[MaDanhMuc] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DonHang]    Script Date: 5/9/2024 3:31:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DonHang](
	[MaDonHang] [int] IDENTITY(1,1) NOT NULL,
	[NgayDatHang] [date] NULL,
	[TongTien] [decimal](10, 2) NULL,
	[MaUser] [int] NULL,
	[HoTenDem] [nvarchar](20) NULL,
	[Ten] [nvarchar](20) NULL,
	[Email] [nchar](30) NULL,
	[SoDienThoai] [nvarchar](11) NULL,
PRIMARY KEY CLUSTERED 
(
	[MaDonHang] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LienHe]    Script Date: 5/9/2024 3:31:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LienHe](
	[MaLienHe] [int] IDENTITY(1,1) NOT NULL,
	[HoTen] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NULL,
	[NoiDung] [nvarchar](max) NULL,
	[NgayLienHe] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[MaLienHe] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PhanHoi]    Script Date: 5/9/2024 3:31:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PhanHoi](
	[MaPhanHoi] [int] IDENTITY(1,1) NOT NULL,
	[MaUser] [int] NULL,
	[NoiDung] [nvarchar](max) NULL,
	[NgayPhanHoi] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[MaPhanHoi] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SanPham]    Script Date: 5/9/2024 3:31:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SanPham](
	[MaSP] [int] IDENTITY(1,1) NOT NULL,
	[TenSP] [nvarchar](50) NULL,
	[NhanHieu] [nvarchar](50) NULL,
	[MoTa] [nvarchar](max) NULL,
	[Gia] [decimal](10, 2) NULL,
	[SoLuongKho] [int] NULL,
	[DuongDanHinh] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[MaSP] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SanPham_DanhMuc]    Script Date: 5/9/2024 3:31:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SanPham_DanhMuc](
	[MaSP] [int] NOT NULL,
	[MaDanhMuc] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[MaSP] ASC,
	[MaDanhMuc] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 5/9/2024 3:31:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[MaUser] [int] IDENTITY(1,1) NOT NULL,
	[HoTenDem] [nvarchar](20) NULL,
	[Ten] [nvarchar](20) NULL,
	[TenDangNhap] [nvarchar](50) NULL,
	[MatKhau] [nvarchar](255) NULL,
	[Email] [nvarchar](30) NULL,
	[SoDienThoai] [nvarchar](11) NULL,
	[mod] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[MaUser] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[ChiTietDonHang] ([MaDonHang], [MaSP], [SoLuong], [GiaSP]) VALUES (5, 18, 2, CAST(10000.00 AS Decimal(10, 2)))
INSERT [dbo].[ChiTietDonHang] ([MaDonHang], [MaSP], [SoLuong], [GiaSP]) VALUES (5, 34, 2, CAST(91515151.00 AS Decimal(10, 2)))
INSERT [dbo].[ChiTietDonHang] ([MaDonHang], [MaSP], [SoLuong], [GiaSP]) VALUES (5, 41, 1, CAST(58695000.00 AS Decimal(10, 2)))
INSERT [dbo].[ChiTietDonHang] ([MaDonHang], [MaSP], [SoLuong], [GiaSP]) VALUES (6, 33, 3, CAST(79000000.00 AS Decimal(10, 2)))
INSERT [dbo].[ChiTietDonHang] ([MaDonHang], [MaSP], [SoLuong], [GiaSP]) VALUES (7, 41, 3, CAST(58695000.00 AS Decimal(10, 2)))
INSERT [dbo].[ChiTietDonHang] ([MaDonHang], [MaSP], [SoLuong], [GiaSP]) VALUES (10, 22, 1, CAST(35490000.00 AS Decimal(10, 2)))
INSERT [dbo].[ChiTietDonHang] ([MaDonHang], [MaSP], [SoLuong], [GiaSP]) VALUES (10, 33, 2, CAST(79000000.00 AS Decimal(10, 2)))
INSERT [dbo].[ChiTietDonHang] ([MaDonHang], [MaSP], [SoLuong], [GiaSP]) VALUES (13, 39, 2, CAST(9100000.00 AS Decimal(10, 2)))
INSERT [dbo].[ChiTietDonHang] ([MaDonHang], [MaSP], [SoLuong], [GiaSP]) VALUES (14, 34, 3, CAST(91515151.00 AS Decimal(10, 2)))
INSERT [dbo].[ChiTietDonHang] ([MaDonHang], [MaSP], [SoLuong], [GiaSP]) VALUES (15, 33, 3, CAST(79000000.00 AS Decimal(10, 2)))
INSERT [dbo].[ChiTietDonHang] ([MaDonHang], [MaSP], [SoLuong], [GiaSP]) VALUES (15, 41, 1, CAST(58695000.00 AS Decimal(10, 2)))
INSERT [dbo].[ChiTietDonHang] ([MaDonHang], [MaSP], [SoLuong], [GiaSP]) VALUES (16, 18, 2, CAST(53187000.00 AS Decimal(10, 2)))
INSERT [dbo].[ChiTietDonHang] ([MaDonHang], [MaSP], [SoLuong], [GiaSP]) VALUES (16, 56, 3, CAST(53188000.00 AS Decimal(10, 2)))
INSERT [dbo].[ChiTietDonHang] ([MaDonHang], [MaSP], [SoLuong], [GiaSP]) VALUES (17, 41, 1, CAST(58695000.00 AS Decimal(10, 2)))
INSERT [dbo].[ChiTietDonHang] ([MaDonHang], [MaSP], [SoLuong], [GiaSP]) VALUES (17, 44, 1, CAST(81937500.00 AS Decimal(10, 2)))
GO
SET IDENTITY_INSERT [dbo].[DanhMuc] ON 

INSERT [dbo].[DanhMuc] ([MaDanhMuc], [TenDanhMuc]) VALUES (1, N'Đồng Hồ Nam')
INSERT [dbo].[DanhMuc] ([MaDanhMuc], [TenDanhMuc]) VALUES (2, N'Đồng Hồ Nữ')
INSERT [dbo].[DanhMuc] ([MaDanhMuc], [TenDanhMuc]) VALUES (3, N'Đồng Hồ Cơ')
INSERT [dbo].[DanhMuc] ([MaDanhMuc], [TenDanhMuc]) VALUES (4, N'Đồng Hồ Pin')
SET IDENTITY_INSERT [dbo].[DanhMuc] OFF
GO
SET IDENTITY_INSERT [dbo].[DonHang] ON 

INSERT [dbo].[DonHang] ([MaDonHang], [NgayDatHang], [TongTien], [MaUser], [HoTenDem], [Ten], [Email], [SoDienThoai]) VALUES (5, CAST(N'2023-12-07' AS Date), CAST(241725.30 AS Decimal(10, 2)), 2, N'Nguyễn Phi ', N'Hùng', N'nguyenhung@gmail.com          ', N'0945968013')
INSERT [dbo].[DonHang] ([MaDonHang], [NgayDatHang], [TongTien], [MaUser], [HoTenDem], [Ten], [Email], [SoDienThoai]) VALUES (6, CAST(N'2023-12-08' AS Date), CAST(237000.00 AS Decimal(10, 2)), 2, N'Nhật', N'Tân', N'tan@gmail.com                 ', N'051515')
INSERT [dbo].[DonHang] ([MaDonHang], [NgayDatHang], [TongTien], [MaUser], [HoTenDem], [Ten], [Email], [SoDienThoai]) VALUES (7, CAST(N'2023-12-09' AS Date), CAST(258022.50 AS Decimal(10, 2)), 10, N'Nguyễn Phi', N'Hùng', N'hung@gmail.com                ', N'06262626')
INSERT [dbo].[DonHang] ([MaDonHang], [NgayDatHang], [TongTien], [MaUser], [HoTenDem], [Ten], [Email], [SoDienThoai]) VALUES (10, CAST(N'2023-12-09' AS Date), CAST(193490.00 AS Decimal(10, 2)), 10, N'Nguyễn Phi', N'Hùng', N'csadv@gmail.com               ', N'09454545')
INSERT [dbo].[DonHang] ([MaDonHang], [NgayDatHang], [TongTien], [MaUser], [HoTenDem], [Ten], [Email], [SoDienThoai]) VALUES (13, CAST(N'2024-01-22' AS Date), CAST(264012.50 AS Decimal(10, 2)), 2, N'Nguyễn Phi', N'Hùng', N'nguyenhung@gmail.com          ', N'06161814')
INSERT [dbo].[DonHang] ([MaDonHang], [NgayDatHang], [TongTien], [MaUser], [HoTenDem], [Ten], [Email], [SoDienThoai]) VALUES (14, CAST(N'2024-01-24' AS Date), CAST(274545.45 AS Decimal(10, 2)), 2, N'Nguyễn Phi', N'Hùng', N'thang@gmail.com               ', N'0115151')
INSERT [dbo].[DonHang] ([MaDonHang], [NgayDatHang], [TongTien], [MaUser], [HoTenDem], [Ten], [Email], [SoDienThoai]) VALUES (15, CAST(N'2024-01-24' AS Date), CAST(295695.00 AS Decimal(10, 2)), 2, N'Nguyễn Phi', N'Hùng', N'nguyenhung@gmail.com          ', N'06161814')
INSERT [dbo].[DonHang] ([MaDonHang], [NgayDatHang], [TongTien], [MaUser], [HoTenDem], [Ten], [Email], [SoDienThoai]) VALUES (16, CAST(N'2024-03-26' AS Date), CAST(265938.00 AS Decimal(10, 2)), 9, N'Nguyễn Phi ', N'Hùng', N'nguyehungtks1@gmail.com       ', N'0945968013')
INSERT [dbo].[DonHang] ([MaDonHang], [NgayDatHang], [TongTien], [MaUser], [HoTenDem], [Ten], [Email], [SoDienThoai]) VALUES (17, CAST(N'2024-04-01' AS Date), CAST(140632.50 AS Decimal(10, 2)), 2, N'Phi', N'Hung', N'nguyenhungtks1@gmail.com      ', N'0945968013')
SET IDENTITY_INSERT [dbo].[DonHang] OFF
GO
SET IDENTITY_INSERT [dbo].[LienHe] ON 

INSERT [dbo].[LienHe] ([MaLienHe], [HoTen], [Email], [NoiDung], [NgayLienHe]) VALUES (1, N'Nguyễn phi Hùng', N'hung@gmail.com', N'Tôi rất thích', CAST(N'2020-02-02T00:00:00.000' AS DateTime))
INSERT [dbo].[LienHe] ([MaLienHe], [HoTen], [Email], [NoiDung], [NgayLienHe]) VALUES (2, N'Nguyễn phi Hùng', N'hung@gmail.com', N'Tôi rất thích', CAST(N'2024-01-23T00:00:00.000' AS DateTime))
INSERT [dbo].[LienHe] ([MaLienHe], [HoTen], [Email], [NoiDung], [NgayLienHe]) VALUES (3, N'Đàm Kiều Trang', N'Trang@gmail.com', N'okoo', CAST(N'2024-01-23T00:00:00.000' AS DateTime))
INSERT [dbo].[LienHe] ([MaLienHe], [HoTen], [Email], [NoiDung], [NgayLienHe]) VALUES (4, N'Phạm Anh Dũng', N'dung@gmail.com', N'rất ok', CAST(N'2024-01-23T00:00:00.000' AS DateTime))
INSERT [dbo].[LienHe] ([MaLienHe], [HoTen], [Email], [NoiDung], [NgayLienHe]) VALUES (5, N'Đoàn Minh Thắng', N'thang@gmail.com', N'kkkk', CAST(N'2024-01-23T00:00:00.000' AS DateTime))
INSERT [dbo].[LienHe] ([MaLienHe], [HoTen], [Email], [NoiDung], [NgayLienHe]) VALUES (6, N'Nguyễn phi Hùng', N'hung@gmail.com', N'ckasbcavsasuv', CAST(N'2024-01-23T00:00:00.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[LienHe] OFF
GO
SET IDENTITY_INSERT [dbo].[PhanHoi] ON 

INSERT [dbo].[PhanHoi] ([MaPhanHoi], [MaUser], [NoiDung], [NgayPhanHoi]) VALUES (1, 1, N'cá', CAST(N'2024-01-23T17:22:42.740' AS DateTime))
INSERT [dbo].[PhanHoi] ([MaPhanHoi], [MaUser], [NoiDung], [NgayPhanHoi]) VALUES (2, 1, N'ngon ngay', CAST(N'2024-01-23T17:27:45.513' AS DateTime))
INSERT [dbo].[PhanHoi] ([MaPhanHoi], [MaUser], [NoiDung], [NgayPhanHoi]) VALUES (3, 15, N'đồng hồ quá đẹp', CAST(N'2024-01-23T17:48:17.500' AS DateTime))
SET IDENTITY_INSERT [dbo].[PhanHoi] OFF
GO
SET IDENTITY_INSERT [dbo].[SanPham] ON 

INSERT [dbo].[SanPham] ([MaSP], [TenSP], [NhanHieu], [MoTa], [Gia], [SoLuongKho], [DuongDanHinh]) VALUES (1, N'Đồng hồ Longines L3.782.3.06.9', N'Longines', N'| Nam | 43.00 x 43.00mm | 11.90mm | Thép không gỉ 316L/ Mạ vàng công nghệ PVD/ Ceramic | Sapphire/ Chống trầy xước 2 lớp phản quang | 30 Bar (300m)', CAST(5300000.00 AS Decimal(10, 2)), 100, N'dongho1.png')
INSERT [dbo].[SanPham] ([MaSP], [TenSP], [NhanHieu], [MoTa], [Gia], [SoLuongKho], [DuongDanHinh]) VALUES (15, N'Đồng hồ Longines L2.755.8.78.5', N'Longines', N'Automatic/ Máy tự động lên dây cót | Nam | 38.50 x 38.50mm | 9.50mm | Vàng nguyên khối 18K | Sapphire/ Chống trầy xước | 3 bar', CAST(19406200.00 AS Decimal(10, 2)), 50, N'dongho2.png')
INSERT [dbo].[SanPham] ([MaSP], [TenSP], [NhanHieu], [MoTa], [Gia], [SoLuongKho], [DuongDanHinh]) VALUES (18, N'Đồng hồ Longines L3.830.4.52.6', N'Longines', N'| Nam | 41.00 x 41.00mm | 10.90mm | Thép không gỉ 316L | Sapphire/ Chống trầy xước | 3 bar (30m)', CAST(53187000.00 AS Decimal(10, 2)), 15, N'donghonamloai1s3.png')
INSERT [dbo].[SanPham] ([MaSP], [TenSP], [NhanHieu], [MoTa], [Gia], [SoLuongKho], [DuongDanHinh]) VALUES (19, N'Đồng hồ Longines L3.830.4.62.6', N'Longines', N'| Nam | 41.00 x 41.00mm | 10.90mm | Thép không gỉ 316L | Sapphire/ Chống trầy xước | 3 bar (30m)

', CAST(53187000.00 AS Decimal(10, 2)), 12, N'donghonamloai1s4.png')
INSERT [dbo].[SanPham] ([MaSP], [TenSP], [NhanHieu], [MoTa], [Gia], [SoLuongKho], [DuongDanHinh]) VALUES (20, N'Đồng hồ Maurice Lacroix AI1018-SS001-330-1', N'Maurice Lacroix', N'Quartz/ Pin | Nam | 44.00 x 44.00mm | 11.00mm | Thép không gỉ 316L | Sapphire/ Chống trầy xước | 10 bar (100m)', CAST(35490000.00 AS Decimal(10, 2)), 11, N'donghonamloai2s1.jpg')
INSERT [dbo].[SanPham] ([MaSP], [TenSP], [NhanHieu], [MoTa], [Gia], [SoLuongKho], [DuongDanHinh]) VALUES (21, N'Đồng hồ Maurice Lacroix AI1018-SS001-330-2', N'Maurice Lacroix', N'Quartz/ Pin | Nam | 44.00 x 44.00mm | 11.00mm | Thép không gỉ 316L | Sapphire/ Chống trầy xước | 10 bar (100m)', CAST(34125000.00 AS Decimal(10, 2)), 40, N'donghonamloai2s2.jpg')
INSERT [dbo].[SanPham] ([MaSP], [TenSP], [NhanHieu], [MoTa], [Gia], [SoLuongKho], [DuongDanHinh]) VALUES (22, N'Đồng hồ Maurice Lacroix AI1018-SS001-333-1', N'Maurice Lacroix', N'Quartz/ Pin | Nam | 44.00 x 44.00mm | 11.00mm | Thép không gỉ 316L | Sapphire/ Chống trầy xước | 10 bar (100m)', CAST(35490000.00 AS Decimal(10, 2)), 54, N'donghonamloai2s3.jpg')
INSERT [dbo].[SanPham] ([MaSP], [TenSP], [NhanHieu], [MoTa], [Gia], [SoLuongKho], [DuongDanHinh]) VALUES (23, N'Đồng hồ Maurice Lacroix AI1018-SS001-430-1', N'Maurice Lacroix', N'Quartz/ Pin | Nam | 44.00 x 44.00mm | 11.00mm | Thép không gỉ 316L | Sapphire/ Chống trầy xước | 10 bar (100m)', CAST(35490000.00 AS Decimal(10, 2)), 12, N'donghonamloai2s4.jpg')
INSERT [dbo].[SanPham] ([MaSP], [TenSP], [NhanHieu], [MoTa], [Gia], [SoLuongKho], [DuongDanHinh]) VALUES (24, N'Đồng hồ Gucci YA126232', N'gucci', N'| Nam | 44.00 x 44.00mm | 10.00mm | Thép không gỉ 316L | Sapphire/ Chống trầy xước | 3 bar (30m)', CAST(27060000.00 AS Decimal(10, 2)), 56, N'donghonamloai3s1.jpg')
INSERT [dbo].[SanPham] ([MaSP], [TenSP], [NhanHieu], [MoTa], [Gia], [SoLuongKho], [DuongDanHinh]) VALUES (25, N'Đồng hồ Gucci YA131301', N'gucci', N'| Nam | 44.00 x 44.00 mm | 6.00mm | Thép không gỉ 316L | Sapphire/ Chống trầy xước | 3 bar (30m)', CAST(27060000.00 AS Decimal(10, 2)), 15, N'donghonamloai3s2.jpg')
INSERT [dbo].[SanPham] ([MaSP], [TenSP], [NhanHieu], [MoTa], [Gia], [SoLuongKho], [DuongDanHinh]) VALUES (26, N'Đồng hồ Gucci YA131201', N'gucci', N'| Nam | 44.00 x 44.00 mm | 10.60 mm | Thép không gỉ 316L | Sapphire/ Chống trầy xước | 5 bar (50m)', CAST(32120000.00 AS Decimal(10, 2)), 19, N'donghonamloai3s3.jpg')
INSERT [dbo].[SanPham] ([MaSP], [TenSP], [NhanHieu], [MoTa], [Gia], [SoLuongKho], [DuongDanHinh]) VALUES (27, N'Đồng hồ Gucci YA136209A', N'gucci', N'| Nam | 45.00 x 45.00mm | 12.00mm | Thép không gỉ 316L | Sapphire/ Chống trầy xước | 5 bar (50m)', CAST(34320000.00 AS Decimal(10, 2)), 20, N'donghonamloai3s4.jpg')
INSERT [dbo].[SanPham] ([MaSP], [TenSP], [NhanHieu], [MoTa], [Gia], [SoLuongKho], [DuongDanHinh]) VALUES (28, N'Đồng hồ Tissot T006.407.11.033.03', N'tissot', N'| Nam | 39.30 x 39.30mm | Thép không gỉ 316L | Sapphire/ Chống trầy xước | 3 bar (30m)', CAST(20220000.00 AS Decimal(10, 2)), 17, N'donghonamloai4s1.png')
INSERT [dbo].[SanPham] ([MaSP], [TenSP], [NhanHieu], [MoTa], [Gia], [SoLuongKho], [DuongDanHinh]) VALUES (29, N'Đồng hồ Tissot T127.407.11.081.00', N'tissot', N'| Nam | 40.00 x 40.00mm | 11.50mm | Thép không gỉ 316L | Sapphire/ Chống trầy xước | 10 bar (100m)', CAST(26000000.00 AS Decimal(10, 2)), 15, N'donghonamloai4s2.png')
INSERT [dbo].[SanPham] ([MaSP], [TenSP], [NhanHieu], [MoTa], [Gia], [SoLuongKho], [DuongDanHinh]) VALUES (30, N'Đồng hồ Tissot T143.410.33.021.0', N'tissot', N'| Nam | 40.00 x 40.00mm | 7.10mm | Thép không gỉ 316L/ Mạ vàng công nghệ PVD | Sapphire/ Chống trầy xước | 5 bar (50m)', CAST(9100000.00 AS Decimal(10, 2)), 60, N'donghonamloai4s3.png')
INSERT [dbo].[SanPham] ([MaSP], [TenSP], [NhanHieu], [MoTa], [Gia], [SoLuongKho], [DuongDanHinh]) VALUES (31, N'Đồng hồ Tissot T139.807.36.031.00', N'tissot', N'| Nam | 39.00 x 39.00mm | 11.20mm | Thép không gỉ 316L/ Mạ vàng công nghệ PVD | Sapphire/ Chống trầy xước | 5 bar (50m)', CAST(24850000.00 AS Decimal(10, 2)), 25, N'donghonamloai4s4.png')
INSERT [dbo].[SanPham] ([MaSP], [TenSP], [NhanHieu], [MoTa], [Gia], [SoLuongKho], [DuongDanHinh]) VALUES (32, N'Đồng hồ Longines L5.200.4.75.2', N'Longines', N'| Nữ | 21.50 x 29.00mm | 6.75mm | Thép không gỉ 316L | Sapphire/ Chống trầy xước | 3 bar (30m)', CAST(5000000.00 AS Decimal(10, 2)), 50, N'donghonuloai1s1.png')
INSERT [dbo].[SanPham] ([MaSP], [TenSP], [NhanHieu], [MoTa], [Gia], [SoLuongKho], [DuongDanHinh]) VALUES (33, N'Đồng hồ Longines L5.200.4.75.6', N'Longines', N'| Nữ | 21.50 x 29.00mm | 6.75mm | Thép không gỉ 316L | Sapphire/ Chống trầy xước | 3 bar (30m)', CAST(79000000.00 AS Decimal(10, 2)), 56, N'donghonuloai1s2.png')
INSERT [dbo].[SanPham] ([MaSP], [TenSP], [NhanHieu], [MoTa], [Gia], [SoLuongKho], [DuongDanHinh]) VALUES (34, N'Đồng hồ Longines L5.200.4.71.5', N'Longines', N'| Nữ | 21.50 x 29.00mm | 6.75mm | Thép không gỉ 316L | Sapphire/ Chống trầy xước | 3 bar (30m)', CAST(91515151.00 AS Decimal(10, 2)), 77, N'donghonuloai1s3.png')
INSERT [dbo].[SanPham] ([MaSP], [TenSP], [NhanHieu], [MoTa], [Gia], [SoLuongKho], [DuongDanHinh]) VALUES (35, N'Đồng hồ Longines L5.200.4.71.6', N'Longines', N'| Nữ | 21.50 x 29.00mm | 6.75mm | Thép không gỉ 316L | Sapphire/ Chống trầy xước | 3 bar (30m)

', CAST(15000000.00 AS Decimal(10, 2)), 22, N'donghonuloai1s4.png')
INSERT [dbo].[SanPham] ([MaSP], [TenSP], [NhanHieu], [MoTa], [Gia], [SoLuongKho], [DuongDanHinh]) VALUES (37, N'Đồng hồ Maurice Lacroix MP6068-SS001-160-1', N'Maurice Lacroix', N'Automatic/ Máy tự động lên dây cót | Nữ | 40.00 x 40.00mm | 14.00mm | Thép không gỉ 316L | Sapphire/ Chống trầy xước | 5 bar (50m)', CAST(20611500.00 AS Decimal(10, 2)), 65, N'donghonuloai2s1.png')
INSERT [dbo].[SanPham] ([MaSP], [TenSP], [NhanHieu], [MoTa], [Gia], [SoLuongKho], [DuongDanHinh]) VALUES (38, N'Đồng hồ Gucci YA143403', N'gucci', N'| Nữ | 35.00 x 35.00 mm | 8.00 mm | Thép không gỉ 316L/ Mạ vàng công nghệ PVD | Sapphire/ Chống trầy xước | 3 bar (30m)', CAST(22220000.00 AS Decimal(10, 2)), 15, N'donghonuloai3s1.png')
INSERT [dbo].[SanPham] ([MaSP], [TenSP], [NhanHieu], [MoTa], [Gia], [SoLuongKho], [DuongDanHinh]) VALUES (39, N'Đồng hồ Tissot T143.210.11.041.00', N'tissot', N'| Nữ | 34.00 x 34.00mm | 6.90mm | Thép không gỉ 316L | Sapphire/ Chống trầy xước | 5 bar (50m)', CAST(9100000.00 AS Decimal(10, 2)), 12, N'donghonuloai4s1.jpg')
INSERT [dbo].[SanPham] ([MaSP], [TenSP], [NhanHieu], [MoTa], [Gia], [SoLuongKho], [DuongDanHinh]) VALUES (40, N'Đồng hồ Longines L2.357.4.07.2', N'Longines', N'Automatic/ Máy tự động lên dây cót | Nữ | 34.00 x 34.00mm | 9.20mm | Thép không gỉ 316L | Sapphire/ Chống trầy xước | 3 bar (30m)', CAST(67562000.00 AS Decimal(10, 2)), 14, N'donghocoloai1s1.jpg')
INSERT [dbo].[SanPham] ([MaSP], [TenSP], [NhanHieu], [MoTa], [Gia], [SoLuongKho], [DuongDanHinh]) VALUES (41, N'Đồng hồ Maurice Lacroix AI6006-PVY11-170-1', N'Maurice Lacroix', N'Automatic/ Máy tự động lên dây cót | Nữ | 35.00 x 35.00mm | 11.00mm | Thép không gỉ 316L/ Mạ vàng công nghệ PVD | Sapphire/ Chống trầy xước | 20 bar (200m)', CAST(58695000.00 AS Decimal(10, 2)), 7, N'donghocoloai2s1.jpg')
INSERT [dbo].[SanPham] ([MaSP], [TenSP], [NhanHieu], [MoTa], [Gia], [SoLuongKho], [DuongDanHinh]) VALUES (42, N'Đồng hồ Gucci YA136202', N'gucci', N'Automatic/ Máy tự động lên dây cót | Nam | 40.00mm x 40.00mm | 12.00mm | Thép không gỉ 316L | Sapphire/ Chống trầy xước | 5 bar (50m)', CAST(32164000.00 AS Decimal(10, 2)), 15, N'donghocoloai3s1.jpg')
INSERT [dbo].[SanPham] ([MaSP], [TenSP], [NhanHieu], [MoTa], [Gia], [SoLuongKho], [DuongDanHinh]) VALUES (43, N'Đồng hồ Gucci YA131307', N'tissot', N'Automatic/ Máy tự động lên dây cót | Nam | 40.00mm x 40.00mm | 12.00mm | Thép không gỉ 316L | Sapphire/ Chống trầy xước | 5 bar (50m)', CAST(67100000.00 AS Decimal(10, 2)), 15, N'donghocoloai3s4.jpg')
INSERT [dbo].[SanPham] ([MaSP], [TenSP], [NhanHieu], [MoTa], [Gia], [SoLuongKho], [DuongDanHinh]) VALUES (44, N'Đồng hồ Longines L5.200.0.05.2', N'Longiness', N'Quartz/ Pin | Nữ | 21.50 x 29.00mm | 6.75mm | Thép không gỉ 316L/ Đính kim cương | Sapphire/ Chống trầy xước 2 lớp phản quang | 3 bar (30m)', CAST(81937500.00 AS Decimal(10, 2)), 7, N'donghopinloai1s1.jpg')
INSERT [dbo].[SanPham] ([MaSP], [TenSP], [NhanHieu], [MoTa], [Gia], [SoLuongKho], [DuongDanHinh]) VALUES (45, N'Đồng hồ Maurice Lacroix AI1018-SS001-330-1', N'Maurice Lacroix', N'Quartz/ Pin | Nam | 44.00 x 44.00mm | 11.00mm | Thép không gỉ 316L | Sapphire/ Chống trầy xước | 10 bar (100m)', CAST(35490000.00 AS Decimal(10, 2)), 25, N'donghopinloai2s1.jpg')
INSERT [dbo].[SanPham] ([MaSP], [TenSP], [NhanHieu], [MoTa], [Gia], [SoLuongKho], [DuongDanHinh]) VALUES (46, N'Đồng hồ Gucci YA131305', N'gucci', N'Quartz/ Pin | Nam | 40.00mm x 40.00mm | 12.00mm | Thép không gỉ 316L | Sapphire/ Chống trầy xước | 3 bar (30m)', CAST(27060000.00 AS Decimal(10, 2)), 55, N'donghopinloai3s1.jpg')
INSERT [dbo].[SanPham] ([MaSP], [TenSP], [NhanHieu], [MoTa], [Gia], [SoLuongKho], [DuongDanHinh]) VALUES (50, N'Đồng hồ đôi Tissot T129.410.22.013', N'tissot', N'Quartz/ Pin | Đồng hồ đôi | 42.00mm x 28.00mm | 7.10mm | Thép không gỉ 316L/ Mạ vàng công nghệ PVD | Sapphire/ Chống trầy xước | 5 bar (50m)', CAST(203000.00 AS Decimal(10, 2)), 22, N'donghopinloai4s4.jpg')
INSERT [dbo].[SanPham] ([MaSP], [TenSP], [NhanHieu], [MoTa], [Gia], [SoLuongKho], [DuongDanHinh]) VALUES (54, N'Đồng hồ Longines L5.200.4.71.5', N'Longiness', N'| Nữ | 43.00 x 43.00mm | 11.90mm | Thép không gỉ 316L/ Mạ vàng công nghệ PVD/ Ceramic | Sapphire/ Chống trầy xước 2 lớp phản quang | 30 Bar (300m)', CAST(3000000.00 AS Decimal(10, 2)), 10, N'donghonuloai1s4.png')
INSERT [dbo].[SanPham] ([MaSP], [TenSP], [NhanHieu], [MoTa], [Gia], [SoLuongKho], [DuongDanHinh]) VALUES (55, N'Đồng hồ Longines L3.782.3.06', N'Maurice Lacroix', N'| Nữ | 21.50 x 29.00mm | 6.75mm | Thép không gỉ 316L | Sapphire/ Chống trầy xước | 3 bar (30m)', CAST(40000000.00 AS Decimal(10, 2)), 10, N'donghopinloai1s1.jpg')
INSERT [dbo].[SanPham] ([MaSP], [TenSP], [NhanHieu], [MoTa], [Gia], [SoLuongKho], [DuongDanHinh]) VALUES (56, N'Đồng hồ Longines L3.782.3.06', N'Maurice Lacroix', N'| Nam | 43.00 x 43.00mm | 11.90mm | Thép không gỉ 316L/ Mạ vàng công nghệ PVD/ Ceramic | Sapphire/ Chống trầy xước 2 lớp phản quang | 30 Bar (300m)', CAST(53188000.00 AS Decimal(10, 2)), 10, N'dongho2.png')
SET IDENTITY_INSERT [dbo].[SanPham] OFF
GO
INSERT [dbo].[SanPham_DanhMuc] ([MaSP], [MaDanhMuc]) VALUES (1, 1)
INSERT [dbo].[SanPham_DanhMuc] ([MaSP], [MaDanhMuc]) VALUES (15, 1)
INSERT [dbo].[SanPham_DanhMuc] ([MaSP], [MaDanhMuc]) VALUES (18, 1)
INSERT [dbo].[SanPham_DanhMuc] ([MaSP], [MaDanhMuc]) VALUES (19, 1)
INSERT [dbo].[SanPham_DanhMuc] ([MaSP], [MaDanhMuc]) VALUES (20, 1)
INSERT [dbo].[SanPham_DanhMuc] ([MaSP], [MaDanhMuc]) VALUES (21, 1)
INSERT [dbo].[SanPham_DanhMuc] ([MaSP], [MaDanhMuc]) VALUES (22, 1)
INSERT [dbo].[SanPham_DanhMuc] ([MaSP], [MaDanhMuc]) VALUES (23, 1)
INSERT [dbo].[SanPham_DanhMuc] ([MaSP], [MaDanhMuc]) VALUES (24, 1)
INSERT [dbo].[SanPham_DanhMuc] ([MaSP], [MaDanhMuc]) VALUES (25, 1)
INSERT [dbo].[SanPham_DanhMuc] ([MaSP], [MaDanhMuc]) VALUES (26, 1)
INSERT [dbo].[SanPham_DanhMuc] ([MaSP], [MaDanhMuc]) VALUES (27, 1)
INSERT [dbo].[SanPham_DanhMuc] ([MaSP], [MaDanhMuc]) VALUES (28, 1)
INSERT [dbo].[SanPham_DanhMuc] ([MaSP], [MaDanhMuc]) VALUES (29, 1)
INSERT [dbo].[SanPham_DanhMuc] ([MaSP], [MaDanhMuc]) VALUES (30, 1)
INSERT [dbo].[SanPham_DanhMuc] ([MaSP], [MaDanhMuc]) VALUES (31, 1)
INSERT [dbo].[SanPham_DanhMuc] ([MaSP], [MaDanhMuc]) VALUES (32, 2)
INSERT [dbo].[SanPham_DanhMuc] ([MaSP], [MaDanhMuc]) VALUES (33, 2)
INSERT [dbo].[SanPham_DanhMuc] ([MaSP], [MaDanhMuc]) VALUES (34, 2)
INSERT [dbo].[SanPham_DanhMuc] ([MaSP], [MaDanhMuc]) VALUES (35, 2)
INSERT [dbo].[SanPham_DanhMuc] ([MaSP], [MaDanhMuc]) VALUES (37, 2)
INSERT [dbo].[SanPham_DanhMuc] ([MaSP], [MaDanhMuc]) VALUES (38, 2)
INSERT [dbo].[SanPham_DanhMuc] ([MaSP], [MaDanhMuc]) VALUES (39, 2)
INSERT [dbo].[SanPham_DanhMuc] ([MaSP], [MaDanhMuc]) VALUES (40, 3)
INSERT [dbo].[SanPham_DanhMuc] ([MaSP], [MaDanhMuc]) VALUES (41, 3)
INSERT [dbo].[SanPham_DanhMuc] ([MaSP], [MaDanhMuc]) VALUES (42, 3)
INSERT [dbo].[SanPham_DanhMuc] ([MaSP], [MaDanhMuc]) VALUES (43, 3)
INSERT [dbo].[SanPham_DanhMuc] ([MaSP], [MaDanhMuc]) VALUES (44, 4)
INSERT [dbo].[SanPham_DanhMuc] ([MaSP], [MaDanhMuc]) VALUES (45, 4)
INSERT [dbo].[SanPham_DanhMuc] ([MaSP], [MaDanhMuc]) VALUES (46, 4)
INSERT [dbo].[SanPham_DanhMuc] ([MaSP], [MaDanhMuc]) VALUES (50, 2)
INSERT [dbo].[SanPham_DanhMuc] ([MaSP], [MaDanhMuc]) VALUES (54, 2)
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([MaUser], [HoTenDem], [Ten], [TenDangNhap], [MatKhau], [Email], [SoDienThoai], [mod]) VALUES (1, N'Nguyễn', N'Hùng', N'phihung123', N'bcb15f821479b4d5772bd0ca866c00ad5f926e3580720659cc80d39c9d09802a', N'phihung@gmail.com', N'094596805', 1)
INSERT [dbo].[Users] ([MaUser], [HoTenDem], [Ten], [TenDangNhap], [MatKhau], [Email], [SoDienThoai], [mod]) VALUES (2, N'Nguyễn Phi', N'Hùng', N'phihung45', N'8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', N'hung@gmail.com', N'0945968013', 2)
INSERT [dbo].[Users] ([MaUser], [HoTenDem], [Ten], [TenDangNhap], [MatKhau], [Email], [SoDienThoai], [mod]) VALUES (3, N'Nguyễn Phi', N'Hùng', N'phihung789', N'8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', N'nguyenhungtks1@gmail.com', N'0945968013', 2)
INSERT [dbo].[Users] ([MaUser], [HoTenDem], [Ten], [TenDangNhap], [MatKhau], [Email], [SoDienThoai], [mod]) VALUES (4, N'Nguyễn Phi', N'Hùng', N'phihung8910', N'8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', N'nguyenhungtks1@gmail.com', N'0945968013', 2)
INSERT [dbo].[Users] ([MaUser], [HoTenDem], [Ten], [TenDangNhap], [MatKhau], [Email], [SoDienThoai], [mod]) VALUES (5, N'Nguyễn Phi', N'Hùng', N'phihung100', N'8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', N'nguyenhungtks1@gmail.com', N'0945968013', 2)
INSERT [dbo].[Users] ([MaUser], [HoTenDem], [Ten], [TenDangNhap], [MatKhau], [Email], [SoDienThoai], [mod]) VALUES (8, N'Nguyễn Phi', N'Hùng', N'phihung500', N'bcb15f821479b4d5772bd0ca866c00ad5f926e3580720659cc80d39c9d09802a', N'hung@gmail.com', N'0945968013', 1)
INSERT [dbo].[Users] ([MaUser], [HoTenDem], [Ten], [TenDangNhap], [MatKhau], [Email], [SoDienThoai], [mod]) VALUES (9, N'Nguyễn Phi', N'Lê Thị Quỳnh', N'quynh123', N'bcb15f821479b4d5772bd0ca866c00ad5f926e3580720659cc80d39c9d09802a', N'hung@gmail.com', N'09454545', 2)
INSERT [dbo].[Users] ([MaUser], [HoTenDem], [Ten], [TenDangNhap], [MatKhau], [Email], [SoDienThoai], [mod]) VALUES (10, N'Đàm Thị ', N'Trang', N'trang123', N'8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', N'nguyenhungtks1@gmail.com', N'06262626', 2)
INSERT [dbo].[Users] ([MaUser], [HoTenDem], [Ten], [TenDangNhap], [MatKhau], [Email], [SoDienThoai], [mod]) VALUES (14, N'Nguyễn Văn', N'Đô', N'do123', N'8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', N'do@gmail.com', N'0515151442', 2)
INSERT [dbo].[Users] ([MaUser], [HoTenDem], [Ten], [TenDangNhap], [MatKhau], [Email], [SoDienThoai], [mod]) VALUES (15, N'Nguyễn Văn', N'Cường', N'cuong123', N'8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', N'cuong@gmail.com', N'05151515', 2)
INSERT [dbo].[Users] ([MaUser], [HoTenDem], [Ten], [TenDangNhap], [MatKhau], [Email], [SoDienThoai], [mod]) VALUES (16, N'Nguyễn Văn', N'Thế', N'the123', N'8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', N'the@gmail.com', N'05151515', 2)
INSERT [dbo].[Users] ([MaUser], [HoTenDem], [Ten], [TenDangNhap], [MatKhau], [Email], [SoDienThoai], [mod]) VALUES (17, N'Nguyễn Văn', N'Thế', N'the234', N'8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', N'the@gmail.com', N'05151515', 2)
INSERT [dbo].[Users] ([MaUser], [HoTenDem], [Ten], [TenDangNhap], [MatKhau], [Email], [SoDienThoai], [mod]) VALUES (18, N'Nguyễn Phi', N'Hùng', N'hung1008', N'8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', N'nguyen@gmail.com', N'05151515', 2)
INSERT [dbo].[Users] ([MaUser], [HoTenDem], [Ten], [TenDangNhap], [MatKhau], [Email], [SoDienThoai], [mod]) VALUES (19, N'Phi ', N'Hung', N'hung123', N'8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', N'nguyenhungtks1@gmail.com', N'0945968013', 2)
INSERT [dbo].[Users] ([MaUser], [HoTenDem], [Ten], [TenDangNhap], [MatKhau], [Email], [SoDienThoai], [mod]) VALUES (20, N'Phi', N'Hung', N'phihung3', N'8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', N'hung@gmail.com', N'123456', 2)
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Users__55F68FC0F3554FC7]    Script Date: 5/9/2024 3:31:11 PM ******/
ALTER TABLE [dbo].[Users] ADD UNIQUE NONCLUSTERED 
(
	[TenDangNhap] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ChiTietDonHang]  WITH CHECK ADD  CONSTRAINT [FK__ChiTietDo__MaDon__5CD6CB2B] FOREIGN KEY([MaDonHang])
REFERENCES [dbo].[DonHang] ([MaDonHang])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ChiTietDonHang] CHECK CONSTRAINT [FK__ChiTietDo__MaDon__5CD6CB2B]
GO
ALTER TABLE [dbo].[ChiTietDonHang]  WITH CHECK ADD FOREIGN KEY([MaSP])
REFERENCES [dbo].[SanPham] ([MaSP])
GO
ALTER TABLE [dbo].[DonHang]  WITH CHECK ADD  CONSTRAINT [FK__DonHang__MaUser__01142BA1] FOREIGN KEY([MaUser])
REFERENCES [dbo].[Users] ([MaUser])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[DonHang] CHECK CONSTRAINT [FK__DonHang__MaUser__01142BA1]
GO
ALTER TABLE [dbo].[PhanHoi]  WITH CHECK ADD FOREIGN KEY([MaUser])
REFERENCES [dbo].[Users] ([MaUser])
GO
ALTER TABLE [dbo].[SanPham_DanhMuc]  WITH CHECK ADD FOREIGN KEY([MaDanhMuc])
REFERENCES [dbo].[DanhMuc] ([MaDanhMuc])
GO
ALTER TABLE [dbo].[SanPham_DanhMuc]  WITH CHECK ADD  CONSTRAINT [fk_SanPham_SanPham_DanhMuc] FOREIGN KEY([MaSP])
REFERENCES [dbo].[SanPham] ([MaSP])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SanPham_DanhMuc] CHECK CONSTRAINT [fk_SanPham_SanPham_DanhMuc]
GO
