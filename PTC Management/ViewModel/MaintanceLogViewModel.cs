using PTC_Management.EF;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;

namespace PTC_Management.ViewModel
{
    internal class MaintanceLogViewModel : BindableBase
    {
        readonly Repository<MaintanceLog> _maintanceLog = new Repository<MaintanceLog>(new PTC_ManagementContext());

        public MaintanceLogViewModel()
        {
            MaintanceLogItems = CollectionViewSource.GetDefaultView(_maintanceLog.GetAll());
            MaintanceLogItems.Filter = FilterMaintanceLog;
        }


        private bool FilterMaintanceLog(object obj)
        {
            bool result = true;
            MaintanceLog current = obj as MaintanceLog;
            if (!string.IsNullOrWhiteSpace(FilterMaintanceLogText) && current != null && !current.Id.ToString().Contains(FilterMaintanceLogText) /*&& !current.LastName.Contains(FilterText)*/)
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
                current.MaintanceLogItems.Filter = null;
                current.MaintanceLogItems.Filter = current.FilterMaintanceLog;

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

        public ICollectionView MaintanceLogItems
        {
            get { return (ICollectionView)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Items.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemsProperty =
            DependencyProperty.Register("MaintanceLogItems", typeof(ICollectionView), typeof(MaintanceLogViewModel), new PropertyMetadata(null));

    }
}
