using Lab7.Base;
using Lab7.Common;
using Lab7.CustomControls;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;

namespace Lab7
{
    internal class ViewModel : INotifyPropertyChanged
    {
        private static ViewModel viewModel;
        private readonly Model model;

        public int autoId = 0;
        private bool _visibilityAddPolyline = false, _visibilityUpdatePolyline = false, _visibilityCanvasValues = false
            , _visibilityPolylineCollection = false, _visibilityPolylineValues = false, _visibilityMenu = true
            , _addNewPolyline = false;
        private double _strokeThickness = 0;
        private CanvasValues _initialValues;
        private Brush selectColorPicker = Brushes.Black;
        private PolylineEl selectElement;

        private ViewModel() => model = new();

        public static ViewModel GetInstance => viewModel ??= new();

        public int SelectItemId { get => model.SelectItemId; private set {  model.SelectItemId = value; OnPropertyChanged(); } }
        public bool VisibilityMenu { get => _visibilityMenu; private set { _visibilityMenu = value; OnPropertyChanged(); } }
        public bool VisibilityAddPolyline { get => _visibilityAddPolyline; private set { _visibilityAddPolyline = value; OnPropertyChanged(); } }
        public bool VisibilityUpdatePolyline { get => _visibilityUpdatePolyline; private set { _visibilityUpdatePolyline= value; OnPropertyChanged(); } }
        public bool VisibilityCanvasValues { get => _visibilityCanvasValues; private set { _visibilityCanvasValues= value; OnPropertyChanged(); } }
        public bool VisibilityPolylineCollection { get => _visibilityPolylineCollection; private set { _visibilityPolylineCollection = value; OnPropertyChanged(); } }
        public bool VisibilityPolylineValues { get => _visibilityPolylineValues; private set { _visibilityPolylineValues = value; OnPropertyChanged(); } }
        public string StrokeThickness { get => $"{_strokeThickness}"; set { _strokeThickness = double.Parse(value); OnPropertyChanged(); } }
        public string X0
        { 
            get => $"{model.X0}"; 
            set 
            { 
                model.X0 = double.Parse(value); 
                OnPropertyChanged(); 
                if (_addNewPolyline) 
                { 
                    UpdateCurve();
                    Polylines.Where(x => x.Id == SelectItemId).First().X0 = model.X0;
                } 
            } 
        }
        public string X1
        {
            get => $"{model.X1}";
            set
            {
                model.X1 = double.Parse(value);
                OnPropertyChanged();
                if (_addNewPolyline)
                {
                    UpdateCurve();
                    Polylines.Where(x => x.Id == SelectItemId).First().X1 = model.X1;
                }
            }
        }
        public string X2
        {
            get => $"{model.X2}";
            set
            {
                model.X2 = double.Parse(value);
                OnPropertyChanged();
                if (_addNewPolyline)
                {
                    UpdateCurve();
                    Polylines.Where(x => x.Id == SelectItemId).First().X2 = model.X2;
                }
            }
        }
        public string X3
        {
            get => $"{model.X3}";
            set
            {
                model.X3 = double.Parse(value);
                OnPropertyChanged();
                if (_addNewPolyline)
                {
                    UpdateCurve();
                    Polylines.Where(x => x.Id == SelectItemId).First().X3 = model.X3;
                }
            }
        }
        public string Y0
        {
            get => $"{model.Y0}";
            set
            {
                model.Y0 = double.Parse(value);
                OnPropertyChanged();
                if (_addNewPolyline)
                {
                    UpdateCurve();
                    Polylines.Where(x => x.Id == SelectItemId).First().Y0 = model.Y0;
                }
            }
        }
        public string Y1
        {
            get => $"{model.Y1}";
            set
            {
                model.Y1 = double.Parse(value);
                OnPropertyChanged();
                if (_addNewPolyline)
                {
                    UpdateCurve();
                    Polylines.Where(x => x.Id == SelectItemId).First().Y1 = model.Y1;
                }
            }
        }
        public string Y2
        {
            get => $"{model.Y2}";
            set
            {
                model.Y2 = double.Parse(value);
                OnPropertyChanged();
                if (_addNewPolyline)
                {
                    UpdateCurve();
                    Polylines.Where(x => x.Id == SelectItemId).First().Y2 = model.Y2;
                }
            }
        }
        public string Y3
        {
            get => $"{model.Y3}";
            set
            {
                model.Y3 = double.Parse(value);
                OnPropertyChanged();
                if (_addNewPolyline)
                {
                    UpdateCurve();
                    Polylines.Where(x => x.Id == SelectItemId).First().Y3 = model.Y3;
                }
            }
        }
        public string CanvasMinX { get => $"{model.CanvasValues.MinX}"; set { model.CanvasValues.MinX = double.Parse(value); OnPropertyChanged(); model.UseCustomCanvasValues = true; UpdatePoints(); } }
        public string CanvasMinY { get => $"{model.CanvasValues.MinY}"; set { model.CanvasValues.MinY = double.Parse(value); OnPropertyChanged(); model.UseCustomCanvasValues = true; UpdatePoints(); } }
        public string CanvasMaxX { get => $"{model.CanvasValues.MaxX}"; set { model.CanvasValues.MaxX = double.Parse(value); OnPropertyChanged(); model.UseCustomCanvasValues = true; UpdatePoints(); } }
        public string CanvasMaxY { get => $"{model.CanvasValues.MaxY}"; set { model.CanvasValues.MaxY = double.Parse(value); OnPropertyChanged(); model.UseCustomCanvasValues = true; UpdatePoints(); } }
        public double MaxX { set { model.SizeX = value - 10; OnPropertyChanged(); UpdatePoints(); } }
        public double MaxY { set { model.SizeY = value - 10; OnPropertyChanged(); UpdatePoints(); } }
        public ObservableCollection<PolylineEl> Polylines { get => model.Polylines; private set { model.Polylines = value; OnPropertyChanged(); } }
        public ObservableCollection<PointValue> Values 
        {
            get
            {
                var item = Polylines.Where(x => x.Id == SelectItemId).FirstOrDefault();
                if (item == null)
                    return new();
                return item.Values;
            }
            private set 
            {
                var item = Polylines.Where(x => x.Id == SelectItemId).FirstOrDefault();
                if (item != null) 
                { 
                    item.Values = value;
                    OnPropertyChanged(); 
                }
            }
        }
        public PointCollection Points
        {
            get  
            {
                var item = Polylines.Where(x => x.Id == SelectItemId).FirstOrDefault();
                if (item == null)
                    return new();
                return item.Points;
            }
            private set
            {
                var item = Polylines.Where(x => x.Id == SelectItemId).FirstOrDefault();
                if (item != null)
                {
                    item.Points = value;
                    OnPropertyChanged();
                }
            }
        }
        public Brush SelectColorPicker { get => selectColorPicker; set { selectColorPicker = value; OnPropertyChanged(); } }
        public PolylineEl SelectElement { get => selectElement; set { selectElement = value; OnPropertyChanged(); VisibilityPolylineValues = false; } }

