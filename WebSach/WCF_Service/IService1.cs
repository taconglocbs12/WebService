using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WCF_Service
{
     
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {
        
      
        [OperationContract]
        List<Sach> GetData();


        [OperationContract]

        List<ChuDe> Chude();

        [OperationContract]
        List<Sach> TimkiemSachTheoChude(int chudeid);

        [OperationContract]
        List<Sach> Sanphambanchay();

        [OperationContract]
        List<ChuDe> DSLoaiTimKiem();
        [OperationContract]
        Sach Chitietsanpham(int masp);

        [OperationContract]
        Sach SPCungLoai1(int Sachid);
        [OperationContract]
        List<Sach> SPCungLoai2(int chudeid,int sachid);

      

        //ForgotPassword 
        [OperationContract]
        List<KhachHang> ThongTinKhachHang(string email);

        //Đăng kí
        [OperationContract]
        bool KiemtraDk(string tendangnhap);

        [OperationContract]
        bool ThemKhacHang(string hoten,string diachi,string sodienthoai,string tendangnhap,string matkhau,DateTime ngaysinh,bool gioitinh,string email);

        //Login 
        [OperationContract]
        List<KhachHang> KiemTraDangNhap(string tendangnhap,string pass);

        [OperationContract]
        bool KiemTraDNForm(string user,string pass);

        [OperationContract]
        List<Sach> HienthiSach();
        [OperationContract]
        bool InsertSach(string tensach, int giaban, int chudeID, int nhaxuatbanID, string mota, string hinhbia, int sotrang, int trongluong, DateTime ngaycapnhap, int soluongban, bool hethang, int daban);
        [OperationContract]
        bool UpdateSach(int sachid, string tensach, int giaban, int chudeID, int nhaxuatbanID, string mota, string hinhbia, int sotrang, int trongluong, DateTime ngaycapnhap, int soluongban, bool hethang, int daban);
        [OperationContract]
        bool DeleteSach(int sachid);


        //Cart
         [OperationContract]
        List<KhachHang> KiemtraTendangnhap(string tendangnhap);

        [OperationContract]
        bool ThemDonDathang(int makh, string tenkh,string diadiem, DateTime ngaydathang, double trigia, bool dagiao);

        [OperationContract]
        Sach LaychitietSach_donDH(int sachid);

        [OperationContract]
        bool ThemChitietDonDatHang(int dathangid,int sachid,int soluong,double dongia,double thanhtien);

        [OperationContract]
        KhachHang LayKhachHang(string tendangnhap);

        // Nhap Hang
        [OperationContract]
        List<PhieuNhap> GetPhieuNhap();

        [OperationContract]
        bool ThemPhieuNhap(DateTime ngayNhap, float tongtien);

         [OperationContract]
        List<PhieuNhapCT> GetPhieuNhapCT(int phieunhapid);

        [OperationContract]
        bool ThemChiTietPhieuNhap(int idphieunhap, int idmasach, float dongia, float thanhtien, int soluong);

        [OperationContract]
        float ThanhTien(int maphieunhap);

        [OperationContract]
        bool UpdatePhieuNhap(int maphieunhap, float thanhtien);

        [OperationContract]
        bool XoaPhieuNhapCT(int ma);

        [OperationContract]
        bool ThemsoLuongSach(int masach,int soluong);
        [OperationContract]
        bool GiamSoLuongSach(int masach, int soluong);
      
   

        // Tac Gia
        [OperationContract]
        List<TacGia> HienTHiTacGia();

        [OperationContract]
        bool ThemTacGia(string tentacgia, string diachi );

        [OperationContract]
        bool XoaTacGia(int matacgia);
        [OperationContract]
        bool UpdateTacGia(int matacgia,string ten,string diachi);








    }
    

    // Use a data contract as illustrated in the sample below to add composite types to service operations.
  
   
}
