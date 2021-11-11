using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Concrete.InMemory
{
    public class InMemoryDurusDal : IDurusDal
    {
        List<Durus> _duruslar;
        public InMemoryDurusDal()
        {
            _duruslar = new List<Durus> {
                new Durus{DurusNedeni="Mola",Baslangic=Convert.ToDateTime("1.1.2017 10:00:00"),Bitis=Convert.ToDateTime("1.1.2017 10:10:00")},
                new Durus{DurusNedeni="Arıza",Baslangic=Convert.ToDateTime("1.1.2017 10:30:00"),Bitis=Convert.ToDateTime("1.1.2017 11:00:00")},
                new Durus{DurusNedeni="Mola",Baslangic=Convert.ToDateTime("1.1.2017 12:00:00"),Bitis=Convert.ToDateTime("1.1.2017 12:30:00")},
                new Durus{DurusNedeni="Mola",Baslangic=Convert.ToDateTime("1.1.2017 14:00:00"),Bitis=Convert.ToDateTime("1.1.2017 14:10:00")},
                new Durus{DurusNedeni="Setup",Baslangic=Convert.ToDateTime("1.1.2017 15:00:00"),Bitis=Convert.ToDateTime("1.1.2017 16:30:00")},
                new Durus{DurusNedeni="Mola",Baslangic=Convert.ToDateTime("1.1.2017 18:00:00"),Bitis=Convert.ToDateTime("1.1.2017 18:10:00")},
                new Durus{DurusNedeni="Mola",Baslangic=Convert.ToDateTime("1.1.2017 20:00:00"),Bitis=Convert.ToDateTime("1.1.2017 20:30:00")},
                new Durus{DurusNedeni="Mola",Baslangic=Convert.ToDateTime("1.1.2017 22:00:00"),Bitis=Convert.ToDateTime("1.1.2017 22:10:00")},
                new Durus{DurusNedeni="Arge",Baslangic=Convert.ToDateTime("1.1.2017 23:00:00"),Bitis=Convert.ToDateTime("2.1.2017 08:30:00")},
                new Durus{DurusNedeni="Mola",Baslangic=Convert.ToDateTime("2.1.2017 10:00:00"),Bitis=Convert.ToDateTime("2.1.2017 10:10:00")},
                new Durus{DurusNedeni="Mola",Baslangic=Convert.ToDateTime("2.1.2017 12:00:00"),Bitis=Convert.ToDateTime("2.1.2017 12:30:00")},
                new Durus{DurusNedeni="Arıza",Baslangic=Convert.ToDateTime("2.1.2017 13:00:00"),Bitis=Convert.ToDateTime("2.1.2017 13:45:00")},
                new Durus{DurusNedeni="Mola",Baslangic=Convert.ToDateTime("2.1.2017 14:00:00"),Bitis=Convert.ToDateTime("2.1.2017 14:10:00")},
                new Durus{DurusNedeni="Mola",Baslangic=Convert.ToDateTime("2.1.2017 18:00:00"),Bitis=Convert.ToDateTime("2.1.2017 18:10:00")},
                new Durus{DurusNedeni="Arge",Baslangic=Convert.ToDateTime("2.1.2017 20:00:00"),Bitis=Convert.ToDateTime("3.1.2017 02:10:00")},
                new Durus{DurusNedeni="Mola",Baslangic=Convert.ToDateTime("3.1.2017 04:00:00"),Bitis=Convert.ToDateTime("3.1.2017 04:30:00")},
                new Durus{DurusNedeni="Setup",Baslangic=Convert.ToDateTime("3.1.2017 06:00:00"),Bitis=Convert.ToDateTime("3.1.2017 09:30:00")},
                new Durus{DurusNedeni="Mola",Baslangic=Convert.ToDateTime("3.1.2017 10:00:00"),Bitis=Convert.ToDateTime("3.1.2017 10:10:00")},
                new Durus{DurusNedeni="Mola",Baslangic=Convert.ToDateTime("3.1.2017 12:00:00"),Bitis=Convert.ToDateTime("3.1.2017 12:30:00")},
                new Durus{DurusNedeni="Mola",Baslangic=Convert.ToDateTime("3.1.2017 14:00:00"),Bitis=Convert.ToDateTime("3.1.2017 14:10:00")},
                new Durus{DurusNedeni="Arıza",Baslangic=Convert.ToDateTime("3.1.2017 15:00:00"),Bitis=Convert.ToDateTime("3.1.2017 18:45:00")},
                new Durus{DurusNedeni="Mola",Baslangic=Convert.ToDateTime("3.1.2017 20:00:00"),Bitis=Convert.ToDateTime("3.1.2017 20:30:00")},
                new Durus{DurusNedeni="Mola",Baslangic=Convert.ToDateTime("3.1.2017 22:00:00"),Bitis=Convert.ToDateTime("3.1.2017 22:10:00")},
                new Durus{DurusNedeni="Elektrik Arıza",Baslangic=Convert.ToDateTime("4.1.2017 03:00:00"),Bitis=Convert.ToDateTime("4.1.2017 05:30:00")},
                new Durus{DurusNedeni="Resmi Tatil",Baslangic=Convert.ToDateTime("4.1.2017 9:00:00"),Bitis=Convert.ToDateTime("4.1.2017 09:30:00")}
            };
        }
        public List<Durus> GetAll()
        {
            return _duruslar;
        }
    }
}
