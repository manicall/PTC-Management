using System;
using System.Data;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace PTC_Management.Model.Converter
{
    internal class CellAligmentConverter : IMultiValueConverter
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
                    case Status.working:
                    case Status.noWorking:
                    case Status.vacation:
                        return TextAlignment.Center;
                    default:
                        return TextAlignment.Left;
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