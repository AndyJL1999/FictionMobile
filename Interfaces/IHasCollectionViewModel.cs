using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FictionMobile.Interfaces
{
    public interface IHasCollectionViewModel
    {
        public IHasCollectionView View { get; set; }
    }
}
