using System;
using System.Windows;
using System.Windows.Controls;

namespace WindowsParty.Utils
{
    /// <summary>
    /// Data template selector for selecting best matching registered view with <see cref="ViewForAttribute"/> attribute.
    /// </summary>
    /// <remarks>
    /// Copied from another project together with <see cref="ViewForAttribute"/>
    /// </remarks>
    public class ViewDataTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item == null)
                return base.SelectTemplate(null, container);

            var dataType = item.GetType();
            var viewType = ViewForAttribute.GetBestMatchingViewType(dataType);
            if (viewType != null)
            {
                return CreateDataTemplate(dataType, viewType, item);
            }

            return base.SelectTemplate(item, container);
        }

        private static DataTemplate CreateDataTemplate(Type dataType, Type viewType, object dataContext)
        {
            var dataTemplate = new DataTemplate(dataType);
            var elementFactory = new FrameworkElementFactory(viewType);
            elementFactory.SetValue(FrameworkElement.DataContextProperty, dataContext);
            dataTemplate.VisualTree = elementFactory;

            return dataTemplate;
        }
    }
}
