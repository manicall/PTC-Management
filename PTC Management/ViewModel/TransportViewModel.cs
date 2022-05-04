using System.ComponentModel;
using System.Windows;
using System.Windows.Data;

namespace PTC_Management.ViewModel
{
    internal class TransportViewModel : BindableBase
    {
        public TransportViewModel()
        {
            TransportItems = CollectionViewSource.GetDefaultView(Transport.GetInfo());
            TransportItems.Filter = FilterTransport;
        }

        private bool FilterTransport(object obj)
        {
            bool result = true;
            Transport current = obj as Transport;
            if (!string.IsNullOrWhiteSpace(FilterTransportText) && current != null && !current.idTransport.ToString().Contains(FilterTransportText))
            {
                result = false;
            }
            return result;
        }
        private static void FilterText_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var current = d as TransportViewModel;
            if (current != null)
            {
                current.TransportItems.Filter = null;
                current.TransportItems.Filter = current.FilterTransport;

            }
        }

        public string FilterTransportText
        {
            get { return (string)GetValue(FilterTextProperty); }
            set { SetValue(FilterTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FilterText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FilterTextProperty =
            DependencyProperty.Register("FilterTransportText", typeof(string), typeof(TransportViewModel), new PropertyMetadata("", FilterText_Changed));

        public ICollectionView TransportItems
        {
            get { return (ICollectionView)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Items.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemsProperty =
            DependencyProperty.Register("TransportItems", typeof(ICollectionView), typeof(TransportViewModel), new PropertyMetadata(null));

    }
}
