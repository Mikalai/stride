using System.Globalization;
using System.Windows.Data;
using Stride.Core.Annotations;
using Stride.Core.Presentation.Commands;
using Stride.Core.Presentation.ValueConverters;
using Stride.Core.Presentation.ViewModels;

namespace Stride.Core.Assets.Editor.View.ValueConverters
{
    internal class ActionToCommand : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] is Action action && values[1] is IViewModelServiceProvider services)
            {
                return new AnonymousTaskCommand(services, async () => await Task.Run(action));
            }
            return null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) => throw new NotSupportedException();
    }
}
