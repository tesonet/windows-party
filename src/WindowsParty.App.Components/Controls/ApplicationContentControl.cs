using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WindowsParty.App.Components.Controls
{
    public class ApplicationContentControl : Control
    {
        public static readonly DependencyProperty AppContentProperty = DependencyProperty.Register(
            nameof(AppContent), typeof(object), typeof(ApplicationContentControl), new PropertyMetadata(default(object)));

        public object AppContent
        {
            get { return (object) GetValue(AppContentProperty); }
            set { SetValue(AppContentProperty, value); }
        }


        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
        }


        static ApplicationContentControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ApplicationContentControl), new FrameworkPropertyMetadata(typeof(ApplicationContentControl)));
        }
    }
}
