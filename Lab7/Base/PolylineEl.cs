﻿using Lab7.CustomControls;
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
        private ColorPicker _color = new();

        public int Id => _id;
        public double X0 { set { _p0.X = value; OnPropertyChanged(); P0 = ""; } }
        public double X1 { set { _p1.X = value; OnPropertyChanged(); P1 = ""; } }
        public double X2 { set { _p2.X = value; OnPropertyChanged(); P2 = ""; } }
        public double X3 { set { _p3.X = value; OnPropertyChanged(); P3 = ""; } }
        public double Y0 { set { _p0.Y = value; OnPropertyChanged(); P0 = ""; } }
        public double Y1 { set { _p1.Y = value; OnPropertyChanged(); P1 = ""; } }
        public double Y2 { set { _p2.Y = value; OnPropertyChanged(); P2 = ""; } }
        public double Y3 { set { _p3.Y = value; OnPropertyChanged(); P3 = ""; } }
        public string P0 { get => $"X = {_p0.X}, Y = {_p0.Y}"; set => OnPropertyChanged(); }
        public string P1 { get => $"X = {_p1.X}, Y = {_p1.Y}"; set => OnPropertyChanged(); }
        public string P2 { get => $"X = {_p2.X}, Y = {_p2.Y}"; set => OnPropertyChanged(); }
        public string P3 { get => $"X = {_p3.X}, Y = {_p3.Y}"; set => OnPropertyChanged(); }
        public ColorPicker Color { get => _color; set { ColorBrush = value.SelectedColor; OnPropertyChanged(); } }
        public Brush ColorBrush { get => Color.SelectedColor; set { Color.SelectedColor = value; OnPropertyChanged(); } }
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
            _color.SelectedColor = Brushes.Red;
            _strokeThickness = 2;
        }

        public PolylineEl(int id, Point p0, Point p1, Point p2, Point p3
            , PointCollection points, ObservableCollection<PointValue> values
            , Brush color, double strokeThickness)
        {
            _id = id;
            _p0 = p0;
            _p1 = p1;
            _p2 = p2;
            _p3 = p3;
            _points = points;
            _values = values;
            _color.SelectedColor = color;
            _strokeThickness = strokeThickness;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}