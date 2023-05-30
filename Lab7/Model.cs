using Lab7.Base;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace Lab7
{
    internal class Model
    {
        public int SelectItemId { get; set; }

        public bool VisibilityMenu { get; set; } = true;

        public bool VisibilityAddPolyline { get; set; } = false;

        public bool VisibilityUpdatePolyline { get; set; } = false;

        public bool VisibilityCanvasValues { get; set; } = false;

        public bool VisibilityPolylineCollection { get; set; } = false;

        public bool VisibilityPolylineValues { get; set; } = false;

        public bool AddNewPolyline { get; set; } = false;

        public bool UseCustomCanvasValues { get; set; } = false;

        public double X0 { get; set; } = 0;

        public double X1 { get; set; } = 0;

        public double X2 { get; set; } = 0;

        public double X3 { get; set; } = 0;

        public double Y0 { get; set; } = 0;

        public double Y1 { get; set; } = 0;

        public double Y2 { get; set; } = 0;

        public double Y3 { get; set; } = 0;

        public CanvasValues CanvasValues { get; set; } = new(999999999999999999, 999999999999999999, -999999999999999999, -999999999999999999);

        public CanvasValues CanvasAutoValues { get; set; } = new(999999999999999999, 999999999999999999, -999999999999999999, -999999999999999999);

        public ObservableCollection<PolylineEl> Polylines { get; set; } = new();

        public SolidColorBrush SelectBrush { get; set; } = Brushes.Black;
    }
}
