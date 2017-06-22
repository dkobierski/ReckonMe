using System;
using Xamarin.Forms;

namespace ReckonMe.Helpers
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Xamarin.Forms.IValueConverter" />
    public class DecimalConverter : IValueConverter
    {
        /// <summary>
        /// Implement this method to convert <paramref name="value" /> to <paramref name="targetType" /> by using <paramref name="parameter" /> and <paramref name="culture" />.
        /// </summary>
        /// <param name="value">To be added.</param>
        /// <param name="targetType">To be added.</param>
        /// <param name="parameter">To be added.</param>
        /// <param name="culture">To be added.</param>
        /// <returns>
        /// To be added.
        /// </returns>
        /// <remarks>
        /// To be added.
        /// </remarks>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is decimal)
                return value.ToString();
            return value;
        }

        /// <summary>
        /// Implement this method to convert <paramref name="value" /> back from <paramref name="targetType" /> by using <paramref name="parameter" /> and <paramref name="culture" />.
        /// </summary>
        /// <param name="value">To be added.</param>
        /// <param name="targetType">To be added.</param>
        /// <param name="parameter">To be added.</param>
        /// <param name="culture">To be added.</param>
        /// <returns>
        /// To be added.
        /// </returns>
        /// <remarks>
        /// To be added.
        /// </remarks>
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            decimal dec;
            if (decimal.TryParse(value as string, out dec))
                return dec;
            return value;
        }
    }
}