using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MancalaAssessment.Views
{
    /// <summary>
    /// Interaction logic for Pit.xaml
    /// </summary>
    public partial class Pit : Button
    {
        public SolidColorBrush Stroke
        {
            get { return (SolidColorBrush)GetValue(StrokeProperty); }
            set { SetValue(StrokeProperty, value); }
        }

        public static readonly DependencyProperty StrokeProperty = DependencyProperty.Register(
            nameof(Stroke),
            typeof(SolidColorBrush),
            typeof(Pit),
            new PropertyMetadata(Brushes.Black)
        );

        public int Stones
        {
            get { return (int)GetValue(StonesProperty); }
            set { SetValue(StonesProperty, value); }
        }
        public static readonly DependencyProperty StonesProperty = DependencyProperty.Register(
            nameof(Stones),
            typeof(int),
            typeof(Pit),
            new PropertyMetadata(0)
        );

        /// <summary>
        /// Add pit location, according to the distance to the store
        /// </summary>
        public int Distance
        {
            get { return (int)GetValue(StonesProperty); }
            set { SetValue(StonesProperty, value); }
        }
        public static readonly DependencyProperty DistanceProperty = DependencyProperty.Register(
            nameof(Distance),
            typeof(int),
            typeof(Pit),
            new PropertyMetadata(0)
        );

        public Pit()
        {
            InitializeComponent();
        }
    }
}
