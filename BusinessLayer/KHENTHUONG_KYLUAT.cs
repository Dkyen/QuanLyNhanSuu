using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.DTO;
using DataLayer;

namespace BusinessLayer
{
    public class KHENTHUONG_KYLUAT
    {
        quanlynhansuEntities db = new quanlynhansuEntities();
        public tb_KHENTHUONGKYLUAT getItem(string soQD) 
        {
            return db.tb_KHENTHUONGKYLUAT.FirstOrDefault(x => x.SOQUYETDINH == soQD);
        }

        public List<tb_KHENTHUONGKYLUAT> getList(int loai)
        {
            return db.tb_KHENTHUONGKYLUAT.Where(x => x.LOAI == loai).ToList();
        }

        public List<KHENTHUONG_KYLUAT_DTO > getListFull(int loai)
        {
            List<tb_KHENTHUONGKYLUAT> lstKT = db.tb_KHENTHUONGKYLUAT.Where(x => x.LOAI == loai).ToList();
            List<KHENTHUONG_KYLUAT_DTO> lstDTO = new List<KHENTHUONG_KYLUAT_DTO>();
            KHENTHUONG_KYLUAT_DTO kt;
            foreach( var item in lstKT)
            {
                kt = new KHENTHUONG_KYLUAT_DTO();
                kt.SOQUYETDINH = item.SOQUYETDINH;
                kt.TUNGAY = item.TUNGAY;
                kt.DENNGAY = item.DENNGAY;
                kt.NOIDUNG = item.NOIDUNG;
                kt.LYDO = item.LYDO;
                kt.NGAY = item.NGAY;
                kt.LOAI = item.LOAI;
                kt.MANV = item.MANV;
                var nv = db.tb_NHANVIEN.FirstOrDefault(n => n.MANV == item.MANV);
                kt.HOTEN = nv.HOTEN;
                kt.UPDATED_BY = item.UPDATED_BY;
                kt.UPDATED_DATE = item.UPDATED_DATE;
                kt.CREATED_BY = item.CREATED_BY;
                kt.CREATED_DATE = item.CREATED_DATE;
                kt.DELETED_BY = item.DELETED_BY;
                kt.DELETED_DATE = item.DELETED_DATE;
                lstDTO.Add(kt);

            }
            return lstDTO;
        }


        public tb_KHENTHUONGKYLUAT Add(tb_KHENTHUONGKYLUAT kt)
        {
            try
            {
                db.tb_KHENTHUONGKYLUAT.Add(kt);
                db.SaveChanges();
                return kt;

            }
            catch(Exception ex)
            {
                throw new Exception("Lỗi:" +ex.Message);

            }
       
        }

        public tb_KHENTHUONGKYLUAT Update(tb_KHENTHUONGKYLUAT kt)
        {
            try
            {
                tb_KHENTHUONGKYLUAT _kt = db.tb_KHENTHUONGKYLUAT.FirstOrDefault(x=>x.SOQUYETDINH==kt.SOQUYETDINH);
                _kt.NGAY = kt.NGAY;
                _kt.TUNGAY = kt.TUNGAY;
                _kt.DENNGAY = kt.DENNGAY;
                _kt.LYDO = kt.LYDO;
                _kt.NOIDUNG = kt.NOIDUNG;
                _kt.LOAI = kt.LOAI;
                _kt.MANV = kt.MANV;
                _kt.UPDATED_BY = kt.UPDATED_BY;
                _kt.UPDATED_DATE = kt.UPDATED_DATE;
                db.SaveChanges();
                return kt;

            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi:" + ex.Message);

            }

        }
        public void Delete (string soQD, int manv)
        {
            
               
            var _kt = db.tb_KHENTHUONGKYLUAT.FirstOrDefault(x => x.SOQUYETDINH == soQD);

            _kt.DELETED_BY = manv;
            _kt.DELETED_DATE = DateTime.Now;
            db.SaveChanges();
        }
        public string MaxSoQuyetDinh(int loai)
        {
            var _kt = db.tb_KHENTHUONGKYLUAT.Where(x =>x.LOAI==loai).OrderByDescending(x => x.CREATED_DATE).FirstOrDefault();
            if(_kt != null)
            {
                return _kt.SOQUYETDINH;
            }
            else
            {
                return "00000";
            }
        }
       

    }
}
