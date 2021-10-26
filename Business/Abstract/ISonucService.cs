using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Abstract
{
    public interface ISonucService
    {
        List<Sonuc> GetAll();
        void Add(String durusNedeni,int isEmri,Decimal durusSuresi);
        void Update(Sonuc sonuc);
    }
}
