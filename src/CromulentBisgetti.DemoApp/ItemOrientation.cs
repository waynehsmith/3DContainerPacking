using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CPE = CromulentBisgetti.ContainerPacking.Entities;
#if UWP
using Windows.Foundation;
using Windows.UI.Xaml.Controls;
#else
namespace CromulentBisgetti.ContainerPacking.Entities
{
    
    public enum Orientation
    {
        Vertical,
        Horizontal
    }
}
#endif

namespace CromulentBisgetti.ContainerPacking.Entities
{



    [DebuggerDisplay("{ToString(),nq}")]
    public class ItemOrientation
    {
        public ItemOrientation() { }
        public ItemOrientation(CPE.Item item)
        {
            // Start with horizontal orientation

            Orientation direction;
            var original = new IntPoint(item.Dim2, item.Dim1);
            var dimensions = new IntPoint(item.Dim2, item.Dim1);
            direction = (original.X >= original.Y) ? Orientation.Horizontal : Orientation.Vertical;
            bool rotated = false;
            Origin = new IntPoint(item.CoordZ, item.CoordX);
            if (item.PackDimX > 0)
            {
                var packedPoint = new IntPoint(
                    item.PackDimZ, item.PackDimX);

                rotated = !original.Equals(packedPoint);
                if (rotated)
                {
                    direction = (direction == Orientation.Horizontal) ? Orientation.Vertical : Orientation.Horizontal;
                    dimensions.Transpose();
                }
            }
            Size = dimensions;
            Orientation = direction;
        }
        public IntPoint Origin { get; set; }
        public IntPoint Size { get; set; }
        public Orientation Orientation { get; set; }

        public override string ToString()
        {
            return $"{Origin} {Size} {Orientation}";
        }
    }
}
