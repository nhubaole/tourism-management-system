using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace TourismManagementSystem.Utils
{
    internal class RadioButtonConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string selectedRadioButton = value as string;
            string radioButtonContent = parameter as string;

            return selectedRadioButton == radioButtonContent;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isChecked = (bool)value;
            string radioButtonContent = parameter as string;

            if (isChecked)
            {
                return radioButtonContent;
            }

            return Binding.DoNothing;
        }
    }
}
