using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Abstract
{
    public interface IIsEmriService
    {
        List<IsEmri> GetAll();
    }
}
