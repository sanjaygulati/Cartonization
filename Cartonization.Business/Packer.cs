using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cartonization.Business
{
    public class Packer
    {
        private readonly List<Carton> _availableCartons;
        private readonly PackerResponse _packerResponse = new PackerResponse();

        public Packer(List<Carton> availableCartons)
        {
            _availableCartons = availableCartons;
        }

        private IEnumerable<Carton> FilterCartons(List<Product> productsToPack)
        {
            Product largestProduct = productsToPack.OrderByDescending(p => p.Space.SurfaceArea).First();

            IEnumerable<Carton> cartons = _availableCartons.Where(c =>
                    c.Width >= largestProduct.Width
                    && c.Height >= largestProduct.Height
                    && c.Length >= largestProduct.Length);

            if (cartons.Count() == 0)
            {
                _packerResponse.OversizeProducts.Add(largestProduct);
                productsToPack.RemoveAll(p => p.ProductId == largestProduct.ProductId);
                return FilterCartons(productsToPack);
            }

            List<Carton> cartonList = new List<Carton>();
            cartonList.AddRange(cartons.Select(c => new Carton(c.Id, c.Height, c.Length, c.Width)));
            return cartonList;
        }


        public PackerResponse Pack(List<Product> products)
        {
            if (products.Count == 0 || _availableCartons.Count == 0)
            {
                return new PackerResponse();
            }

            products.ForEach(product =>
            {
                if (product.Height <= 0 || product.Width <= 0 || product.Length <= 0)
                {
                    _packerResponse.BadDimensionProducts.Add(product);
                    products.RemoveAll(p => p.ProductId == product.ProductId);
                }
            });


            return PackRecursively(products);
        }

        private PackerResponse PackRecursively(List<Product> productsToPack)
        {
            
            // filter cartons.
            IEnumerable<Carton> potentialCartons = FilterCartons(productsToPack);

            // sort the cartons by volume so that packer packs the carton with minimum volume first.
            potentialCartons = potentialCartons.OrderBy(c => c.Space.Volume).ToList();

            if (potentialCartons.Count() == 0)
            {
                return null;
            }

            Carton potentialCarton = null;

            foreach (Carton carton in potentialCartons)
            {
                //TODO: build factory to return the algorithm.
                LargestAreaFirstFitAlgorithm algorithm = new LargestAreaFirstFitAlgorithm(carton);

                productsToPack = productsToPack.Select(p => new Product(p.ProductId, p.Height, p.Length, p.Width)).ToList();

                algorithm.Pack(productsToPack);

                if (productsToPack.Except(carton.ProductsInCarton).Count() == 0)
                {
                    potentialCarton = carton;

                    break;
                }
            }

            potentialCarton = potentialCarton ?? potentialCartons.OrderByDescending(c=>c.ProductsInCarton.Count).FirstOrDefault();

            _packerResponse.Cartons.Add(potentialCarton);

            List<Product> remainingProducts = productsToPack.Except(potentialCarton.ProductsInCarton).ToList();

            if (remainingProducts.Count > 0)
            {
                PackRecursively(remainingProducts);
            }

            return _packerResponse;
        }
    }
}
