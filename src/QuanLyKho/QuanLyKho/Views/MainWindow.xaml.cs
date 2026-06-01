using QuanLyKho.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace QuanLyKho.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }

        private void Sidebar_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (SidebarBorder.IsVisible)
            {
                this.Dispatcher.InvokeAsync(() =>
                {
                    MenuButton_Click(btnTrangChu, null);
                }, System.Windows.Threading.DispatcherPriority.Loaded);
            }
        }

        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            if (!(sender is Button clickedBtn)) return;

            Point relativePoint = clickedBtn.TransformToAncestor(MenuContainer).Transform(new Point(0, 0));

            DoubleAnimation slideAnimation = new DoubleAnimation
            {
                To = relativePoint.Y,
                Duration = TimeSpan.FromSeconds(0.25),
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseInOut }
            };

            IndicatorTransform.BeginAnimation(TranslateTransform.YProperty, slideAnimation);

            foreach (var child in MenuStackPanel.Children)
            {
                if (child is Button btn)
                {
                    var unselectedColor = (SolidColorBrush)(new BrushConverter().ConvertFrom("#64748B"));
                    btn.Foreground = (btn == clickedBtn) ? Brushes.White : unselectedColor;
                }
            }
        }
    }
}