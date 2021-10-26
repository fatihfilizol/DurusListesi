using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class IsEmriManager : IIsEmriService
    {
        IIsEmriDal _isEmriDal;

        public IsEmriManager(IIsEmriDal isEmriDal)
        {
            _isEmriDal = isEmriDal;
        }

        public List<IsEmri> GetAll()
        {
            return _isEmriDal.GetAll();
        }
    }
}
