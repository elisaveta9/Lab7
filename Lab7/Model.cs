using Lab7.Base;
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
        private int _selectItemId = 0;
        private double _x0 = 0, _x1 = 0, _x2 = 0, _x3 = 0,
            _y0 = 0, _y1 = 0, _y2 = 0, _y3 = 0;
        private double _sizeX, _sizeY;
        private bool _useCustomCanvasValues = false;
        private ObservableCollection<PolylineEl> _polylines = new();
        private ObservableCollection<PointValue> _values = new();
        private CanvasValues _autoValues = new() 
        { 
            MaxY = -999999999999999999, MinY = 999999999999999999,
            MaxX = -999999999999999999, MinX = 999999999999999999 }
        , _canvasValues = new()
        {
            MaxY = -999999999999999999, MinY = 999999999999999999,
            MaxX = -999999999999999999, MinX = 999999999999999999
        };

        public int SelectItemId { get => _selectItemId; set { _selectItemId = value; OnPropertyChanged(); } }
        public bool UseCustomCanvasValues { get => _useCustomCanvasValues; set { _useCustomCanvasValues = value; OnPropertyChanged(); } }
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
        public CanvasValues CanvasValues { get => _canvasValues; set { _canvasValues = value; OnPropertyChanged(); } }
        public CanvasValues CanvasAutoValues { get => _autoValues; set { _autoValues = value; OnPropertyChanged(); } }
        public ObservableCollection<PolylineEl> Polylines { get => _polylines; set { _polylines = value; OnPropertyChanged(); } }
        public ObservableCollection<PointValue> Values { get =>  _values; set { _values = value; OnPropertyChanged(); } }

        public ObservableCollection<PointValue> GenerateCurve(CanvasValues initialValues)
        {
            ObservableCollection<PointValue> values = new();
            _autoValues = (CanvasValues)initialValues.Clone();
            for (double t = 0.0; t <= 1.0; t = Math.Round(t + 0.005, 3))
            {
                double x = Math.Pow(1 - t, 3) * _x0 + 3 * t * Math.Pow(1 - t, 2) * _x1 +
                    3 * Math.Pow(t, 2) * (1 - t) * _x2 + Math.Pow(t, 3) * _x3;
                double y = Math.Pow(1 - t, 3) * _y0 + 3 * t * Math.Pow(1 - t, 2) * _y1 +
                    3 * Math.Pow(t, 2) * (1 - t) * _y2 + Math.Pow(t, 3) * _y3;
                if (x > _autoValues.MaxX) _autoValues.MaxX = x;
                if (x < _autoValues.MinX) _autoValues.MinX = x;
                if (y > _autoValues.MaxY) _autoValues.MaxY = y;
                if (y < _autoValues.MinY) _autoValues.MinY = y;
                values.Add(new PointValue(x, y, t));
            }
            return values;
        }

        public PointCollection GeneratePoints(ObservableCollection<PointValue> Values)
        {
            double xD = SizeX / Math.Abs(_canvasValues.MaxX - _canvasValues.MinX), yD = SizeY / Math.Abs(_canvasValues.MaxY - _canvasValues.MinY);
            PointCollection points = new();
            if (Values != null)
            {
                foreach (PointValue point in Values)
                {
                    double x = 5 + (point.X - _canvasValues.MinX) * xD,
                        y = SizeY + 5 - (point.Y - _canvasValues.MinY) * yD;
                    points.Add(new Point(x, y));
                }
            }
            return points;
        } 

        public CanvasValues RefreshCanvasValues()
        {
            CanvasValues canvas = new()
            {
                MaxY = -999999999999999999,
                MinY = 999999999999999999,
                MaxX = -999999999999999999,
                MinX = 999999999999999999
            };
            foreach (PolylineEl polyline in _polylines)
            {
                foreach (PointValue point in polyline.Values)
                {
                    if (point.X > canvas.MaxX) canvas.MaxX = point.X;
                    if (point.X < canvas.MinX) canvas.MinX = point.X;
                    if (point.Y > canvas.MaxY) canvas.MaxY = point.Y;
                    if (point.Y < canvas.MinY) canvas.MinY = point.Y;
                }
            }
            return canvas;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
