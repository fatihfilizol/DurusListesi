using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class DurusManager : IDurusService
    {
        IDurusDal _durusDal;

        public DurusManager(IDurusDal durusDal)
        {
            _durusDal = durusDal;
        }

        public List<Durus> GetAll()
        {
            return _durusDal.GetAll();
        }
    }
}
