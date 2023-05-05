using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Lab7
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ViewModel ViewModel { get; set; }
        public MainWindow()
        {
            ViewModel = ViewModel.GetInstance;
            InitializeComponent();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new("[^0-9]+-,");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Canvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ItemsControl canvas = sender as ItemsControl;
            ViewModel.MaxY = canvas.ActualHeight;
            ViewModel.MaxX = canvas.ActualWidth;
        }
    }
}
