using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cartonization.Business
{
    public class Product
    {
        public Product(string id ,double height, double length, double width)
        {
            double[] array = new[] {height, width, length};
            double[] result = array.OrderByDescending(d => d).ToArray();
            
            Space = new Space(new Dimension( result[0], result[1], result[2]));
            
            ProductId = id;
        }

        public string ProductId { get; private set; }

        public Space Space { get; private set; }

        public double Width
        {
            get
            {
                return Space.Dimension.Width;
            }
        }

        public double Height
        {
            get
            {
                return Space.Dimension.Height;
            }
        }

        public double Length
        {
            get
            {
                return Space.Dimension.Length;
            }
        }

        public void Rotate2D()
        {
            Dimension newDimension = Space.Dimension.TwoDRotate();
            Space = new Space(newDimension);
        }

        public bool CanRotateTwoDimensional( Space space )
        {
            //3drotate product.
            Dimension newDimension = Space.Dimension.TwoDRotate();
            Space newSpace = new Space(newDimension);
            if (space >= newSpace)
            {
                return true;
            }
            return false;
        }

        public void SetDimension( Dimension d)
        {
            Space = new Space(d);
        }

        public override string ToString() 
        {
            return "ProductId: " + ProductId + "," + Space.ToString();
        }
    }
}
