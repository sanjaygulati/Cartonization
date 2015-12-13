using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cartonization.Business
{
    public class MultipleBoxWithMaxHeight
    {
        private readonly List<Product> _productsToPack;
        private readonly Packer _cartonList;

        public MultipleBoxWithMaxHeight(List<Product> products, List<Carton> availableCartons)
        {
            if (products == null || products.Count == 0)
            {
                throw new ArgumentException("products");
            }

            _cartonList = new Packer(availableCartons);

        }

        public PackerResponse GetCartons()
        {
            return PackCartoonsRecursively();
        }


        private PackerResponse PackCartoonsRecursively(PackerResponse response = null)
        {
            response = response ?? new PackerResponse();

            //Product productToPack = _productsToPack.First();

            Carton bestFitCarton = _cartonList.Pack(_productsToPack);

            if(bestFitCarton != null)
            { 
                foreach (Product product in bestFitCarton.ProductsInCarton)
                {
                    _productsToPack.RemoveAll(p => p.ProductId == product.ProductId);
                }

                response.Cartons.Add(bestFitCarton);
            }
            //else
            //{
            //    response.OversizeProducts.Add(productToPack);

            //    _productsToPack.RemoveAll(p => p.ProductId == productToPack.ProductId);

            //}

            if (_productsToPack.Count > 0)
            {
                return PackCartoonsRecursively(response);
            }

            return response;
        }

    }
}
