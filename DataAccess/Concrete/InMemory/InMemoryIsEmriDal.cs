using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Concrete.InMemory
{
    public class InMemoryIsEmriDal : IIsEmriDal
    {
        List<IsEmri> _isemri;
        public InMemoryIsEmriDal()
        {
            _isemri = new List<IsEmri>
            {
                new IsEmri{IsEmriNumarasi=1001,Baslangic=Convert.ToDateTime("1/1/2017 08:00:00"),Bitis=Convert.ToDateTime("1/1/2017 16:00:00")},
                new IsEmri{IsEmriNumarasi=1002,Baslangic=Convert.ToDateTime("1/1/2017 16:00:00"),Bitis=Convert.ToDateTime("2/1/2017 00:00:00")},
                new IsEmri{IsEmriNumarasi=1003,Baslangic=Convert.ToDateTime("2/1/2017 00:00:00"),Bitis=Convert.ToDateTime("2/1/2017 08:00:00")},
                new IsEmri{IsEmriNumarasi=1004,Baslangic=Convert.ToDateTime("2/1/2017 08:00:00"),Bitis=Convert.ToDateTime("2/1/2017 16:00:00")},
                new IsEmri{IsEmriNumarasi=1005,Baslangic=Convert.ToDateTime("2/1/2017 16:00:00"),Bitis=Convert.ToDateTime("3/1/2017 00:00:00")},
                new IsEmri{IsEmriNumarasi=1006,Baslangic=Convert.ToDateTime("3/1/2017 00:00:00"),Bitis=Convert.ToDateTime("3/1/2017 08:00:00")},
                new IsEmri{IsEmriNumarasi=1007,Baslangic=Convert.ToDateTime("3/1/2017 08:00:00"),Bitis=Convert.ToDateTime("3/1/2017 16:00:00")},
                new IsEmri{IsEmriNumarasi=1008,Baslangic=Convert.ToDateTime("3/1/2017 16:00:00"),Bitis=Convert.ToDateTime("4/1/2017 00:00:00")},
                new IsEmri{IsEmriNumarasi=1009,Baslangic=Convert.ToDateTime("4/1/2017 00:00:00"),Bitis=Convert.ToDateTime("4/1/2017 08:00:00")},
                new IsEmri{IsEmriNumarasi=1010,Baslangic=Convert.ToDateTime("4/1/2017 08:00:00"),Bitis=Convert.ToDateTime("4/1/2017 16:00:00")}//ekleme
            };
        }
        public List<IsEmri> GetAll()
        {
            return _isemri;
        }
    }
}
