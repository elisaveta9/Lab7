namespace Lab7
{
    internal class PointValue
    {
        private readonly double _x, _y, _h;

        public double X => _x;
        public double Y => _y;
        public double H => _h;

        public PointValue(double x, double y, double h)
        {
            _x = x;
            _y = y;
            _h = h;
        }
    }
}
