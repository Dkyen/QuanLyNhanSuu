using BusinessLayer.DTO;
using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using DataLayer;
using BusinessLayer;
namespace QLNhanSu.Reports
{
    public partial class rptDanhSachNhanVien : DevExpress.XtraReports.UI.XtraReport
    {
        public rptDanhSachNhanVien()
        {
            InitializeComponent();
        }
        List<NHANVIEN_DTO> _lstNV;

        public rptDanhSachNhanVien(List<NHANVIEN_DTO> lstNV)
        {
            InitializeComponent();
            this._lstNV = lstNV;
            this.DataSource = _lstNV;
            loadData();
        }
        void loadData()
        {
            lblMaNV.DataBindings.Add("Text", _lstNV, "MANV");
            lblHoTen.DataBindings.Add("Text", _lstNV, "HOTEN");
            lblGioiTinh.DataBindings.Add("Text", _lstNV, "GIOITINH");
            lblNgaySinh.DataBindings.Add("Text", _lstNV, "NGAYSINH");
            lblCCCD.DataBindings.Add("Text", _lstNV, "CCCD");
            lblDienThoai.DataBindings.Add("Text", _lstNV, "DIENTHOAI");
            lblPhongBan.DataBindings.Add("Text", _lstNV, "PHONGBAN");
            lblBoPhan.DataBindings.Add("Text", _lstNV, "BOPHAN");
            lblChucVu.DataBindings.Add("Text", _lstNV, "CHUCVU");
            lblTrinhDo.DataBindings.Add("Text", _lstNV, "TRINHDO");
            lblDanToc.DataBindings.Add("Text", _lstNV, "DANTOC");
            lblTonGiao.DataBindings.Add("Text", _lstNV, "TONGIAO");
            lblDiaChi.DataBindings.Add("Text", _lstNV, "DIACHI");

        }

    }
}
