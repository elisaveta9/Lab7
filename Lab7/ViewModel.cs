using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;

namespace Lab7
{
    internal class ViewModel : INotifyPropertyChanged
    {
        private static ViewModel viewModel;
        private readonly Model model;

        private ViewModel()
        {
            model = new();
        }

        public static ViewModel GetInstance => viewModel ??= new();

        public string X0 { get => $"{model.X0}"; set { model.X0 = double.Parse(value); OnPropertyChanged(); UpdateCurve(); } }
        public string X1 { get => $"{model.X1}"; set { model.X1 = double.Parse(value); OnPropertyChanged(); UpdateCurve(); } }
        public string X2 { get => $"{model.X2}"; set { model.X2 = double.Parse(value); OnPropertyChanged(); UpdateCurve(); } }
        public string X3 { get => $"{model.X3}"; set { model.X3 = double.Parse(value); OnPropertyChanged(); UpdateCurve(); } }
        public string Y0 { get => $"{model.Y0}"; set { model.Y0 = double.Parse(value); OnPropertyChanged(); UpdateCurve(); } }
        public string Y1 { get => $"{model.Y1}"; set { model.Y1 = double.Parse(value); OnPropertyChanged(); UpdateCurve(); } }
        public string Y2 { get => $"{model.Y2}"; set { model.Y2 = double.Parse(value); OnPropertyChanged(); UpdateCurve(); } }
        public string Y3 { get => $"{model.Y3}"; set { model.Y3 = double.Parse(value); OnPropertyChanged(); UpdateCurve(); } }
        public double MaxX { set { model.SizeX = value - 10; OnPropertyChanged(); UpdatePoints(); } }
        public double MaxY { set { model.SizeY = value - 10; OnPropertyChanged(); UpdatePoints(); } }
        public ObservableCollection<PointValue> Values { get => model.Values; set { model.Values = value; OnPropertyChanged(); } }
        public PointCollection Points { get => model.Points; set { model.Points = value; OnPropertyChanged(); } }

        public void UpdateCurve()
        {
            Values.Clear();
            foreach (PointValue point in model.GenerateCurve())
                Values.Add(point);
            Points = model.GeneratePoints();
        }

        public void UpdatePoints()
        {
            Points = model.GeneratePoints();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
