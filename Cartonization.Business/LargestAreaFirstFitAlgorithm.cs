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

        public void Pack(List<Product> productsToPack)
        {
            PackLevel(ref productsToPack, _carton.Space);
        }

        private void PackLevel(ref List<Product> productsToPack, Space space, int level = 0)
        {
            if (productsToPack.Count == 0) return;

            // sort the product with widest surface area.
            productsToPack = productsToPack.OrderByDescending(p => p.Space.SurfaceArea).ToList();

            Product biggestProduct = productsToPack.First();

            // Check if a biggest product can be added.
            if (!_carton.CanAddProductWithoutHeightOverflow(biggestProduct))
            {
                // if remaining height can be used, then fill it with fillable products.
                if (_carton.Height - _carton.UsedHeight > 0)
                {
                    FillSpace(
                    ref productsToPack,
                    new Space(new Dimension(
                            width: _carton.Width,
                            length: _carton.Length,
                            height: _carton.Height - _carton.UsedHeight
                    )),
                    level);
                }

                return;
            }

            if (biggestProduct.CanRotateTwoDimensional(_carton.Space))
            {
                biggestProduct.Rotate2D();
            }

            _carton.Add(level, biggestProduct);

            productsToPack.RemoveAll(p => p.ProductId == biggestProduct.ProductId);

            List<Space> availableSpaces = new List<Space>();

            if (_carton.Length - biggestProduct.Length > 0)
            {
                availableSpaces.Add(new Space(
                    new Dimension(
                        width: _carton.Width,
                        length: _carton.Length - biggestProduct.Length,
                        height: biggestProduct.Height
                        )
                    ));
            }

            if (_carton.Width - biggestProduct.Width > 0)
            {
                availableSpaces.Add(new Space(
                    new Dimension(
                        width: _carton.Width - biggestProduct.Width,
                        length: biggestProduct.Length,
                        height: biggestProduct.Height
                        )
                    ));
            }

            foreach (Space s in availableSpaces)
            {
                FillSpace(ref productsToPack, s, level);
            }

            if (productsToPack.Count > 0)
            {
                PackLevel(ref productsToPack, space, ++level);
            }

        }

        /// <summary>
        /// Fills space with products recursively
        /// </summary>
        /// <param name="productsToPack"></param>
        /// <param name="remainingSpace"></param>
        private void FillSpace(ref List<Product> productsToPack, Space remainingSpace, decimal level)
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

            Dictionary<decimal, List<Space>> availableSpaces = new Dictionary<decimal, List<Space>>();

            if (remainingSpace.Length - fittingProduct.Length > 0)
            {
                AddAvailableSpace(level, new Space(
                        new Dimension(
                            width: remainingSpace.Width,
                            length: remainingSpace.Length - fittingProduct.Length,
                            height: remainingSpace.Height
                            )
                        ), availableSpaces
                    );
            }

            if (remainingSpace.Width - fittingProduct.Width > 0)
            {
                AddAvailableSpace(level, new Space(
                    new Dimension(
                        width: remainingSpace.Width - fittingProduct.Width,
                        length: fittingProduct.Length,
                        height: remainingSpace.Height
                        )
                    ), availableSpaces
                    );
            }

            if (remainingSpace.Height - fittingProduct.Height > 0)
            {
                AddAvailableSpace(level + 0.1m, new Space(
                    new Dimension(
                            width: fittingProduct.Width,
                            length: fittingProduct.Length,
                            height: remainingSpace.Height - fittingProduct.Height
                        )
                    ), availableSpaces
                    );
            }

            foreach (KeyValuePair<decimal, List<Space>> availableSpace in availableSpaces)
            {
                foreach (Space space in availableSpace.Value)
                {
                    FillSpace(ref productsToPack, space, availableSpace.Key);
                }
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

        private void AddAvailableSpace(decimal level, Space space, Dictionary<decimal, List<Space>> spaces)
        {
            if (spaces.ContainsKey(level))
            {
                spaces[level].Add(space);
            }
            else
            {
                spaces.Add(level, new List<Space>() { space });
            }
        }

    }
}
