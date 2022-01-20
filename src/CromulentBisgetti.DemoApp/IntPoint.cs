using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CromulentBisgetti.ContainerPacking.Entities
{
    [DebuggerDisplay("{ToString(),nq}")]
    public struct IntPoint
    {
        public int X { get; set; }
        public int Y { get; set; }

        public IntPoint(decimal x, decimal y)
        {
            X = (int)x;
            Y = (int)y;
        }
        public IntPoint(int x, int y)
        {
            X = (int)x;
            Y = (int)y;
        }
        public IntPoint(double x, double y)
        {
            X = (int)x;
            Y = (int)y;
        }

        public void Transpose()
        {
            int tempX = X;
            X = Y;
            Y = tempX;
        }
        public override bool Equals(object obj)
        {
            if (!(obj is IntPoint))
                return false;

            IntPoint target = (IntPoint)obj;
            return (X == target.X && 
                Y == target.Y);
        }
        public override int GetHashCode()
        {
            int hash = 23;
            hash = hash * 31 + X.GetHashCode();
            hash = hash * 31 + Y.GetHashCode();
            return hash;
        }

        public override string ToString()
        {
            return $"({X},{Y})";
        }
    }
}
