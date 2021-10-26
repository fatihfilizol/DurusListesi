using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entities.Concrete
{
    public class Sonuc : IEntity
    {
        public int IsEmri { get; set; }
        public String DurusNedeni { get; set; }
        public Decimal DurusSuresi { get; set; }

    }
}
