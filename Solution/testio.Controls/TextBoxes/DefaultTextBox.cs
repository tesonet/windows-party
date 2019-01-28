using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;

namespace testio.Controls.TextBoxes
{
    public class DefaultTextBox: TextBox
    {
        public static readonly DependencyProperty WatermarkProperty =
            DependencyProperty.Register("Watermark", typeof(string), typeof(DefaultTextBox));
        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register("Source", typeof(ImageSource), typeof(DefaultTextBox));

        public string Watermark
        {
            get { return (string)GetValue(WatermarkProperty); }
            set { SetValue(WatermarkProperty, value); }
        }

        public ImageSource Source
		{
			get { return (ImageSource) GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
		}
    }
}
