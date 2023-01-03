using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace UI.Converters
{
    public class DictionaryConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if(values.Length== 0) return null;
            if (values[0] == DependencyProperty.UnsetValue) return null;
            if (values[1] == DependencyProperty.UnsetValue) return null;
            Dictionary<Guid, string> dictionary = (Dictionary<Guid, string>)values[0];
            Guid ide = (Guid)values[1];
            if (!dictionary.TryGetValue(ide, out string? retorno))
                retorno = "Não encontrado";
            return retorno;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
