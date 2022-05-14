using PTC_Management.EF;
using PTC_Management.Model;
using PTC_Management.ViewModel.Base;

using System.ComponentModel;
using System.Windows;
using System.Windows.Data;

namespace PTC_Management.ViewModel
{
    internal class ItineraryViewModel : BindableBase
    {
        //readonly Repository<Itinerary> _itinerary = new Repository<Itinerary>(new PTC_ManagementContext());

        //public ItineraryViewModel()
        //{
        //    ItineraryItems = CollectionViewSource.GetDefaultView(_itinerary.GetObservableCollection());
        //    ItineraryItems.Filter = FilterItinerary;
        //}

        //private bool FilterItinerary(object obj)
        //{
        //    bool result = true;
        //    Itinerary current = obj as Itinerary;
        //    if (!string.IsNullOrWhiteSpace(FilterItineraryText) && current != null && !current.Id.ToString().Contains(FilterItineraryText))
        //    {
        //        result = false;
        //    }
        //    return result;
        //}

        //private static void FilterText_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        //{
        //    var current = d as ItineraryViewModel;
        //    if (current != null)
        //    {
        //        current.ItineraryItems.Filter = null;
        //        current.ItineraryItems.Filter = current.FilterItinerary;

        //    }
        //}

        //public string FilterItineraryText
        //{
        //    get { return (string)GetValue(FilterTextProperty); }
        //    set { SetValue(FilterTextProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for FilterText.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty FilterTextProperty =
        //    DependencyProperty.Register(MyLiterals<Itinerary>.FilterText, typeof(string), typeof(ItineraryViewModel), new PropertyMetadata("", FilterText_Changed));

        //public ICollectionView ItineraryItems
        //{
        //    get { return (ICollectionView)GetValue(ItemsProperty); }
        //    set { SetValue(ItemsProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for Items.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty ItemsProperty =
        //    DependencyProperty.Register(MyLiterals<Itinerary>.Items, typeof(ICollectionView), typeof(ItineraryViewModel), new PropertyMetadata(null));

    }
}

