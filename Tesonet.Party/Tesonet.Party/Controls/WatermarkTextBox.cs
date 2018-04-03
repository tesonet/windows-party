using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Tesonet.Party.Controls
{
    public class WatermarkTextBox : TextBox
    {
        public WatermarkTextBox()
        {
            DefaultStyleKey = typeof(WatermarkTextBox);

            GotFocus += new RoutedEventHandler(OnGotFocus);
            LostFocus += new RoutedEventHandler(OnLostFocus);
            Loaded += new RoutedEventHandler(OnLoaded);
            TextChanged += new TextChangedEventHandler(OnTextChanged);
            AddHandler(UIElement.MouseLeftButtonDownEvent, new MouseButtonEventHandler(OnMouseLeftButtonDown), true);
        }

        #region Properties

        public static readonly DependencyProperty WatermarkContentProperty =
            DependencyProperty.Register("WatermarkContent", typeof(object), typeof(WatermarkTextBox),
                new PropertyMetadata((object)null));

        public object WatermarkContent
        {
            get { return (object)GetValue(WatermarkContentProperty); }
            set { SetValue(WatermarkContentProperty, value); }
        }

        public static readonly DependencyProperty IconContentProperty =
            DependencyProperty.Register("IconContent", typeof(object), typeof(WatermarkTextBox),
                new PropertyMetadata((object)null));

        public object IconContent
        {
            get { return (object)GetValue(IconContentProperty); }
            set { SetValue(IconContentProperty, value); }
        }

        public static readonly DependencyProperty IsWatermarkVisibleProperty =
            DependencyProperty.Register("IsWatermarkVisible", typeof(bool), typeof(WatermarkTextBox),
                new PropertyMetadata((bool)false,
                    new PropertyChangedCallback(OnIsWatermarkVisibleChanged)));

        public bool IsWatermarkVisible
        {
            get { return (bool)GetValue(IsWatermarkVisibleProperty); }
            set { SetValue(IsWatermarkVisibleProperty, value); }
        } 

        private static void OnIsWatermarkVisibleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((WatermarkTextBox)d).UpdateVisualState(true);
        }

        #endregion

        #region Event handlers
        private void OnGotFocus(object sender, RoutedEventArgs e)
        {
            this.IsWatermarkVisible = false;
            Dispatcher.BeginInvoke(new Action(() => SelectAll()));
        }

        private void OnLostFocus(object sender, RoutedEventArgs e)
        {
            UpdateWatermarkVisibility();
        }

        private void OnMouseLeftButtonDown(object sender, MouseEventArgs e)
        {
            IsWatermarkVisible = false;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            UpdateWatermarkVisibility();
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateWatermarkVisibility();
        }
        #endregion

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            UpdateWatermarkVisibility();
            UpdateVisualState(false);
        }

        private void UpdateWatermarkVisibility()
        {
            IsWatermarkVisible = (!string.IsNullOrEmpty(Text) ? false : !GetIsFocused());
        }

        private void UpdateVisualState(bool useTransitions)
        {
            if (IsWatermarkVisible)
            {
                VisualStateManager.GoToState(this, "WatermarkVisible", useTransitions);
                return;
            }
            VisualStateManager.GoToState(this, "WatermarkHidden", useTransitions);
        }

        private bool GetIsFocused()
        {
            var focusedElement = FocusManager.GetFocusedElement(this) as UIElement;
            if (focusedElement == null)
            {
                return false;
            }
            if (focusedElement == this)
            {
                return true;
            }
            return IsAncestorOf(focusedElement);
        }
    }
}
