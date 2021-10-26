using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entities.Concrete
{
    public class IsEmri:IEntity
    {
        public int IsEmriNumarasi { get; set; }
        public DateTime Baslangic { get; set; }
        public DateTime Bitis { get; set; }
    }
}
