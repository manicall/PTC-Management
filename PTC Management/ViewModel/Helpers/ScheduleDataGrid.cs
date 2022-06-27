
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace PTC_Management.ViewModel.Helpers
{
    public class ScheduleDataGrid : DataGrid
    {
        public ScheduleDataGrid()
        {
            SelectedCellsChanged += CustomDataGrid_SelectedCellsChanged;
        }

        void CustomDataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            ItemsList = Items;
            SelectedCellsList = SelectedCells;
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
