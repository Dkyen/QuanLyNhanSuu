using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataLayer;
using BusinessLayer;
using QLNhanSu.Reports;
using DevExpress.XtraReports.UI;
using BusinessLayer.DTO;

namespace QLNhanSu
{
    public partial class frmHopDongLaoDong : DevExpress.XtraEditors.XtraForm
    {
        public frmHopDongLaoDong()
        {
            InitializeComponent();
        }
        HOPDONGLAODONG _hdld;
        NHANVIEN _nhanvien;
        bool _them;
        string _soHD;
        //string _MaxSoHD;
        List<HOPDONG_DTO> _lstHD;

        void _showHide(bool kt)
        {
            btnLuu.Enabled = !kt;
            btnHuy.Enabled = !kt;
            btnThem.Enabled = kt;
            btnSua.Enabled = kt;
            btnXoa.Enabled = kt;
            btnDong.Enabled = kt;
            btnIn.Enabled = kt;
            gcDanhSach.Enabled = kt;
            txtSoHD.Enabled = !kt;
            dtNgayBatDau.Enabled = !kt;
            dtNgayKT.Enabled = !kt;
            dtNgayKy.Enabled = !kt;
            spHeSoLuong.Enabled = !kt;
            spLanKy.Enabled = !kt;
            slkNhanVien.Enabled = !kt;
        }
        private void _reset()
        {
            txtSoHD.Text = string.Empty;
            dtNgayBatDau.Value = DateTime.Now;
            dtNgayKT.Value = dtNgayBatDau.Value.AddMonths(6);
            dtNgayKy.Value = DateTime.Now;
            spLanKy.Text = "1";
            spHeSoLuong.Text = "1";

            

        }
        void loadNhanVien()
        {
            slkNhanVien.Properties.DataSource = _nhanvien.getList();
            slkNhanVien.Properties.ValueMember = "MANV";
            slkNhanVien.Properties.DisplayMember = "HOTEN";


        }
        private void loadData()
        {
            gcDanhSach.DataSource = _hdld.getListFull();
            gvDanhSach.OptionsBehavior.Editable = false;
            
        }

        private void frmHopDongLaoDong_Load(object sender, EventArgs e)
        {
            _hdld = new HOPDONGLAODONG();
            _nhanvien = new NHANVIEN();
            _them = false;
            _showHide(true);
            loadData();
            loadNhanVien();
            splitContainer1.Panel1Collapsed = true;

        }

        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _showHide(false);
            _them = true;
            _reset();
            splitContainer1.Panel1Collapsed = false;
        }

        private void btnSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _them = false;
            _showHide(false);
            splitContainer1.Panel1Collapsed = false;
            gcDanhSach.Enabled = true;

        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                 _hdld.Delete(_soHD,1);
                loadData();
            }
        }

        private void btnLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SaveData();
            loadData();
            _them = false;
            _showHide(true);
            splitContainer1.Panel1Collapsed = true;
        }

        private void btnHuy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _them = false;
            _showHide(true);
            splitContainer1.Panel1Collapsed = true;
        }

        private void btnIn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _lstHD = _hdld.getItemFull(_soHD);
            rptHopDongLaoDong rpt = new rptHopDongLaoDong(_lstHD);
            rpt.ShowPreviewDialog();
        }

        private void btnDong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }
        void SaveData()
        {
            if (_them)
            {
                // Số hợp đồng có dạng: 00001/2022/HDLD
                var maxSoHD = _hdld.MaxSoHopDong();
                int so = int.Parse(maxSoHD.Substring(0, 5)) + 1;

                tb_HOPDONG hd = new tb_HOPDONG();
                hd.SOHD = so.ToString("00000") + @"/2022/HDLD";
                hd.NGAYBD = dtNgayBatDau.Value;
                hd.NGAYKT = dtNgayKT.Value;
                hd.NGAYKY = dtNgayKy.Value;
                hd.THOIHAN = cbThoiHan.Text;
                hd.HESOLUONG = double.Parse(spHeSoLuong.EditValue.ToString());
                hd.LANKY = int.Parse(spLanKy.EditValue.ToString());
                hd.MANV = int.Parse(slkNhanVien.EditValue.ToString());
                hd.NOIDUNG = txtNoiDung.RtfText;
                hd.MACT = 1;
                hd.CREATED_BY = 1;
                hd.CREATED_DATE = DateTime.Now;
                _hdld.Add(hd);

            }
            else
            {
                var hd = _hdld.getItem(_soHD);
                
                hd.NGAYBD = dtNgayBatDau.Value;
                hd.NGAYKT = dtNgayKT.Value;
                hd.NGAYKY = dtNgayKy.Value;
                hd.THOIHAN = cbThoiHan.Text;
                hd.HESOLUONG = double.Parse(spHeSoLuong.EditValue.ToString());
                hd.LANKY = int.Parse(spLanKy.EditValue.ToString());
                hd.MANV = int.Parse(slkNhanVien.EditValue.ToString());
                hd.NOIDUNG = txtNoiDung.RtfText;
                hd.MACT = 1;
                hd.CREATED_BY = 1;
                hd.CREATED_DATE = DateTime.Now;
               
                _hdld.Update(hd);

            }
        }

        private void gvDanhSach_Click(object sender, EventArgs e)
        {
            if (gvDanhSach.RowCount > 0)
            {
                _soHD = gvDanhSach.GetFocusedRowCellValue("SOHD").ToString();
                var hd = _hdld.getItem(_soHD);
                txtSoHD.Text =_soHD;
                dtNgayBatDau.Value=hd.NGAYBD.Value;
                dtNgayKT.Value = hd.NGAYKT.Value;
                dtNgayKy.Value = hd.NGAYKY.Value;
                cbThoiHan.Text = hd.THOIHAN;
                spHeSoLuong.Text = hd.HESOLUONG.ToString();
                txtNoiDung.RtfText = hd.NOIDUNG;
                spLanKy.Text = hd.LANKY.ToString();
                slkNhanVien.EditValue = hd.MANV;
                _lstHD = _hdld.getItemFull(_soHD);
                
            }
        }

    }
}