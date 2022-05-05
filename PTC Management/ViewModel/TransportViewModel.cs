using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using PTC_Management.EF;

namespace PTC_Management.ViewModel
{
    internal class TransportViewModel : BindableBase
    {
        readonly Repository<Transport> _transport = new Repository<Transport>(new PTC_ManagementContext());

        public TransportViewModel()
        {
            TransportItems = CollectionViewSource.GetDefaultView(_transport.GetAll());
            TransportItems.Filter = FilterTransport;
        }

        private bool FilterTransport(object obj)
        {
            bool result = true;
            Transport current = obj as Transport;
            if (!string.IsNullOrWhiteSpace(FilterTransportText) && current != null && !current.Id.ToString().Contains(FilterTransportText))
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
