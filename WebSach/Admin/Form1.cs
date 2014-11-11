using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using System.Data.Linq.SqlClient;
using Admin.ServiceSach;


namespace Admin
{
    public partial class Form1 : Form
    {
        Service1Client ds = new Service1Client();

        public Form1()
        {
            InitializeComponent();
            groupBox3.Hide();
            label_Chao.Hide();
            pictureBox7.Hide();
            label1.Hide();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
          reload();
        }

        private void reload()
        {
            //hienthi sách
            this.DVGV_Sach.AutoGenerateColumns = false;
            DVGV_Sach.DataSource = ds.HienthiSach();

            //hien thi tac gia gia
            this.DTGV_tacgia.AutoGenerateColumns = false;
            DTGV_tacgia.DataSource = ds.HienTHiTacGia().ToList();

            this.dgvphieunhaphang.DataSource = ds.GetPhieuNhap();
        }
       #region Tìm kiếm sach
        private void textBox16_TextChanged(object sender, EventArgs e)
        {
            DVGV_Sach.DataSource = null;
          
            DVGV_Sach.DataSource = ds.HienthiSach().Where(a => a.TenSach.ToLower().Contains(textBox16.Text.ToString().ToLower())).ToList();

        }
       #endregion

        private void DVGV_Sach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DVGV_Sach.SelectedRows.Count == 0)
                return;
            Txt_masach.Text = DVGV_Sach.CurrentRow.Cells[0].Value.ToString();
            txt_tensach.Text = DVGV_Sach.CurrentRow.Cells[1].Value.ToString();
            txt_giaban.Text = DVGV_Sach.CurrentRow.Cells[2].Value.ToString();
            combox_chude.Text = DVGV_Sach.CurrentRow.Cells[3].Value.ToString();
            comboBox_nhaxuatban.Text = DVGV_Sach.CurrentRow.Cells[4].Value.ToString();
            Txt_mota.Text = DVGV_Sach.CurrentRow.Cells[5].Value.ToString();
            Txt_hinhbia.Text = DVGV_Sach.CurrentRow.Cells[6].Value.ToString();
            txt_sotrang.Text = DVGV_Sach.CurrentRow.Cells[7].Value.ToString();
            txt_trongluong.Text = DVGV_Sach.CurrentRow.Cells[8].Value.ToString();

            string ngaycapnhap = DVGV_Sach.CurrentRow.Cells[9].Value.ToString().Trim();
            DateTime ns = DateTime.Parse(ngaycapnhap);
            dateTimePicker1.Value = (DateTime)ns;
          
            txt_solanban.Text = DVGV_Sach.CurrentRow.Cells[10].Value.ToString();
            bool _quyen = bool.Parse(DVGV_Sach.CurrentRow.Cells[11].Value.ToString());
            if (_quyen)
                checkBox1.Checked = true;
            else
                checkBox1.Checked = false;

