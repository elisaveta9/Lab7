using System;

namespace Lab7.Base
{
    class CanvasValues : ICloneable
    {
        public double MinX, MinY, MaxX, MaxY;

        public CanvasValues() { }

        public CanvasValues(double minX, double minY, double maxX, double maxY) 
        {
            MinX = minX;
            MinY = minY;
            MaxX = maxX;
            MaxY = maxY;
        }

        public object Clone() => MemberwiseClone();
    }
}
