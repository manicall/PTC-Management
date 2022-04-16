using PTC_Management.Model;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;

namespace PTC_Management.ViewModel
{
    internal class MaintanceLogViewModel : BindableBase
    {
        public MaintanceLogViewModel()
        {
            Items = CollectionViewSource.GetDefaultView(MaintanceLog.GetInfo());
            Items.Filter = FilterMaintanceLog;
        }


        private bool FilterMaintanceLog(object obj)
        {
            bool result = true;
            MaintanceLog current = obj as MaintanceLog;
            if (!string.IsNullOrWhiteSpace(FilterMaintanceLogText) && current != null && !current.idMaintanceLog.ToString().Contains(FilterMaintanceLogText) /*&& !current.LastName.Contains(FilterText)*/)
            {
                result = false;
            }
            return result;
        }
        private static void FilterText_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var current = d as MaintanceLogViewModel;
            if (current != null)
            {
                current.Items.Filter = null;
                current.Items.Filter = current.FilterMaintanceLog;

            }
        }

        public string FilterMaintanceLogText
        {
            get { return (string)GetValue(FilterTextProperty); }
            set { SetValue(FilterTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FilterText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FilterTextProperty =
            DependencyProperty.Register("FilterMaintanceLogText", typeof(string), typeof(MaintanceLogViewModel), new PropertyMetadata("", FilterText_Changed));

        public ICollectionView Items
        {
            get { return (ICollectionView)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Items.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemsProperty =
            DependencyProperty.Register("MaintanceLogItems", typeof(ICollectionView), typeof(MaintanceLogViewModel), new PropertyMetadata(null));

    }
}
