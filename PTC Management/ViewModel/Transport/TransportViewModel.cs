using PTC_Management.Commands;
using PTC_Management.EF;
using PTC_Management.Model;
using PTC_Management.Model.MainWindow;
using PTC_Management.ViewModel.Base;
using PTC_Management.ViewModel.DialogViewModels;
using PTC_Management.ViewModel.Helpers;

using System.IdentityModel.Metadata;
using System.Windows.Input;

namespace PTC_Management.ViewModel
{
    internal class TransportViewModel : ViewModelBaseEntity
    {
        ViewModelHelper<Transport> viewModelHelper;

        public ICommand TransportInfoCommand { get; set; }

        public string TansportInfoButtonsVisibility { get; set; }

        public TransportViewModel()
        {
            TransportInfoCommand = new Command<string>(OnTransportInfo);

            viewModelHelper =
                new ViewModelHelper<Transport>(Transport.repository);

            Items = viewModelHelper.GetItems();
            Items.Filter = Filter;

            TansportInfoButtonsVisibility = Visibility.visible;
        }

        public TransportViewModel(string tansportInfoVisibility) : base()
        {
            TansportInfoButtonsVisibility = tansportInfoVisibility;
        }

        /// <summary>
        /// Проверка подходит ли заданный текст под условие фильтра.
        /// </summary>
        protected override bool Filter(object entity)
        {
            Transport current = entity as Transport;

            if (string.IsNullOrWhiteSpace(FilterText)
                 // || current.Id.ToString().Contains(FilterText)
                 || current.Name.Contains(FilterText)
                 || current.LicensePlate.Contains(FilterText))
            {
                return true;
            }
            return false;
        }

        public Destinations Destinations => new Destinations();

        #region Методы
        public void OnTransportInfo(string destination)
        {
            // DONE: отправить сообщение о том, что должен быть выбран транспорт
            if ((Transport)SelectedItem is null)
            {
                WindowParameters.StatusBarMessage = "Необходимо выбрать транспорт";
                return;
            }

            var transportInfo = new TransportInfoWindowViewModel();
            switch (destination)
            {
                case Destinations.maintanceLog:
                    transportInfo.CurrentViewModel = new MaintanceLogViewModel((Transport)SelectedItem);
                    transportInfo.Title = "Журнал технического обслуживания";

                    break;

                case Destinations.logOfDepartureAndEntry:
                    transportInfo.CurrentViewModel = new LogOfDepartureAndEntryViewModel((Transport)SelectedItem);
                    transportInfo.Title = "Журнал регистрации въезда и выезда";         

                    break;
            }

            transportInfo.Show();
        }

        /// <summary>
        /// Выполняет заданное действие для вызывающей кнопки.
        /// </summary>
        public override void OnTableAction(string action)
        {
            // инициализация представление-модель диалогового окна
            var dialogViewModel = GetDialogViewModel<TransportDialogViewModel>(action, Destinations.transport);
            dialogViewModel.ViewModelHelper = viewModelHelper;

            var actionPerformer = new ActionPerformer<Transport>
                 (this, dialogViewModel, viewModelHelper.ItemsList);

            actionPerformer.doAction(action);
        }

        #endregion
    }
}
