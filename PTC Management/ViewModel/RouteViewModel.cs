﻿using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using PTC_Management.EF;

namespace PTC_Management.ViewModel
{
    internal class RouteViewModel : BindableBase
    {
        readonly Repository<Route> _route = new Repository<Route>(new AppContext());

        public RouteViewModel()
        {
            RouteItems = CollectionViewSource.GetDefaultView(_route.GetAll());
            RouteItems.Filter = FilterRoute;
        }

        private bool FilterRoute(object obj)
        {
            bool result = true;
            Route current = obj as Route;
            if (!string.IsNullOrWhiteSpace(FilterRouteText) && current != null && !current.Id.ToString().Contains(FilterRouteText))
            {
                result = false;
            }
            return result;
        }
        private static void FilterText_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var current = d as RouteViewModel;
            if (current != null)
            {
                current.RouteItems.Filter = null;
                current.RouteItems.Filter = current.FilterRoute;

            }
        }

        public string FilterRouteText
        {
            get { return (string)GetValue(FilterTextProperty); }
            set { SetValue(FilterTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FilterText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FilterTextProperty =
            DependencyProperty.Register("FilterRouteText", typeof(string), typeof(RouteViewModel), new PropertyMetadata("", FilterText_Changed));

        public ICollectionView RouteItems
        {
            get { return (ICollectionView)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Items.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemsProperty =
            DependencyProperty.Register("RouteItems", typeof(ICollectionView), typeof(RouteViewModel), new PropertyMetadata(null));

    }
}
