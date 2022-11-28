using BusinessLayer;
using DataLayer;
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

namespace QLNhanSu
{
    public partial class frmNhanVien_DieuChuyen : DevExpress.XtraEditors.XtraForm
    {
        public frmNhanVien_DieuChuyen()
        {
            InitializeComponent();
        }

        NHANVIEN_DIEUCHUYEN _nvdc;
        NHANVIEN _nhanvien;
        PHONGBAN _phongban;
        bool _them;
        string _soQD;

        private void frmNhanVien_DieuChuyen_Load(object sender, EventArgs e)
        {
            _nvdc = new NHANVIEN_DIEUCHUYEN();
            _nhanvien = new NHANVIEN();
            _phongban = new PHONGBAN();
            _them = false;

            _showHide(true);
            loadNhanVien();
            loadData();
            loadDonViDen();
            splitContainer1.Panel1Collapsed = true;
        }
        void _showHide(bool dc)
        {
            btnLuu.Enabled = !dc;
            btnHuy.Enabled = !dc;
            btnThem.Enabled = dc;
            btnSua.Enabled = dc;
            btnXoa.Enabled = dc;
            btnDong.Enabled = dc;
            btnIn.Enabled = dc;
            gcDanhSach.Enabled = dc;
            txtSoQD.Enabled = !dc;
            txtLyDo.Enabled = !dc;
            txtGhiChu.Enabled = !dc;
            cboDonVi.Enabled = !dc;
            slkNhanVien.Enabled = !dc;


        }
        private void _reset()
        {
            txtSoQD.Text = string.Empty;
            txtLyDo.Text = string.Empty;
            txtGhiChu.Text = string.Empty;

        }
        void loadNhanVien()
        {
            slkNhanVien.Properties.DataSource = _nhanvien.getList();
            slkNhanVien.Properties.ValueMember = "MANV";
            slkNhanVien.Properties.DisplayMember = "HOTEN";
        }
        void loadDonViDen()
        {
            cboDonVi.DataSource = _phongban.getList();
            cboDonVi.DisplayMember = "TENPB";
            cboDonVi.ValueMember = "IDPB";
        }
        private void loadData()
        {
            gcDanhSach.DataSource = _nvdc.getListFull();
            gvDanhSach.OptionsBehavior.Editable = false;
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
                _nvdc.Delete(_soQD, 1);
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

        }

        private void btnDong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }
        private void SaveData()
        {
            tb_NHANVIEN_DIEUCHUYEN dc;
            if (_them)
            {
                var maxSoQD = _nvdc.MaxSoQuyetDinh();
                int so = int.Parse(maxSoQD.Substring(0, 5)) + 1;

                dc = new tb_NHANVIEN_DIEUCHUYEN();
                dc.SOQD = so.ToString("00000") + @"/" + DateTime.Now.Year.ToString() + @"/QDDC";

                dc.NGAY = dtNgay.Value;
                dc.LYDO = txtLyDo.Text;
                dc.GHICHU = txtGhiChu.Text;
                dc.MANV = int.Parse(slkNhanVien.EditValue.ToString());
                dc.IDPB = _nhanvien.getItem(int.Parse(slkNhanVien.EditValue.ToString())).IDBP;
                dc.IDPB2 = int.Parse(cboDonVi.SelectedValue.ToString());
                dc.CREATED_BY = 1;
                dc.CREATED_DATE = DateTime.Now;
                _nvdc.Add(dc);

            }
            else
            {
                dc = _nvdc.getItem(_soQD);

                dc.NGAY = dtNgay.Value;
                dc.LYDO = txtLyDo.Text;
                dc.GHICHU = txtGhiChu.Text;
                dc.MANV = int.Parse(slkNhanVien.EditValue.ToString());
                dc.IDPB = _nhanvien.getItem(int.Parse(slkNhanVien.EditValue.ToString())).IDBP;
                dc.IDPB2 = int.Parse(cboDonVi.SelectedValue.ToString());
                dc.CREATED_BY = 1;
                dc.CREATED_DATE = DateTime.Now;
                _nvdc.Update(dc);
            }
            var nv = _nhanvien.getItem(dc.MANV.Value);
            nv.IDBP = dc.IDPB2;
            _nhanvien.Update(nv);
        }

       

        private void gvDanhSach_Click(object sender, EventArgs e)
        {
            _soQD = gvDanhSach.GetFocusedRowCellValue("SOQD").ToString();
            var dc = _nvdc.getItem(_soQD);
            txtSoQD.Text = _soQD;
            dtNgay.Value = dc.NGAY.Value;
            slkNhanVien.EditValue = dc.MANV;
            txtLyDo.Text = dc.LYDO;
            txtGhiChu.Text = dc.GHICHU;
            cboDonVi.SelectedValue = dc.IDPB2;
        }
    }
        /*private void gvDanhSach_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column.Name == "DELETED_BY" && e.CellValue != null)
            {
               // Image img = Properties.Resources.delete_icon;
                e.Graphics.DrawImage(img, e.Bounds.X, e.Bounds.Y);
                e.Handled = true;
            }
        }*/
    }
    
