using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Abstract
{
    public interface ISonucDal
    {
        List<Sonuc> GetAll();
        void Add(String durusNedeni,int isEmri,Decimal durusSuresi);
        void Update(Sonuc sonuc);
    }
}
