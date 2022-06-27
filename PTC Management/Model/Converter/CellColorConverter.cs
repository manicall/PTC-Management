using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows;

namespace PTC_Management.Model.Converter
{
    public class CellColorConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[1] is DataRow)
            {
                var cell = (DataGridCell)values[0];
                var row = (DataRow)values[1];
                var columnName = cell.Column.SortMemberPath;
                string input = (row[columnName] as string);

                switch (input)
                {
                    case Status.free:
                        return Brushes.DarkGray;
                    case Status.working:
                        return Brushes.LimeGreen;
                    case Status.noWorking:
                        return Brushes.DeepPink;
                    case Status.vacation:
                        return Brushes.DeepSkyBlue;
                    default:
                        return Brushes.White;
                }
            }
            else
            {
                return SystemColors.AppWorkspaceColor;
            }

        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
