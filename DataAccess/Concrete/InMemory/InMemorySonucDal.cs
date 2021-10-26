using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Concrete.InMemory
{
    public class InMemorySonucDal : ISonucDal
    {
        List<Sonuc> _sonuc;

        public InMemorySonucDal()
        {
            _sonuc = new List<Sonuc>();
        }

       
        public void Add(String durusNedeni,int isEmri,Decimal durusSuresi)
        {
            _sonuc.Add(new Sonuc() { DurusNedeni = durusNedeni, IsEmri = isEmri, DurusSuresi = durusSuresi }); 
        }

        public List<Sonuc> GetAll()
        {
            return _sonuc;
        }

        public void Update(Sonuc sonuc)
        {
            Sonuc sonucToUpdate = _sonuc.SingleOrDefault(s => s.IsEmri == sonuc.IsEmri && s.DurusNedeni == sonuc.DurusNedeni);
            sonucToUpdate.DurusSuresi += sonuc.DurusSuresi;
        }
    }
}
