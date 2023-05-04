using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;

namespace Lab7
{
    internal class Model
    {
        private double _x0 = 0, _x1 = 0, _x2 = 0, _x3 = 0,
            _y0 = 0, _y1 = 0, _y2 = 0, _y3 = 0;
        private double minX, minY, maxX, maxY, _sizeX, _sizeY;
        private ObservableCollection<PointValue> _values = new();
        private PointCollection _points = new();

        public double X0 { get => _x0; set { _x0 = value; OnPropertyChanged(); } }
        public double X1 { get => _x1; set { _x1 = value; OnPropertyChanged(); } }
        public double X2 { get => _x2; set { _x2 = value; OnPropertyChanged(); } }
        public double X3 { get => _x3; set { _x3 = value; OnPropertyChanged(); } }
        public double Y0 { get => _y0; set { _y0 = value; OnPropertyChanged(); } }
        public double Y1 { get => _y1; set { _y1 = value; OnPropertyChanged(); } }
        public double Y2 { get => _y2; set { _y2 = value; OnPropertyChanged(); } }
        public double Y3 { get => _y3; set { _y3 = value; OnPropertyChanged(); } }
        public double SizeX { get => _sizeX; set { _sizeX = value; OnPropertyChanged(); } }
        public double SizeY { get => _sizeY; set { _sizeY = value; OnPropertyChanged(); } }
        public ObservableCollection<PointValue> Values { get => _values; set { _values = value; OnPropertyChanged(); } }
        public PointCollection Points { get => _points; set { _points = value; OnPropertyChanged(); } }

        public ObservableCollection<PointValue> GenerateCurve()
        {
            maxY = -999999999999999999; minY = 999999999999999999;
            maxX = -999999999999999999; minX = 999999999999999999;
            ObservableCollection<PointValue> values = new(); _points = new();
            for (double t = 0.0; t <= 1.0; t = Math.Round(t + 0.005, 3))
            {
                double x = Math.Pow(1 - t, 3) * _x0 + 3 * t * Math.Pow(1 - t, 2) * _x1 +
                    3 * Math.Pow(t, 2) * (1 - t) * _x2 + Math.Pow(t, 3) * _x3;
                double y = Math.Pow(1 - t, 3) * _y0 + 3 * t * Math.Pow(1 - t, 2) * _y1 +
                    3 * Math.Pow(t, 2) * (1 - t) * _y2 + Math.Pow(t, 3) * _y3;
                if (x >  maxX) maxX = x;
                if (x < minX) minX = x;
                if (y > maxY) maxY = y;
                if (y < minY) minY = y;
                values.Add(new PointValue(x, y, t));
            }
            return values;
        }

        public PointCollection GeneratePoints()
        {
            double xD = SizeX / Math.Abs(maxX - minX), yD = SizeY / Math.Abs(maxY - minY);
            PointCollection points = new();
            if (Values != null)
            {
                foreach (PointValue point in Values)
                {
                    double x = 5 + (point.X - minX) * xD,
                        y = SizeY + 5 - (point.Y - minY) * yD;
                    points.Add(new Point(x, y));
                }
            }
            return points;
        } 

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
