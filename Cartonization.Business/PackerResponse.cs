using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cartonization.Business
{
    public class PackerResponse
    {
        private List<Product> _oversizeProducts;
        private List<Product> _badDimensionProducts;
        private List<Carton> _cartons;

        public List<Carton> Cartons 
        {
            get
            {
                return _cartons ?? (_cartons = new List<Carton>());
            }
        }

        public List<Product> OversizeProducts { get { return _oversizeProducts ?? (_oversizeProducts = new List<Product>()); } }

        public List<Product> BadDimensionProducts { get { return _badDimensionProducts ?? (_badDimensionProducts = new List<Product>()); } }
    }
}
