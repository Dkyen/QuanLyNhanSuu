using BusinessLayer;
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

namespace QLNhanSu.CHAMCONG
{
    public partial class frmCapNhatNgayCong : DevExpress.XtraEditors.XtraForm
    {
        public frmCapNhatNgayCong()
        {
            InitializeComponent();
        }
        public int _manv;
        public string _hoten;
        public int _makycong;
        public string _ngay;
        public int _cNgay;
        KYCONGCHITIET _kcct;
        frmBangCongChiTiet frmBCCC = (frmBangCongChiTiet)Application.OpenForms["frmBangCongChiTiet"];
        private void frmCapNhatNgayCong_Load(object sender, EventArgs e)
        {
            _kcct = new KYCONGCHITIET();
            lblID.Text = _manv.ToString();
            lblHoTen.Text = _hoten;
            string nam = _makycong.ToString().Substring(0, 4);
            string thang = _makycong.ToString().Substring(4);
            string ngay = _ngay.Substring(1);
            DateTime _d = DateTime.Parse(nam + "-" + thang + "-" + ngay);
            cldNgayCong.SetDate(_d);

        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            string _valueChamCong = rdgChamCong.Properties.Items[rdgChamCong.SelectedIndex].Value.ToString();
            string _valueNgayNghi = rdgNgayNghi.Properties.Items[rdgNgayNghi.SelectedIndex].Value.ToString();
            string fieldName = "D" + _cNgay.ToString();
            var kcct = _kcct.getItem(_makycong, _manv);

            double? tongngaycong = kcct.TONGNGAYCONG;
            double? tongngayphep = kcct.NGAYPHEP;
            double? tongngaykhongphep = kcct.NGHIKHONGPHEP; ;
            double? tongngayle = kcct.CONGNGAYLE;//2022*100+1=202201
            if (cldNgayCong.SelectionRange.Start.Year * 100 + cldNgayCong.SelectionRange.Start.Month != +_makycong)
            {
                MessageBox.Show("Thực hiện chấm công không đúng kỳ công. Vui lòng kiểm tra lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //Cập nhật KYCONGCHITIET=> cập nhật BANGCONG_NV_CT
          QL_Functions.execQuery("UPDATE tb_KYCONGCHITIET SET " + fieldName + "='" + _valueChamCong + "' WHERE MAKYCONG=" + _makycong + " AND MANV=" + _manv);

            frmBCCC.loadBangCong();


            //MessageBox.Show(_valueChamCong +"--"+ _valueNgayNghi);
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rdgChamCong_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cldNgayCong_DateSelected(object sender, DateRangeEventArgs e)
        {
            _cNgay = cldNgayCong.SelectionRange.Start.Day;
        }
    }
}