﻿using PTC_Management.ViewModel;
using PTC_Management.ViewModel.Base;

namespace PTC_Management.Model.Dialog
{
    class ViewModels : BindableBase
    {
        public static readonly BindableBase _employee = new EmployeeViewModel();
        public static readonly BindableBase _route = new RouteViewModel();
        public static readonly BindableBase _transport = new TransportViewModel();
        public static readonly BindableBase _itinerary = new ItineraryViewModel();
        public static readonly BindableBase _scheduleOfEmployee = new ScheduleOfEmployeeViewModel();
    }

}
