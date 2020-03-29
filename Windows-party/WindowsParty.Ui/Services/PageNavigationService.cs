namespace WindowsParty.Ui.Services
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    internal class PageNavigationService : IPageNavigationService
    {
        private readonly string _frameName = "MainFrame";
        private string _currentPageKey;

        public event PropertyChangedEventHandler PropertyChanged;

        public string CurrentPageKey
        {
            get => _currentPageKey;
            private set
            {
                if (_currentPageKey != value)
                {
                    _currentPageKey = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public object Parameter { get; private set; }

        public void NavigateTo<T>(object parameter = null) where T : Page
        {
            var pageKey = typeof(T).Name;

            if (pageKey != CurrentPageKey)
            {
                if (GetDescendantFromName(Application.Current.MainWindow, _frameName) is Frame frame)
                {
                    frame.Source = new Uri($"../Views/{pageKey}.xaml", UriKind.Relative);
                }

                Parameter = parameter;
                CurrentPageKey = pageKey;
            }
        }

        private static FrameworkElement GetDescendantFromName(DependencyObject parent, string name)
        {
            var count = VisualTreeHelper.GetChildrenCount(parent);

            if (count < 1)
            {
                return null;
            }

            for (var i = 0; i < count; i++)
            {
                if (VisualTreeHelper.GetChild(parent, i) is FrameworkElement frameworkElement)
                {
                    if (frameworkElement.Name == name)
                    {
                        return frameworkElement;
                    }

                    frameworkElement = GetDescendantFromName(frameworkElement, name);

                    if (frameworkElement != null)
                    {
                        return frameworkElement;
                    }
                }
            }

            return null;
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}