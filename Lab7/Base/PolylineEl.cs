using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;

namespace Lab7
{
    class PolylineEl : INotifyPropertyChanged
    {
        private readonly int _id;
        private double _strokeThickness;
        private Point _p0, _p1, _p2, _p3;
        private PointCollection _points;
        private ObservableCollection<PointValue> _values;
        private SolidColorBrush _color;

        public int Id => _id;
        public double X0 { get => _p0.X; set { _p0.X = value; OnPropertyChanged(); P0 = ""; } }
        public double X1 { get => _p1.X; set { _p1.X = value; OnPropertyChanged(); P1 = ""; } }
        public double X2 { get => _p2.X; set { _p2.X = value; OnPropertyChanged(); P2 = ""; } }
        public double X3 { get => _p3.X; set { _p3.X = value; OnPropertyChanged(); P3 = ""; } }
        public double Y0 { get => _p0.Y; set { _p0.Y = value; OnPropertyChanged(); P0 = ""; } }
        public double Y1 { get => _p1.Y; set { _p1.Y = value; OnPropertyChanged(); P1 = ""; } }
        public double Y2 { get => _p2.Y; set { _p2.Y = value; OnPropertyChanged(); P2 = ""; } }
        public double Y3 { get => _p3.Y; set { _p3.Y = value; OnPropertyChanged(); P3 = ""; } }
        public string P0 { get => $"X = {_p0.X}, Y = {_p0.Y}"; set => OnPropertyChanged(); }
        public string P1 { get => $"X = {_p1.X}, Y = {_p1.Y}"; set => OnPropertyChanged(); }
        public string P2 { get => $"X = {_p2.X}, Y = {_p2.Y}"; set => OnPropertyChanged(); }
        public string P3 { get => $"X = {_p3.X}, Y = {_p3.Y}"; set => OnPropertyChanged(); }
        public SolidColorBrush Color { get => _color; set { _color = value; OnPropertyChanged(); } }
        public double StrokeThickness { get => _strokeThickness; set { _strokeThickness = value; OnPropertyChanged(); } }
        public PointCollection Points { get => _points; set { _points = value; OnPropertyChanged(); } }
        public ObservableCollection<PointValue> Values { get => _values; set { _values = value; OnPropertyChanged(); } }

        public PolylineEl(int id, Point p0, Point p1, Point p2, Point p3, 
            PointCollection points, ObservableCollection<PointValue> values) 
        {
            _id = id;
            _p0 = p0;
            _p1 = p1;
            _p2 = p2;
            _p3 = p3;
            _points = points;
            _values = values;
            _color = Brushes.Red;
            _strokeThickness = 2;
        }

        public PolylineEl(int id, Point p0, Point p1, Point p2, Point p3
            , PointCollection points, ObservableCollection<PointValue> values
            , SolidColorBrush color, double strokeThickness)
        {
            _id = id;
            _p0 = p0;
            _p1 = p1;
            _p2 = p2;
            _p3 = p3;
            _points = points;
            _values = values;
            _color = color;
            _strokeThickness = strokeThickness;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}