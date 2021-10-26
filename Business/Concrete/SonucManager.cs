using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class SonucManager : ISonucService
    {
        ISonucDal _sonucDal;

        public SonucManager(ISonucDal sonucDal)
        {
            _sonucDal = sonucDal;
        }

        public void Add(String durusNedeni, int isEmri, Decimal durusSuresi)
        {
            _sonucDal.Add(durusNedeni,isEmri,durusSuresi);
        }

        public List<Sonuc> GetAll()
        {
            return _sonucDal.GetAll();
        }

        public void Update(Sonuc sonuc)
        {
            _sonucDal.Update(sonuc);
        }
    }
}
