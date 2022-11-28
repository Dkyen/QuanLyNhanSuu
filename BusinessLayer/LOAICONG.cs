using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;

namespace BusinessLayer
{
   public class LOAICONG
    {
        quanlynhansuEntities db = new quanlynhansuEntities();

        public tb_LOAICONG getItem(int idloaicong)
        {
            return db.tb_LOAICONG.FirstOrDefault(x => x.IDLC == idloaicong);
        }
        public List<tb_LOAICONG> getList()
        {
            return db.tb_LOAICONG.ToList();
        }
        public tb_LOAICONG Add(tb_LOAICONG lcong)
        {
            try
            {
                db.tb_LOAICONG.Add(lcong);
                db.SaveChanges();
                return lcong;

            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi" + ex.Message);
            }
        }
        public tb_LOAICONG Update(tb_LOAICONG lcong)
        {
            try
            {
                var _lcong = db.tb_LOAICONG.FirstOrDefault(x => x.IDLC == lcong.IDLC);
                _lcong.TENLC = lcong.TENLC;
                _lcong.HESO = lcong.HESO;
                _lcong.UPDATED_BY = lcong.UPDATED_BY;
                _lcong.UPDATED_DATE = lcong.UPDATED_DATE;
                db.tb_LOAICONG.Add(lcong);
                db.SaveChanges();
                return lcong; ;
                

            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi" + ex.Message);
            }
        }

        public void Delete(int idloaica, int iduser)
        {
            var _lc = db.tb_LOAICA.FirstOrDefault(x => x.IDLOAICA == idloaica);

            _lc.DELETED_BY = iduser;
            _lc.DELETED_DATE = DateTime.Now;

            db.SaveChanges();

        }
    }
}

