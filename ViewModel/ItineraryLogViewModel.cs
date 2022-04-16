using PTC_Management.Model;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;

namespace PTC_Management.ViewModel
{
    internal class ItineraryLogViewModel : BindableBase
    {
        public ItineraryLogViewModel()
        {
            ItineraryLogItems = CollectionViewSource.GetDefaultView(ItineraryLog.GetInfo());
            ItineraryLogItems.Filter = FilterItineraryLog;
        }

        private bool FilterItineraryLog(object obj)
        {
            bool result = true;
            ItineraryLog current = obj as ItineraryLog;
            if (!string.IsNullOrWhiteSpace(FilterItineraryLogText) && current != null && !current.idItineraryLog.ToString().Contains(FilterItineraryLogText))
            {
                result = false;
            }
            return result;
        }
        private static void FilterText_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var current = d as ItineraryLogViewModel;
            if (current != null)
            {
                current.ItineraryLogItems.Filter = null;
                current.ItineraryLogItems.Filter = current.FilterItineraryLog;

            }
        }

        public string FilterItineraryLogText
        {
            get { return (string)GetValue(FilterTextProperty); }
            set { SetValue(FilterTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FilterText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FilterTextProperty =
            DependencyProperty.Register("FilterItineraryLogText", typeof(string), typeof(ItineraryLogViewModel), new PropertyMetadata("", FilterText_Changed));

        public ICollectionView ItineraryLogItems
        {
            get { return (ICollectionView)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Items.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemsProperty =
            DependencyProperty.Register("ItineraryLogItems", typeof(ICollectionView), typeof(ItineraryLogViewModel), new PropertyMetadata(null));

    }
}

