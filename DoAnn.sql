create database DoAnCuoiKii
use	DoAnCuoiKii
create table SanPham
(
	idsanpham nvarchar(20) primary key,
	price float,
	soluong int,
)
create table NhapHang
(
	idphieunhap nvarchar(20) primary key,
	idsanpham nvarchar(20),
	foreign key (idsanpham) references SanPham(idsanpham),
)
create table Userr
(
	taikhoan nvarchar(20) primary key,
	matkhau nvarchar(20)
)