        public void CreateNewPolyline()
        {
            var polyline = new PolylineEl(++autoId, new Point(model.X0, model.Y0), new Point(model.X1, model.Y1)
                , new Point(model.X2, model.Y2), new Point(model.X3, model.Y3), new(), new());
            Polylines.Add(polyline);
        }

        public void UpdateCurve()
        {
            Values = model.GenerateCurve((CanvasValues)_initialValues.Clone());
            SetCanvasAutoValues();
            UpdatePoints();
        }

        public void UpdatePoints()
        {
            foreach (PolylineEl polyline in Polylines)            
                polyline.Points = model.GeneratePoints(polyline.Values);
        }

        public void SetCanvasAutoValues()
        {
            if (!model.UseCustomCanvasValues)
            {
                CanvasMinX = $"{model.CanvasAutoValues.MinX}";
                CanvasMinY = $"{model.CanvasAutoValues.MinY}";
                CanvasMaxX = $"{model.CanvasAutoValues.MaxX}";
                CanvasMaxY = $"{model.CanvasAutoValues.MaxY}";
                model.UseCustomCanvasValues = false;
            }
        }

        private RelayCommand _addNewPolylineVisibility;
        public RelayCommand AddNewPolylineVisibility => _addNewPolylineVisibility ??= new RelayCommand(obj =>
        {
            X0 = "0"; X1 = "0"; X2 = "0"; X3 = "0";
            Y0 = "0"; Y1 = "0"; Y2 = "0"; Y3 = "0";
            _initialValues = (CanvasValues)model.CanvasAutoValues.Clone();
            CreateNewPolyline();
            SelectItemId = autoId;
            _addNewPolyline = true;
            VisibilityAddPolyline = true;
            VisibilityMenu = false;
        });

        private RelayCommand _showPolylineCollection;
        public RelayCommand ShowPolylineCollection => _showPolylineCollection ??= new RelayCommand(obj =>
        {
            VisibilityPolylineCollection = !VisibilityPolylineCollection;
            if (!VisibilityPolylineCollection)
                VisibilityPolylineValues = !VisibilityPolylineValues;
        });

