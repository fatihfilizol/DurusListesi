using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Abstract
{
    public interface IDurusDal
    {
        List<Durus> GetAll();
    }
}
