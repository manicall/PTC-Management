
using System.Collections;
using System.Windows;
using System.Windows.Controls;

namespace PTC_Management.ViewModel.Helpers
{
    public class MultiselectDataGrid : DataGrid
    {
        public MultiselectDataGrid()
        {
            SelectionChanged += CustomDataGrid_SelectionChanged;
        }

        void CustomDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedItemsList = SelectedItems;
        }

        #region SelectedItemsList

        public IList SelectedItemsList
        {
            get { return (IList)GetValue(SelectedItemsListProperty); }
            set { SetValue(SelectedItemsListProperty, value); }
        }

        public static readonly DependencyProperty SelectedItemsListProperty =
                DependencyProperty.Register("SelectedItemsList", typeof(IList), typeof(MultiselectDataGrid), new PropertyMetadata(null));

        #endregion
    }
}
