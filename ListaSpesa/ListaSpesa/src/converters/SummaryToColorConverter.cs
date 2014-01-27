using System.Windows.Data;
using System.Windows.Media;

namespace ListaSpesa.Converters
{
    public class SummaryToColorConverter : IValueConverter
    {

        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is bool)
            {
                bool isSpesaFinished = (bool)value;
                return isSpesaFinished ? new SolidColorBrush(Colors.Green) : new SolidColorBrush(Colors.Red);
            }
            return null;
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new System.NotImplementedException();
        }
    }
}
