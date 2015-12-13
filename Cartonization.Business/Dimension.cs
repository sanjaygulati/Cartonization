using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cartonization.Business
{
    public class Dimension : IComparable<Dimension>
    {
        public Dimension(decimal width, decimal length, decimal height)
        {
            Length = length;
            Height = height;    
            Width = width;
        }

        public decimal Length{get;private set;}

        public decimal Width {get; private set;}

        public decimal Height { get; private set; }

        public Dimension Clone()
        {
            return (Dimension)this.MemberwiseClone();
        }

        private List<decimal> ToList()
        {
            return new List<decimal> { Width, Length, Height };
        }

        public IEnumerable<Dimension> RotationAngles()
        {
            List<Dimension> rotations = new List<Dimension>();

            Dimension d = TwoDRotate();

            rotations.Add(d);

            List<decimal> dimensions = this.ToList();

            for (int i = 1; i < dimensions.Count; i++)
            {
                d = ThreeDRotate(d);

                rotations.Add(d);

                d = TwoDRotate();

                rotations.Add(d);
            }

            return rotations;
        }
       
        public Dimension TwoDRotate()
        {
            List<decimal> dimensions = Swap(this.ToList(), 0, 1);

            return new Dimension(dimensions[0], dimensions[1], dimensions[2]);
        }

        private Dimension ThreeDRotate(Dimension d)
        {
            List<decimal> dimensions = d.ToList();

            decimal first = dimensions.First();
            dimensions.RemoveAt(0);
            dimensions.Add(first);

            return new Dimension(dimensions[0], dimensions[1], dimensions[2]);
        }

        private List<decimal> Swap(List<decimal> dimension, int index1, int index2)
        {
            decimal temp = dimension[index1];
            dimension[index1] = dimension[index2];
            dimension[index2] = temp;
            return dimension;
        }

        public override string ToString()
        {
            return "Width: " + Width + ",Length: " + Length + ",Height: " + Height;
        }

        public int CompareTo(Dimension other)
        {
            if(other == null) return 1;

            if (Length >= other.Length && Height >= other.Height && Width >= other.Width) return 1;

            return -1;
        }
    }
}
