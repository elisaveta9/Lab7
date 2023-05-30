using Lab7.Base;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace Lab7.Common.Converters
{
    internal class PointCollectionConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            double sizeX = (double)values[0] - 20, sizeY = (double)values[1] - 20, xD, yD;
            CanvasValues canvasValues = (CanvasValues)values[2];
            ObservableCollection <PointValue> pointValues = (ObservableCollection <PointValue>)values[3];
            xD = canvasValues.MaxX == canvasValues.MinX ? sizeX / 2 : sizeX / Math.Abs(canvasValues.MaxX - canvasValues.MinX);
            yD = canvasValues.MaxY == canvasValues.MinY ? sizeY / 2 : sizeY / Math.Abs(canvasValues.MaxY - canvasValues.MinY);      
            PointCollection points = new();
            if (pointValues != null)
            {
                foreach (PointValue point in pointValues)
                {
                    double x = 10 + (point.X - canvasValues.MinX) * xD,
                        y = sizeY + 10 - (point.Y - canvasValues.MinY) * yD;
                    points.Add(new Point(x, y));
                }
            }
            return points;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
