using PTC_Management.EF;
using PTC_Management.Model.MainWindow;
using PTC_Management.ViewModel.Base;
using PTC_Management.ViewModel.DialogViewModels;
using PTC_Management.ViewModel.Helpers;

using System.Collections.ObjectModel;

namespace PTC_Management.ViewModel
{
    internal class ItineraryViewModel : ViewModelBaseEntity
    {
        ViewModelHelper<Itinerary, ObservableCollection<Itinerary>> viewModelHelper;

        public ItineraryViewModel()
        {
            viewModelHelper = new ViewModelHelper<Itinerary, ObservableCollection<Itinerary>>(Itinerary.repository);

            Items = viewModelHelper.GetItems();
            Items.Filter = Filter;
        }

        #region FilterText

        /// <summary>
        /// Проверка подходит ли заданный текст под условие фильтра.
        /// </summary>
        protected override bool Filter(object entity)
        {
            Itinerary current = entity as Itinerary;
            return true;
        }
        #endregion


        #region Методы

        /// <summary>
        /// Выполняет заданное действие для вызывающей кнопки.
        /// </summary>
        public override void OnDialog(string action)
        {
            var actionPerformer =
                 new ActionPerformer<Itinerary, ObservableCollection<Itinerary>> 
                    (this, GetDialogViewModel(action), viewModelHelper.ObservableCollection);

            actionPerformer.doAction(action);
        }

        /// <summary>
        /// Выполняет инициализацию диалогового окна и возвращает его экземпляр.
        /// </summary>
        public DialogViewModel GetDialogViewModel(string action)
        {
            return new ItineraryDialogViewModel()
            {
                MainWindowAction = action,
                Title = ViewModels.GetDialogTitle(action, Destinations.itinerary),
                ViewModelHelper = viewModelHelper
            };
        }

        #endregion
    }
}
