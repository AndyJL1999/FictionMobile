using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FictionMobile.Interfaces
{
    public interface IHasCollectionView
    {
        public CarouselView CarouselView { get; }
    }
}
