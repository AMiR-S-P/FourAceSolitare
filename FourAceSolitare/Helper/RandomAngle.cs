using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourAceSolitaire.Helper
{
    public static class RandomAngle
    {
        public static double Angle { get => new Random(Guid.NewGuid().GetHashCode()).Next(-30, 30); }


    }
}
