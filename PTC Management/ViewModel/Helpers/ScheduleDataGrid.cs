using PTC_Management.Model;

using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace PTC_Management.ViewModel.Helpers
{
    public class ScheduleDataGrid : DataGrid
    {
        public ScheduleDataGrid()
        {
            Loaded += CustomDataGrid_Loaded;
            SelectedCellsChanged += CustomDataGrid_SelectedCellsChanged;
        }

        void CustomDataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            ItemsList = Items;
            SelectedCellsList = SelectedCells;
        }

        void CustomDataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            SelectAllCells();

            foreach (DataGridCellInfo cellInfo in SelectedCells)
            {
                if (cellInfo.Column.DisplayIndex == 0) continue;
                if (cellInfo.IsValid)
                {
                    var content = cellInfo.Column.GetCellContent(cellInfo.Item);

                    if (content is TextBlock textBlock)
                    {
                        if (textBlock.Background == null)
                        {
                            textBlock.Margin = new Thickness(-1, -1, -1, -1);
                            textBlock.TextAlignment = TextAlignment.Center;
                        }

                        textBlock.Background = new SolidColorBrush(GetColor(textBlock.Text));

                        var row = (DataRowView)content.DataContext;
                        row[cellInfo.Column.Header.ToString()] = textBlock.Text;
                    }
                }
            }

            UnselectAllCells();
        }

        Color GetColor(string status)
        {
            switch (status)
            {
                case Status.working:
                    return (Color)ColorConverter.ConvertFromString("LimeGreen");
                case Status.noWorking:
                    return (Color)ColorConverter.ConvertFromString("DeepPink");
                case Status.free:
                    return (Color)ColorConverter.ConvertFromString("DarkGray");
                case Status.vacation:
                    return (Color)ColorConverter.ConvertFromString("DeepSkyBlue");

                default:
                    return new Color();
            }
        }

        #region SelectedItemsList

        public IList<DataGridCellInfo> SelectedCellsList
        {
            get { return (IList<DataGridCellInfo>)GetValue(SelectedCellsListProperty); }
            set { SetValue(SelectedCellsListProperty, value); }
        }

        public static readonly DependencyProperty SelectedCellsListProperty =
                DependencyProperty.Register("SelectedCellsList", typeof(IList<DataGridCellInfo>), typeof(ScheduleDataGrid), new PropertyMetadata(null));

        public ItemCollection ItemsList
        {
            get { return (ItemCollection)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }

        public static readonly DependencyProperty ItemsProperty =
                DependencyProperty.Register("ItemsList", typeof(ItemCollection), typeof(ScheduleDataGrid), new PropertyMetadata(null));

        #endregion
    }
}