            txt_daban.Text = DVGV_Sach.CurrentRow.Cells[12].Value.ToString();

            

            

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
          
           
            string tensach = txt_tensach.Text;
            int giaban = Convert.ToInt32(txt_giaban.Text);
            int chude=Convert.ToInt32(combox_chude.Text);
            int xuatban=Convert.ToInt32(comboBox_nhaxuatban.Text);
            string mota = Txt_mota.Text;
            string hinhbia = Txt_hinhbia.Text;
             int sotrang = Convert.ToInt32(txt_sotrang.Text);
               int trongluong = Convert.ToInt32(txt_trongluong.Text);
            DateTime ngaycapnhap = Convert.ToDateTime(dateTimePicker1.Value.Date);
            int soluongban = Convert.ToInt32(txt_solanban.Text);
            int daban = Convert.ToInt32(txt_daban.Text);
            try
            {
                if ( ds.InsertSach(tensach, giaban, chude, xuatban, mota, hinhbia, sotrang, trongluong, ngaycapnhap, soluongban, checkBox1.Checked, daban)==true)
                {
                    MessageBox.Show("Thêm Thành Công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    reload();
                }
               
              
            }
            catch (Exception)
            {

                MessageBox.Show("Lỗi rồi :(", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            int masach = Convert.ToInt32(Txt_masach.Text);
            string tensach = txt_tensach.Text;
            int giaban = Convert.ToInt32(txt_giaban.Text);
            int chude=Convert.ToInt32(combox_chude.Text);
            int xuatban=Convert.ToInt32(comboBox_nhaxuatban.Text);
            string mota = Txt_mota.Text;
            string hinhbia = Txt_hinhbia.Text;
             int sotrang = Convert.ToInt32(txt_sotrang.Text);
               int trongluong = Convert.ToInt32(txt_trongluong.Text);
            DateTime ngaycapnhap = Convert.ToDateTime(dateTimePicker1.Value.Date);
            int soluongban = Convert.ToInt32(txt_solanban.Text);
            int daban = Convert.ToInt32(txt_daban.Text);

           

            
            try
            {
                if (ds.UpdateSach(masach,tensach,giaban,chude,xuatban,mota,hinhbia,sotrang,trongluong,ngaycapnhap,soluongban,checkBox1.Checked,daban) == true)
                {
                    MessageBox.Show("Sửa Thành Công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    reload();
                }


            }
            catch (Exception)
            {

                MessageBox.Show("Lỗi rồi :(", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            if (ds.DeleteSach(Convert.ToInt32(Txt_masach.Text)) == true)
            {
                MessageBox.Show("Xóa Thành Công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                reload();
            }
            else
            {
                MessageBox.Show("Lỗi rồi :(", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button_dangnhap_Click(object sender, EventArgs e)
        {
            if (ds.KiemTraDNForm(txt_tendangnhap.Text, txt_matkhau.Text))
            {
                MessageBox.Show("Chào Quản Trị Viên", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                groupBox3.Visible=Enabled;
                label_Chao.Visible = Enabled;
                pictureBox7.Visible = Enabled;
                label1.Visible = Enabled;

                txt_tendangnhap.Hide();
                txt_matkhau.Hide();
                label_tendangnhap.Hide();
                label_matkhau.Hide();
                button_dangnhap.Hide();
                button_thoat.Hide();
            }
            else
            {
                MessageBox.Show("Sai Thông Tin Đăng Nhập", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            txt_tendangnhap.Visible = Enabled;
            txt_matkhau.Visible = Enabled;
            label_tendangnhap.Visible = Enabled;
            label_matkhau.Visible = Enabled;
            button_dangnhap.Visible = Enabled;
            button_thoat.Visible = Enabled;
            pictureBox7.Hide();
            label_Chao.Hide();
            label1.Hide();
        }

        private void dgvphieunhaphang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
                
            if (dgvphieunhaphang.SelectedRows.Count == 0)
                return;
            string ngaycapnhap = dgvphieunhaphang.CurrentRow.Cells[1].Value.ToString().Trim();
            DateTime ns = DateTime.Parse(ngaycapnhap);
            dtngaynhap.Value = (DateTime)ns;

            

            string idphieunhap = dgvphieunhaphang.CurrentRow.Cells[0].Value.ToString().Trim();

            txttt.Text = dgvphieunhaphang.CurrentRow.Cells[2].Value.ToString().Trim();

            this.DTGV_chitietphieuNhap.AutoGenerateColumns = false;
            DTGV_chitietphieuNhap.DataSource = ds.GetPhieuNhapCT(int.Parse(idphieunhap));

        }

     
        private void DTGV_chitietphieuNhap_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txt_idphieunhapchitiet.Text = DTGV_chitietphieuNhap.CurrentRow.Cells[0].Value.ToString().Trim();
            txt_maphieunhap.Text = DTGV_chitietphieuNhap.CurrentRow.Cells[1].Value.ToString().Trim();


            int masach = Convert.ToInt32(DTGV_chitietphieuNhap.CurrentRow.Cells[2].Value.ToString().Trim());
            Combox_tensach.Text = ds.Chitietsanpham(masach).TenSach;

            Txt_dongia.Text = DTGV_chitietphieuNhap.CurrentRow.Cells[3].Value.ToString().Trim();
            txt_thanhtien.Text = DTGV_chitietphieuNhap.CurrentRow.Cells[4].Value.ToString().Trim();
            txt_soluongCT.Text = DTGV_chitietphieuNhap.CurrentRow.Cells[5].Value.ToString().Trim();

        }

   
        private void Combox_tensach_DataSourceChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            txt_idphieunhapchitiet.Text = null;
            txt_maphieunhap.Text = dgvphieunhaphang.CurrentRow.Cells[0].Value.ToString().Trim();

            Combox_tensach.DataSource = ds.GetData();
            

            Txt_dongia.Text = null;
            txt_thanhtien.Text = null;
            txt_soluongCT.Text = "0";
        }

        private void Combox_tensach_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach ( Sach item in ds.GetData())
            {
                if (item.TenSach == Combox_tensach.Text.Trim())
                {
                    Txt_dongia.Text = item.GiaBan.ToString();
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void txt_soluongCT_KeyPress(object sender, KeyPressEventArgs e)
        {

            try
            {
                int sl = int.Parse(txt_soluongCT.Text);
                float gia = float.Parse(Txt_dongia.Text);
                float tt = sl * gia;
                txt_thanhtien.Text = tt.ToString();
            }
            catch
            {

            }
        }

     

     

        private void pic_them_Click(object sender, EventArgs e)
        {
            int maphieunhap = Convert.ToInt32(txt_maphieunhap.Text);
            int masach = Convert.ToInt32(Combox_tensach.SelectedValue);
            float dongia = Convert.ToSingle(Txt_dongia.Text);
            float thanhtien = Convert.ToSingle(txt_thanhtien.Text);
            int soluong = Convert.ToInt32(txt_soluongCT.Text);
           
            ds.ThemChiTietPhieuNhap(maphieunhap, masach, dongia, thanhtien, soluong);
            ds.ThemsoLuongSach(masach, soluong);
            DTGV_chitietphieuNhap.DataSource = ds.GetPhieuNhapCT(maphieunhap);
        }

        private void pic_themphieunhap_Click(object sender, EventArgs e)
        {
            ds.ThemPhieuNhap(DateTime.Parse(dtngaynhap.Text),0);
            reload();
        }

        private void pic_luu_Click(object sender, EventArgs e)
        {
            string idphieunhap = dgvphieunhaphang.CurrentRow.Cells[0].Value.ToString().Trim();

            if (ds.UpdatePhieuNhap(int.Parse(idphieunhap), float.Parse(txttt.Text)))
            {
                MessageBox.Show("Lưu Thành Công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Lỗi", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            reload();
        }

        private void pic_refesh_Click(object sender, EventArgs e)
        {
            txttt.Text = Convert.ToString(ds.ThanhTien(int.Parse(dgvphieunhaphang.CurrentRow.Cells[0].Value.ToString().Trim())));
        }

        private void pic_xoa_Click(object sender, EventArgs e)
        {
            int masach = Convert.ToInt32(DTGV_chitietphieuNhap.CurrentRow.Cells[2].Value.ToString());
            int soluong = Convert.ToInt32(txt_soluongCT.Text);
            if (ds.XoaPhieuNhapCT(int.Parse(txt_idphieunhapchitiet.Text)))
                {
                    DTGV_chitietphieuNhap.DataSource = ds.GetPhieuNhapCT(int.Parse(dgvphieunhaphang.CurrentRow.Cells[0].Value.ToString().Trim()));
                     
                    ds.GiamSoLuongSach(masach,soluong);
                

                }
            else
            {
                MessageBox.Show("Lỗi", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
            }
          
        }

        private void DTGV_tacgia_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txt_matacgia.Text = DTGV_tacgia.CurrentRow.Cells[0].Value.ToString();
            txt_tenTacgia.Text = DTGV_tacgia.CurrentRow.Cells[1].Value.ToString();
            Txt_diachi.Text = DTGV_tacgia.CurrentRow.Cells[2].Value.ToString();
        }

        private void Pic_themtacgia_Click(object sender, EventArgs e)
        {
            
            string ten = txt_tenTacgia.Text;
            string diachi = Txt_diachi.Text;
            if (ds.ThemTacGia(ten, diachi))
            {
                MessageBox.Show("Thành Công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                reload();
            }
            else
            {
                MessageBox.Show("Lỗi", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void pic_xoatacgia_Click(object sender, EventArgs e)
        {

            if (ds.XoaTacGia(int.Parse(DTGV_tacgia.CurrentRow.Cells[0].Value.ToString())))
            {
                MessageBox.Show("Thành Công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                reload();
            }
            else
            {
                MessageBox.Show("Lỗi", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


                

        }

        private void pic_suatacgia_Click(object sender, EventArgs e)
        {
            int ma = int.Parse(DTGV_tacgia.CurrentRow.Cells[0].Value.ToString()); 
          

            if (ds.UpdateTacGia(ma, txt_tenTacgia.Text, Txt_diachi.Text))
            {
                MessageBox.Show("Thành Công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                reload();
            }
            else
            {
                MessageBox.Show("Lỗi", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

  
















    }
}