        private RelayCommand _showCanvasValues;
        public RelayCommand ShowCanvasValues => _showCanvasValues ??= new RelayCommand(obj =>
        {
            VisibilityCanvasValues = !VisibilityCanvasValues;
        });

        private RelayCommand _setAutoCanvasValues;
        public RelayCommand SetAutoCanvasValues => _setAutoCanvasValues ??= new RelayCommand(obj =>
        {
            model.UseCustomCanvasValues = false;
            SetCanvasAutoValues();
        });

        private RelayCommand _addNewPolylineCmd;
        public RelayCommand AddNewPolyline => _addNewPolylineCmd ??= new RelayCommand(obj =>
        {
            Polylines.Where(x => x.Id == SelectItemId).First().ColorBrush = Brushes.Black;
            _addNewPolyline = false;
            VisibilityAddPolyline = false;
            VisibilityMenu = true;
        });

        private RelayCommand _cancelAddNewPolyline;
        public RelayCommand CancelAddNewPolyline => _cancelAddNewPolyline ??= new RelayCommand(obj =>
        {
            var item = Polylines.Where(x => x.Id == SelectItemId).First();
            model.CanvasAutoValues = (CanvasValues)_initialValues.Clone();
            SetCanvasAutoValues();
            Polylines.Remove(item);
            _addNewPolyline = false;
            VisibilityAddPolyline = false;
            VisibilityMenu = true;
        });

        private RelayCommand _showValuesSelectedLine;
        public RelayCommand ShowValuesSelectedLine => _showValuesSelectedLine ??= new RelayCommand(obj =>
        {
            if (SelectElement != null)
            {
                SelectItemId = SelectElement.Id;
                OnPropertyChanged("Values");
                VisibilityPolylineValues = true;
            }
        });

        private RelayCommand _hideValuesSelectedLine;
        public RelayCommand HideValuesSelectedLine => _hideValuesSelectedLine ??= new RelayCommand(obj =>
        {
            VisibilityPolylineValues = false;
        });

        private RelayCommand _showUpdatePolyline;
        public RelayCommand ShowUpdatePolyline => _showUpdatePolyline ??= new RelayCommand(obj =>
        {
            if (SelectElement != null)
            {
                VisibilityPolylineValues = false;
                VisibilityPolylineCollection = false;
                VisibilityMenu = false;
                VisibilityUpdatePolyline = true;
                X0 = $"{SelectElement.X0}"; X1 = $"{SelectElement.X1}";
                X2 = $"{SelectElement.X2}"; X3 = $"{SelectElement.X3}";
                Y0 = $"{SelectElement.Y0}"; Y1 = $"{SelectElement.Y1}";
                Y2 = $"{SelectElement.Y2}"; Y3 = $"{SelectElement.Y3}";
                SelectColorPicker = SelectElement.ColorBrush;
                StrokeThickness = $"{SelectElement.StrokeThickness}";
            }
        });

        private void SetColor()
        {

        }

        private RelayCommand _deletePolyline;
        public RelayCommand DeletePolyline => _deletePolyline ??= new RelayCommand(obj =>
        {
            Polylines.Remove(SelectElement);
            model.CanvasAutoValues = model.RefreshCanvasValues();
            if (!model.UseCustomCanvasValues)
                SetCanvasAutoValues();
            UpdatePoints();
        });

        private RelayCommand _updatePolyline;
        public RelayCommand UpdatePolyline => _updatePolyline ??= new RelayCommand(obj =>
        {
            SelectElement.X0 = double.Parse(X0); SelectElement.X1 = double.Parse(X1);
            SelectElement.X2 = double.Parse(X2); SelectElement.X3 = double.Parse(X3);
            SelectElement.Y0 = double.Parse(Y0); SelectElement.Y1 = double.Parse(Y1);
            SelectElement.Y2 = double.Parse(Y2); SelectElement.Y3 = double.Parse(Y3);
            SelectElement.Values = model.GenerateCurve((CanvasValues)model.CanvasAutoValues.Clone());
            model.CanvasAutoValues = model.RefreshCanvasValues();
            SetCanvasAutoValues();
            UpdatePoints();
            SelectElement.StrokeThickness = double.Parse(StrokeThickness);
            SelectElement.ColorBrush = SelectColorPicker;
            VisibilityPolylineCollection = true;
            VisibilityMenu = true;
            VisibilityUpdatePolyline = false;
        });

        private RelayCommand _cancelUpdatePolyline;
        public RelayCommand CancelUpdatePolyline => _cancelUpdatePolyline ??= new RelayCommand(obj =>
        {
            VisibilityPolylineCollection = true;
            VisibilityMenu = true;
            VisibilityUpdatePolyline = false;
        });

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
