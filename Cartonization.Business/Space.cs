using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cartonization.Business
{
    public class Space : IComparable<Space>
    {
        public Space(Dimension dimension)
        {
            Dimension = dimension;
        }

        public Dimension Dimension { get; private set; }

        public decimal Volume
        {
            get
            {
                return Dimension.Width * Dimension.Height * Dimension.Length;
            }
        }

        public decimal SurfaceArea
        {
            get
            {
                return Dimension.Length * Dimension.Width;
            }
        }

        public decimal Width
        {
            get
            {
                return Dimension.Width;
            }
        }

        public decimal Height
        {
            get
            {
                return Dimension.Height;
            }
        }

        public decimal Length
        {
            get
            {
                return Dimension.Length;
            }
        }

        public int CompareTo(Space other)
        {
            if(other == null)
            { return 1; }

            return Dimension.CompareTo(other.Dimension);
        }

        // Define the is greater than operator.
        public static bool operator >=(Space operand1, Space operand2)
        {
            return operand1.CompareTo(operand2) == 1;
        }

        // Define the is less than operator.
        public static bool operator <=(Space operand1, Space operand2)
        {
            return operand1.CompareTo(operand2) == -1;
        }

        public override string ToString()
        {
            return Dimension.ToString() + " SurfaceArea: " + SurfaceArea + ",Volume: " + Volume;
        }
    }
}
