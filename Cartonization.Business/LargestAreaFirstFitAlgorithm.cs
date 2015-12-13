using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cartonization.Business
{
    public class LargestAreaFirstFitAlgorithm
    {
        private Carton _carton;

        public LargestAreaFirstFitAlgorithm(Carton carton)
        {
            _carton = carton;
        }

        public void Pack(List<Product> productsToPack )
        {
            PackLevel(ref productsToPack);
        }

        private void PackLevel(ref List<Product> productsToPack, int level = 0)
        {
            if (productsToPack.Count == 0) return;

            // sort the product with widest surface area.
            productsToPack = productsToPack.OrderByDescending(p => p.Space.SurfaceArea).ToList();

            Product biggestProduct = productsToPack.First();

            // Check if a new level can be added.
            if (!_carton.CanAddLevel(biggestProduct.Space.Dimension.Height)) { return; }

            _carton.Add(level, biggestProduct);

            productsToPack.RemoveAll(p=> p.ProductId == biggestProduct.ProductId);

            if (_carton.Space <= biggestProduct.Space)
            {
                PackLevel(ref productsToPack, level++);
            }
            else 
            {
                List<Space> availableSpaces = new List<Space>();

                if (biggestProduct.CanRotateTwoDimensional(_carton.Space))
                {
                    biggestProduct.Rotate2D();
                }

                if (_carton.Length - biggestProduct.Length > 0)
                {
                    availableSpaces.Add( new Space(
                        new Dimension(
                            width: _carton.Width,
                            length: _carton.Length - biggestProduct.Length,
                            height: biggestProduct.Height
                            )
                        ));
                }

                if (_carton.Width - biggestProduct.Width > 0)
                {
                    availableSpaces.Add( new Space(
                        new Dimension(
                            width: _carton.Width - biggestProduct.Width,
                            length: biggestProduct.Length,
                            height: biggestProduct.Height
                            )
                        ));
                }

                foreach (Space space in availableSpaces)
                {
                    FillSpace(ref productsToPack, space, level);
                }

                if (productsToPack.Count > 0)
                {
                    PackLevel(ref productsToPack, ++level);
                }
            }

            
        }

        /// <summary>
        /// Fills space with products recursively
        /// </summary>
        /// <param name="productsToPack"></param>
        /// <param name="remainingSpace"></param>
        private void FillSpace(ref List<Product> productsToPack, Space remainingSpace, int level)
        {
            if (productsToPack.Count == 0) return;

            productsToPack = productsToPack.OrderByDescending(p => p.Space.Volume).ToList();

            Product fittingProduct = null;

            foreach (Product product in productsToPack)
            {
                // Skip products that have a higher volume than target space.
                if (product.Space.Volume >= remainingSpace.Volume) continue;

                if (!CanFitProductWithThreeDRotation(product, remainingSpace)) continue;

                fittingProduct = product;
                
                break;
            }

            if (fittingProduct == null) return;

            _carton.Add(level, fittingProduct);

            productsToPack.RemoveAll(p => p.ProductId == fittingProduct.ProductId);

            List<Space> availableSpaces = new List<Space>();

            if (remainingSpace.Length - fittingProduct.Length > 0)
            {
                availableSpaces.Add(new Space(
                    new Dimension(
                        width: remainingSpace.Width,
                        length: remainingSpace.Length - fittingProduct.Length,
                        height: remainingSpace.Height
                        )
                    ));
            }

            if (remainingSpace.Width - fittingProduct.Width > 0)
            {
                availableSpaces.Add(new Space(
                    new Dimension(
                        width: remainingSpace.Width - fittingProduct.Width,
                        length: fittingProduct.Length,
                        height: remainingSpace.Height
                        )
                    ));
            }

            foreach (Space space in availableSpaces)
            {
                FillSpace(ref productsToPack, space, level);
            }

        }

        /// <summary>
        /// Check if box fits in specified space and rotate (3d) if necessary.
        /// </summary>
        /// <param name="product"></param>
        /// <param name="space"></param>
        /// <returns></returns>
        private bool CanFitProductWithThreeDRotation(Product product, Space space)
        {
            //3drotate product.
            foreach (Dimension d in product.Space.Dimension.RotationAngles())
            {
                Space newSpace = new Space(d);
                if (space >= newSpace)
                {
                    product.SetDimension(d);
                    return true;
                }
            }
            return false;
        }

        

    }
}
