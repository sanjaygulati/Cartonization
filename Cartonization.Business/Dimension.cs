using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cartonization.Business
{
    public class Dimension : IComparable<Dimension>
    {
        public Dimension(double width, double length, double height)
        {
            Length = length;
            Height = height;    
            Width = width;
        }

        public double Length{get;private set;}

        public double Width {get; private set;}

        public double Height { get; private set; }

        public Dimension Clone()
        {
            return (Dimension)this.MemberwiseClone();
        }

        private List<Double> ToList()
        {
            return new List<double> { Width, Length, Height };
        }

        public IEnumerable<Dimension> RotationAngles()
        {
            List<Dimension> rotations = new List<Dimension>();

            Dimension d = TwoDRotate();

            rotations.Add(d);

            List<double> dimensions = this.ToList();

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
            List<double> dimensions = Swap(this.ToList(), 0, 1);

            return new Dimension(dimensions[0], dimensions[1], dimensions[2]);
        }

        private Dimension ThreeDRotate(Dimension d)
        {
            List<double> dimensions = d.ToList();

            double first = dimensions.First();
            dimensions.RemoveAt(0);
            dimensions.Add(first);

            return new Dimension(dimensions[0], dimensions[1], dimensions[2]);
        }

        private List<double> Swap(List<double> dimension, int index1, int index2)
        {
            double temp = dimension[index1];
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
